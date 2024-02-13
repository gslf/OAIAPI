using Promezio.OAIAPI.Capabilities.Chat;
using Promezio.OAIAPI.Utils;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace Promezio.OAIAPI.Capabilities.Embedding;

/// <summary>
/// Represents the embeddings capability of the Promezio OAI API.
/// </summary>
public class Embeddings : Capability{

    /// <summary>
    /// Initializes a new instance of the <see cref="Embeddings"/> class.
    /// </summary>
    /// <param name="apikey">The API key to use for authentication.</param>
    /// <param name="logger">The logger to use for logging messages.</param>
    public Embeddings(string apikey, Logger logger) : base(apikey, logger) { }

    /// <summary>
    /// Creates embeddings for a given set of input texts.
    /// </summary>
    /// <param name="input">An array of text strings to create embeddings for.</param>
    /// <param name="config">The configuration options for the embedding creation.</param>
    /// <returns>An instance of <see cref="EmbeddingResponse"/> containing the resulting embeddings, or null if the request failed.</returns>
    /// <exception cref="ArgumentException">Thrown if the length of the `input` array exceeds 2048 characters.</exception>
    /// <exception cref="HttpRequestException">Thrown if the HTTP request to the server fails.</exception>
    public async Task<EmbeddingResponse?> Create(string[] input, EmbeddingsConfig config) {

        _logger.Info("[Embeddings.Create] New request");

        // Parameters validation
        if (input.Length > 2048) {
            string errorMessage = $"[Embeddings.Create] {nameof(input)} string array lenght must be less than 2048 chars";
            _logger.Error(errorMessage);
            throw new ArgumentException(errorMessage);
        }


        // HTTP request
        using (var client = new HttpClient()) {
            var requestBody = new {
                model = config.Model.ToString(),
                input = input,
                encoding_format = config.EncodingFormat,
                user = config.User ?? ""
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apikey);

            var response = await client.PostAsync(Endpoints.EMBEDDINGS, content);

            if (!response.IsSuccessStatusCode) {
                string message = await response.Content.ReadAsStringAsync();
                string errorMessage = $"[Embeddings.Create] An HTTP request failed - {message}";
                _logger.Error(errorMessage);
                throw new HttpRequestException(errorMessage);
            }


            var responseContent = await response.Content.ReadAsStringAsync();
            EmbeddingResponse? parsed_response = JsonSerializer.Deserialize<EmbeddingResponse>(responseContent, _serializerOptions);

            if (parsed_response is null) {
                string errorMessage = $"[Embeddings.Create] The server response is empty";
                _logger.Error(errorMessage);
            }

            _logger.Info("[Embeddings.Create] Request Completed.");

            return parsed_response;
        }
    }
}
