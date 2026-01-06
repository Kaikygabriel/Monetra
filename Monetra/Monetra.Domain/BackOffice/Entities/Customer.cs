using Monetra.Domain.BackOffice.Commum.Abstraction;
using Monetra.Domain.BackOffice.Entities.Abstraction;

namespace Monetra.Domain.BackOffice.Entities;

public class Customer : Entity
{
    protected Customer()
    {
        
    }
    public Customer(User user, string name,decimal salary)
    {
        User = user;
        Name = name;
        Id = Guid.NewGuid();
        Salary = salary;
        FinancialHealth = new ();
    }

    public string Name { get;private set; }
    public User User { get;private set; }
    public Mark Mark { get;private set; }
    public decimal Salary { get;private set; }
    public Expense Expense { get;private set; }
    public FinancialHealth FinancialHealth { get;private set; }
    public List<Portfolio> Portfolios { get; private set; } = new();

    public void AddPortifolio(Portfolio port)
    {
        if (port is null) return; 
        Portfolios.Add(port);
    }

    public void AddMark(Mark mark)
        => Mark = mark;
    public void AddExpense(Expense expense)
        => Expense = expense;
    
    public Result AlterSalary(decimal newSalary)
    {
        if (SalaryIsInvalid(newSalary))
            return Result.Failure(new("Salary.Invalid", "Salary invalid"));
        Salary = newSalary;
        return Result.Success();
    }
    private bool SalaryIsInvalid(decimal salary)
        => salary < 0; 
}