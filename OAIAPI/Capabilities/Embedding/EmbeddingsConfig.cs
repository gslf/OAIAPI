using Promezio.OAIAPI.Capabilities.Embedding;

namespace Promezio.OAIAPI.Capabilities.Embedding;
public class EmbeddingsConfig {

    public EmbeddingModel Model { get; }
    public string EncodingFormat { get; }
    public int? Dimensions { get; }
    public string? User { get; }

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
