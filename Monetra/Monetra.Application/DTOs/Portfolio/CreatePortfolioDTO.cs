using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Application.DTOs.Portfolio;

public class CreatePortfolioDTO
{
    public string Userid { get; set; }
    public string Title { get; set; }
    public decimal ValueFixed { get; set; }
    public decimal ValueVisible { get; set; }

    public static implicit operator Domain.BackOffice.Entities.Portfolio(CreatePortfolioDTO model)
        => new(new Investment(model.ValueFixed), new(model.ValueVisible), model.Title);
}