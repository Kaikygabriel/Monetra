using Monetra.Domain.BackOffice.Interfaces.Repostiries.Customer;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Mark;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Portifolio;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.RecurringTransaction;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Transaction;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.User;

namespace Monetra.Domain.BackOffice.Interfaces.Repostiries;

public interface IUnitOfWork
{
    public ICustomerRepository CustomerRepository { get; }
    public IPortfolioRepository PortfolioRepository { get; }
    public IUserRepository UserRepository { get; }
    public ITransactionRepository TransactionRepository { get; }
    public IMarkRepository MarkRepository { get;  }
    public IRecurringTransactionRepository RecurringTransactionRepository { get;  }
    Task CommitAsync();
}