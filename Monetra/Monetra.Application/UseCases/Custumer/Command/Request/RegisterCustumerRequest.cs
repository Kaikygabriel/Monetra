using MediatR;
using Monetra.Application.DTOs.Custumer;

namespace Monetra.Application.UseCases.Custumer.Command.Request;

public record RegisterCustumerRequest(RegisterCustomerDTO Model) : IRequest<string>;