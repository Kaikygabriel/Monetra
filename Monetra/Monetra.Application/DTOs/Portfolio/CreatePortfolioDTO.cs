namespace Monetra.Application.DTOs.Portfolio;

public class CreatePortfolioDTO
{
    public string Userid { get; set; }
    public  Domain.BackOffice.Entities.Portfolio Portfolio { get; set; }
}