namespace Promezio.OAIAPI.Capabilities.Chat;
#region MessageStructure
#endregion

// ////////////////////////////////////////////
#region RequestStructures

public struct ResponseFormat {
    public string Type { get; set; }

    public object ToAnonymousType() {
        return new { type = Type };
    }
}
#endregion

