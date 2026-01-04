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

            if (now.Hour == 12 && (_lastExecution == null || _lastExecution.Value.Date != now.Date))
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

        var portfolios =
            await unitOfWork.PortfolioRepository.GetByRecussingTransactionByDayCurrent();

        foreach (var port in portfolios)
        {
            port.RemoveValue(port.RecurringTransaction.Value,port.RecurringTransaction.TransactionType);
            unitOfWork.TransactionRepository.Create(port.Transactions.Last());
            unitOfWork.PortfolioRepository.Update(port);
        }
        
        await unitOfWork.CommitAsync();
    }
}