using MediatR;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Commum.Abstraction;
using Monetra.Domain.BackOffice.Enum;

namespace Monetra.Application.UseCases.Portfolio.Commands.Request;

public record AddedValuePortfolioRequest(Guid IdPortfolio,Guid CustomerId,TransactionType Type,decimal Value)
    : IRequest<Result>; 