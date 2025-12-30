using MediatR;
using Monetra.Application.DTOs.Custumer;
using Monetra.Domain.BackOffice.Commum;

namespace Monetra.Application.UseCases.Customer.Query.Request;

public record GetCustomerDashbordRequest(Guid CustomerId)  : IRequest<Result<CustomerDashboardDTO>>;