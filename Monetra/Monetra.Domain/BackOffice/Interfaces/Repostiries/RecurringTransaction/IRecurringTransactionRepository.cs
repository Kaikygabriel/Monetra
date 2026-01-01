namespace Monetra.Domain.BackOffice.Interfaces.Repostiries.RecurringTransaction;

public interface IRecurringTransactionRepository : IRepository<Entities.RecurringTransaction>
{
    Task<IEnumerable<Entities.RecurringTransaction>> GetPendingTransactions();
}