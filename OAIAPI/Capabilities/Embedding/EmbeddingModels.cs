namespace Promezio.OAIAPI.Capabilities.Embedding;

/// <summary>
///  Enumerates the possible embedding models available in the OpenAI API
/// </summary>
public enum AvailableEmbeddingsModels {
    EMBEDDING_ADA,
    EMBEDDING_V3_LARGE,
    EMBEDDING_V3_SMALL
}

/// <summary>
/// Represents a model that can be used to generate embeddings.
/// </summary>
public class EmbeddingModel {
    /// <summary>
    /// The underlying embedding model.
    /// </summary>
    private AvailableEmbeddingsModels _model;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmbeddingModel"/> class.
    /// </summary>
    /// <param name="model">The <see cref="AvailableEmbeddingsModels"/> to use.</param>
    public EmbeddingModel(AvailableEmbeddingsModels model) {
        _model = model;
    }

    /// <summary>
    /// Returns a string representation of the model, suitable for use in API requests.
    /// </summary>
    /// <returns>The string representation of the model.</returns>
    public override string ToString() {
        switch (_model) {
            case AvailableEmbeddingsModels.EMBEDDING_ADA:
                return "text-embedding-ada-002";
            case AvailableEmbeddingsModels.EMBEDDING_V3_LARGE:
                return "text-embedding-3-large";
            case AvailableEmbeddingsModels.EMBEDDING_V3_SMALL:
                return "text-embedding-3-small";
        }

        return "";
    }
}
