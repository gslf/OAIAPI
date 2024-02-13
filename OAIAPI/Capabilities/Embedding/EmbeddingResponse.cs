
using Promezio.OAIAPI.Capabilities.CommonModels;

namespace Promezio.OAIAPI.Capabilities.Embedding;

/// <summary>
/// Represents the response object returned by the `Embeddings.Create` method.
/// </summary>
public class EmbeddingResponse {
    /// <summary>
    /// An array of embedding objects, each containing the embedding for a corresponding input text.
    /// Can be null if the request failed.
    /// </summary>
    public EmbeddingObject[]? Data {  get; set; }
    /// <summary>
    /// The embedding model used to generate the embeddings (matches the model specified in the request).
    /// </summary>
    public string? Model { get; set; }
    /// <summary>
    /// Information about the usage limits and costs associated with the request.
    /// </summary>
    public Usage? Usage { get; set; }
}
