using Monetra.Domain.BackOffice.Interfaces.Repostiries.User;
using Monetra.Infra.Data.Context;

namespace Monetra.Infra.Repositories.User;

public class UserRepository : Repository<Domain.BackOffice.Entities.User>,IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
}