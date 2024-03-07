namespace Promezio.OAIAPI.Capabilities.Assistants;
public class AssistantObject {
    public string? Id { get; set; }
    public string? Object {  get; set; }
    public int? Created_at { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Model { get; set; }
    public string? Instructions { get; set; }
    public Tool? Tools { get; set; }

    public string[]? File_ids { get; set; }
    public Dictionary<string, string>? Metadata { get; set; }
}
