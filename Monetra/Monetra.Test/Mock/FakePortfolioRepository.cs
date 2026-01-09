using System.Linq.Expressions;
using Monetra.Domain.BackOffice.Entities;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Portifolio;
using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Test.Mock;

public class FakePortfolioRepository : IPortfolioRepository
{
    private readonly List<Portfolio> _portfolios;

    public FakePortfolioRepository()
    {
        _portfolios = Seed();
    }

    private static List<Portfolio> Seed()
    {
        var customerId1 = Guid.Parse("3f6c9b8a-4e21-4c7d-9c5b-8e2d7a4f1c92");
        var customerId2 = Guid.NewGuid();
        var port1 = 
            Portfolio.Factories.
                Create(new Investment(200),new(200),
                   new(200),"teste").Value;
        port1.Id = Guid.Parse("3f6c9b8a-4e21-4c7d-9c5b-8e2d7a4f1c92");
        port1.CustomerId = customerId1;
        
        return new List<Portfolio>
        {
            port1
            
        };
    }

    public Task<IEnumerable<Portfolio>> GetAllAsync()
        => Task.FromResult(_portfolios.AsEnumerable());

    public Task<Portfolio?> GetByPredicate(Expression<Func<Portfolio, bool>> predicate)
        => Task.FromResult(_portfolios.AsQueryable().FirstOrDefault(predicate));

    public void Create(Portfolio entity)
        => _portfolios.Add(entity);

    public void Update(Portfolio entity)
    {
        var index = _portfolios.FindIndex(p => p.Id == entity.Id);
        if (index >= 0)
            _portfolios[index] = entity;
    }

    public void Delete(Portfolio entity)
        => _portfolios.RemoveAll(p => p.Id == entity.Id);

    public Task<IEnumerable<Portfolio>> GetPortfolioFromCustumer(Guid id)
        => Task.FromResult(
            _portfolios.Where(p => p.CustomerId == id)
        );

    public Task<IEnumerable<Portfolio>> GetPortfolioWithTransactionFromCustumer(Guid id)
        => Task.FromResult(
            _portfolios
                .Where(p => p.CustomerId == id && p.Transactions.Any())
        );

    public Task<IEnumerable<Portfolio>> GetAllByVisible(int skip, int take)
    {
        throw new NotImplementedException();
    }
}
