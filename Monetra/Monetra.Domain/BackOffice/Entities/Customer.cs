using Monetra.Domain.BackOffice.Entities.Abstraction;

namespace Monetra.Domain.BackOffice.Entities;

public class Customer : Entity
{
    protected Customer()
    {
        
    }
    public Customer(User user, string name)
    {
        User = user;
        Name = name;
        Id = Guid.NewGuid();
    }

    public string Name { get;private set; }
    public User User { get;private set; }
    public Mark Mark { get;private set; }
    public List<Portfolio> Portfolios { get; private set; } = new();

    public void AddPortifolio(Portfolio port)
    {
        if (port is null) return; 
        Portfolios.Add(port);
    }

    public void AddMark(Mark mark)
        => Mark = mark;
    
    
}