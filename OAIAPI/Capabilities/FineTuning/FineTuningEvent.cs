namespace Promezio.OAIAPI.Capabilities.FineTuning;

/// <summary>
/// Represents an event in the context of fine-tuning operations in the Promezio OAIAPI.
/// This class is designed to capture and represent various aspects of an event that occurs during the fine-tuning process.
/// </summary>
public class FineTuningEvent {
    /// <summary>
    /// Gets or sets the unique identifier of the event.
    /// This ID can be used to reference and track specific events within the fine-tuning process.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the timestamp indicating when the event was created, represented as an integer.
    /// This timestamp can be used to determine the chronological order of events and to timestamp specific operations or changes.
    /// </summary>
    public int? Created_at { get; set; }

    /// <summary>
    /// Gets or sets the level of the event, which could be indicative of its severity or type (e.g., 'Info', 'Warning', 'Error').
    /// This property helps categorize the event and can be useful for filtering or handling events based on their level.
    /// </summary>
    public string? Level { get; set; }

    /// <summary>
    /// Gets or sets a message associated with the event.
    /// This message typically provides additional details or context about the event, aiding in understanding what occurred.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Gets or sets the object associated with the event.
    /// This can be a reference to an entity or component within the fine-tuning process that is relevant to the event.
    /// </summary>
    public string? Object { get; set; }
}

