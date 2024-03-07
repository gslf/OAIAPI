using Promezio.OAIAPI.Capabilities.FineTuning;
using Promezio.OAIAPI.Utils;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace Promezio.OAIAPI.Capabilities.Assistants;
public class Assistants: Capability {

    public Assistants(string apikey, Logger logger) : base(apikey, logger) { }


    //TODO - BETA: Create
    public async Task<AssistantObject> Create(
        Models model,
        string? name = null,
        string? description = null,
        string? instructions = null,
        Tool[]? tools = null,
        string[]? filesIDS = null,
        Dictionary<string, string>? metadata = null) {

        _logger.Info("[Assistants.Create] New request.");

        // Request
        using (var client = new HttpClient()) {

            var requestBody = new {
                model = model.ToString(),
                name = name,
                description = description,
                instructions = instructions,
                tools = tools?.Select(t => new {
                    type = t.Type,
                    function = new { 
                        description = t.Function?.Description,
                        name = t.Function?.Name,
                        parameters = t.Function?.Parameters
                    }
                }).ToArray(),
                file_ids = filesIDS,
                metadata = metadata
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apikey);

            var response = await client.PostAsync(Endpoints.ASSISTANTS, content);
            if (!response.IsSuccessStatusCode) {
                string message = await response.Content.ReadAsStringAsync();
                _logger.Error($"[Assistants.Create] The HTTP request failed with status code: {message}.");
                throw new HttpRequestException($"The HTTP request failed with status code: {message}.");
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            AssistantObject? parsed_response = JsonSerializer.Deserialize<AssistantObject>(responseContent, _serializerOptions);

            if (parsed_response == null) {
                _logger.Error("[Assistants.Create] Parsing the response did not produce a valid object.");
                throw new JsonException("Parsing the response did not produce a valid object.");
            }

            return parsed_response;
        }
    }

    //TODO - BETA: List
    //TODO - BETA: Retrieve
    //TODO - BETA: Modify
    //TODO - BETA: Delete
}
