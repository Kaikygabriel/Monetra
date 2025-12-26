using MediatR;
using Monetra.Application.Commum;
using Monetra.Application.DTOs.Custumer;

namespace Monetra.Application.UseCases.Custumer.Command.Request;

public record LoginCustomerRequest(LoginCustomerDTO Model) : IRequest<Result<string>>;