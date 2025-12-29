using Microsoft.EntityFrameworkCore;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Customer;
using Monetra.Infra.Data.Context;

namespace Monetra.Infra.Repositories.Customer;

public class CustomerRepository : Repository<Domain.BackOffice.Entities.Customer>,ICustomerRepository
{
    public CustomerRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Domain.BackOffice.Entities.Customer?> GetByEmail(string email)
    {
        return await  _context.Customers.AsNoTracking().Include(x=>x.User)
            .FirstOrDefaultAsync(x => x.User.Email.Address == email);
    }
    
}