using System.Text.Json.Serialization;
using Monetra.Domain.BackOffice.Entities.Abstraction;
using Monetra.Domain.BackOffice.Enum;
using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Domain.BackOffice.Entities;

public class Portfolio : Entity
{
    protected Portfolio()
    {
        
    }
    
    public Portfolio(Investment fixedIncome, Investment variableIncome,Investment reservation, string title)
    {
        if (string.IsNullOrEmpty(title) || title.Length < 3)
            throw new System.Exception("Title in portfolio is invalid");
        Visible = false;
        CreateDate = DateTime.Now;
        Title = title;
        FixedIncome = fixedIncome;
        VariableIncome = variableIncome;
        Id = Guid.NewGuid();
        Reservation = reservation;
    }

    public bool Visible { get;private set; }
    public string Title { get;private set; }
    public DateTime CreateDate { get;private set; }
    public Investment Reservation { get;private set; }
    public Investment FixedIncome  { get;private set; }
    public Investment VariableIncome  { get;private set; }
    public Customer Customer { get;private set; }
    public Guid CustomerId { get;set; }
    public List<Transaction> Transactions { get; private set; } = new();
    [JsonIgnore]
    public Expense? Exepense  { get;private set; }
    public Guid? ExpenseId { get;private set; }
    public decimal TotalPrice()
        => FixedIncome.Value + VariableIncome.Value;

    public void ConvertToNoVisible()
        => Visible = false;
    public void ConvertToVisible()
        => Visible = true;

    private void ApplyTransaction(decimal signedValue, TransactionType type)
    {
        if (type == TransactionType.FixedIncome)
            FixedIncome.AddValue(signedValue);
        else
            VariableIncome.AddValue(signedValue);

        Transactions.Add(
            Transaction.Factories.Create(Id, signedValue, type)
        );
    }

    public void AddValue(decimal value, TransactionType type)
    {
        if (value <= 0) throw new System.Exception("Invalid value");
        ApplyTransaction(value, type);
    }

    public void RemoveValue(decimal value, TransactionType type)
    {
        if (value <= 0) throw new System.Exception("Invalid value");
        ApplyTransaction(-value, type);
    }
}