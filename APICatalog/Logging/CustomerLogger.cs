
namespace APICatalog.Logging;

public class CustomerLogger : ILogger
{
    readonly string loggerName;
    readonly CustomLoggerProviderConfiguration loggerConfig;

    public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
    {
        loggerName = name;
        loggerConfig = config;
    }
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == loggerConfig.LogLevel;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        string message = $"{logLevel.ToString()} {eventId.Id} - {formatter(state, exception)}";
        WriteTxt(message);
    }

    private void WriteTxt(string message)
    {
        string output = @"G:/loginfo.txt";

        using (StreamWriter sw = new StreamWriter(output, true))
        {
            try
            { 
                sw.WriteLine(message);
                sw.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
