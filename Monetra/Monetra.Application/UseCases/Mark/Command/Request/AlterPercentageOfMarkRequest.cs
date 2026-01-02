using MediatR;
using Monetra.Domain.BackOffice.Commum.Abstraction;

namespace Monetra.Application.UseCases.Mark.Command.Request;

public record AlterPercentageOfMarkRequest(Guid CustomerId,decimal Value) : IRequest<Result>;