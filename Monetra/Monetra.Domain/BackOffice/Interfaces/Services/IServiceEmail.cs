using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Domain.BackOffice.Interfaces.Services;

public interface IServiceEmail
{
    Task<bool> TrySendEmail(EmailSending email);
}