namespace Promezio.OAIAPI.Capabilities.Chat;

/// <summary>
/// Represents a response from a chat stream.
/// </summary>
/// <remarks>
/// This class encapsulates the data received as a response from a chat stream, 
/// including the unique identifier of the response, creation timestamp, model used, 
/// system fingerprint, and the array of response choices. It also provides a method 
/// to retrieve the message content from the first choice, if available.
/// </remarks>

public class ChatStreamResponse {
    public string? Id { get; set; }
    public int? Created { get; set; }
    public string? Model { get; set; }

    public string? System_fingerprint { get; set; }
    public StreamChoice[]? Choices { get; set; }


    /// <summary>
    /// Retrieves the content of the message from the first choice in the response.
    /// </summary>
    /// <returns>A string containing the message content, or an empty string if no content is available.</returns>
    public string? GetMessage() {
        if (Choices != null && Choices.Length > 0) {
            if (Choices[0].Delta != null) {
                return Choices[0].Delta?.Content;
            }
        }

        return "";
    }
}

