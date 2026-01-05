using Microsoft.EntityFrameworkCore;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.RecurringTransaction;
using Monetra.Infra.Data.Context;

namespace Monetra.Infra.Repositories.RecurringTransaction;

public class RecurringTransactionRepositry : Repository<Domain.BackOffice.Entities.RecurringTransaction>
    ,IRecurringTransactionRepository
{
    public RecurringTransactionRepositry(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Domain.BackOffice.Entities.RecurringTransaction>> GetPendingTransactionsByCurrentDay()
    {
        var dayCurrent = DateTime.Now.Day;
        return await _context
            .RecurringTransactions
            .Where(x => x.MonthDayPayment == dayCurrent)
            .ToListAsync();
    }
}