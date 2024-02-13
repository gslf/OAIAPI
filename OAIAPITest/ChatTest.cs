using Promezio.OAIAPI.Capabilities.Chat;
using Promezio.OAIAPI.Utils;


namespace Promezio.OAIAPI.Test;

//
// !! WARNING !!
// 
// You need a valid OpenAI API key in your user-secrets to test
// the chat capabitilies. You can find more information in the
// README.md file.
//

[TestClass]
public class ChatTest : Test {

    [TestMethod]
    public async Task TestWrongConfigParameters() {
        Config config;
        // Test Out of Range exception
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                config = new Config(new ChatModel(), frequencyPenalty: 100));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                config = new Config(new ChatModel(), topLogprobs: 100));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                config = new Config(new ChatModel(), presencePenalty: 100));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                config = new Config(new ChatModel(), temperature: 100));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
               config = new Config(new ChatModel(), topP: 100));
    }

    [TestMethod]
    public async Task TestResponse() {
        OAIAPI api = new OAIAPI(_apikey);
        Config config = new Config(new ChatModel());

        // Check single message
        ChatResponse? result = await api.Chat.Dispatch("Hello, I'm Luke.", config);
        Assert.IsFalse(String.IsNullOrEmpty(result?.GetMessage()));

        // Check token counter
        int? token = result?.Usage?.Total_tokens;
        Assert.IsTrue(token.HasValue);

    }

    [TestMethod]
    public async Task TestJSONResponse() {
        OAIAPI api = new OAIAPI(_apikey);
        Config config = new Config(new ChatModel(), responseFormat: new ResponseFormat(AvailableResponseFormat.JSON));

        ChatResponse? result = await api.Chat.Dispatch("Who won the world series in 2020? Output a JSON.", config);
        string? response = result?.GetMessage();
        Assert.IsNotNull(response);
        Assert.IsTrue(JSONTools.IsValidJSON(response));
    }

    [TestMethod]
    public async Task TestStreamResponse() {
        OAIAPI api = new OAIAPI(_apikey);
        Config config = new Config(new ChatModel());

        int stream_count = 0;
        await foreach (var res in api.Chat.DispatchStream("Hello, write numbers from 1 to 10.", config)) {
            stream_count++;
        }

        Assert.IsTrue(stream_count > 1);
    }
}


