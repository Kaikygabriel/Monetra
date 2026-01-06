namespace Monetra.Application.DTOs.Custumer;

public class CustomerDashboardDto
{
    public CustomerDashboardDto(int financialHealth)
    {
        FinancialHealth = financialHealth;
    }
    public CustomerDashboardDto(string name, string email,Domain.BackOffice.Entities.Mark mark,
        IEnumerable<Domain.BackOffice.Entities.Portfolio> portfolios, int financialHealth)
    {
        Name = name;
        Email = email;
        Portfolios = portfolios;
        FinancialHealth = financialHealth;
        Mark = mark;
    }

    public string Name { get; set; } 
    public int FinancialHealth { get; set; } 
    public string Email { get; set; }
    public IEnumerable<Domain.BackOffice.Entities.Portfolio>Portfolios { get; set; }
    public Domain.BackOffice.Entities.Mark? Mark { get; set; } 
}