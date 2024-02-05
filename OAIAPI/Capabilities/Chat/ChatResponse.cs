using Promezio.OAIAPI.Capabilities.CommonModels;

namespace Promezio.OAIAPI.Capabilities.Chat;

/// <summary>
/// Represents the OpenAI API response for a chat completition request.
/// </summary>
public class ChatResponse {
    // Response status properties
    public bool Status { get; set; }
    public string? Error { get; set; }


    // //////////////////////
    // OpenAI response model
    public string? Id { get; set; }
    public int? Created { get; set; }
    public string? Model { get; set; }
    public string? System_fingerprint { get; set; }
    public Choice[]? Choices { get; set; }
    public Usage? Usage { get; set; }

    public string? GetMessage() {
        if (Choices != null && Choices.Length > 0) {
            if (Choices[0].Message != null) {
                return Choices[0].Message?.Content;
            }
        }

        return "";
    }
}

