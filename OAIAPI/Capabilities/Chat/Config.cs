﻿namespace Promezio.OAIAPI.Capabilities.Chat;

/// <summary>
/// Represents a configuration object for customizing chat responses generated by the OpenAI chat API.
/// </summary>
public class Config {
    /// <summary>
    /// The OpenAI chat model identifier used for generating responses.
    /// </summary>
    public ChatModel Model { get; set; }
    /// <summary>
    /// A penalty that discourages repetition of common words, ranging from -2.0 (heavy penalty) to 2.0 (no penalty). Default is 0.
    /// </summary>
    public int FrequencyPenalty { get; set; }
    /// <summary>
    /// A flag indicating whether to include log probabilities in the response for analysis. Default is false.
    /// </summary>
    public bool Logprobs { get; set; }
    /// <summary>
    /// The maximum number of top log probabilities to return if `Logprobs` is enabled, between 0 and 5. Default is null (unlimited).
    /// </summary>
    public int? TopLogprobs { get; set; }
    /// <summary>
    /// The maximum number of tokens (words or characters) to generate in the response. Default is null (no limit).
    /// </summary>
    public int? MaxTokens { get; set; }
    /// <summary>
    /// The number of different response candidates to generate. Default is 1.
    /// </summary>
    public int N { get; set; }
    /// <summary>
    /// A penalty that discourages repetition of previously used words, ranging from -2.0 (heavy penalty) to 2.0 (no penalty). Default is 0.
    /// </summary>
    public decimal PresencePenalty { get; set; }
    /// <summary>
    /// The desired format for the response (JSON or TEXT). Default is null (API default).
    /// </summary>
    public ResponseFormat? ResponseFormat { get; set; }
    /// <summary>
    /// An optional seed value for random number generation, influencing response variety. Default is null (random).
    /// </summary>
    public int? Seed { get; set; }
    /// <summary>
    /// A specific list of strings that interupt the generation.
    /// </summary>
    public string[]? Stop { get; set; }
    /// <summary>
    /// A flag indicating whether to use streaming for receiving partial responses over time. Default is false.
    /// </summary>
    public bool Stream { get; set; }
    /// <summary>
    /// Controls the randomness in response generation, ranging from 0 (deterministic) to 2 (highly random). Default is 1.
    /// </summary>
    public decimal Temperature { get; set; }
    /// <summary>
    /// Controls the diversity of response options, ranging from 0 (most predictable) to 1 (maximally diverse). Default is 1.
    /// </summary>
    public decimal TopP { get; set; }
    /// <summary>
    /// An optional user identifier associated with the request. Default is null.
    /// </summary>
    public string? User { get; set; }

    /// <summary>
    /// Initializes a new configuration instance with the specified parameters.
    /// </summary>
    /// <param name="model">The chat model to use for generation.</param>
    /// <param name="frequencyPenalty">Optional frequency penalty (default: 0).</param>
    /// <param name="logprobs">Optional flag to include log probabilities (default: false).</param>
    /// <param name="topLogprobs">Optional limit for top log probabilities (default: null).</param>
    /// <param name="maxTokens">Optional maximum number of tokens (default: null).</param>
    /// <param name="n">Number of responses to generate (default: 1).</param>
    /// <param name="presencePenalty">Optional presence penalty (default: 0m).</param>
    /// <param name="responseFormat">Optional response format (default: null).</param>
    /// <param name="seed">Optional random number generation seed (default: null).</param>
    /// <param name="temperature">Controls randomness in generation (default: 1m).</param>
    /// <param name="topP">Controls diversity of response options (default: 1m).</param>
    /// <param name="user">Optional user identifier (default: null
    /// <exception cref="ArgumentOutOfRangeException">Thrown when provided arguments are outside their allowed range.</exception>
    /// <remarks>
    /// This method sets up the parameters for generating responses using a specific model. It includes various options to control the generation behavior, such as penalties, probabilities, and format of the response.
    /// </remarks>
    public Config(ChatModel model,
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
            throw new ArgumentOutOfRangeException(nameof(frequencyPenalty), "The parameter must be between -2.0 and 2.0.");
        }

        if (topLogprobs < 0 || topLogprobs > 5) {
            throw new ArgumentOutOfRangeException(nameof(topLogprobs), "The parameter must be between 0 and 5.");
        }

        if (presencePenalty < -2 || presencePenalty > 2) {
            throw new ArgumentOutOfRangeException(nameof(presencePenalty), "The parameter must be between -2.0 and 2.0.");
        }

        if (temperature < 0 || temperature > 2) {
            throw new ArgumentOutOfRangeException(nameof(temperature), "The parameter must be between 0 and 2.");
        }

        if (topP < 0 || topP > 1) {
            throw new ArgumentOutOfRangeException(nameof(topP), "The parameter must be between 0 and 1.");
        }

        Model = model;
        FrequencyPenalty = frequencyPenalty;
        Logprobs = logprobs;
        TopLogprobs = topLogprobs;
        MaxTokens = maxTokens;
        N = n;
        PresencePenalty = presencePenalty;
        ResponseFormat = responseFormat ?? new ResponseFormat();
        Seed = seed;
        Temperature = temperature;
        TopP = topP;
        User = user;
    }
}
