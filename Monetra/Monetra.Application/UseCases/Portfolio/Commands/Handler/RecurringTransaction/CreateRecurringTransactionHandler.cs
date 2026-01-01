using MediatR;
using Monetra.Application.UseCases.Portfolio.Commands.Request.RecurringTransaction;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Commum.Abstraction;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;

namespace Monetra.Application.UseCases.Portfolio.Commands.Handler.RecurringTransaction;

public class CreateRecurringTransactionHandler: HandlerBase ,
    IRequestHandler<CreateRecurringTransactionRequest,Result>
{
    public CreateRecurringTransactionHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        
    }

    public async Task<Result> Handle(CreateRecurringTransactionRequest request, CancellationToken cancellationToken)
    {
        Result<Domain.BackOffice.Entities.RecurringTransaction> resultTransaction = request.Model;
        if (!resultTransaction.IsSuccess)
            return Result.Failure(resultTransaction.Error);
        var portfolio = await _unitOfWork.PortfolioRepository.GetByPredicate(x => x.Id == request.Model.IdPortfolio);
        if (portfolio is null)
            return Result.Failure(Errors.PortfolioNoExisting);
        if(!IdCustomerIsEqualsPortfolioIdCustomer(portfolio,request.Model.IdCustomerId))
            return Result.Failure(Errors.CustomerIdIsNotEqualPortfolioCustomerId);
        //portfolio.AddRecurringTransaction(resultTransaction.Value);
        //_unitOfWork.PortfolioRepository.Update(portfolio);
        resultTransaction.Value.PortfolioId = portfolio.Id;
        _unitOfWork.RecurringTransactionRepository.Create(resultTransaction.Value);
        
        await _unitOfWork.CommitAsync();
        
        return Result.Success();
    }

    private bool IdCustomerIsEqualsPortfolioIdCustomer(Domain.BackOffice.Entities.Portfolio port, Guid customerId)
        => port.CustomerId == customerId;
}