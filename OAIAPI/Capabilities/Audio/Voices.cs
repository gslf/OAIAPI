namespace Promezio.OAIAPI.Capabilities.Audio;

public enum AvailableVoices {
    ALLOY,
    ECHO,
    FABLE,
    ONYX,
    SHIMMER
}

public class Voices {
    private AvailableVoices _voice;

    public Voices(AvailableVoices voice = AvailableVoices.ALLOY) {
        _voice = voice;
    }

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
