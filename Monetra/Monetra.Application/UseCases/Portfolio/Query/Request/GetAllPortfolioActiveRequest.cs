using MediatR;
using Monetra.Application.DTOs.Portfolio;
using Monetra.Domain.BackOffice.Commum;

namespace Monetra.Application.UseCases.Portfolio.Query.Request;

public record GetAllPortfolioActiveRequest(int Skip,int Take):IRequest<Result<IEnumerable<GetAllPortfolioActiveDTo>>>;