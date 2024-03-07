namespace Promezio.OAIAPI.Capabilities.FineTuning;

/// <summary>
/// Represents the different OpenAI fine-tuning models that can be selected.
/// </summary>
public enum AvailableModels {
    GPT_3_5_TURBO_1106,
    GPT_3_5_TURBO_0613,
    BABBAGE_002,
    DAVINCI_002,
    GPT_4_0613
}

/// <summary>
/// Represents an OpenAI fine-tuning model and its string representation used when interacting with the API.
/// </summary>
public class Models {
    private AvailableModels _model;

    /// <summary>
    /// Initializes a new instance of the <see cref="Models"/> class.
    /// </summary>
    /// <param name="model">The OpenAI model type, with gpt-3.5-turbo-1106 as the default.</param>
    public Models(AvailableModels model = AvailableModels.GPT_3_5_TURBO_1106) {
        _model = model;
    }

    /// <summary>
    /// Gets the string representation of the OpenAI model used for API calls.
    /// </summary>
    /// <returns>A string representing the OpenAI model.</returns>
    public override string ToString() {
        switch (_model) {
            case AvailableModels.GPT_3_5_TURBO_1106:
                return "gpt-3.5-turbo-1106";
            case AvailableModels.GPT_3_5_TURBO_0613:
                return "gpt-3.5-turbo-0613";
            case AvailableModels.BABBAGE_002:
                return "babbage-002";
            case AvailableModels.DAVINCI_002:
                return "davinci-002";
            case AvailableModels.GPT_4_0613:
                return "gpt-4-0613";
        }

        return "INVALID";
    }
}
