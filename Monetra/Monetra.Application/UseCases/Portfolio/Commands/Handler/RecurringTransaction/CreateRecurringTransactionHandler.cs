using MediatR;
using Microsoft.AspNetCore.Identity;
using Monetra.Application.UseCases.Portfolio.Commands.Request.RecurringTransaction;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Commum.Abstraction;
using Monetra.Domain.BackOffice.Entities;
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

        var customer =
            await _unitOfWork.CustomerRepository.GetByPredicateWithUserAndMarkAndExpense(x =>
                x.Id == request.Model.IdCustomerId);
        if(customer is null)
            return Result.Failure(Errors.CustumerNoExisting);
        if (request.Model.IdPortfolio is not null)
        {
            var resultPortfolio = await AddRepositoryInExpense(request.Model.IdPortfolio, customer.Expense);
            if (!resultPortfolio.IsSuccess)
                return resultPortfolio;
        }
        customer.Expense.AddRecurringTransaction(resultTransaction.Value);
        _unitOfWork.ExpenseRepository.Update(customer.Expense);
        resultTransaction.Value.ExpenseId = customer.Expense.Id;
        
        _unitOfWork.RecurringTransactionRepository.Create(resultTransaction.Value);
        
        await _unitOfWork.CommitAsync();
        
        return Result.Success();
    }

    private async Task<Result> AddRepositoryInExpense(Guid? idPortfolio,Expense expense)
    {
        
        var port = await _unitOfWork.PortfolioRepository.GetByPredicate(x => x.Id == idPortfolio);
        
        if (port is null) return Result.Failure(Errors.PortfolioNoExisting);
        
        if(!IdCustomerIsEqualsPortfolioIdCustomer(port,expense.CustomerId))
            return Result.Failure(Errors.CustomerIdIsNotEqualPortfolioCustomerId);
        
        expense.SetPortfolio(port);
        return Result.Success();
    }
    
    private bool IdCustomerIsEqualsPortfolioIdCustomer(Domain.BackOffice.Entities.Portfolio port, Guid customerId)
        => port.CustomerId == customerId;
    
    
}