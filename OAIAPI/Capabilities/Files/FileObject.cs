namespace Promezio.OAIAPI.Capabilities.Files;

/// <summary>
/// Represents a file object with specific attributes for managing and tracking 
/// files within the Promezio.OAIAPI context.
/// </summary>
public class FileObject {

    /// <summary>
    /// A static dictionary defining the purposes of different file types within 
    /// the system. Each purpose is associated with a string key.
    /// </summary>
    public static Dictionary<string,string> Purposes = new() {
        { "FINE_TUNE", "fine-tune" },
        { "FINE_TUNE_RESULTS", "fine-tune-results" },
        { "ASSISTANTS", "assistants" },
        { "ASSISTANTS_OUTPUT", "assistants_output" }
    };

    /// <summary>
    /// Gets or sets the unique identifier for the file.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the size of the file in bytes.
    /// </summary>
    public int Bytes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp of the file.
    /// </summary>
    public int Created_at {  get; set; }

    /// <summary>
    /// Gets or sets the filename.
    /// </summary>
    public string? Filename { get; set; }

    /// <summary>
    /// Gets or sets the purpose of the file, based on the predefined purposes in the Purposes dictionary.
    /// </summary>
    public string? Purpose { get; set; }
}
