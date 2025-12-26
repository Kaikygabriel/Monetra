using MediatR;
using Monetra.Application.UseCases.Custumer.Notification.Request;
using Monetra.Domain.BackOffice.Interfaces.Services;

namespace Monetra.Application.UseCases.Custumer.Notification.Handler;

public class CustumerAuthNotificationHandler: INotificationHandler<CustumerAuthNotification>
{
    private IServiceEmail _emailService;

    public CustumerAuthNotificationHandler(IServiceEmail emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(CustumerAuthNotification notification, CancellationToken cancellationToken)
    {
        await _emailService.TrySendEmail(notification.Email);
    }
}