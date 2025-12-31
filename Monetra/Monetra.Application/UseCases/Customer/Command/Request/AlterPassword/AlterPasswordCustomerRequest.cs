using MediatR;
using Monetra.Domain.BackOffice.Commum.Abstraction;

namespace Monetra.Application.UseCases.Customer.Command.Request.AlterPassword;

public record AlterPasswordCustomerRequest(string Token,string NewPassword) : IRequest<Result>;