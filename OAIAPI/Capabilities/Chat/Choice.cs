namespace Promezio.OAIAPI.Capabilities.Chat;

/// <summary>
/// Represents a list of chat completion choices.
/// </summary>
public class Choice {
    public int Index { get; set; }
    public ChatMessage? Message { get; set; }
    public string? Finish_reason { get; set; }
}

