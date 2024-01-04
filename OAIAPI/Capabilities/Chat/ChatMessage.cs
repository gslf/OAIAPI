namespace Promezio.OAIAPI.Capabilities.Chat;

/// <summary>
/// Represents a chat message sent and received from the API.
/// </summary>
public struct ChatMessage{
    public string? Role { get; set; }
    public string? Content { get; set; }
}


