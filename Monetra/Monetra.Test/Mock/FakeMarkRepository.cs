using System.Linq.Expressions;
using Monetra.Domain.BackOffice.Entities;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Mark;

namespace Monetra.Test.Mock;

public class FakeMarkRepository : IMarkRepository
{
    private readonly List<Mark> _marks;

    public FakeMarkRepository()
    {
        _marks = new List<Mark>();

        Seed();
    }

    private void Seed()
    {
        var customerId1 = Guid.Parse("3f6c9b8a-4e21-4c7d-9c5b-8e2d7a4f1c92");
        var customerId2 = Guid.NewGuid();

        var mark1 = Mark.Factories.Create(
            title: "Comprar notebook",
            value: 5000,
            customerId: customerId1,
            deadline: DateTime.UtcNow.AddMonths(6)
        ).Value;

        var mark2 = Mark.Factories.Create(
            title: "Viagem de férias",
            value: 8000,
            customerId: customerId1,
            deadline: DateTime.UtcNow.AddMonths(10)
        ).Value;

        var mark3 = Mark.Factories.Create(
            title: "Reserva de emergência",
            value: 10000,
            customerId: customerId2,
            deadline: DateTime.UtcNow.AddYears(1)
        ).Value;

        _marks.AddRange(new[] { mark1, mark2, mark3 });
    }

    public Task<IEnumerable<Mark>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Mark>>(_marks);
    }

    public Task<Mark?> GetByPredicate(Expression<Func<Mark, bool>> predicate)
    {
        var compiled = predicate.Compile();
        return Task.FromResult(_marks.FirstOrDefault(compiled));
    }

    public void Create(Mark entity)
    {
        _marks.Add(entity);
    }

    public void Update(Mark entity)
    {
        var index = _marks.FindIndex(x => x.Id == entity.Id);
        if (index >= 0)
            _marks[index] = entity;
    }

    public void Delete(Mark entity)
    {
        _marks.RemoveAll(x => x.Id == entity.Id);
    }
}