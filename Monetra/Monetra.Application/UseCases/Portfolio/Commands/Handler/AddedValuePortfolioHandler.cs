using MediatR;
using Monetra.Application.UseCases.Portfolio.Commands.Request;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Commum.Abstraction;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;

namespace Monetra.Application.UseCases.Portfolio.Commands.Handler;

public class AddedValuePortfolioHandler : HandlerBase, IRequestHandler<AddedValuePortfolioRequest,Result>
{
    public AddedValuePortfolioHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result> Handle(AddedValuePortfolioRequest request, CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.PortfolioRepository.GetByPredicate(x => x.Id == request.IdPortfolio);
        
        if (portfolio is null || request.Value <= 0)
            return Result.Failure(Errors.PortfolioNoExisting);
        
        if(!CustomerIdIsEquals(portfolio,request.CustomerId))
            return Result.Failure(Errors.CustomerIdIsNotEqualPortfolioCustomerId);
        
        portfolio.AddValue(request.Value,request.Type);
        
        _unitOfWork.PortfolioRepository.Update(portfolio);
        
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }

    private bool CustomerIdIsEquals(Domain.BackOffice.Entities.Portfolio port, Guid idCustomer)
        => port.CustomerId == idCustomer;
}