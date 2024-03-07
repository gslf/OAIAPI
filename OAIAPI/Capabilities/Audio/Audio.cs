using Promezio.OAIAPI.Utils;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Globalization;

namespace Promezio.OAIAPI.Capabilities.Audio;
public class Audio : Capability {

    public Audio(string apikey, Logger logger) : base(apikey, logger) { }

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

            var response = await client.PostAsync(Endpoints.SPEECH, content);

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
            HttpResponseMessage response = await client.PostAsync(Endpoints.TRANSCRIPTION, content);

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

    // TODO: Translation
}
