namespace Promezio.OAIAPI.Capabilities.Audio;

public enum AvailableSpeechFormats {
    MP3 ,
    OPUS,
    AAC, 
    FLAC
}
public class SpeechResponseFormats {
    private AvailableSpeechFormats _format;

    public SpeechResponseFormats(AvailableSpeechFormats format = AvailableSpeechFormats.MP3) {
        _format = format;
    }

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
