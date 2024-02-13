using System.Reflection;

namespace Promezio.OAIAPI.Capabilities.Chat;

/// <summary>
/// Enumerates the possible roles associated with messages in OpenAI chat responses.
/// </summary>
public enum AvailableRoles {
    SYSTEM,
    USER,
    ASSISTANT
}

/// <summary>
/// Represents the role associated with a message in the OpenAI chat API response.
/// </summary>
public class Role {
    /// <summary>
    /// The underlying role.
    /// </summary>
    private AvailableRoles _role;

    /// <summary>
    /// Initializes a new instance of the Role class with the specified role.
    /// Defaults to USER role.
    /// </summary>
    /// <param name="role">The role associated with the message.</param>
    public Role(AvailableRoles role = AvailableRoles.USER) {
        _role = role;
    }

    /// <summary>
    /// Returns a string representation of the role.
    /// </summary>
    /// <returns>A string indicating either "system", "user", or "assistant" based on the chosen role.</returns>
    public override string ToString() {
        switch (_role) {
            case AvailableRoles.SYSTEM:
                return "system";
            case AvailableRoles.USER:
                return "user";
            case AvailableRoles.ASSISTANT:
                return "assistant";
        }

        return "";
    }

}


