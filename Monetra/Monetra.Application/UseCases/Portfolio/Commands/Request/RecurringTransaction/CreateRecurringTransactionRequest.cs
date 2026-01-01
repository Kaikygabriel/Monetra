using MediatR;
using Monetra.Application.DTOs.RecurringTransaction;
using Monetra.Domain.BackOffice.Commum.Abstraction;

namespace Monetra.Application.UseCases.Portfolio.Commands.Request.RecurringTransaction;

public record CreateRecurringTransactionRequest(CreateRecurringTransactionDto Model) : IRequest<Result>;