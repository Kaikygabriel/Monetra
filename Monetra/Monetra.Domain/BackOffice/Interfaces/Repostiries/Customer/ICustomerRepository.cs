using Monetra.Domain.BackOffice.Entities;

namespace Monetra.Domain.BackOffice.Interfaces.Repostiries.Customer;

public interface ICustomerRepository : IRepository<Entities.Customer>
{
    Task<Entities.Customer?> GetByEmail(string email);
}

