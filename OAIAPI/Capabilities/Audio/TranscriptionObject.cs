using Promezio.OAIAPI.Capabilities.Audio;

namespace Promezio.OAIAPI.Capabilities.Audio;


/// <summary>
/// Class representing a transcription object in OpenAI's Audio API.
/// </summary>
public class TranscriptionObject {
    /// <summary>
    /// Gets or sets the language of the transcription.
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// Gets or sets the duration of the audio stream.
    /// </summary>
    public string? Duration { get; set; }

    /// <summary>
    /// Gets or sets the transcribed text.
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Gets or sets the transcription segments.
    /// </summary>
    public TranscriptionSegments? Segments { get; set; }

    /// <summary>
    /// Gets or sets the transcribed words.
    /// </summary>
    public TranscriptionWords? Words { get; set; }
}
