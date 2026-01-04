using MediatR;
using Monetra.Application.Service;
using Monetra.Application.UseCases.Mark.Command.Request;
using Monetra.Domain.BackOffice.Commum.Abstraction;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;
using Monetra.Domain.BackOffice.Interfaces.Services;

namespace Monetra.Application.UseCases.Mark.Command.Handler;

public class AlterPercentageHandler : HandlerBase, IRequestHandler<AlterPercentageOfMarkRequest,Result>
{
    private readonly IServiceEmail _serviceEmail;
    public AlterPercentageHandler(IUnitOfWork unitOfWork, IServiceEmail serviceEmail) : base(unitOfWork)
    {
        _serviceEmail = serviceEmail;
    }

    public async Task<Result> Handle(AlterPercentageOfMarkRequest request, CancellationToken cancellationToken)
    {
        var mark = await _unitOfWork.MarkRepository.GetByPredicate(x => x.CustomerId == request.CustomerId);
        if (mark is null)
            return Result.Success();
        if (mark.Percentage > 99)
            await SendEmailCompletedMark(await _unitOfWork.CustomerRepository.GetByPredicate(x=>x.Id ==mark.CustomerId)
                ,mark);
        var currentMoney = GetValueCurrentInMoney(mark);
        
        var newTotalMoney = Math.Abs(currentMoney + request.Value);
        
        var newPercentage = CalculateNewTotalPercentage(newTotalMoney, mark.TargetAmount);
        
        var resultUpdate = mark.AlterPercentage(newPercentage);
    
        if (!resultUpdate.IsSuccess)
            return resultUpdate;

        _unitOfWork.MarkRepository.Update(mark);
        await _unitOfWork.CommitAsync();
    
        return resultUpdate;
    }

    private decimal GetValueCurrentInMoney(Domain.BackOffice.Entities.Mark mark)
    {
        return (mark.Percentage / 100m) * mark.TargetAmount;
    }

    private ushort CalculateNewTotalPercentage(decimal newTotalValue, decimal targetAmount)
    {
        if (targetAmount <= 0) return 0;
        
        var result = (newTotalValue * 100) / targetAmount;
        
        return (ushort)Math.Round(result);
    }
    
    private async Task SendEmailCompletedMark(Domain.BackOffice.Entities.Customer customer,Domain.BackOffice.Entities.Mark mark)
    {
        _serviceEmail.TrySendEmail(
            ServiceEmail.CreateMenssageOfEmail
                (customer,"Completed mark!","Completed you Mark :" + mark.Title));
    }
}