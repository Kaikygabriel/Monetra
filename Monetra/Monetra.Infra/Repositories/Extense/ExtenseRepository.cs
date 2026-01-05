using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Monetra.Domain.BackOffice.Entities;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Expense;
using Monetra.Infra.Data.Context;

namespace Monetra.Infra.Repositories.Extense;

public class ExtenseRepository : Repository<Expense>,IExpenseRepository
{
    public ExtenseRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Expense> GetWithPortfolioByPredicate(Expression<Func<Expense, bool>> predicate)
    {
        return await _context
            .Expenses
            .Include(x => x.Portfolio)
            .FirstOrDefaultAsync(predicate);
    }
}