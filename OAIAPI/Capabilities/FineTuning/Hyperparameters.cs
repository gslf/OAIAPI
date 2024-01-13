using System.Text.Json.Serialization;
using System.Text.Json;

namespace Promezio.OAIAPI.Capabilities.FineTuning;

/// <summary>
/// Custom JSON converter for handling the conversion of decimal values from JSON.
/// This converter supports reading decimal values from JSON, whether they are represented as numbers or strings.
/// </summary>
public class DecimalFromStringConverter : JsonConverter<decimal> {
    /// <summary>
    /// Reads a decimal value from a JSON reader.
    /// </summary>
    /// <param name="reader">The UTF8 JSON reader to read from.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">The serializer options to use.</param>
    /// <returns>The decimal value read from the JSON.</returns>
    /// <exception cref="JsonException">Thrown when unable to convert the string to a decimal.</exception>
    public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        if (reader.TokenType == JsonTokenType.Number) {
            return reader.GetDecimal();
        } else if (reader.TokenType == JsonTokenType.String) {
            return -1000;
        }

        throw new JsonException("Unable to convert string to decimal.");
    }

    /// <summary>
    /// Writes a decimal value to a JSON writer.
    /// </summary>
    /// <param name="writer">The UTF8 JSON writer to write to.</param>
    /// <param name="value">The decimal value to write.</param>
    /// <param name="options">The serializer options to use.</param>
    public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options) {
        writer.WriteStringValue(value.ToString());
    }
}

/// <summary>
/// Represents the hyperparameters for a fine-tuning process in the Promezio OAIAPI.
/// </summary>
public class Hyperparameters {
    /// <summary>
    /// The number of epochs to run during training, nullable. Uses <see cref="DecimalFromStringConverter"/> for JSON conversion.
    /// </summary>
    [JsonConverter(typeof(DecimalFromStringConverter))]
    public decimal? N_epochs { get; set; }

    /// <summary>
    /// The size of each batch during training, nullable. Uses <see cref="DecimalFromStringConverter"/> for JSON conversion.
    /// </summary>
    [JsonConverter(typeof(DecimalFromStringConverter))]
    public decimal? Batch_size { get; set; }

    /// <summary>
    /// The learning rate multiplier for training, nullable. Uses <see cref="DecimalFromStringConverter"/> for JSON conversion.
    /// </summary>
    [JsonConverter(typeof(DecimalFromStringConverter))]
    public decimal? Learning_rate_multiplier { get; set; }
}