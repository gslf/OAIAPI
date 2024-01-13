using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Promezio.OAIAPI.Capabilities.FineTuning;
/// <summary>
/// Provides a collection of predefined AI model names and a method to validate model names.
/// This class serves as a central repository for referencing different AI models supported by the Promezio OAIAPI.
/// </summary>
public static class Models {
    /// <summary>
    /// Represents the GPT-3.5 Turbo model released in November 2023.
    /// </summary>
    public static string GPT_3_5_TURBO_1106 { get; } = "gpt-3.5-turbo-1106";

    /// <summary>
    /// Represents the GPT-3.5 Turbo model released in June 2023.
    /// </summary>
    public static string GPT_3_5_TURBO_0613 { get; } = "gpt-3.5-turbo-0613";

    /// <summary>
    /// Represents the Babbage model, version 002.
    /// </summary>
    public static string BABBAGE_002 { get; } = "babbage-002";

    /// <summary>
    /// Represents the Davinci model, version 002.
    /// </summary>
    public static string DAVINCI_002 { get; } = "davinci-002";

    /// <summary>
    /// Represents the GPT-4 model released in June 2023.
    /// </summary>
    public static string GPT_4_0613 { get; } = "gpt-4-0613";

    /// <summary>
    /// Validates whether a given model name corresponds to any of the predefined model names in this class.
    /// </summary>
    /// <param name="model">The name of the model to validate.</param>
    /// <returns>True if the model name exists in the predefined models; otherwise, false.</returns>
    public static bool IsValid(string model) {
        Type selfType = typeof(Models);
        PropertyInfo[] selfProperties = selfType.GetProperties();

        foreach (PropertyInfo propertyInfo in selfProperties) {
            string? propertyValue = (string?)propertyInfo.GetValue(null);

            if (propertyValue != null && propertyValue == model) {
                return true;
            }
        }

        return false;
    }
}
