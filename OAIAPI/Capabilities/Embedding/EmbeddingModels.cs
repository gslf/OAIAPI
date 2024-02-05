namespace Promezio.OAIAPI.Capabilities.Embedding;
public enum AvailableEmbeddingsModels {
    EMBEDDING_ADA,
    EMBEDDING_V3_LARGE,
    EMBEDDING_V3_SMALL
}

public class EmbeddingModel {
    private AvailableEmbeddingsModels _model;

    public EmbeddingModel(AvailableEmbeddingsModels model) {
        _model = model;
    }

    public string Value() {
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
