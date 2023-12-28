using System.Text.Json;

namespace Promezio.OAIAPI.Utils;
public class LowerCaseNamingPolicy : JsonNamingPolicy {
    public override string ConvertName(string name) => name.ToLower();
}
