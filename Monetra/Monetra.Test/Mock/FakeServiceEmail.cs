using Monetra.Domain.BackOffice.Interfaces.Services;
using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Test.Mock;

public class FakeServiceEmail : IServiceEmail
{
    public async Task<bool> TrySendEmail(EmailSending email)
    {
        await Task.Delay(0);
        return true;
    }
}