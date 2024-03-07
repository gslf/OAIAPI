namespace Promezio.OAIAPI.Capabilities.Audio;

public class TranscriptionSegments {
    public int? Id { get; set; }
    public int? Seek { get; set; }
    public double? Start { get; set; }
    public double? End { get; set; }
    public string? Text { get; set; }
    public int[]? Tokens { get; set; }
    public double? Temperature { get; set; }
    public double? Ava_logprob { get; set; }
    public double? Compression_ratio { get; set; }
    public double? No_speech_prob { get; set; }
}
