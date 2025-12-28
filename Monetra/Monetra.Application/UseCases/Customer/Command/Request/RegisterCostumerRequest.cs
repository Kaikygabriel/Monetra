using MediatR;
using Monetra.Application.Commum;
using Monetra.Application.DTOs.Custumer;

namespace Monetra.Application.UseCases.Customer.Command.Request;

public record RegisterCostumerRequest(RegisterCustomerDTO Model) : IRequest<Result<string>>;