using Promezio.OAIAPI.Utils;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Promezio.OAIAPI.Capabilities.Chat;

/// <summary>
/// Represents a chat client for interacting with OpenAI completition chat model API.
/// </summary>
public class Chat : Capability {

    private const string END_STREAM = "data: [DONE]";
    private const string PRE_STREAM = "data: ";

    private List<ChatMessage> _messages;

    /// <summary>
    /// Initializes a new instance of the Chat class with the specified API key.
    /// </summary>
    /// <param name="apikey">The API key for authentication.</param>
    /// <param name="logger">An instance of a logging object to record actions and errors.</param>
    public Chat(string apikey, Logger logger) : base(apikey, logger) {
        _messages = new List<ChatMessage>();
    }

    /// <summary>
    /// Sends a single prompt to the OpenAI chat API and returns the complete response.
    /// </summary>
    /// <param name="prompt">The text prompt to send to the chat model.</param>
    /// <param name="config">Configuration options for the OpenAI API request.</param>
    /// <returns>A ChatResponse object containing the API's response and details.</returns>
    /// <exception cref="HttpRequestException">Thrown if the HTTP request to the API fails.</exception>
    public async Task<ChatResponse?> Dispatch(string prompt, Config config) {

        _logger.Info("[Chat.Dispatch] New request.");

        _messages.Add(new ChatMessage { Role = new Role(AvailableRoles.USER).ToString(), Content = prompt });

        var messages_array = _messages.Select(x => new {
            role = x.Role,
            content = x.Content,
        }).ToArray();

        using (var client = new HttpClient()) {
            var requestBody = new {
                model = config.Model.ToString(),
                messages = messages_array,
                frequency_penalty = config.FrequencyPenalty,
                logprobs = config.Logprobs,
                top_logprobs = config.TopLogprobs,
                max_tokens = config.MaxTokens,
                n = config.N,
                presence_penalty = config.PresencePenalty,
                response_format = new {
                    type = config.ResponseFormat?.ToString()
                },
                seed = config.Seed,
                stream = false,
                temperature = config.Temperature,
                top_p = config.TopP,
                user = config.User
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apikey);

            var response = await client.PostAsync(Endpoints.CHAT, content);

            if (!response.IsSuccessStatusCode) {
                string message = await response.Content.ReadAsStringAsync();
                string errorMessage = $"[[Chat.Dispatch] An HTTP request failed - {message}";
                _logger.Error(errorMessage);
                throw new HttpRequestException(errorMessage);
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            ChatResponse? parsed_response = JsonSerializer.Deserialize<ChatResponse>(responseContent, _serializerOptions);

            if (parsed_response is null) {
                string errorMessage = $"[[Chat.Dispatch] The server response is empty";
                _logger.Error(errorMessage);
            }else {
                _messages.Add(new ChatMessage { Role = new Role(AvailableRoles.USER).ToString(), Content = parsed_response.GetMessage() });
            }
           
            _logger.Info("[Chat.Dispatch] Request Completed.");
            return parsed_response;   
        }
    }

    /// <summary>
    /// Opens a stream to the OpenAI chat API and sends a prompt, receiving responses in real-time.
    /// </summary>
    /// <param name="prompt">The text prompt to send to the chat model.</param>
    /// <param name="config">Configuration options for the OpenAI API request.</param>
    /// <returns>An IAsyncEnumerable of ChatStreamResponse objects containing partial responses.</returns>
    /// <exception cref="HttpRequestException">Thrown if the HTTP request to the API fails.</exception>
    public async IAsyncEnumerable<ChatStreamResponse?> DispatchStream(string prompt, Config config) {

        _logger.Info("[Chat.DispatchStream] New request.");

        _messages.Add(new ChatMessage { Role = new Role(AvailableRoles.USER).ToString(), Content = prompt });

        var messages_array = _messages.Select(x => new {
            role = x.Role,
            content = x.Content,
        }).ToArray();

        using (var client = new HttpClient()) {
            var requestBody = new {
                model = config.Model.ToString(),
                messages = messages_array,
                frequency_penalty = config.FrequencyPenalty,
                logprobs = config.Logprobs,
                top_logprobs = config.TopLogprobs,
                max_tokens = config.MaxTokens,
                n = config.N,
                presence_penalty = config.PresencePenalty,
                response_format = new {
                    type = config.ResponseFormat?.ToString()
                },
                seed = config.Seed,
                stream = true,
                temperature = config.Temperature,
                top_p = config.TopP,
                user = config.User
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apikey);

            var response = await client.PostAsync(Endpoints.CHAT, content);

            if (!response.IsSuccessStatusCode) {
                string message = await response.Content.ReadAsStringAsync();
                string errorMessage = $"[Chat.DispatchStream] An HTTP request failed - {message}";
                _logger.Error(errorMessage);
                throw new HttpRequestException(errorMessage);
            }

            using (var stream = await response.Content.ReadAsStreamAsync())
            using (StreamReader reader = new StreamReader(stream)) {
                string? line;
                string fullMessage = "";

                while ((line = await reader.ReadLineAsync()) != null) {

                    if (line == null) {
                        _logger.Warning("[Chat.DispatchStream] The stream reader read a null line.");
                        yield break;
                    }

                    if (line == END_STREAM) {
                        _logger.Info("[Chat.DispatchStream] Stream Ended.");
                        _messages.Add(new ChatMessage { Role = new Role(AvailableRoles.USER).ToString(), Content = fullMessage });
                        yield break;
                    }

                    if (line != "") {
                        string parsableLine = line.Replace(PRE_STREAM, "");
                        ChatStreamResponse? parsed_response = JsonSerializer.Deserialize<ChatStreamResponse>(parsableLine, _serializerOptions);

                        if (parsed_response is null) {
                            string errorMessage = $"[[Chat.DispatchStream] The server response is empty";
                            _logger.Error(errorMessage);
                            continue;
                        }

                        fullMessage += parsed_response.GetMessage();
          
                        yield return parsed_response;
                        
                    } else {
                        _logger.Warning("[Chat.DispatchStream] The stream reader read an empty line.");
                        continue;
                    }

                }
            }
        }
    }
}


