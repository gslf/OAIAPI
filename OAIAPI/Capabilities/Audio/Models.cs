namespace Promezio.OAIAPI.Capabilities.Audio;
public enum AvailableModels {
    TTS_1,
    TTS_1_HD
}
public class Models {
    private AvailableModels _model;
    public Models(AvailableModels model = AvailableModels.TTS_1) {
        _model = model;
    }

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
