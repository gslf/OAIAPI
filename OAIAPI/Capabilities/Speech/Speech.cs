using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Promezio.OAIAPI.Utils;

namespace Promezio.OAIAPI.Capabilities.Speech;
public class Speech: Capability {
    private string _apikey;
    private Logger _logger;
    private string _model;
    private string _voice;
    private string _responseFormat;
    private decimal _speed;


    public Speech(string apikey, Logger logger) : base() {
        _apikey = apikey;
        _logger = logger;
    }

    public void Init(string model = "tts-1", string voice = "alloy", string responseFormat = "mp3", decimal speed = 1m) {
        _model = model;
        _voice = voice;
        _responseFormat = responseFormat;
        _speed = speed;
    }

    public async Task<SpeechResponse?> Dispatch(string text, string outputFile) {

        if (_model == null)
            return new SpeechResponse { Status = false, Error = "The chat class has not been initialized." };

        using (var client = new HttpClient()) {
            var requestBody = new {
                model = _model,
                input = text,
                voice = _voice,
                response_format = _responseFormat,
                speed = _speed
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apikey);

            var response = await client.PostAsync(Endpoints.SPEECH, content);

            if (response.IsSuccessStatusCode) {

                using (var stream = await response.Content.ReadAsStreamAsync()) {
                    // Percorso del file in cui vuoi salvare il MP3
                    string filePath = outputFile;

                    // Scrivi il flusso sul file
                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write)) {
                        await stream.CopyToAsync(fileStream);
                        return new SpeechResponse { Status = true };
                    }
                }

            } else {
                return new SpeechResponse { Status = false, Error = $"Error: {response.StatusCode}" };
            }
        }
    }
    
}

/// <summary>
/// Represents predefined voice constants for use with audio services.
/// </summary>
public struct Voice {
    public const string ALLOY = "alloy";
    public const string ECHO = "echo";
    public const string FABLE = "fable";
    public const string ONYX = "onyx";
    public const string SHIMMER = "shimmer";
}

/// <summary>
/// Represents predefined response format constants for audio output.
/// </summary>
public struct ResponseFormat {
    public const string MP3 = "mp3";
    public const string OPUS = "opus";
    public const string AAC = "aac";
    public const string FLAC = "flac";
}

// ////////////////////////////////////////////
#region Response class structure

/// <summary>
/// Represents the OpenAI API response for a chat completition request.
/// </summary>
public class SpeechResponse {
    // Response status properties
    public bool Status { get; set; }
    public string? Error { get; set; }

}
#endregion
