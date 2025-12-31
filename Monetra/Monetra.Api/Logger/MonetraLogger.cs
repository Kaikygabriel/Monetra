namespace Monetra.Api.Logger;

public class MonetraLogger : ILogger
{
    private readonly string _categoryName;

    public MonetraLogger(string categoryName)
    {
        _categoryName = categoryName;
    }
    
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        => null;

    public bool IsEnabled(LogLevel logLevel)
        => logLevel == LogLevel.Critical;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
            return;

        var message = formatter(state, exception);

        var log = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
                  $"- [{logLevel}] " +
                  $"- [{_categoryName}] " +
                  $"- [ {message} ]";

        using var writeStream = new StreamWriter("./Logs.txt",true);
        writeStream.WriteLine(log);
    }
}