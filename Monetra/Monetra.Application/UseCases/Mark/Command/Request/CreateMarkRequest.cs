using MediatR;
using Monetra.Application.DTOs.Mark;
using Monetra.Domain.BackOffice.Commum.Abstraction;

namespace Monetra.Application.UseCases.Mark.Command.Request;

public record CreateMarkRequest(CreateMarkDTO Model) : IRequest<Result>;