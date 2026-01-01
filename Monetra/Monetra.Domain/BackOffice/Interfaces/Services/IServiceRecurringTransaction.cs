namespace Monetra.Domain.BackOffice.Interfaces.Services;

public interface IServiceRecurringTransaction
{
    Task MakeRecurringTransactionByDayCurrent();
}