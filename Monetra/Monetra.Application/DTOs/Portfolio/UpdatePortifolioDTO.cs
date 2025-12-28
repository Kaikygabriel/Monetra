namespace Monetra.Application.DTOs.Portfolio;

public class UpdatePortifolioDTO
{
    public Guid CustomerId { get; set; }
    public  Domain.BackOffice.Entities.Portfolio Portfolio { get; set; }

}