using MediatR;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Application.DTOs.Portfolio;

namespace Monetra.Application.UseCases.Portfolio.Query.Request;

public record GetValueResultOfMonthRequest(Guid IdCustomer , Guid IdPortfolio) :
    IRequest<Result<GetValueResultMonthDto>>;