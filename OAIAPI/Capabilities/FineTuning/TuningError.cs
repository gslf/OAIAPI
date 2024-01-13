namespace Promezio.OAIAPI.Capabilities.FineTuning;


/// <summary>
/// Represents an error encountered during the fine-tuning process
/// </summary>
public class TuningError {
    /// <summary>
    /// Gets or sets the error code associated with the tuning error, if any.
    /// The code can be used to identify specific types of errors in a standardized manner.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Gets or sets a message that describes the tuning error.
    /// This message is intended to be human-readable and may provide additional context or details about the nature of the error.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Gets or sets the parameter name or identifier that is related to the error, if applicable.
    /// This can be useful for pinpointing the specific input or configuration setting that triggered the error.
    /// </summary>
    public string? Param { get; set; }
}
