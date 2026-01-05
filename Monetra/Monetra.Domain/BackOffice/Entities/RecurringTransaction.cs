using System.Text.Json.Serialization;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Entities.Abstraction;
using Monetra.Domain.BackOffice.Enum;

namespace Monetra.Domain.BackOffice.Entities;

public class RecurringTransaction  : Entity
{
    protected RecurringTransaction(TransactionType transactionType)
    {
        TransactionType = transactionType;
    }
    protected RecurringTransaction(string costName, decimal value, int dayPayment, TransactionType transactionType)
    {
        CostName = costName;
        Value = value;
        MonthDayPayment = dayPayment;
        TransactionType = transactionType;
        Id = Guid.NewGuid();
    }

    public TransactionType TransactionType { get; set; }
    public string CostName { get;init; }
    public decimal Value { get;init; }
    public int MonthDayPayment { get;init; }
    [JsonIgnore]
    public Expense Expense{ get; private set;}

    public Guid ExpenseId { get; set; }

    public static class Factories
    {
        public static Result<RecurringTransaction> Create(string costName, decimal value, int dayPayment,TransactionType transactionType)
        {
            if (!IsValid(costName, value, dayPayment))
                return Result<RecurringTransaction>.Failure(new("Parameters.RecurringTransaction",
                    "Parameters is invalid!"));
            
            return Result<RecurringTransaction>.Success(new RecurringTransaction(costName,value,dayPayment,transactionType));
        }

        private static bool IsValid(string costName, decimal value, int dayPayment)
        {
            if (dayPayment > 30 || dayPayment < 1)
                return false;
            if (value < 1)
                return false;
            if (string.IsNullOrWhiteSpace(costName))
                return false;
            return true;
        }
    }
}