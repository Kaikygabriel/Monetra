using System.Linq.Expressions;
using Monetra.Domain.BackOffice.Entities;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Customer;
using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Test.Mock;

public class FakeCustomerRepository : ICustomerRepository
{
    private readonly List<Customer> _customers;

    public FakeCustomerRepository()
    {
        var user1 = new User("123456", new Email("joao@email.com"));
        var user2 = new User("123456", new Email("maria@email.com"));
        var user3 = new User("123456", new Email("carlos@email.com"));
        
        _customers = new List<Customer>
        {
            new Customer(user1, "João Silva"),
            new Customer(user2, "Maria Oliveira"),
            new Customer(user3, "Carlos Souza")
        };
    }

    public Task<IEnumerable<Customer>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Customer>>(_customers);
    }

    public Task<Customer?> GetByPredicate(Expression<Func<Customer, bool>> predicate)
    {
        var customer = _customers.AsQueryable().FirstOrDefault(predicate);
        return Task.FromResult(customer);
    }

    public void Create(Customer entity)
    {
        if (entity == null) return;
        _customers.Add(entity);
    }

    public void Update(Customer entity)
    {
        if (entity == null) return;

        var index = _customers.FindIndex(c => c.Id == entity.Id);
        if (index >= 0)
            _customers[index] = entity;
    }

    public void Delete(Customer entity)
    {
        if (entity == null) return;
        _customers.Remove(entity);
    }

    public Task<Customer?> GetByEmail(string email)
    {
        var customer = _customers
            .FirstOrDefault(c => c.User.Email.Address == email);

        return Task.FromResult(customer);
    }
}