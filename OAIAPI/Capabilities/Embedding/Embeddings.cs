using Promezio.OAIAPI.Capabilities.Chat;
using Promezio.OAIAPI.Utils;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace Promezio.OAIAPI.Capabilities.Embedding;
public class Embeddings : Capability{

    public Embeddings(string apikey, Logger logger) : base(apikey, logger) { }

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
                model = config.Model.Value(),
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
