namespace Monetra.Application.DTOs.Portfolio;

public class UpdatePortifolioDTO
{
    public Guid Userid { get; set; }
    public  Domain.BackOffice.Entities.Portfolio Portfolio { get; set; }

}