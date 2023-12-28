using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Promezio.OAIAPI.Utils;

namespace Promezio.OAIAPI.Capabilities.Chat;

/// <summary>
/// Represents a chat client for interacting with OpenAI completition chat model API.
/// </summary>
public class Chat: Capability {

    private const string END_STREAM = "data: [DONE]";
    private const string PRE_STREAM = "data: ";

    private string _apikey;
    private Logger _logger;
    private List<ChatMessage> _messages;

    // Request params
    private Config? _config;


    /// <summary>
    /// Create a new instance of the Chat class with the specified API key.
    /// </summary>
    /// <param name="apikey">The API key for authentication.</param>
    public Chat(string apikey, Logger logger) : base() {
        _apikey = apikey;
        _logger = logger;
        _messages = new List<ChatMessage>();
    }

    /// <summary>
    /// Init the instance of the Chat class with the configuration.
    /// </summary>
    /// <param name="apikey">The API key for authentication.</param>
    public void Init(Config config) {
        _config = config;
    }

    /// <summary>
    /// Sends a message to the chat model and receives a response.
    /// </summary>
    /// <param name="prompt">The message to send to the model.</param>
    /// <returns>A task that represents the asynchronous operation, containing the chat response.</returns>
    public async Task<ChatResponse?> Dispatch(string prompt) {

        if (_config == null) {
            _logger.Error("[Chat.Dispatch] Function called without class initialization.");
            return new ChatResponse { Status = false, Error = "The chat class has not been initialized." };
        }

        _logger.Info("[Chat.Dispatch] New request.");
        _messages.Add(new ChatMessage { Role = Role.USER, Content = prompt });

        var messages_array = _messages.Select(x => new {
            role = x.Role,
            content = x.Content,
        }).ToArray();

        using (var client = new HttpClient()) {
            var requestBody = new {
                model = _config.Model,
                messages = messages_array,
                frequency_penalty = _config.FrequencyPenalty,
                logprobs = _config.Logprobs,
                top_logprobs = _config.TopLogprobs,
                max_tokens = _config.MaxTokens,
                n = _config.N,
                presence_penalty = _config.PresencePenalty,
                response_format = _config.ResponseFormat?.ToAnonymousType(),
                seed = _config.Seed,
                stream = false,
                temperature = _config.Temperature,
                top_p = _config.TopP,
                user = _config.User
        };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apikey);

            var response = await client.PostAsync(Endpoints.CHAT, content);

            if (response.IsSuccessStatusCode) {
                var responseContent = await response.Content.ReadAsStringAsync();
                ChatResponse? parsed_response = JsonSerializer.Deserialize<ChatResponse>(responseContent, _serializerOptions);

                if (parsed_response != null) {
                    parsed_response.Status = true;
                    _messages.Add(new ChatMessage { Role = Role.SYSTEM, Content = parsed_response.GetMessage() });
                } else {
                    _logger.Warning("[Chat.Dispatch] Response parsing return a null value.");
                }

                _logger.Info("[Chat.Dispatch] Request Completed.");
                return parsed_response;
            } else {
                _logger.Error("[Chat.Dispatch] An HTTP request failed");
                return new ChatResponse { Status = false, Error = $"Error: {response.StatusCode}" };
            }
        }
    }

    public async IAsyncEnumerable<ChatStreamResponse?>  DispatchStream(string prompt) {
        if (_config?.Model == null) {
            _logger.Error("[Chat.DispatchStream] Function called without class initialization.");
            yield break;
        }

        _logger.Info("[Chat.DispatchStream] New request.");

        _messages.Add(new ChatMessage { Role = Role.USER, Content = prompt });

        var messages_array = _messages.Select(x => new {
            role = x.Role,
            content = x.Content,
        }).ToArray();

        using (var client = new HttpClient()) {
            var requestBody = new {
                model = _config.Model,
                messages = messages_array,
                frequency_penalty = _config.FrequencyPenalty,
                logprobs = _config.Logprobs,
                top_logprobs = _config.TopLogprobs,
                max_tokens = _config.MaxTokens,
                n = _config.N,
                presence_penalty = _config.PresencePenalty,
                response_format = _config.ResponseFormat?.ToAnonymousType(),
                seed = _config.Seed,
                stream = true,
                temperature = _config.Temperature,
                top_p = _config.TopP,
                user = _config.User
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apikey);

            var response = await client.PostAsync(Endpoints.CHAT, content);

            using (var stream = await response.Content.ReadAsStreamAsync())
            using (StreamReader reader = new StreamReader(stream)) {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null) {

                    if (line == null) {
                        _logger.Warning("[Chat.DispatchStream] The stream reader read a null line.");
                        yield break;
                    }

                    if (line == END_STREAM) {
                        _logger.Info("[Chat.DispatchStream] Stream Ended.");
                        yield break;
                    }

                    if (line != "") {
                        string parsableLine = line.Replace(PRE_STREAM, "");
                        ChatStreamResponse? parsed_response = JsonSerializer.Deserialize<ChatStreamResponse>(parsableLine, _serializerOptions);

                        if (parsed_response != null) {
                            yield return parsed_response;
                        } else {
                            _logger.Warning("[Chat.DispatchStream] Response parsing return a null value.");
                            continue;
                        }
                    } else {
                        _logger.Warning("[Chat.DispatchStream] The stream reader read an empty line.");
                        continue;
                    }
                    
                }
            }
        }
    }
}


