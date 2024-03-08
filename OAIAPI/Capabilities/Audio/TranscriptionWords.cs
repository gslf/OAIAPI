namespace Promezio.OAIAPI.Capabilities.Audio;

/// <summary>
/// Class representing a transcribed word in OpenAI's Audio API.
/// </summary>
public class TranscriptionWords {
    /// <summary>
    /// Gets or sets the transcribed word.
    /// </summary>
    public string? Word { get; set; }
    /// <summary>
    /// Gets or sets the start time of the word in the audio stream.
    /// </summary>
    public double Start { get; set; }
    /// <summary>
    /// Gets or sets the end time of the word in the audio stream.
    /// </summary>
    public double End { get; set; }
}
