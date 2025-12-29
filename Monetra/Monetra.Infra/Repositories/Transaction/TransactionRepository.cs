using Microsoft.EntityFrameworkCore;
using Monetra.Domain.BackOffice.Entities;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Transaction;
using Monetra.Infra.Data.Context;

namespace Monetra.Infra.Repositories.Transaction;

public class TransactionRepository : Repository<Domain.BackOffice.Entities.Transaction>, ITransactionRepository
{
    public TransactionRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Domain.BackOffice.Entities.Transaction>> GetByPortfolioId(Guid id)
    {
        return  await _context.Transactions.AsNoTracking().Where(x => x.PortfolioId == id).ToListAsync();
    }
}