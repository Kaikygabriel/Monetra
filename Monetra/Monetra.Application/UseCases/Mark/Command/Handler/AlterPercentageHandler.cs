using MediatR;
using Monetra.Application.UseCases.Mark.Command.Request;
using Monetra.Domain.BackOffice.Commum.Abstraction;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;

namespace Monetra.Application.UseCases.Mark.Command.Handler;

public class AlterPercentageHandler : HandlerBase, IRequestHandler<AlterPercentageOfMarkRequest,Result>
{
    public AlterPercentageHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result> Handle(AlterPercentageOfMarkRequest request, CancellationToken cancellationToken)
    {
        var mark = await _unitOfWork.MarkRepository.GetByPredicate(x => x.CustomerId == request.CustomerId);
        if (mark is null)
            return Result.Success();
        
        var value = request.Value;
        var resultPercentage = CalculatePercentageByValues(request.Value, GetValueCurrent(mark));
        
        var resultUpdate = mark.AlterPercentage(resultPercentage);
        
     //  _unitOfWork.MarkRepository.Update(mark);
        await _unitOfWork.CommitAsync();
        return resultUpdate ;
    }
    //esta dando erro de concorrencia , ajustar ! 
    private decimal GetValueCurrent(Domain.BackOffice.Entities.Mark mark)
    {
        var valuePercentage = mark.Percentage / 100;
        var valueRemove = valuePercentage * mark.TargetAmount;
        var valueFinal = valueRemove - mark.TargetAmount;
        return valueFinal;
    }

    private ushort CalculatePercentageByValues(decimal value, decimal valueMark)
    {
        var oneOperation = value * 100;
        var result =value / valueMark;
        return (ushort)result;
    }
}