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

        return new List<Portfolio>
        {
            new Portfolio(
                fixedIncome: new Investment(5000),
                variableIncome: new Investment(3000),
                reservation: new Investment(2000),
                title: "Carteira Principal"
            )
            {
                Id = Guid.Parse("3f6c9b8a-4e21-4c7d-9c5b-8e2d7a4f1c92"),
                CustomerId = customerId1
            },

            new Portfolio(
                fixedIncome: new Investment(8000),
                variableIncome: new Investment(12000),
                reservation: new Investment(5000),
                title: "Investimentos Longo Prazo"
            )
            {
                CustomerId = customerId1
            },

            new Portfolio(
                fixedIncome: new Investment(2000),
                variableIncome: new Investment(1500),
                reservation: new Investment(1000),
                title: "Carteira Secundária"
            )
            {
                CustomerId = customerId2
            }
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
}
