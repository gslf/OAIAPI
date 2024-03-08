namespace Promezio.OAIAPI.Capabilities.Audio;

/// <summary>
/// Enum representing the available speech formats in OpenAI's Audio API.
/// </summary>
public enum AvailableSpeechFormats {
    MP3 ,
    OPUS,
    AAC, 
    FLAC
}

/// <summary>
/// Class representing the available speech response formats in OpenAI's Audio API.
/// </summary>
public class SpeechResponseFormats {
    private AvailableSpeechFormats _format;

    /// <summary>
    /// Constructor that initializes a new instance of the SpeechResponseFormats class with a specified format.
    /// </summary>
    /// <param name="format">The format to be used. Default is MP3.</param>
    public SpeechResponseFormats(AvailableSpeechFormats format = AvailableSpeechFormats.MP3) {
        _format = format;
    }

    /// <summary>
    /// Returns a string representation of the current format.
    /// </summary>
    /// <returns>A string representation of the current format.</returns>
    public override string ToString() {
        switch (_format) {
            case AvailableSpeechFormats.MP3:
                return "mp3";
            case AvailableSpeechFormats.OPUS:
                return "opus";
            case AvailableSpeechFormats.AAC:
                return "aac";
            case AvailableSpeechFormats.FLAC:
                return "flac";
        }

        return "INVALID";
    }
}
