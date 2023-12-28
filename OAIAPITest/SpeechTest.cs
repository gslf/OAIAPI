using Promezio.OAIAPI.Capabilities.Speech;
using Microsoft.Extensions.Configuration;

namespace Promezio.OAIAPI.Test;

//
// !! WARNING !!
// 
// You need a valid OpenAI API key in your user-secrets to test
// the chat capabitilies. You can find more information in the
// README.md file.
//

[TestClass]
public class SpeechTest : Test {

    [TestMethod]
    public async Task TestResponse() {
        OAIAPI api = new OAIAPI(_apikey);
        api.Speech.Init();

        // Check single message
        SpeechResponse? result = await api.Speech.Dispatch("Hello Learners!", "../../../TestResources/speech.mp3");
    }
}

