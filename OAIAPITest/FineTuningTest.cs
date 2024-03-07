using Promezio.OAIAPI.Capabilities.Files;
using Promezio.OAIAPI.Capabilities.FineTuning;

namespace Promezio.OAIAPI.Test;

[TestClass]
public class FineTuningTest : Test {

    [TestMethod]
    public async Task TestCreateCancel() {
        OAIAPI api = new OAIAPI(_apikey);
        FileObject? uploadResult = await api.Files.Upload("../../../TestResources/SarcasticTuning.jsonl", new Purposes());
        Assert.IsFalse(string.IsNullOrEmpty(uploadResult.Id));

        FineTuningObject? fineTuningResult = await api.FineTuning.Create(
            uploadResult.Id,
            new Models(),
            batch_size: 5,
            learning_rate_multiplier: 2,
            n_epochs: 10,
            suffix: "test",
            validationFileID: null);

        Assert.IsNotNull(fineTuningResult);
        Assert.IsFalse(string.IsNullOrEmpty(fineTuningResult.Id));
        Assert.AreEqual(fineTuningResult?.Hyperparameters?.Batch_size, 5);

        Assert.IsNotNull(fineTuningResult);
        FineTuningObject? fineTuningCancelResult = await api.FineTuning.Cancel(fineTuningResult.Id);
        Assert.IsNotNull(fineTuningCancelResult);
        Assert.AreEqual(fineTuningCancelResult.Status, "cancelled");
    }

    [TestMethod]
    public async Task TestListJobs() {
        OAIAPI api = new OAIAPI(_apikey);

        FileObject? uploadResult = await api.Files.Upload("../../../TestResources/SarcasticTuning.jsonl", new Purposes());
        Assert.IsFalse(string.IsNullOrEmpty(uploadResult.Id));
        FineTuningObject? fineTuningResult = await api.FineTuning.Create(uploadResult.Id, new Models());

        FineTuningObject[]? fineTuningList = await api.FineTuning.ListJobs();
        Assert.IsNotNull(fineTuningList);
        Assert.IsTrue(fineTuningList.Length > 0);

        Assert.IsNotNull(fineTuningResult.Id);
        FineTuningObject? fineTuningCancelResult = await api.FineTuning.Cancel(fineTuningResult.Id);
    }


    [TestMethod]
    public async Task TestListEvents() {
        OAIAPI api = new OAIAPI(_apikey);

        FileObject? uploadResult = await api.Files.Upload("../../../TestResources/SarcasticTuning.jsonl", new Purposes());
        Assert.IsFalse(string.IsNullOrEmpty(uploadResult.Id));

        FineTuningObject? fineTuningResult = await api.FineTuning.Create(uploadResult.Id, new Models());
        Assert.IsNotNull(fineTuningResult.Id);

        FineTuningEvent[]? fineTuningEvents = await api.FineTuning.ListEvents(fineTuningResult.Id);
        Assert.IsNotNull(fineTuningEvents);
        Assert.IsTrue(fineTuningEvents.Length > 0);
        Assert.IsFalse(string.IsNullOrEmpty(fineTuningEvents[0].Id));

        Assert.IsNotNull(fineTuningResult.Id);
        FineTuningObject? fineTuningCancelResult = await api.FineTuning.Cancel(fineTuningResult.Id);
    }

    [TestMethod]
    public async Task TestRetrieve() {
        OAIAPI api = new OAIAPI(_apikey);

        FileObject? uploadResult = await api.Files.Upload("../../../TestResources/SarcasticTuning.jsonl", new Purposes());
        Assert.IsFalse(string.IsNullOrEmpty(uploadResult.Id));

        FineTuningObject? fineTuningResult = await api.FineTuning.Create(uploadResult.Id, new Models());
        Assert.IsNotNull(fineTuningResult.Id);

        FineTuningObject? fineTuningRetrieveResult = await api.FineTuning.Retrieve(fineTuningResult.Id);
        Assert.IsNotNull(fineTuningRetrieveResult);
        Assert.IsNotNull(fineTuningRetrieveResult.Id);

        Assert.IsNotNull(fineTuningResult.Id);
        FineTuningObject? fineTuningCancelResult = await api.FineTuning.Cancel(fineTuningResult.Id);
    }
}
