namespace Monetra.Application.DTOs.Custumer;

public record CustomerDashboardDTO
    (
        string Name,
        string Email,
        IEnumerable<Domain.BackOffice.Entities.Portfolio>? Portfolios,
        Domain.BackOffice.Entities.Mark? Mark);