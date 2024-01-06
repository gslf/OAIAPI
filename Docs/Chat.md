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
Config config = new Config("gpt-3.5-turbo", temperature: 2);
api.Chat.Init(config);

// Call the standard API
ChatResponse? result = await api.Chat.Dispatch("Hello from space: the final frontier.");
Console.WriteLine(result?.GetMessage());

// Call the stream API
await foreach (var streamResult in api.Chat.DispatchStream("Hello, write numbers from 1 to 10.")) {
    Console.WriteLine(streamResult?.GetMessage());
}
```

## Configuration

The `Config` class provides a consistent way to configure the settings for the chat capability.

### Properties
- `model` (string): The model identifier used for generating responses.
- `frequencyPenalty` (int, optional): Frequency penalty to apply, ranging from -2.0 to 2.0. Default is 0.
- `logprobs` (bool, optional): Flag to include log probabilities in the response. Default is false.
- `topLogprobs` (int?, optional): Limit for the number of top log probabilities to return, must be between 0 and 5. Default is null.
- `maxTokens` (int?, optional): Maximum number of tokens to generate. Default is null.
- `n` (int): Number of responses to generate. Default is 1.
- `presencePenalty` (decimal, optional): Presence penalty to apply, ranging from -2.0 to 2.0. Default is 0.
- `responseFormat` (ResponseFormat?, optional): Format for the response (JSON or TEXT). Default is null.
- `seed` (int?, optional): Seed for random number generation. Default is null.
- `temperature` (decimal): Controls randomness in response generation, ranging from 0 to 2. Default is 1.
- `topP` (decimal): Controls diversity of response, ranging from 0 to 1. Default is 1.
- `user` (string?, optional): User identifier. Default is null.

### Constructor Exceptions
- `ArgumentOutOfRangeException`: Thrown when provided arguments are outside their allowed range.

## Parameters
- `apikey`: The API key for authentication.
- `logger`: The `Logger` instance used for logging operations. You can read more about the logger [here](Logger.md).

## Methods

### Init(Config config)
Initializes the instance of the `Chat` class with the specified configuration.

#### Parameters
- `config`: The configuration object.

### async Task<ChatResponse?> Dispatch(string prompt)
Sends a message to the chat model and receives a response asynchronously.

#### Parameters
- `prompt`: The message to send to the model.

#### Returns
A task representing the asynchronous operation, containing the chat response.

### async IAsyncEnumerable<ChatStreamResponse?> DispatchStream(string prompt)
Sends a message to the chat model and streams responses back asynchronously.

#### Parameters
- `prompt`: The message to send to the model.

#### Returns
An asynchronous stream of `ChatStreamResponse`.

## Remarks
- The class maintains a list of messages sent and received to/from the chat model.
- Proper initialization with API key and configuration is necessary before sending requests.
- The class handles JSON serialization and deserialization of the requests and responses.
- Includes error handling and logging for various stages of request and response processing.

