using System.Linq.Expressions;
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
        var listP = await _context.Portfolios.Where(x => x.CustomerId == id)
            .Include(x=>x.Transactions)
            .ToListAsync();
        return listP;
    }

    public async Task<IEnumerable<Domain.BackOffice.Entities.Portfolio>> GetAllByVisible(int skip, int take)
    {
        if (skip < 0 || take < 0)
        {
            skip = 0;
            take = 25;
        }
        if (take > 50)
            take = 50;

        return await _context.Portfolios
            .AsNoTracking()
            .Skip(skip)
            .Take(take)
            .Where(x => x.Visible)
            .ToListAsync();
    }
}