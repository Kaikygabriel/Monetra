using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;
using Monetra.Domain.BackOffice.Interfaces.Services;

namespace Monetra.Application.Service;

public class ServiceRecurringTransaction :
    BackgroundService,
    IServiceRecurringTransaction
{
    private DateTime? _lastExecution;
    private readonly IServiceScopeFactory _scopeFactory;

    public ServiceRecurringTransaction(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;

            if (now.Minute == 47 && (_lastExecution == null || _lastExecution.Value.Date != now.Date))
            {
                await MakeRecurringTransactionByDayCurrent();
                _lastExecution = now;
            } 
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
    
    public async Task MakeRecurringTransactionByDayCurrent()
    {
        using var scope = _scopeFactory.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var transactions  =
            await unitOfWork.RecurringTransactionRepository.GetPendingTransactionsByCurrentDay();

        foreach(var transaction in transactions)
        {
            var expense = await unitOfWork.ExpenseRepository.GetWithPortfolioByPredicate(x => x.Id == transaction.ExpenseId);
            if (expense.Portfolio is not null)
            {
                expense.Portfolio.RemoveValue(transaction.Value,
                    transaction.TransactionType,new ("Recurring Transaction"));
                unitOfWork.TransactionRepository.Create(expense.Portfolio.Transactions.Last());
                unitOfWork.PortfolioRepository.Update(expense.Portfolio);
                await unitOfWork.CommitAsync();
            }
        }
        
        await unitOfWork.CommitAsync();
    }
}