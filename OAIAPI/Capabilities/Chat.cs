using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Promezio.OAIAPI.Utils;

namespace Promezio.OAIAPI.Capabilities.Chat;

/// <summary>
/// Represents a chat client for interacting with OpenAI completition chat model API.
/// </summary>
public class Chat: Capability {

    private string _apikey;
    private List<ChatMessage> _messages;

    // Request params
    private string? _model;
    private int _frequency_penalty;
    private bool _logprobs;
    private int? _top_logprobs;
    private int? _max_tokens;
    private int _n;
    private decimal _presence_penalty;
    private ResponseFormat? _response_format;
    private int? _seed;
    private string[]? _stop;
    private bool _stream;
    private decimal _temperature;
    private decimal _top_p;
    private string? _user;


    /// <summary>
    /// Initializes a new instance of the Chat class with the specified API key.
    /// </summary>
    /// <param name="apikey">The API key for authentication.</param>
    public Chat(string apikey) : base() {
        _apikey = apikey;
        _messages = new List<ChatMessage>();
    }

    /// <summary>
    /// Initializes che Chat instance with specified parameters for generating responses.
    /// </summary>
    /// <param name="model">The model identifier used for generating responses.</param>
    /// <param name="frequency_penalty">Optional frequency penalty to apply, ranging from -2.0 to 2.0. Default is 0.</param>
    /// <param name="logprobs">Optional flag to include log probabilities in the response. Default is false.</param>
    /// <param name="top_logprobs">Optional limit for the number of top log probabilities to return. Must be between 0 and 5. Default is null.</param>
    /// <param name="max_tokens">Optional maximum number of tokens to generate. Default is null.</param>
    /// <param name="n">Number of responses to generate. Default is 1.</param>
    /// <param name="presence_penalty">Optional presence penalty to apply, ranging from -2.0 to 2.0. Default is 0.</param>
    /// <param name="response_format">Optional format for the response (JSON or TEXT). Default is null.</param>
    /// <param name="seed">Optional seed for random number generation. Default is null.</param>
    /// <param name="stream">Flag indicating whether to stream the response. Default is false.</param>
    /// <param name="temperature">Controls randomness in response generation, ranging from 0 to 2. Default is 1.</param>
    /// <param name="top_p">Controls diversity of response, ranging from 0 to 1. Default is 1.</param>
    /// <param name="user">Optional user identifier. Default is null.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when provided arguments are outside their allowed range.</exception>
    /// <remarks>
    /// This method sets up the parameters for generating responses using a specific model. It includes various options to control the generation behavior, such as penalties, probabilities, and format of the response.
    /// </remarks>
    public void Init(string model,
                     int frequency_penalty = 0,
                     bool logprobs = false,
                     int? top_logprobs = null,
                     int? max_tokens = null,
                     int n = 1,
                     decimal presence_penalty = 0m,
                     ResponseFormat? response_format = null,
                     int? seed = null,
                     bool stream = false,
                     decimal temperature = 1m,
                     decimal top_p = 1m,
                     string? user = null) {

        // Parameters validation
        if (frequency_penalty < -2 && frequency_penalty > 2) {
            throw new ArgumentOutOfRangeException(nameof(frequency_penalty), "Frequency penalty must be between -2.0 and 2.0.");
        }

        if (top_logprobs < 0 && top_logprobs > 5) {
            throw new ArgumentOutOfRangeException(nameof(top_logprobs), "Top logprobs must be between 0 and 5.");
        }

        if (presence_penalty < -2 && presence_penalty > 2) {
            throw new ArgumentOutOfRangeException(nameof(presence_penalty), "Presence penalty must be between -2.0 and 2.0.");
        }

        if (response_format !=  null && response_format?.Type != ResponseFormatTypes.JSON && response_format?.Type != ResponseFormatTypes.TEXT) {
            throw new ArgumentOutOfRangeException(nameof(response_format), "Response Format type property must be a string from ResponseFormatTypes structure.");
        }

        if (temperature < 0 && temperature > 2) {
            throw new ArgumentOutOfRangeException(nameof(temperature), "Temperature must be between 0 and 2.");
        }

        if (top_p < 0 && top_p > 1) {
            throw new ArgumentOutOfRangeException(nameof(top_p), "Top p must be between 0 and 1.");
        }

        _model = model;
        _frequency_penalty = frequency_penalty;
        _logprobs = logprobs;
        _top_logprobs = top_logprobs;
        _max_tokens = max_tokens;
        _n = n;
        _presence_penalty = presence_penalty;
        _response_format = response_format;
        _seed = seed;
        _stream = stream;
        _temperature = temperature;
        _top_p = top_p;
        _user = user;
    }

