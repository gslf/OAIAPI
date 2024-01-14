using Promezio.OAIAPI.Capabilities.FineTuning;
using System.Reflection;

namespace Promezio.OAIAPI.Capabilities.Files;

public class Purposes {
    public static string FINE_TUNE { get; } = "fine-tune";
    public static string FINE_TUNE_RESULTS { get; } = "fine-tune-results";
    public static string ASSISTANTS { get; } = "assistants";
    public static string ASSISTANTS_OUTPUT { get; } = "assistants_output";

    /// <summary>
    /// Validates whether a given purpose name corresponds to any of the predefined purpose names in this class.
    /// </summary>
    /// <param name="purpose">The name of the purpose to validate.</param>
    /// <returns>True if the purpos name exists in the predefined purposes; otherwise, false.</returns>
    public static bool IsValid(string purpose) {
        Type selfType = typeof(Purposes);
        PropertyInfo[] selfProperties = selfType.GetProperties();

        foreach (PropertyInfo propertyInfo in selfProperties) {
            string? propertyValue = (string?)propertyInfo.GetValue(null);

            if (propertyValue != null && propertyValue == purpose) {
                return true;
            }
        }

        return false;
    }
}
