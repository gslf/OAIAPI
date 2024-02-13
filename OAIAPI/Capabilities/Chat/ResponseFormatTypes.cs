using Promezio.OAIAPI.Capabilities.Embedding;
using Promezio.OAIAPI.Utils;
using System.Reflection;

namespace Promezio.OAIAPI.Capabilities.Chat;

/// <summary>
/// Enumerates the available response formats for OpenAI chat API responses.
/// </summary>
public enum AvailableResponseFormat {
    JSON,
    TEXT
}

/// <summary>
/// Represents the desired format for the OpenAI chat API response.
/// </summary>
public class ResponseFormat {
    /// <summary>
    /// The underlying response format.
    /// </summary>
    private AvailableResponseFormat _type;

    /// <summary>
    /// Initializes a new instance of the ResponseFormat class with the specified format.
    /// Defaults to TEXT format.
    /// </summary>
    /// <param name="type">The desired response format.</param>
    public ResponseFormat(AvailableResponseFormat type = AvailableResponseFormat.TEXT) {
        _type = type;
    }

    /// <summary>
    /// Returns a string representation of the chosen response format.
    /// </summary>
    /// <returns>A string indicating either "json_object" or "text" based on the chosen format.</returns>
    public override string ToString() {
        switch (_type) {
            case AvailableResponseFormat.JSON:
                return "json_object";
            case AvailableResponseFormat.TEXT:
                return "text";
        }

        return "";
    }
}
