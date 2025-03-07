using Microsoft.Extensions.Logging;

public class MyInMemoryLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) => new MyInMemoryLogger();
    public void Dispose() { }
}

public class MyInMemoryLogger : ILogger
{
    private static readonly List<string> _logs = new();

    public IDisposable BeginScope<TState>(TState state) => null;
    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception exception,
        Func<TState, Exception, string> formatter)
    {
        var message = $"{DateTime.Now:HH:mm:ss} [{logLevel}] {formatter(state, exception)}";
        _logs.Add(message);
    }

    public static List<string> GetLogs() => _logs;
}