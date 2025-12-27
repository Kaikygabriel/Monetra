using MediatR;
using Monetra.Domain.BackOffice.Entities;
using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Application.UseCases.Custumer.Notification.Request;

public record CustumerAuthNotification(Customer Customer, string Title)  : INotification; 