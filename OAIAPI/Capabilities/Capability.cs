using System.Text.Json;
using Promezio.OAIAPI.Utils;

namespace Promezio.OAIAPI.Capabilities;
public abstract class Capability {
    protected JsonSerializerOptions _serializerOptions;
    protected string _apikey;
    protected Logger _logger;

    public Capability(string apikey, Logger logger) {
        _serializerOptions = new JsonSerializerOptions {
            PropertyNamingPolicy = new LowerCaseNamingPolicy(),
        };

        _apikey = apikey;
        _logger = logger;
    }
}

