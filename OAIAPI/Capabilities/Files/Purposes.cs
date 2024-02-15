namespace Promezio.OAIAPI.Capabilities.Files;

/// <summary>
/// Enumerates the different purposes available for an OpenAI API file.
/// </summary>
public enum AvailablePurposes {
    FINE_TUNE,
    FINE_TUNE_RESULTS,
    ASSISTANTS,
    ASSISTANTS_OUTPUT
}

/// <summary>
/// Represents the purpose of an OpenAI API file with user-friendly, customizable text representations.
/// </summary>
public class Purposes {

    private AvailablePurposes _purpose;

    /// <summary>
    /// Initializes a new instance of the <see cref="Purposes"/> class.
    /// </summary>
    /// <param name="purpose">The purpose of the file, defaults to FINE_TUNE.</param>
    public Purposes(AvailablePurposes purpose = AvailablePurposes.FINE_TUNE) {
        _purpose = purpose;
    }

    /// <summary>
    /// Gets a string representation of the purpose, designed for interaction with the OpenAI API.
    /// </summary>
    /// <returns>A string representing the purpose.</returns>
    public override string ToString() {
        switch(_purpose) {
            case AvailablePurposes.FINE_TUNE:
                return "fine-tune";
            case AvailablePurposes.FINE_TUNE_RESULTS:
                return "fine-tune-results";
            case AvailablePurposes.ASSISTANTS:
                return "assistants";
            case AvailablePurposes.ASSISTANTS_OUTPUT:
                return "assistants_output";
        }

        return "INVALID";
    }
}
