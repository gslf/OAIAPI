using Promezio.OAIAPI.Capabilities.Audio;

namespace Promezio.OAIAPI.Capabilities.Audio;
public class TranscriptionObject {
    public string? Language { get; set; }
    public string? Duration { get; set; }
    public string? Text { get; set; }
    public TranscriptionSegments? Segments { get; set; }
    public TranscriptionWords? Words { get; set; }
}
