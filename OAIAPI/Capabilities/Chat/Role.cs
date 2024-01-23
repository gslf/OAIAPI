using System.Reflection;

namespace Promezio.OAIAPI.Capabilities.Chat;

/// <summary>
/// Represents the role of the author of a specific message.
/// </summary>
public class Role {
    public static string SYSTEM { get; } = "system";
    public static string USER { get; } = "user";
    public static string ASSISTANT { get; } = "assistant";

    /// <summary>
    /// Validates whether a given role name corresponds to any of the predefined role names in this class.
    /// </summary>
    /// <param name="role">The name of the role to validate.</param>
    /// <returns>True if the role name exists in the predefined roles; otherwise, false.</returns>
    public static bool IsValid(string role) {
        Type selfType = typeof(Role);
        PropertyInfo[] selfProperties = selfType.GetProperties();

        foreach (PropertyInfo propertyInfo in selfProperties) {
            string? propertyValue = (string?)propertyInfo.GetValue(null);

            if (propertyValue != null && propertyValue == role) {
                return true;
            }
        }

        return false;
    }
}


