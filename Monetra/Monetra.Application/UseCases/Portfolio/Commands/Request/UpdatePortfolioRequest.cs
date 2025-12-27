using MediatR;
using Monetra.Application.Commum.Abstraction;
using Monetra.Application.DTOs.Portfolio;

namespace Monetra.Application.UseCases.Portfolio.Commands.Request;

public record UpdatePortfolioRequest(UpdatePortifolioDTO Model) : IRequest<Result>;