using MediatR;
using Monetra.Domain.BackOffice.Commum.Abstraction;

namespace Monetra.Application.UseCases.Mark.Command.Request;

public record DeleteMarkRequest(Guid CustomerId,Guid MarkId): IRequest<Result>; 