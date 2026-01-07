using System.ComponentModel.DataAnnotations;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Application.DTOs.Portfolio;

public class CreatePortfolioDTO
{
    [Required]
    public string Userid { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public decimal ValueFixed { get; set; }
    [Required]
    public decimal ValueVisible { get; set; }
    [Required]
    public decimal Reservation { get; set; }
    
    public static implicit operator Result<Domain.BackOffice.Entities.Portfolio>(CreatePortfolioDTO model)
        =>Domain.BackOffice.Entities.Portfolio.Factories.Create(
            new (model.ValueFixed), 
            new (model.ValueVisible),
            new (model.Reservation),
            model.Title);
}