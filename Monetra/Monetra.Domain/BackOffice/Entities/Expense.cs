using System.Text.Json.Serialization;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Entities.Abstraction;

namespace Monetra.Domain.BackOffice.Entities;

public class Expense : Entity
{
    protected Expense()
    {
        
    }
    private Expense(string descriptions)
    {
        Description = descriptions;
    }

    [JsonIgnore]
    public Guid CustomerId { get;  }
    [JsonIgnore]
    public Customer Customer { get;}

    public decimal Value { get; private set; }
    public List<RecurringTransaction> RecurringTransactions { get; private set; } = new();
    public string Description { get; private set; }
    [JsonIgnore]
    public Guid? PortfolioId { get;private set; }
    [JsonIgnore]
    public Portfolio? Portfolio { get;private set; }
    

    public static class Factories
    {
        public static Result<Expense> Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description) || description.Length < 2)
                return Result<Expense>.Failure(new("Expense.InvalidParameters", "Expense InvalidParameters"));
            return Result<Expense>.Success(new(description));
        }
    }
    public void SetPortfolio(Portfolio portfolio)
    {
        Portfolio = portfolio;
        PortfolioId = portfolio.Id;
    }

    public void AddRecurringTransaction(RecurringTransaction transaction)
    {
        Value += transaction.Value; 
        RecurringTransactions.Add(transaction);
    } 

}