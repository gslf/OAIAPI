using System.Text.Json;
using Promezio.OAIAPI.Utils;

namespace Promezio.OAIAPI.Capabilities;
public abstract class Capability {
    protected JsonSerializerOptions _serializerOptions;

    public Capability() {
        _serializerOptions = new JsonSerializerOptions {
            PropertyNamingPolicy = new LowerCaseNamingPolicy(),
        };
    }
}

