namespace Promezio.OAIAPI.Capabilities.Audio;

/// <summary>
/// Enum representing the available models in OpenAI's Audio API.
/// </summary>
public enum AvailableModels {
    TTS_1,
    TTS_1_HD
}

/// <summary>
/// Class representing the available models in OpenAI's Audio API.
/// </summary>
public class Models {
    private AvailableModels _model;

    /// <summary>
    /// Constructor that initializes a new instance of the Models class with a specified model.
    /// </summary>
    /// <param name="model">The model to be used. Default is TTS_1.</param>
    public Models(AvailableModels model = AvailableModels.TTS_1) {
        _model = model;
    }

    /// <summary>
    /// Returns a string representation of the current model.
    /// </summary>
    /// <returns>A string representation of the current model.</returns>
    public override string ToString() {
        switch (_model) {
            case AvailableModels.TTS_1:
                return "tts-1";
            case AvailableModels.TTS_1_HD:
                return "tts-1-hd";
        }

        return "INVALID";
    }
}
