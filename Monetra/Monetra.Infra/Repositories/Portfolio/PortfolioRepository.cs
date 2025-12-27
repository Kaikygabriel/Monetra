using Monetra.Domain.BackOffice.Interfaces.Repostiries.Portifolio;
using Monetra.Infra.Data.Context;

namespace Monetra.Infra.Repositories.Portfolio;

public class PortfolioRepository: Repository<Domain.BackOffice.Entities.Portfolio>, IPortfolioRepository
{
    public PortfolioRepository(AppDbContext context) : base(context)
    {
    }
}