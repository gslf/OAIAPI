namespace Promezio.OAIAPI.Capabilities.Chat;

/// <summary>
/// Represents a single chat message sent or received through the OpenAI chat API.
/// </summary>
public struct ChatMessage {
    /// <summary>
    /// Gets or sets the role of the message sender (e.g., "user", "assistant").
    /// </summary>
    public string? Role { get; set; }

    /// <summary>
    /// Gets or sets the content of the message, which can be plain text or other supported formats.
    /// </summary>
    public string? Content { get; set; }
}


