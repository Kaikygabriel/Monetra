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
    }

    public string Name { get; set; }
    public User User { get; set; }
    public Portfolio Portfolio { get; set; }
    public Guid PortfolioId { get; set; }

    public void AddPortfolio(Portfolio port)
    {
        if (Portfolio is null) return; 
        Portfolio = port;
    } 
}