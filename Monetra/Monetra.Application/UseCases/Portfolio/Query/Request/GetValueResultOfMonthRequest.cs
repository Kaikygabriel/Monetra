using MediatR;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Entities;

namespace Monetra.Application.UseCases.Portfolio.Query.Request;

public record GetValueResultOfMonthRequest(Guid IdCustomer , Guid IdPortfolio, int MonthsQuantity) :
    IRequest<Result<IEnumerable<Transaction>>>;