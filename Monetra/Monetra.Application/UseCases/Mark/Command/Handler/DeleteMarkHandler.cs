using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Monetra.Application.UseCases.Mark.Command.Request;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Commum.Abstraction;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;

namespace Monetra.Application.UseCases.Mark.Command.Handler;

public class DeleteMarkHandler : HandlerBase,IRequestHandler<DeleteMarkRequest,Result>
{
    public DeleteMarkHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result> Handle(DeleteMarkRequest request, CancellationToken cancellationToken)
    {
        var mark = await _unitOfWork.MarkRepository.GetByPredicate(x => x.Id == request.MarkId);
        if (mark is null)
            return Result.Failure(new Error("Mark.NoExisting", "Mark no exists"));
        var customer = await _unitOfWork.CustomerRepository.GetByPredicate(x => x.Id == request.CustomerId);
        if(customer is null)
            return Result.Failure(Errors.CustumerNoExisting);
        if (customer.Id != mark.CustomerId)
            return Result.Failure(new("CustomerId.NotEquals.MarkCustomerId",
                "CustomerId is Not Equals MarkCustomerId"));
        _unitOfWork.MarkRepository.Delete(mark);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }
}