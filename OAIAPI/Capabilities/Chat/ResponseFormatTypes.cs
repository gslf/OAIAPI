using System.Reflection;

namespace Promezio.OAIAPI.Capabilities.Chat;

public class ResponseFormatTypes {
    public static string JSON { get; } = "json_object";
    public static string TEXT { get; } = "text";

    /// <summary>
    /// Validates whether a given format name corresponds to any of the predefined format names in this class.
    /// </summary>
    /// <param name="format">The name of the response format to validate.</param>
    /// <returns>True if the format name exists in the predefined formats; otherwise, false.</returns>
    public static bool IsValid(string? format) {

        if (format == null)
            return false;

        Type selfType = typeof(ResponseFormatTypes);
        PropertyInfo[] selfProperties = selfType.GetProperties();

        foreach (PropertyInfo propertyInfo in selfProperties) {
            string? propertyValue = (string?)propertyInfo.GetValue(null);

            if (propertyValue != null && propertyValue == format) {
                return true;
            }
        }

        return false;
    }
}
