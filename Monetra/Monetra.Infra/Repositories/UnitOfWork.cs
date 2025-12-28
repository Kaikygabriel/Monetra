using Monetra.Domain.BackOffice.Interfaces.Repostiries;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Customer;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Portifolio;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.User;
using Monetra.Infra.Data.Context;
using Monetra.Infra.Repositories.Customer;
using Monetra.Infra.Repositories.Portfolio;
using Monetra.Infra.Repositories.User;

namespace Monetra.Infra.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private PortfolioRepository _portfolioRepository;
    private UserRepository _userRepository;
    private CustomerRepository _customerRepository;
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

    public async Task CommitAsync()
        => await _context.SaveChangesAsync();
}