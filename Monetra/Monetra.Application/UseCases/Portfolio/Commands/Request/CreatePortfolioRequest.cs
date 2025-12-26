using MediatR;
using Monetra.Application.Commum;
using Monetra.Application.Commum.Abstraction;
using Monetra.Application.DTOs.Portfolio;

namespace Monetra.Application.UseCases.Portfolio.Commands.Request;

public record CreatePortfolioRequest(CreatePortfolioDTO Model)  : IRequest<Result>; 