using Promezio.OAIAPI.Capabilities.Assistants;
using Promezio.OAIAPI.Capabilities.Audio;
using Promezio.OAIAPI.Capabilities.Chat;
using Promezio.OAIAPI.Capabilities.Embedding;
using Promezio.OAIAPI.Capabilities.Files;
using Promezio.OAIAPI.Capabilities.FineTuning;
using Promezio.OAIAPI.Utils;

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
        FineTuning = new FineTuning(_apikey, _logger);
        Embeddings = new Embeddings(_apikey, _logger);
        Assistants = new Assistants(_apikey, _logger);
        Audio = new Audio(_apikey, _logger);
    }

    // Capabilities
    public Chat Chat { get; private set; }
    public Files Files { get; private set; }
    public FineTuning FineTuning { get; private set; }
    public Embeddings Embeddings { get; private set; }
    public Audio Audio { get; private set; }


    // BETA
    public Assistants Assistants { get; private set; }
}

