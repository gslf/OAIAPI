namespace Promezio.OAIAPI.Capabilities.Chat;

/// <summary>
/// Represents an object with statistics for the completition request.
/// </summary>
public class Usage {
    public int Prompt_tokens { get; set; }
    public int Completition_tokens { get; set; }
    public int Total_tokens { get; set; }
}

