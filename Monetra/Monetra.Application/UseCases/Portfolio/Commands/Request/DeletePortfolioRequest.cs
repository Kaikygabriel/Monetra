using MediatR;
using Monetra.Domain.BackOffice.Commum.Abstraction;

namespace Monetra.Application.UseCases.Portfolio.Commands.Request;

public record DeletePortfolioRequest(Guid CustomerId,Guid PortfolioId) : IRequest<Result>;