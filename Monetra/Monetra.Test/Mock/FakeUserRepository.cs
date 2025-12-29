using System.Linq.Expressions;
using Monetra.Domain.BackOffice.Entities;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.User;
using Monetra.Domain.BackOffice.ObjectValues;

public class FakeUserRepository : IUserRepository
{
    private readonly List<User> _users;

    public FakeUserRepository()
    {
        _users = new List<User>
        {
            new User("12345", new Email("admin@monetra.com")),
            new User("12345", new Email("user@monetra.com")),
            new User("12345", new Email("test@monetra.com"))
        };
    }

    public Task<IEnumerable<User>> GetAllAsync()
        => Task.FromResult(_users.AsEnumerable());

    public Task<User?> GetByPredicate(Expression<Func<User, bool>> predicate)
        => Task.FromResult(_users.FirstOrDefault(predicate.Compile()));

    public void Create(User entity) => _users.Add(entity);

    public void Update(User entity)
    {
        var index = _users.FindIndex(x => x.Id == entity.Id);
        if (index >= 0)
            _users[index] = entity;
    }

    public void Delete(User entity)
        => _users.RemoveAll(x => x.Id == entity.Id);
}