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
    public async Task TestWrongInitParameters() {
        OAIAPI api = new OAIAPI(_apikey);

        // Test Out of Range exception
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                api.Chat.Init("gpt-3.5-turbo", frequency_penalty: 100));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                api.Chat.Init("gpt-3.5-turbo", top_logprobs: 100));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                api.Chat.Init("gpt-3.5-turbo", presence_penalty: 100));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                api.Chat.Init("gpt-3.5-turbo", response_format: new ResponseFormat { Type = "not-exist" }));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                api.Chat.Init("gpt-3.5-turbo", temperature: 100));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                api.Chat.Init("gpt-3.5-turbo", top_p: 100));

        // Test a model that not exist
        api.Chat.Init("not-exist");
        ChatResponse? result = await api.Chat.Dispatch("HELLO");
        Assert.IsFalse(result?.Status);
    }

    [TestMethod]
    public async Task TestResponse() {
        OAIAPI api = new OAIAPI(_apikey);
        api.Chat.Init("gpt-3.5-turbo");

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
        api.Chat.Init("gpt-3.5-turbo-1106", response_format: new ResponseFormat { Type = ResponseFormatTypes.JSON});

        ChatResponse? result = await api.Chat.Dispatch("Who won the world series in 2020? Output a JSON.");
        string? response = result?.GetMessage();
        Assert.IsNotNull(response);
        Assert.IsTrue(JSONTools.IsValidJSON(response));
    }

    [TestMethod]
    public async Task TestStreamResponse() {
        OAIAPI api = new OAIAPI(_apikey);
        api.Chat.Init("gpt-3.5-turbo", stream: true);

        // TODO Write Stream Response test
        ChatResponse? result = await api.Chat.Dispatch("");

    }
}


