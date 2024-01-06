using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;
using Promezio.OAIAPI.Utils;

namespace Promezio.OAIAPI.Capabilities.Transcription;

/// <summary>
/// Represents a client for interacting with OpenAI audio API.
/// </summary>

public class Transcription: Capability {

    private decimal _temperature;

    /// <summary>
    /// Initializes a new instance of the Transcription class with the specified API key.
    /// </summary>
    /// <param name="apikey">The API key used for authentication with the service.</param>
    public Transcription(string apikey, Logger logger) : base(apikey, logger) {
        _apikey = apikey;
        _logger = logger;
    }

    /// <summary>
    /// Initializes the transcription service with a specified temperature.
    /// </summary>
    /// <param name="temperature">The temperature parameter used for processing, defaulting to 0.</param>
    public void Init(decimal temperature = 0) {
        _temperature = temperature;
    }

    /// <summary>
    /// Performs audio transcription on the provided audio URL.
    /// </summary>
    /// <param name="audioURL">The URL of the audio file to transcribe.</param>
    /// <returns>A Task representing the asynchronous operation, with a TranscriptionResponse.</returns>
    public async Task<TranscriptionResponse?> Dispatch(string audioURL) {
           
        using (var client = new HttpClient()) {
            // Read audio file
            byte[] soundBytes = await File.ReadAllBytesAsync(audioURL);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apikey);
            MultipartFormDataContent content = new(){
                    { new ByteArrayContent(soundBytes), "file", Path.GetFileName(audioURL) },
                    { new StringContent("whisper-1"), "model" },
                    { new StringContent(_temperature.ToString(CultureInfo.InvariantCulture)), "temperature" }
            };
            HttpResponseMessage response = await client.PostAsync(Endpoints.TRANSCRIPTION, content);


            if (response.IsSuccessStatusCode) {
                var responseContent = await response.Content.ReadAsStringAsync();
                TranscriptionResponse? parsed_response = JsonSerializer.Deserialize<TranscriptionResponse>(responseContent, _serializerOptions);

                if (parsed_response != null) 
                    parsed_response.Status = true;
                    
                return parsed_response;
            } else {
                return new TranscriptionResponse { Status = false, Error = $"Error: {response.StatusCode}" };
            }

        }

    }
}


// ////////////////////////////////////////////
#region Response class structures

/// <summary>
/// Encapsulates the response from an audio transcription request.
/// </summary>
public class TranscriptionResponse() {
    // Response status properties
    public bool Status { get; set; }
    public string? Error { get; set; }
    public string? Text {  get; set; }
}

#endregion
