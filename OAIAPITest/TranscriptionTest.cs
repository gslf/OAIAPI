using Promezio.OAIAPI.Capabilities.Transcription;

namespace Promezio.OAIAPI.Test;

//
// !! WARNING !!
// 
// You need a valid OpenAI API key in your user-secrets to test
// the chat capabitilies. You can find more information in the
// README.md file.
//

[TestClass]
public class TranscriptionTest : Test {

    [TestMethod]
    public async Task Test() {
        OAIAPI api = new OAIAPI(_apikey);
        api.Transcription.Init(0.8m);
        TranscriptionResponse response = await api.Transcription.Dispatch("../../../TestResources/hello.mp3");
        Assert.IsTrue(response.Status);
        Assert.IsFalse(String.IsNullOrEmpty(response.Text));
    }

}

