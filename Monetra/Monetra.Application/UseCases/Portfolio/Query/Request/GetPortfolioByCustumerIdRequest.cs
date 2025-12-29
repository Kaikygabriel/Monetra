using MediatR;
using Monetra.Domain.BackOffice.Commum;

namespace Monetra.Application.UseCases.Portfolio.Query.Request;

public record GetPortfolioByCustumerIdRequest(Guid Id) : IRequest<Result<IEnumerable<Domain.BackOffice.Entities.Portfolio>>>;