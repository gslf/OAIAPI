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

    /// <summary>
    /// A unique identifier for the response.
    /// </summary>
    public string? Id { get; set; }
    /// <summary>
    /// Unix timestamp indicating when the response was created.
    /// </summary>
    public int? Created { get; set; }
    /// <summary>
    /// The OpenAI chat model used to generate the response.
    /// </summary>
    public string? Model { get; set; }
    /// <summary>
    /// A security fingerprint associated with the response.
    /// </summary>
    public string? System_fingerprint { get; set; }

    /// <summary>
    /// An array of potential message updates within the response stream.
    /// Each element contains a `Delta` object with the updated text content.
    /// </summary>
    public StreamChoice[]? Choices { get; set; }


    /// <summary>
    /// Extracts the updated message content from the first choice of the response, if available.
    /// </summary>
    /// <returns>The updated message content string, or an empty string if unavailable.</returns>
    public string? GetMessage() {
        if (Choices != null && Choices.Length > 0) {
            if (Choices[0].Delta != null) {
                return Choices[0].Delta?.Content;
            }
        }

        return "";
    }
}

