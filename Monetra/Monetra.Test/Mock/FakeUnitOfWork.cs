using Monetra.Domain.BackOffice.Interfaces.Repostiries;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Customer;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Mark;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Portifolio;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.RecurringTransaction;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Transaction;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.User;

namespace Monetra.Test.Mock;

public class FakeUnitOfWork : IUnitOfWork
{
    public ICustomerRepository CustomerRepository { get; } = new FakeCustomerRepository();
    public IPortfolioRepository PortfolioRepository { get; }
    public IUserRepository UserRepository { get; } = new FakeUserRepository();
    public ITransactionRepository TransactionRepository { get; }
    public IMarkRepository MarkRepository { get; } = new FakeMarkRepository();
    public IRecurringTransactionRepository RecurringTransactionRepository { get; }

    public Task CommitAsync()
        => Task.CompletedTask;
}