using Monetra.Domain.BackOffice.Entities.Abstraction;

namespace Monetra.Domain.BackOffice.Entities;

public class FinancialHealth  : Entity
{
    public FinancialHealth()
    {
        
    }
    public FinancialHealth(int percentage)
    {
        Percentage = percentage;
    }
    public int Percentage { get; set; }

    public void Recalculate(Customer customer,Expense expense,IEnumerable<Portfolio>portfolios)
    {
        var liquidity = CalculateLiquidityScore
            (customer.Salary,expense.RecurringTransactions.Sum(x=>x.Value));
        var commitment = CalculateIncomeCommitmentScore
            (customer.Salary,expense.RecurringTransactions.Sum(x=>x.Value));
        var transactions = portfolios.Select(x => x.Transactions.ToList());
        int regularity = 0 ; 
        foreach(var transaction in transactions)
            regularity += CalculateRegularityScore(transaction);

        Percentage = (int)(
            liquidity * 0.30m +
            commitment * 0.25m +
            regularity * 0.35m
        );
    }
    private int CalculateLiquidityScore(decimal totalValue,decimal recurringTransactions)
    {
        var monthts = totalValue/recurringTransactions;
        
        if (monthts <= 0) return 0;
        if (monthts >= 1 && monthts < 3) return 20;
        if (monthts >= 3 && monthts < 6) return 60;
        
        return 100; 
    }
    private int CalculateIncomeCommitmentScore(decimal salary,decimal recurringTransactions)
    {
        if (salary <= 0) return 0;
        
        var percentageFixedRemovePortfolio = (recurringTransactions / salary) * 100;
        if (percentageFixedRemovePortfolio > 70) return 0;
        if (percentageFixedRemovePortfolio >= 50) return 40;
        if (percentageFixedRemovePortfolio >= 30) return 70;

        return 100;
    }

    private int CalculateRegularityScore(List<Transaction>transactions)
    {
        if (transactions is null)
            return 0;
        var average = transactions.Average(x => x.Amount);
        var listValues = transactions.Select(x => x.Amount);
        if (average <= 0)
            return 0;
        
        var variation = StandardDeviation(listValues, average) / average;
               
        if (variation < 0.10m) return 100;
        if (variation < 0.25m) return 70;
        return 40;
    }
    private decimal StandardDeviation(IEnumerable<decimal>values,decimal average)
    {
        var variance = values
            .Select(x => x - average)
            .Select(x => x * x)
            .Average();

        return (decimal)Math.Sqrt((double)variance);
    }
}