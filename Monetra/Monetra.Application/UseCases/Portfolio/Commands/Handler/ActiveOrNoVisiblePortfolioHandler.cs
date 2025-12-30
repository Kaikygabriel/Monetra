using MediatR;
using Monetra.Application.UseCases.Portfolio.Commands.Request;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Commum.Abstraction;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;

namespace Monetra.Application.UseCases.Portfolio.Commands.Handler;

public class ActiveOrNoVisiblePortfolioHandler : HandlerBase,IRequestHandler
    <ActiveOrNoVisiblePortfolioRequest,Result>
{
    public ActiveOrNoVisiblePortfolioHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result> Handle(ActiveOrNoVisiblePortfolioRequest request, CancellationToken cancellationToken)
    {
        var port = await _unitOfWork.PortfolioRepository.GetByPredicate(x => x.Id == request.PortfolioId);
        if (port is null)
            return Result.Failure(Errors.PortfolioNoExisting);
        if (!IdCustomerAreEqualsIdCustomerFromPortfolio(port, request.CustomerId))
            return Result.Failure(Errors.CustomerIdIsNotEqualPortfolioCustomerId);

        ConvertVisible(port);
        _unitOfWork.PortfolioRepository.Update(port);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }

    private bool IdCustomerAreEqualsIdCustomerFromPortfolio(Domain.BackOffice.Entities.Portfolio port, Guid customerId)
        => port.CustomerId == customerId;

    private void ConvertVisible(Domain.BackOffice.Entities.Portfolio port)
    {
        if(!port.Visible)
            port.ConvertToVisible();
        else
            port.ConvertToNoVisible();
    }
}