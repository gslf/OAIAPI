using Promezio.OAIAPI.Capabilities.CommonModels;

namespace Promezio.OAIAPI.Capabilities.Chat;

/// <summary>
/// Represents the response object received from the OpenAI API after sending a chat completion request.
/// </summary>

public class ChatResponse {

    // //////////////////////
    // OpenAI response model

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
    /// An array of possible completions, with the first element considered the "best" response.
    /// Each element contains a `Message` object with the generated text content.
    /// </summary>
    public Choice[]? Choices { get; set; }

    /// <summary>
    /// An object containing usage information related to the request.
    /// </summary>
    public Usage? Usage { get; set; }


    /// <summary>
    /// Extracts the main text content from the first choice of the response.
    /// </summary>
    /// <returns>The plain text content of the first choice's message, or an empty string if unavailable.</returns>
    public string? GetMessage() {
        if (Choices != null && Choices.Length > 0) {
            if (Choices[0].Message != null) {
                return Choices[0].Message?.Content;
            }
        }

        return "";
    }
}

