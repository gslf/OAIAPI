namespace Promezio.OAIAPI.Capabilities.Chat;


public struct ResponseFormat {
    public string Type { get; set; }

    public object ToAnonymousType() {
        return new { type = Type };
    }
}

