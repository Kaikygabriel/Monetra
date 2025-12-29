using MediatR;
using Monetra.Application.DTOs.Portfolio;
using Monetra.Domain.BackOffice.Commum.Abstraction;

namespace Monetra.Application.UseCases.Portfolio.Commands.Request;

public record UpdatePortfolioRequest(UpdatePortifolioDTO Model) : IRequest<Result>;