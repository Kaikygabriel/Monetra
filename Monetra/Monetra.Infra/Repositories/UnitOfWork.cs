using Monetra.Domain.BackOffice.Interfaces.Repostiries;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Customer;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Mark;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Portifolio;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.RecurringTransaction;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Transaction;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.User;
using Monetra.Infra.Data.Context;
using Monetra.Infra.Repositories.Customer;
using Monetra.Infra.Repositories.Mark;
using Monetra.Infra.Repositories.Portfolio;
using Monetra.Infra.Repositories.RecurringTransaction;
using Monetra.Infra.Repositories.Transaction;
using Monetra.Infra.Repositories.User;

namespace Monetra.Infra.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private TransactionRepository _transactionRepository;
    private RecurringTransactionRepositry _recurringTransactionRepository;
    private PortfolioRepository _portfolioRepository;
    private UserRepository _userRepository;
    private CustomerRepository _customerRepository;
    private MarkRepository _markRepository;
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public ICustomerRepository CustomerRepository
    {
        get
        {
            return _customerRepository = _customerRepository ?? new(_context);
        }
    }

    public IPortfolioRepository PortfolioRepository
    {
        get
        {
            return _portfolioRepository = _portfolioRepository ?? new(_context);
        }
    }

    public IUserRepository UserRepository
    {
        get
        {
            return _userRepository = _userRepository ?? new(_context);
        }
    }

    public ITransactionRepository TransactionRepository
    {
        get
        {
            return _transactionRepository = _transactionRepository ?? new(_context);
        }
    }

    public IMarkRepository MarkRepository
    {
        get
        {
            return  _markRepository =_markRepository?? new (_context);
        }
    }

    public IRecurringTransactionRepository RecurringTransactionRepository
    {
        get
        {
            return _recurringTransactionRepository = _recurringTransactionRepository ?? new(_context);
        }
    }

    public async Task CommitAsync()
        => await _context.SaveChangesAsync();
}