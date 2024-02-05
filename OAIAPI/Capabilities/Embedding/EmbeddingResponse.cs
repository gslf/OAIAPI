
using Promezio.OAIAPI.Capabilities.CommonModels;

namespace Promezio.OAIAPI.Capabilities.Embedding;
public class EmbeddingResponse {
    public EmbeddingObject[]? Data {  get; set; }
    public string? Model { get; set; }
    public Usage? Usage { get; set; }
}
