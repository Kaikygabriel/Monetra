using MediatR;
using Monetra.Application.UseCases.Portfolio.Commands.Request;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Commum.Abstraction;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;

namespace Monetra.Application.UseCases.Portfolio.Commands.Handler;

public class DeletePortfolioHandler :HandlerBase, IRequestHandler<DeletePortfolioRequest,Result>
{
    public DeletePortfolioHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result> Handle(DeletePortfolioRequest request, CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.PortfolioRepository.GetByPredicate(x => x.Id == request.PortfolioId);
        if (portfolio is null)
            return Result.Failure(Errors.PortfolioNoExisting);
        var customer = await _unitOfWork.CustomerRepository.GetByPredicate(x => x.Id == request.CustomerId);
        if(customer is null)
            return Result.Failure(Errors.CustumerNoExisting);
        if(customer.Id != portfolio.CustomerId)
            return Result.Failure(Errors.CustomerIdIsNotEqualPortfolioCustomerId);
        _unitOfWork.PortfolioRepository.Delete(portfolio);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }
}