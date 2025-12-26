using MediatR;
using Monetra.Application.Commum;

namespace Monetra.Application.UseCases.Portfolio.Query.Request;

public record GetPortfolioByCustumerIdRequest(Guid Id) : IRequest<Result<Domain.BackOffice.Entities.Portfolio>>;