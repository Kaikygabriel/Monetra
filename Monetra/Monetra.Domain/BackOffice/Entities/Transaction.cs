using System.Text.Json.Serialization;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Entities.Abstraction;
using Monetra.Domain.BackOffice.Enum;
using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Domain.BackOffice.Entities;

public class Transaction : Entity
{
    protected Transaction(Category category)
    {
        Category = category;
    }
    
    
    private Transaction(
        Guid portfolioId,
        decimal amount,
        TransactionType type, Category category)
    {
        PortfolioId = portfolioId;
        Amount = amount;
        Type = type;
        Category = category;
        CreatedAt = DateTime.UtcNow;
        Id = Guid.NewGuid();
    }
    
    public Guid PortfolioId { get; }
    public decimal Amount { get; private set; }
    public TransactionType Type { get; private set;}
    public DateTime CreatedAt { get; private set;}
    public Category Category { get; set; }
    [JsonIgnore]
    public Portfolio Portfolio { get; private set;}


    public static class Factories
    {
        public static Transaction Create(
            Guid portfolioId,
            decimal amount,
            TransactionType type,
            Category category)
        {
            return new Transaction(portfolioId, amount, type,category);
        }
    }
}