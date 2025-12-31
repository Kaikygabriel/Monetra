using MediatR;
using Monetra.Domain.BackOffice.Commum.Abstraction;

namespace Monetra.Application.UseCases.Customer.Command.Request.AlterPassword;

public record SendEmailForAlterPasswordRequest(string Email) : IRequest<Result>;