using System.Text.Json.Serialization;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Entities.Abstraction;
using Monetra.Domain.BackOffice.Enum;

namespace Monetra.Domain.BackOffice.Entities;

public class Transaction : Entity
{
    protected Transaction()
    {
        
    }
    public Guid PortfolioId { get; }
    public decimal Amount { get; private set; }
    public TransactionType Type { get; private set;}
    public DateTime CreatedAt { get; private set;}
    [JsonIgnore]
    public Portfolio Portfolio { get; private set;}

    private Transaction(
        Guid portfolioId,
        decimal amount,
        TransactionType type)
    {
        PortfolioId = portfolioId;
        Amount = amount;
        Type = type;
        CreatedAt = DateTime.Now;
    }

    public static class Factories
    {
        public static Transaction Create(
            Guid portfolioId,
            decimal amount,
            TransactionType type)
        {
            return new Transaction(portfolioId, amount, type);
        }
    }
}