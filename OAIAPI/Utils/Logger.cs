using System.Diagnostics;


namespace Promezio.OAIAPI.Utils;

/// <summary>
/// Defines the logging levels supported by the Logger class.
/// </summary>
public enum LogLevel {
    None = 0,
    Error = 1,
    Warning = 2,
    Info = 3
}

/// <summary>
/// A Logger class that manages logging using TraceSource.
/// It provides methods for logging messages at various severity levels: Error, Warning, Info.
/// The logging level can be set to control which messages are output to the log.
/// </summary>
public class Logger {
    private TraceSource traceSource;
    public LogLevel Level { get; set; }

    /// <summary>
    /// Initializes a new instance of the Logger class with a specified logging level.
    /// </summary>
    /// <param name="level">The logging level threshold. Only messages at this level or higher will be logged.</param>
    public Logger(LogLevel level) {
        Level = level;
        traceSource = new TraceSource("OAIAPI");

        traceSource.Switch = new SourceSwitch("sourceSwitch", "Verbose");
        traceSource.Listeners.Clear();

        var textListener = new TextWriterTraceListener("log.txt");
        var consoleListener = new ConsoleTraceListener();

        textListener.TraceOutputOptions = TraceOptions.DateTime;
        consoleListener.TraceOutputOptions = TraceOptions.DateTime;

        traceSource.Listeners.Add(textListener);
        traceSource.Listeners.Add(consoleListener);
    }

    /// <summary>
    /// Logs an error message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public void Error(string message) {
        if (Level >= LogLevel.Error) {
            Log(message, TraceEventType.Error);
        }
    }

    /// <summary>
    /// Logs a warning message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public void Warning(string message) {
        if (Level >= LogLevel.Warning) {
            Log(message, TraceEventType.Warning);
        }
    }

    /// <summary>
    /// Logs an informational message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public void Info(string message) {
        if (Level >= LogLevel.Info) {
            Log(message, TraceEventType.Information);
        }
    }

    /// <summary>
    /// A private method to log a message with a specified event type.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="eventType">The type of event to log.</param>
    private void Log(string message, TraceEventType eventType) {
        traceSource.TraceEvent(eventType, 0, message);
        traceSource.Flush();
    }

    /// <summary>
    /// Closes the TraceSource and releases all resources.
    /// </summary>
    public void Close() {
        traceSource.Close();
    }
}