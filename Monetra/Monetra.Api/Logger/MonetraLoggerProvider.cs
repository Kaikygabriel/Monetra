namespace Monetra.Api.Logger;

public class MonetraLoggerProvider : ILoggerProvider
{
    public void Dispose()
    {
        // TODO release managed resources here
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new MonetraLogger(categoryName);
    }
}