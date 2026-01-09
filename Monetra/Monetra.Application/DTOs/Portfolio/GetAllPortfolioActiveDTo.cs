namespace Monetra.Application.DTOs.Portfolio;

public record GetAllPortfolioActiveDTo
{
    public GetAllPortfolioActiveDTo()
    {
    }
    public GetAllPortfolioActiveDTo(string title, decimal reservation, decimal fixedIncome, decimal variableIncome)
    {
        Title = title;
        Reservation = reservation;
        FixedIncome = fixedIncome;
        VariableIncome = variableIncome;
    }

    public string Title { get; set; }
    public decimal Reservation { get; set; }
    public decimal FixedIncome { get; set; }
    public decimal VariableIncome { get; set; }
    
    public static IEnumerable<GetAllPortfolioActiveDTo> ToGetAllPortfolioActiveDTos
        (IEnumerable<Domain.BackOffice.Entities.Portfolio>ports)
            => ports.Select(x=> new GetAllPortfolioActiveDTo(
                x.Title,
                x.Reservation.Value,
                x.FixedIncome.Value,
                x.VariableIncome.Value));
}