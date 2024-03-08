namespace Promezio.OAIAPI.Capabilities.Audio;

/// <summary>
/// Class representing a segment of transcription in OpenAI's Audio API.
/// </summary>
public class TranscriptionSegments {
    /// <summary>
    /// Gets or sets the ID of the segment.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the seek position of the segment.
    /// </summary>
    public int? Seek { get; set; }

    /// <summary>
    /// Gets or sets the start time of the segment in the audio stream.
    /// </summary>
    public double? Start { get; set; }

    /// <summary>
    /// Gets or sets the end time of the segment in the audio stream.
    /// </summary>
    public double? End { get; set; }

    /// <summary>
    /// Gets or sets the transcribed text of the segment.
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Gets or sets the tokens of the segment.
    /// </summary>
    public int[]? Tokens { get; set; }

    /// <summary>
    /// Gets or sets the temperature of the segment.
    /// </summary>
    public double? Temperature { get; set; }

    /// <summary>
    /// Gets or sets the Ava log probability of the segment.
    /// </summary>
    public double? Ava_logprob { get; set; }

    /// <summary>
    /// Gets or sets the compression ratio of the segment.
    /// </summary>
    public double? Compression_ratio { get; set; }

    /// <summary>
    /// Gets or sets the probability of no speech in the segment.
    /// </summary>
    public double? No_speech_prob { get; set; }
}
