using MediatR;
using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Application.UseCases.Custumer.Notification.Request;

public record RegisterCustomerNotification(EmailSending Email)  : INotification; 