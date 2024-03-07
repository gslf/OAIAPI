namespace Promezio.OAIAPI.Capabilities.Assistants;
public enum AvailableModels {
    GPT_3_5_TURBO_0125,
    GPT_4,
    GPT_4_VISION_PREVIEW,
    GPT_4_TURBO_PREVIEW
}
public class Models {
    private AvailableModels _model;

    /// <summary>
    /// Initializes a new instance of the <see cref="Models"/> class.
    /// </summary>
    /// <param name="model">The OpenAI model type, with gpt-3.5-turbo-1106 as the default.</param>
    public Models(AvailableModels model = AvailableModels.GPT_3_5_TURBO_0125) {
        _model = model;
    }

    /// <summary>
    /// Gets the string representation of the OpenAI model used for API calls.
    /// </summary>
    /// <returns>A string representing the OpenAI model.</returns>
    public override string ToString() {
        switch (_model) {
            case AvailableModels.GPT_3_5_TURBO_0125:
                return "gpt-3.5-turbo-0125";
            case AvailableModels.GPT_4:
                return "gpt-4";
            case AvailableModels.GPT_4_VISION_PREVIEW:
                return "gpt4-vision-preview";
            case AvailableModels.GPT_4_TURBO_PREVIEW:
                return "gpt4-turbo-preview";
        }

        return "INVALID";
    }
}