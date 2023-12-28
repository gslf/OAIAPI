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
    public async Task TestClassUninitialized() {
        Console.WriteLine();
        OAIAPI api = new OAIAPI(_apikey);
        ChatResponse? result = await api.Chat.Dispatch("HELLO");
        Assert.IsFalse(result?.Status);
    }

    [TestMethod]
    public async Task TestWrongConfigParameters() {
        Config config;
        // Test Out of Range exception
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                config = new Config("gpt-3.5-turbo", frequencyPenalty: 100));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                config = new Config("gpt-3.5-turbo", topLogprobs: 100));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                config = new Config("gpt-3.5-turbo", presencePenalty: 100));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                config = new Config("gpt-3.5-turbo", responseFormat: new ResponseFormat { Type = "not-exist" }));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                config = new Config("gpt-3.5-turbo", temperature: 100));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
               config = new Config("gpt-3.5-turbo", topP: 100));

        // Test a model that not exist
        config = new Config("not-exist");
        OAIAPI api = new OAIAPI(_apikey);
        api.Chat.Init(config);
        ChatResponse? result = await api.Chat.Dispatch("HELLO");
        Assert.IsFalse(result?.Status);
    }

    [TestMethod]
    public async Task TestResponse() {
        OAIAPI api = new OAIAPI(_apikey);
        Config config = new Config("gpt-3.5-turbo");
        api.Chat.Init(config);

        // Check single message
        ChatResponse? result = await api.Chat.Dispatch("Hello, I'm Luke.");
        Assert.IsFalse(String.IsNullOrEmpty(result?.GetMessage()));

        // Check token counter
        int? token = result?.Usage?.Total_tokens;
        Assert.IsTrue(token.HasValue);

    }

    [TestMethod]
    public async Task TestJSONResponse() {
        OAIAPI api = new OAIAPI(_apikey);
        Config config = new Config("gpt-3.5-turbo-1106", responseFormat: new ResponseFormat { Type = ResponseFormatTypes.JSON });
        api.Chat.Init(config);

        ChatResponse? result = await api.Chat.Dispatch("Who won the world series in 2020? Output a JSON.");
        string? response = result?.GetMessage();
        Assert.IsNotNull(response);
        Assert.IsTrue(JSONTools.IsValidJSON(response));
    }

    [TestMethod]
    public async Task TestStreamResponse() {
        OAIAPI api = new OAIAPI(_apikey);
        Config config = new Config("gpt-3.5-turbo");
        api.Chat.Init(config);

        int stream_count = 0;
        await foreach (var res in api.Chat.DispatchStream("Hello, write numbers from 1 to 10.")) {
            stream_count++;
        }

        Assert.IsTrue(stream_count > 1);
    }
}


