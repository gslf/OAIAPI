namespace Promezio.OAIAPI.Capabilities.Chat;

/// <summary>
/// Represents a choice within a stream, encapsulating information about the selection index, 
/// a potential chat message update, and the reason for concluding the stream.
/// </summary>
public class StreamChoice {
    /// <summary>
    /// The numerical index of this update within the stream, where 0 is the most recent change.
    /// </summary>
    public int Index { get; set; }
    /// <summary>
    /// The updated text content for the chat message, if available.
    /// </summary>
    public ChatMessage? Delta { get; set; }
    /// <summary>
    /// The reason why the stream was concluded with this update, such as "stop" for reaching the completion limit or "length" for reaching the desired text length.
    /// </summary>
    public string? Finish_reason { get; set; }
}

