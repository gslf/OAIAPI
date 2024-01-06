using Promezio.OAIAPI.Capabilities.Chat;
using Promezio.OAIAPI.Capabilities.Transcription;
using Promezio.OAIAPI.Capabilities.Speech;
using Promezio.OAIAPI.Utils;
using Promezio.OAIAPI.Capabilities.Files;

namespace Promezio.OAIAPI;
public class OAIAPI {
    private string _apikey;
    private Logger _logger;

    // Constructor
    public OAIAPI(string apikey, LogLevel logLevel = LogLevel.Warning) {
        _apikey = apikey;
        _logger = new Logger(logLevel);

        // Init capabilities
        Chat = new Chat(_apikey, _logger);
        Files = new Files(_apikey, _logger);
        Transcription = new Transcription(_apikey, _logger);
        Speech = new Speech(_apikey, _logger);
    }

    // Capabilities
    public Chat Chat { get; private set; }
    public Files Files { get; private set; }
    public Transcription Transcription { get; private set; }
    public Speech Speech { get; private set; }

}

