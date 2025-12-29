namespace Monetra.Domain.BackOffice.Interfaces.Repostiries.Transaction;

public interface ITransactionRepository : IRepository<Entities.Transaction>
{
    Task<IEnumerable<Entities.Transaction>> GetByPortfolioId(Guid id);
}