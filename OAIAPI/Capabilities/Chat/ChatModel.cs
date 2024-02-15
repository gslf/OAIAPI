using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promezio.OAIAPI.Capabilities.Chat;

/// <summary>
/// Enumerates the available chat models that can be used with the OpenAI API.
/// </summary>
public enum AvailableChatModels {
    GPT_3_5_TURBO_0125,
    GPT_3_5_TURBO,
    GPT_3_5_TURBO_1106,
    GPT4,
    GPT4_TURBO_0125,
    GPT4_VISION_PREVIEW,
    GPT4_32K,
}


/// <summary>
/// Represents a chat model that can be used with the OpenAI API.
/// </summary>
public class ChatModel {
    /// <summary>
    /// The underlying available chat model.
    /// </summary>
    private AvailableChatModels _model;

    /// <summary>
    /// Initializes a new instance of the ChatModel class with the specified model.
    /// The default model is GPT-3.5-turbo-0125 if not specified.
    /// </summary>
    /// <param name="model">The available chat model to use.</param>
    public ChatModel(AvailableChatModels model = AvailableChatModels.GPT_3_5_TURBO_0125) {
        _model = model;
    }

    /// <summary>
    /// Returns a string representation of the chat model name, formatted for use in API requests.
    /// </summary>
    /// <returns>The string representation of the chat model name.</returns>
    public override string ToString() {
        switch (_model) {
            case AvailableChatModels.GPT_3_5_TURBO:
                return "gpt-3.5-turbo";
            case AvailableChatModels.GPT_3_5_TURBO_0125:
                return "gpt-3.5-turbo-0125";
            case AvailableChatModels.GPT_3_5_TURBO_1106:
                return "gpt-3.5-turbo-1106";
            case AvailableChatModels.GPT4:
                return "gpt-4";
            case AvailableChatModels.GPT4_TURBO_0125:
                return "gpt-4-0125-preview";
            case AvailableChatModels.GPT4_VISION_PREVIEW:
                return "gpt-4-vision-preview";
            case AvailableChatModels.GPT4_32K:
                return "gpt-4-32k";
        }

        return "INVALID";
    }
}
