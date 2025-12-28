using MediatR;

namespace Monetra.Application.UseCases.Customer.Notification.Request;

public record CustumerAuthNotification(Domain.BackOffice.Entities.Customer Customer, string Title,string menssage)  : INotification; 