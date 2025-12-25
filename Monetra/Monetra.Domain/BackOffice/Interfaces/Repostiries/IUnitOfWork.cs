using Monetra.Domain.BackOffice.Interfaces.Repostiries.Customer;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Portifolio;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.User;

namespace Monetra.Domain.BackOffice.Interfaces.Repostiries;

public interface IUnitOfWork
{
    public ICustomerRepository CustomerRepository { get; }
    public IPortfolioRepository PortfolioRepository { get; }
    public IUserRepository UserRepository { get; }
    Task CommitAsync();
}