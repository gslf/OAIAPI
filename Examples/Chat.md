# OAIAPI - Chat Capability

## Namespace
`Promezio.OAIAPI.Capabilities.Chat`

## Summary
The `Chat` class provides functionality to interact with the OpenAI completion chat model API. It encapsulates the process of sending messages and receiving responses from the chat model.

## Examples

```csharp
// Retrieve the API Key
 Configuration = new ConfigurationBuilder()
    .AddUserSecrets<Test>()
    .Build();

if (Configuration == null) {
    throw new NullReferenceException("User Secrets configuration not found."); 
}

string myapikey = Configuration["OpenAIApiKey"] ?? throw new InvalidOperationException("API Key not found");


// Init OAIAPI Class
OAIAPI api = new OAIAPI(myapikey);
Config config = new Config(
    new ChatModel(), 
    frequencyPenalty: 0, // Optional
    logprobs: true,  // Optional
    topLogprobs: 5,  // Optional
    maxTokens: 1000,  // Optional
    n: 1,  // Optional
    presencePenalty: 0m,  // Optional
    responseFormat: new ResponseFormat(AvailableResponseFormat.TEXT),  // Optional
    seed: 123456789,  // Optional
    temperature: 1m,  // Optional
    topP: 1m,  // Optional
    user: "test");  // Optional


// Call the standard API
ChatResponse? result = await api.Chat.Dispatch("Hello from space: the final frontier.", config);
Console.WriteLine(result?.GetMessage());

// Call the stream API
await foreach (var streamResult in api.Chat.DispatchStream("Hello, write numbers from 1 to 10.", config)) {
    Console.WriteLine(streamResult?.GetMessage());
}
```

## Documentation Links
#### [Configuration parameters](/api/Promezio.OAIAPI.Capabilities.Chat.Config.html#parameters)
