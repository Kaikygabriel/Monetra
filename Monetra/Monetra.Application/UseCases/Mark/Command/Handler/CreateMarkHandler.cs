using MediatR;
using Monetra.Application.UseCases.Mark.Command.Request;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Commum.Abstraction;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;

namespace Monetra.Application.UseCases.Mark.Command.Handler;

public class CreateMarkHandler : HandlerBase,IRequestHandler<CreateMarkRequest,Result>
{
    public CreateMarkHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result> Handle(CreateMarkRequest request, CancellationToken cancellationToken)
    {
        Result<Domain.BackOffice.Entities.Mark> resultMark = request.Model;
        if (!resultMark.IsSuccess)
            return Result.Failure(resultMark.Error);
        var customer = await _unitOfWork.CustomerRepository.GetByPredicate(x => x.Id == resultMark.Value.CustomerId);
        if(customer is null)
            return Result.Failure(Errors.CustumerNoExisting);
        customer.AddMark(resultMark.Value);
        _unitOfWork.MarkRepository.Create(resultMark.Value);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }
  
}