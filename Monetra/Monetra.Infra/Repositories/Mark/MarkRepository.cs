using Monetra.Domain.BackOffice.Interfaces.Repostiries.Mark;
using Monetra.Infra.Data.Context;

namespace Monetra.Infra.Repositories.Mark;

public class MarkRepository : Repository<Domain.BackOffice.Entities.Mark>,IMarkRepository
{
    public MarkRepository(AppDbContext context) : base(context)
    {
    }
}