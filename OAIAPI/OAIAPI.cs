using Promezio.OAIAPI.Capabilities.Chat;
using Promezio.OAIAPI.Capabilities.Transcription;
using Promezio.OAIAPI.Capabilities.Speech;

namespace Promezio.OAIAPI;
public class OAIAPI {
    private string _apikey;

    // Constructor
    public OAIAPI(string apikey) {
        _apikey = apikey;

        // Init capabilities
        Chat = new Chat(_apikey);
        Transcription = new Transcription(_apikey);
        Speech = new Speech(_apikey);
    }

    // Capabilities
    public Chat Chat { get; private set; }
    public Transcription Transcription { get; private set; }
    public Speech Speech { get; private set; }

}

