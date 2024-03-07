namespace Promezio.OAIAPI.Capabilities.Assistants;

public class ToolFunction {
    public string? Description { get; set; }
    public string? Name { get; set; }
    public Dictionary<string, string>? Parameters { get; set; }
}