    /// <summary>
    /// Sends a message to the chat model and receives a response.
    /// </summary>
    /// <param name="prompt">The message to send to the model.</param>
    /// <returns>A task that represents the asynchronous operation, containing the chat response.</returns>
    public async Task<ChatResponse?> Dispatch(string prompt) {
        if (_model == null)
            return new ChatResponse { Status = false, Error = "The chat class has not been initialized." };

        _messages.Add(new ChatMessage { Role = Role.USER, Content = prompt });

        var messages_array = _messages.Select(x => new {
            role = x.Role,
            content = x.Content,
        }).ToArray();

        using (var client = new HttpClient()) {
            var requestBody = new {
                model = _model,
                messages = messages_array,
                frequency_penalty = _frequency_penalty,
                logprobs = _logprobs,
                top_logprobs = _top_logprobs,
                max_tokens = _max_tokens,
                n = _n,
                presence_penalty = _presence_penalty,
                response_format = _response_format?.ToAnonymousType(),
                seed = _seed,
                stream = _stream,
                temperature = _temperature,
                top_p = _top_p,
                user = _user
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
                }

                return parsed_response;
            } else {
                return new ChatResponse { Status = false, Error = $"Error: {response.StatusCode}" };
            }
        }
    }
}

// ////////////////////////////////////////////
#region MessageStructure

/// <summary>
/// Represents a chat message sent and received from the API.
/// </summary>
public struct ChatMessage{
    public string? Role { get; set; }
    public string? Content { get; set; }
}


/// <summary>
/// Represents the role of the author of a specific message.
/// </summary>
public struct Role {
    public const string SYSTEM = "system";
    public const string USER = "user";
    public const string ASSISTANT = "assistant";
}
#endregion

// ////////////////////////////////////////////
#region RequestStructures

public struct ResponseFormat {
    public string Type { get; set; }

    public object ToAnonymousType() {
        return new { type = Type };
    }
}

public struct ResponseFormatTypes {
    public const string JSON = "json_object";
    public const string TEXT = "text";
}
#endregion


// ////////////////////////////////////////////
#region ResponseStructures

/// <summary>
/// Represents the OpenAI API response for a chat completition request.
/// </summary>
public class ChatResponse {
    // Response status properties
    public bool Status { get; set; }
    public string? Error { get; set; }

    // OpenAI response model
  
    public string? Id { get; set; }
    public int Created {  get; set; }
    public string? Model { get; set; }
    public string? System_fingerprint { get; set; }
    public Choice[]? Choices { get; set; }
    public Usage? Usage { get; set; }

    public string? GetMessage() {
        if (Choices != null && Choices.Length > 0){
            if (Choices[0].Message != null) {
                return Choices[0].Message?.Content;
            }
        }
            
        return "";
    }
}

/// <summary>
/// Represents a list of chat completion choices.
/// </summary>
public class Choice {
    public int Index { get; set; }
    public ChatMessage? Message { get; set; }
    public string? Finish_reason { get; set; }
}

/// <summary>
/// Represents an object with statistics for the completition request.
/// </summary>
public class Usage { 
    public int Prompt_tokens { get; set; }
    public int Completition_tokens { get; set; }
    public int Total_tokens { get; set; }
}
#endregion

