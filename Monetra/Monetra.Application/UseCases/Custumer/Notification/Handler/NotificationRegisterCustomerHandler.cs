using MediatR;
using Monetra.Application.UseCases.Custumer.Notification.Request;
using Monetra.Domain.BackOffice.Interfaces.Services;

namespace Monetra.Application.UseCases.Custumer.Notification.Handler;

public class NotificationRegisterCustomerHandler: INotificationHandler<RegisterCustomerNotification>
{
    private IServiceEmail _emailService;

    public NotificationRegisterCustomerHandler(IServiceEmail emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(RegisterCustomerNotification notification, CancellationToken cancellationToken)
    {
        _emailService.TrySendEmail(notification.Email);
    }
}