using Promezio.OAIAPI.Capabilities.Files;

namespace Promezio.OAIAPI.Test;

//
// !! WARNING !!
// 
// You need a valid OpenAI API key in your user-secrets to test
// the chat capabitilies. You can find more information in the
// README.md file.
//

[TestClass]
public class FilesTest : Test {

    [TestMethod]
    public async Task TestFileUploadDelete() {
        OAIAPI api = new OAIAPI(_apikey);
        FileObject? uploadResult = await api.Files.Upload("../../../TestResources/SarcasticTuning.jsonl", Purposes.FINE_TUNE);
        Assert.IsFalse(string.IsNullOrEmpty(uploadResult.Id));

        bool deleteResult = await api.Files.Delete(uploadResult.Id);
        Assert.IsTrue(deleteResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task TestFileUploadWrongParameter() {
        OAIAPI api = new OAIAPI(_apikey);
        FileObject? result = await api.Files.Upload("../../../TestResources/SarcasticTuning.jsonl", "NOT_EXIST");
    }

    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException))]
    public async Task TestFileUploadInexistentFile() {
        OAIAPI api = new OAIAPI(_apikey);
        FileObject? result = await api.Files.Upload("NOT-EXIST", Purposes.FINE_TUNE);
    }

    [TestMethod]
    [ExpectedException(typeof(HttpRequestException))]
    public async Task TestFileUploadWrongFile() {
        OAIAPI api = new OAIAPI(_apikey);
        FileObject? result = await api.Files.Upload("../../../TestResources/Config.json", Purposes.FINE_TUNE);
    }

    [TestMethod]
    public async Task TestFileListRetrieveContentDelete() {
        OAIAPI api = new OAIAPI(_apikey);

        FileObject? uploadResult = await api.Files.Upload("../../../TestResources/SarcasticTuning.jsonl", Purposes.FINE_TUNE);
        Assert.IsFalse(string.IsNullOrEmpty(uploadResult.Id));

        FileObject[]? listResults = await api.Files.List();
        Assert.IsNotNull(listResults);
        Assert.IsFalse(string.IsNullOrEmpty(listResults[0].Id));

        FileObject? retrieveResult = await api.Files.Retrieve(uploadResult.Id);
        Assert.IsNotNull(retrieveResult);
        Assert.IsFalse(string.IsNullOrEmpty(retrieveResult.Id));

        string? contentResult = await api.Files.Content(uploadResult.Id);
        Assert.IsFalse(string.IsNullOrEmpty(contentResult));

        bool deleteResult = await api.Files.Delete(uploadResult.Id);
        Assert.IsTrue(deleteResult);
    }

}