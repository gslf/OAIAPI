using Promezio.OAIAPI.Capabilities.Embedding;

namespace Promezio.OAIAPI.Test;

//
// !! WARNING !!
// 
// You need a valid OpenAI API key in your user-secrets to test
// the chat capabitilies. You can find more information in the
// README.md file.
//

[TestClass]
public class EmbeddingsTest : Test {

    [TestMethod]
    public async Task TestCreateEmbedding() {
        OAIAPI api = new OAIAPI(_apikey);
        EmbeddingsConfig config = new EmbeddingsConfig(AvailableEmbeddingsModels.EMBEDDING_ADA);
        EmbeddingResponse? response = await api.Embeddings.Create(["Test Sentence1", "Test Sentence2"], config);

        Assert.IsNotNull(response);
        Assert.IsNotNull(response.Data);
        Assert.IsTrue(response.Data.Length > 0);
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task TestCreateEmbeddingParamsValidation() {
        OAIAPI api = new OAIAPI(_apikey);
        EmbeddingsConfig config = new EmbeddingsConfig(AvailableEmbeddingsModels.EMBEDDING_ADA);
        EmbeddingResponse? response = await api.Embeddings.Create(Enumerable.Repeat("WRONG", 3000).ToArray(), config);
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestConfigParamsValidation() {
        EmbeddingsConfig config = new EmbeddingsConfig(AvailableEmbeddingsModels.EMBEDDING_ADA, dimensions: 2);
    }

}
