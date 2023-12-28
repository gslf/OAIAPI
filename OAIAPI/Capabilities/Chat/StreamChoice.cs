namespace Promezio.OAIAPI.Capabilities.Chat;

/// <summary>
/// Represents a choice within a stream, encapsulating information about the selection index, a potential chat message update, and the reason for concluding the stream.
/// </summary>
public class StreamChoice {
    public int Index { get; set; }

    public ChatMessage? Delta { get; set; }

    public string? Finish_reason { get; set; }
}

