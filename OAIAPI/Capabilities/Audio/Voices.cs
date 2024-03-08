namespace Promezio.OAIAPI.Capabilities.Audio;

/// <summary>
/// Enum representing the available voices in OpenAI's Audio API.
/// </summary>
public enum AvailableVoices {
    ALLOY,
    ECHO,
    FABLE,
    ONYX,
    SHIMMER
}

/// <summary>
/// Class representing the available voices in OpenAI's Audio API.
/// </summary>
public class Voices {
    private AvailableVoices _voice;

    /// <summary>
    /// Constructor that initializes a new instance of the Voices class with a specified voice.
    /// </summary>
    /// <param name="voice">The voice to be used. Default is ALLOY.</param>
    public Voices(AvailableVoices voice = AvailableVoices.ALLOY) {
        _voice = voice;
    }

    /// <summary>
    /// Returns a string representation of the current voice.
    /// </summary>
    /// <returns>A string representation of the current voice.</returns>
    public override string ToString() {
        switch (_voice) {
            case AvailableVoices.ALLOY:
                return "alloy";
            case AvailableVoices.ECHO:
                return "echo";
            case AvailableVoices.FABLE:
                return "fable";
            case AvailableVoices.ONYX:
                return "onyx";
            case AvailableVoices.SHIMMER:
                return "shimmer";
        }

        return "INVALID";
    }
}
