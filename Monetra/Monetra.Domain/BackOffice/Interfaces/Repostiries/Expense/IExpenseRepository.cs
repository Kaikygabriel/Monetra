using System.Linq.Expressions;

namespace Monetra.Domain.BackOffice.Interfaces.Repostiries.Expense;

public interface IExpenseRepository :IRepository<Entities.Expense>
{
    Task<Entities.Expense> GetWithRecurringTransaction(Expression<Func<Entities.Expense, bool>> predicate);
    Task<Entities.Expense> GetWithPortfolioByPredicate(Expression<Func<Entities.Expense, bool>> predicate);
}