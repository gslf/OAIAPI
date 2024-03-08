using Promezio.OAIAPI.Utils;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Globalization;

namespace Promezio.OAIAPI.Capabilities.Audio;

/// <summary>
/// Class representing the Audio capability in OpenAI's API.
/// </summary>
public class Audio : Capability { 

    /// <summary>
    /// Constructor that initializes a new instance of the Audio class with a specified API key and logger.
    /// </summary>
    /// <param name="apikey">The API key to be used.</param>
    /// <param name="logger">The logger to be used.</param>
    public Audio(string apikey, Logger logger) : base(apikey, logger) { }

    /// <summary>
    /// Method to generate speech from text.
    /// </summary>
    /// <param name="model">The model to be used.</param>
    /// <param name="text">The text to be converted to speech.</param>
    /// <param name="outputFile">The output file where the speech will be saved.</param>
    /// <param name="voice">The voice to be used. Default is null.</param>
    /// <param name="responseFormat">The response format to be used. Default is null.</param>
    /// <param name="speed">The speed of the speech. Default is 1.0.</param>
    /// <returns>A boolean indicating the success of the operation.</returns>
    public async Task<bool> Speech(Models model, 
                                     string text, 
                                     string outputFile,
                                     Voices? voice = null,
                                     SpeechResponseFormats? responseFormat = null,
                                     double speed = 1.0) {

        // Parameters validation
        if (voice is null)
            voice = new Voices();

        if (responseFormat is null)
            responseFormat = new SpeechResponseFormats();

        if ((speed< 0.25) || (speed > 4)) {
            string errorMessage = $"[Audio.Speech] {nameof(speed)} parameter is invalid";
            _logger.Error(errorMessage);
            throw new ArgumentException(errorMessage);
        }


        // API Request
        _logger.Info("[Audio.Speech] New request.");

        using (var client = new HttpClient()) {
            var requestBody = new {
                model = model.ToString(),
                input = text,
                voice = voice.ToString(),
                response_format = responseFormat.ToString(),
                speed = speed
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apikey);

            var response = await client.PostAsync(Endpoints.AUDIO + "/speech", content);

            if (!response.IsSuccessStatusCode) {
                string message = await response.Content.ReadAsStringAsync();
                string errorMessage = $"[Audio.Speech] The HTTP request failed. \n {message}.";
                _logger.Error(errorMessage);
                throw new HttpRequestException(errorMessage);
            }

            using (var stream = await response.Content.ReadAsStreamAsync()) {
                // Percorso del file in cui vuoi salvare il MP3
                string filePath = outputFile;

                // Scrivi il flusso sul file
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write)) {
                    await stream.CopyToAsync(fileStream);
                    return true;
                }
            }
        }
    }

    /// <summary>
    /// Method to transcribe audio.
    /// </summary>
    /// <param name="audioURL">The URL of the audio to be transcribed.</param>
    /// <param name="prompt">The prompt to be used. Default is an empty string.</param>
    /// <param name="temperature">The temperature to be used. Default is 0.</param>
    /// <returns>A TranscriptionObject representing the transcription of the audio.</returns>
    public async Task<TranscriptionObject?> Transcription(
        string audioURL,
        string prompt = "",
        double temperature = 0) {

        // Validate parameters
        if ((temperature < 0) || (temperature > 1)) {
            string errorMessage = $"[Audio.Transcription] {nameof(temperature)} parameter is invalid";
            _logger.Error(errorMessage);
            throw new ArgumentException(errorMessage);
        }

        // API request
        using (var client = new HttpClient()) {
            // Read audio file
            byte[] soundBytes = await File.ReadAllBytesAsync(audioURL);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apikey);
            MultipartFormDataContent content = new(){
                    { new ByteArrayContent(soundBytes), "file", Path.GetFileName(audioURL) },
                    { new StringContent("whisper-1"), "model" },
                    { new StringContent(temperature.ToString(CultureInfo.InvariantCulture)), "temperature" }
            };
            HttpResponseMessage response = await client.PostAsync(Endpoints.AUDIO + "/transcriptions", content);

            if (!response.IsSuccessStatusCode) {
                string message = await response.Content.ReadAsStringAsync();
                string errorMessage = $"[Audio.Transcription] The HTTP request failed. \n {message}.";
                _logger.Error(errorMessage);
                throw new HttpRequestException(errorMessage);
            }


            var responseContent = await response.Content.ReadAsStringAsync();
            TranscriptionObject? parsed_response = JsonSerializer.Deserialize<TranscriptionObject>(responseContent, _serializerOptions);

            if (parsed_response == null) {
                _logger.Error("[Audio.Transcription] Parsing the response did not produce a valid object.");
                throw new JsonException("Parsing the response did not produce a valid object.");
            }

            return parsed_response;

        }

    }

    /// <summary>
    /// Method to translate audio.
    /// </summary>
    /// <param name="audioURL">The URL of the audio to be translated.</param>
    /// <param name="prompt">The prompt to be used. Default is an empty string.</param>
    /// <param name="temperature">The temperature to be used. Default is 0.</param>
    /// <returns>A string representing the translation of the audio.</returns>
    public async Task<string> Translation(
        string audioURL,
        string prompt = "",
        double temperature = 0) {

        // Validate parameters
        if ((temperature < 0) || (temperature > 1)) {
            string errorMessage = $"[Audio.Translations] {nameof(temperature)} parameter is invalid";
            _logger.Error(errorMessage);
            throw new ArgumentException(errorMessage);
        }

        // API request
        using (var client = new HttpClient()) {
            // Read audio file
            byte[] soundBytes = await File.ReadAllBytesAsync(audioURL);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apikey);
            MultipartFormDataContent content = new(){
                    { new ByteArrayContent(soundBytes), "file", Path.GetFileName(audioURL) },
                    { new StringContent("whisper-1"), "model" },
                    { new StringContent(temperature.ToString(CultureInfo.InvariantCulture)), "temperature" }
            };
            HttpResponseMessage response = await client.PostAsync(Endpoints.AUDIO + "/translations", content);

            if (!response.IsSuccessStatusCode) {
                string message = await response.Content.ReadAsStringAsync();
                string errorMessage = $"[Audio.Translations] The HTTP request failed. \n {message}.";
                _logger.Error(errorMessage);
                throw new HttpRequestException(errorMessage);
            }


            var responseContent = await response.Content.ReadAsStringAsync();
            JsonDocument jsonDoc = JsonDocument.Parse(responseContent);
            JsonElement dataElement = jsonDoc.RootElement.GetProperty("text");
            string parsedString = dataElement.ToString();

            return parsedString;
        }
    }
}
