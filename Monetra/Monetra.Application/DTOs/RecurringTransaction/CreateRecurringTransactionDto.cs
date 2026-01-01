using System.ComponentModel.DataAnnotations;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Enum;

namespace Monetra.Application.DTOs.RecurringTransaction;

public class CreateRecurringTransactionDto
{
    [Required]
    public decimal Value { get; set; }
    [Required]
    public string CostName { get; set; }
    [Required]
    public int MonthDayPayment { get; set; }
    [Required]
    public Guid IdPortfolio { get; set; }
    [Required]
    public Guid IdCustomerId { get; set; }
    [Required] 
    public TransactionType TransactionType { get; set; }

    public static implicit operator
        Result<Domain.BackOffice.Entities.RecurringTransaction>(CreateRecurringTransactionDto model)
        => Domain.BackOffice.Entities.RecurringTransaction.Factories.Create(model.CostName, model.Value,
            model.MonthDayPayment,model.TransactionType);
}