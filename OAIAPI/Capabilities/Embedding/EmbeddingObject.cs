namespace Promezio.OAIAPI.Capabilities.Embedding;

/// <summary>
/// Represents a single embedding returned by the Promezio OAI API.
/// </summary>
public class EmbeddingObject {
    /// <summary>
    /// The index of the corresponding input text (matches the order in the request). Can be null.
    /// </summary>
    public int? Index { get; set; }
    /// <summary>
    /// An array of floating-point numbers representing the numerical embedding vector. Can be null.
    /// </summary>
    public float[]? Embedding { get; set; }
    /// <summary>
    /// An optional object identifier associated with the input text. Can be null.
    /// </summary>
    public string? Object { get; set; }
}
