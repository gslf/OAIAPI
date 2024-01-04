# OAIAPI Utils - Logger

## Namespace
`Promezio.OAIAPI.Utils`

## Summary
The `Logger` class provides a robust logging mechanism, utilizing `TraceSource` for logging messages at different severity levels such as Error, Warning, and Info. It enables control over which messages are logged based on a specified logging level.

## Enumerations

### LogLevel
Defines the logging levels supported by the Logger class.
- `None` (0): No logging.
- `Error` (1): Logs error messages.
- `Warning` (2): Logs warning messages.
- `Info` (3): Logs informational messages.

## Constructor

### Logger(LogLevel level)
Initializes a new instance of the `Logger` class with the specified logging level.

#### Parameters
- `level` (`LogLevel`): The logging level threshold. Only messages at this level or higher will be logged.

## Properties

- `Level` (`LogLevel`): Gets or sets the logging level.

## Methods

### void Error(string message)
Logs an error message.

#### Parameters
- `message` (string): The error message to log.

### void Warning(string message)
Logs a warning message.

#### Parameters
- `message` (string): The warning message to log.

### void Info(string message)
Logs an informational message.

#### Parameters
- `message` (string): The informational message to log.

### void Close()
Closes the `TraceSource` and releases all resources.

## Remarks
- The logger uses `TraceSource` to provide a flexible and extensible way to log messages.
- It supports logging to different outputs such as text files and the console.
- The logger's behavior can be customized through listeners and trace options.
- The `LogLevel` can be dynamically set to control the verbosity of the log output.
- Properly closing the logger is important to ensure that all logged messages are flushed to their outputs.

## Example Usage
```csharp
// Creating a logger instance with Info level logging
var logger = new Logger(LogLevel.Info);

// Logging messages
logger.Info("Informational message");
logger.Warning("Warning message");
logger.Error("Error message");

// Closing the logger
logger.Close();