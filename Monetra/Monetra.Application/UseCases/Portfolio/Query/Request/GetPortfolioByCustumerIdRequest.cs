using MediatR;
using Monetra.Application.Commum;
using Monetra.Application.UseCases.Portfolio.Query.Response;

namespace Monetra.Application.UseCases.Portfolio.Query.Request;

public record GetPortfolioByCustumerIdRequest(Guid Id) : IRequest<Result<IEnumerable<PotfolioGetByUserReponse>>>;