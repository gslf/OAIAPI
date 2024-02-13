using Promezio.OAIAPI.Capabilities.Embedding;

namespace Promezio.OAIAPI.Capabilities.Embedding;

/// <summary>
/// Represents the configuration options for creating embeddings.
/// </summary>
public class EmbeddingsConfig {
    /// <summary>
    /// The embedding model to use.
    /// </summary>
    public EmbeddingModel Model { get; }
    /// <summary>
    /// The format of the output embeddings. Defaults to "float".
    /// </summary>
    public string EncodingFormat { get; }
    /// <summary>
    /// The desired dimensionality of the embedding vectors. Only supported for models "text-embedding-3" and later.
    /// </summary>
    public int? Dimensions { get; }
    /// <summary>
    /// An optional user identifier associated with the request.
    /// </summary>
    public string? User { get; }


    /// <summary>
    /// Initializes a new instance of the <see cref="EmbeddingsConfig"/> class.
    /// </summary>
    /// <param name="model">The <see cref="AvailableEmbeddingsModels"/> to use.</param>
    /// <param name="encodingFormat">The format of the output embeddings. Defaults to "float".</param>
    /// <param name="dimensions">The desired dimensionality of the embedding vectors (only supported for models "text-embedding-3" and later). Defaults to null.</param>
    /// <param name="user">An optional user identifier associated with the request.</param>
    /// <exception cref="ArgumentException">Thrown if the specified number of dimensions is not supported for the chosen embedding model.</exception>
    public EmbeddingsConfig(AvailableEmbeddingsModels model, string encodingFormat = "float", int? dimensions = null, string? user = null) {
        
        // Parameters validation
        if (dimensions is not null){
            if(model == AvailableEmbeddingsModels.EMBEDDING_ADA) {
                throw new ArgumentException("The number of dimensions only supported in text-embedding-3 and later models", nameof(dimensions));
            }
        }

        Model = new EmbeddingModel(model);
        EncodingFormat = encodingFormat;
        Dimensions = dimensions;
        User = user;
    }
}
