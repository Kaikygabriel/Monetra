using Monetra.Domain.BackOffice.Entities;
using Monetra.Domain.BackOffice.Enum;

namespace Monetra.Test.Domain.BackOffice.Entities;

public class RecurringTransactionsTest
{
    [Fact]
    public void Should_Return_False_If_Parameters_Of_Create_IsInvalid()
    {
        var result = RecurringTransaction.Factories.Create
            (string.Empty, -10, 0, TransactionType.FixedIncome);
        Assert.False(result.IsSuccess);
    }
    
    [Fact]
    public void Should_Return_True_If_Parameters_Is_Valid()
    {
        var result = RecurringTransaction.Factories.Create
            ("teste", 100, 1, TransactionType.FixedIncome);
        Assert.True(result.IsSuccess);
    }
}