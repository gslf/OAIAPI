using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promezio.OAIAPI.Capabilities.Chat;

/// <summary>
/// A Configuration class for the chat capability
/// </summary>
public class Config {
    public string? Model {  get; set; }
    public int FrequencyPenalty { get; set; }
    public bool Logprobs { get; set; }
    public int? TopLogprobs { get; set; }
    public int? MaxTokens { get; set; }
    public int N { get; set; }
    public decimal PresencePenalty { get; set; }
    public ResponseFormat? ResponseFormat { get; set; }
    public int? Seed { get; set; }
    public string[]? Stop { get; set; }
    public bool Stream { get; set; }
    public decimal Temperature { get; set; }
    public decimal TopP { get; set; }
    public string? User { get; set; }

    /// <summary>
    /// Initializes che configuration instance with specified parameters.
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
    public Config(string model,
                     int frequencyPenalty = 0,
                     bool logprobs = false,
                     int? topLogprobs = null,
                     int? maxTokens = null,
                     int n = 1,
                     decimal presencePenalty = 0m,
                     ResponseFormat? responseFormat = null,
                     int? seed = null,
                     decimal temperature = 1m,
                     decimal topP = 1m,
                     string? user = null) {

        // Parameters validation
        if (frequencyPenalty < -2 || frequencyPenalty > 2) {
            throw new ArgumentOutOfRangeException(nameof(frequencyPenalty), "Frequency penalty must be between -2.0 and 2.0.");
        }

        if (topLogprobs < 0 || topLogprobs > 5) {
            throw new ArgumentOutOfRangeException(nameof(topLogprobs), "Top logprobs must be between 0 and 5.");
        }

        if (presencePenalty < -2 || presencePenalty > 2) {
            throw new ArgumentOutOfRangeException(nameof(presencePenalty), "Presence penalty must be between -2.0 and 2.0.");
        }

        if (responseFormat != null && responseFormat?.Type != ResponseFormatTypes.JSON && responseFormat?.Type != ResponseFormatTypes.TEXT) {
            throw new ArgumentOutOfRangeException(nameof(responseFormat), "Response Format type property must be a string from ResponseFormatTypes structure.");
        }

        if (temperature < 0 || temperature > 2) {
            throw new ArgumentOutOfRangeException(nameof(temperature), "Temperature must be between 0 and 2.");
        }

        if (topP < 0 || topP > 1) {
            throw new ArgumentOutOfRangeException(nameof(topP), "Top p must be between 0 and 1.");
        }

        Model = model;
        FrequencyPenalty = frequencyPenalty;
        Logprobs = logprobs;
        TopLogprobs = topLogprobs;
        MaxTokens = maxTokens;
        N = n;
        PresencePenalty = presencePenalty;
        ResponseFormat = responseFormat;
        Seed = seed;
        Temperature = temperature;
        TopP = topP;
        User = user;
    }
}
