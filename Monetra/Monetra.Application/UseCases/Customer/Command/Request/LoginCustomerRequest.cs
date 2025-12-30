using MediatR;
using Monetra.Application.DTOs.Custumer;
using Monetra.Application.UseCases.Customer.Command.Response;
using Monetra.Domain.BackOffice.Commum;

namespace Monetra.Application.UseCases.Customer.Command.Request;

public record LoginCustomerRequest(LoginCustomerDTO Model) : IRequest<Result<CustomerAuthResponse>>;