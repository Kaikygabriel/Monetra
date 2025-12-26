using MediatR;
using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Application.UseCases.Custumer.Notification.Request;

public record CustumerAuthNotification(EmailSending Email)  : INotification; 