# OAIAPI - Embeddings Capability

## Namespace
`Promezio.OAIAPI.Capabilities.Embedding`

## Summary
This class enables developers to interact with the "Embeddings" capability of the Promezio OAI API. This capability generates numerical representations, called embeddings, for given text inputs. These embeddings capture the semantic meaning of the text and can be used in various applications like machine learning and natural language processing.

The Embeddings class provides a single method, Create, which takes two arguments:

- input: An array of text strings for which embeddings will be generated. Each string should be less than 2048 characters long.
- config: An EmbeddingsConfig object specifying options for the embedding generation, such as the desired model, encoding format, and user information (optional).

The Create method performs the following actions:

1. Validates input: It checks if the input array length exceeds the limit and throws an ArgumentException if it does.
2. Prepares HTTP request: It builds a JSON payload containing the input, configuration, and API key. It then sets up an HTTP client and configures the request headers for authentication.
3. Sends request and handles response: It sends a POST request to the API endpoint and validates the response status code. If successful, it parses the JSON response into an EmbeddingResponse object containing the generated embeddings. If unsuccessful, it throws an HttpRequestException.
4. Logs information: It logs messages throughout the process for debugging and monitoring purposes.


Overall, the Embeddings class offers a convenient way to leverage the Promezio OAI API for text embedding generation. It handles authentication, request construction, response parsing, and error handling, allowing developers to focus on integrating embeddings into their applications.

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

// Obtain embeddings
EmbeddingsConfig config = new EmbeddingsConfig(AvailableEmbeddingsModels.EMBEDDING_ADA);
EmbeddingResponse? response = await api.Embeddings.Create(["Test Sentence1", "Test Sentence2"], config);

```