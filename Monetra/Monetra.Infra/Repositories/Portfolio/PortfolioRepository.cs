using Microsoft.EntityFrameworkCore;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Portifolio;
using Monetra.Infra.Data.Context;

namespace Monetra.Infra.Repositories.Portfolio;

public class PortfolioRepository: Repository<Domain.BackOffice.Entities.Portfolio>, IPortfolioRepository
{
    public PortfolioRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Domain.BackOffice.Entities.Portfolio>> GetPortfolioFromCustumer(Guid id)
    {
        var listP = await _context.Portfolios.AsNoTracking().Where(x => x.CustomerId == id).ToListAsync();
        return listP;
    }
    public async Task<IEnumerable<Domain.BackOffice.Entities.Portfolio>> GetPortfolioWithTransactionFromCustumer(Guid id)
    {
        var listP = await _context.Portfolios.AsNoTracking().Where(x => x.CustomerId == id)
            .Include(x=>x.Transactions)
            .ToListAsync();
        return listP;
    }
}