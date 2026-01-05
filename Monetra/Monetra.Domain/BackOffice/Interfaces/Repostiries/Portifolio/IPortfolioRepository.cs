using Monetra.Domain.BackOffice.Entities;

namespace Monetra.Domain.BackOffice.Interfaces.Repostiries.Portifolio;

public interface IPortfolioRepository : IRepository<Portfolio>
{
    Task<IEnumerable<Portfolio>> GetPortfolioFromCustumer(Guid id);
    Task<IEnumerable<Portfolio>> GetPortfolioWithTransactionFromCustumer(Guid id);


}