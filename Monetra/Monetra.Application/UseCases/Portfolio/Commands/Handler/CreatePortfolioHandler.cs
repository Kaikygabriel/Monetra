using MediatR;
using Monetra.Application.UseCases.Portfolio.Commands.Request;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Commum.Abstraction;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;

namespace Monetra.Application.UseCases.Portfolio.Commands.Handler;

public class CreatePortfolioHandler: HandlerBase, IRequestHandler<CreatePortfolioRequest,Result>
{
    public CreatePortfolioHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    
    public async Task<Result> Handle(CreatePortfolioRequest request, CancellationToken cancellationToken)
    {
        Result<Domain.BackOffice.Entities.Portfolio> portfolioResultCreate = request.Model;
        if (!portfolioResultCreate.IsSuccess)
            return Result.Failure(portfolioResultCreate.Error);
        if (!Guid.TryParse(request.Model.Userid, out var customerId))
           return Result.Failure(Errors.CustomerIdIsNotEqualPortfolioCustomerId);

        var customerExists = await _unitOfWork.CustomerRepository
            .GetByPredicate(x => x.Id == customerId);

        if (customerExists is null)
            return Result.Failure(Errors.CustumerNoExisting);

        var portfolio = portfolioResultCreate.Value;
        portfolio.CustomerId = customerExists.Id;
        customerExists.AddPortifolio(portfolio);
        
        _unitOfWork.PortfolioRepository.Create(portfolio);
        await _unitOfWork.CommitAsync();

        return Result.Success();
    }

}