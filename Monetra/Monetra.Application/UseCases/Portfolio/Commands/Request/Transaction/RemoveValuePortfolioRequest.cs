using MediatR;
using Monetra.Domain.BackOffice.Commum.Abstraction;
using Monetra.Domain.BackOffice.Enum;

namespace Monetra.Application.UseCases.Portfolio.Commands.Request.Transaction;

public record RemoveValuePortfolioRequest(Guid IdPortfolio,Guid CustomerId,TransactionType Type,decimal Value,string NameCategory)
    : IRequest<Result>; 