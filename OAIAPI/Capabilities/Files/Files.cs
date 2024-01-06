using Promezio.OAIAPI.Capabilities.Transcription;
using Promezio.OAIAPI.Utils;
using System.Globalization;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text.Json;

namespace Promezio.OAIAPI.Capabilities.Files;


/// <summary>
/// The Files class provides functionalities to interact with the OpenAI REST API for file operations.
/// It allows for uploading, listing, retrieving, deleting, and accessing the content of files.
/// </summary>
/// <remarks>
/// This class is part of the Promezio.OAIAPI library and extends the Capability base class.
/// It requires an API key and a Logger instance for initialization.
/// </remarks>
public class Files : Capability {

    /// <summary>
    /// Initializes a new instance of the Files class.
    /// </summary>
    /// <param name="apikey">The API key for authenticating with the OpenAI API.</param>
    /// <param name="logger">The logger instance for logging purposes.</param>
    public Files(string apikey, Logger logger) : base(apikey, logger) { }


    /// <summary>
    /// Uploads a file to the OpenAI API for processing.
    /// </summary>
    /// <param name="fileURI">The URI of the file to be uploaded.</param>
    /// <param name="filePurpose">The purpose of the file upload, which must be a valid purpose as defined by OpenAI.</param>
    /// <returns>A FileObject representing the uploaded file.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided file purpose is not valid.</exception>
    /// <exception cref="HttpRequestException">Thrown when the HTTP request to the OpenAI API fails.</exception>
    /// <exception cref="JsonException">Thrown when parsing the API response fails.</exception>
    public async Task<FileObject> Upload(string fileURI, string filePurpose) {

        // Validate filePurpose
        if (!FileObject.Purposes.ContainsValue(filePurpose)) {
            throw new ArgumentException("The parameter is invalid", nameof(filePurpose));
        }

        using (var client = new HttpClient()) {
            // Read audio file
            byte[] fileBytes = await File.ReadAllBytesAsync(fileURI);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apikey);
            MultipartFormDataContent content = new(){
                    { new ByteArrayContent(fileBytes), "file", Path.GetFileName(fileURI) },
                    { new StringContent(filePurpose), "purpose" }
            };

            HttpResponseMessage response = await client.PostAsync(Endpoints.FILES, content);


            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"The HTTP request failed with status code: {response.StatusCode}.");

            var responseContent = await response.Content.ReadAsStringAsync();
            FileObject? parsed_response = JsonSerializer.Deserialize<FileObject>(responseContent, _serializerOptions);

            if (parsed_response == null)
                throw new JsonException("Parsing the response did not produce a valid object.");

            return parsed_response;
        }
    }


    /// <summary>
    /// Lists all files uploaded by the user to the OpenAI API.
    /// </summary>
    /// <returns>An array of FileObject instances, or null if no files are found.</returns>
    /// <exception cref="HttpRequestException">Thrown when the HTTP request to the OpenAI API fails.</exception>
    public async Task<FileObject[]?> List() {

        using (var client = new HttpClient()) {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apikey);

            var response = await client.GetAsync(Endpoints.FILES);
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"The HTTP request failed with status code: {response.StatusCode}.");

            var responseContent = await response.Content.ReadAsStringAsync();
            JsonDocument jsonDoc = JsonDocument.Parse(responseContent);
            JsonElement dataElement = jsonDoc.RootElement.GetProperty("data");

            FileObject[]? retrievedFiles = JsonSerializer.Deserialize<FileObject[]>(dataElement.ToString(), _serializerOptions);

            return retrievedFiles;
        }
    }

    /// <summary>
    /// Retrieves a specific file by its ID from the OpenAI API.
    /// </summary>
    /// <param name="fileID">The ID of the file to retrieve.</param>
    /// <returns>A FileObject representing the retrieved file.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided file ID is not in a valid format.</exception>
    /// <exception cref="HttpRequestException">Thrown when the HTTP request to the OpenAI API fails.</exception>
    /// <exception cref="JsonException">Thrown when parsing the API response fails.</exception>
    public async Task<FileObject> Retrieve(string fileID) {
        using (var client = new HttpClient()) {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apikey);

            string fullEndpoint = $"{Endpoints.FILES}/{fileID}";

            if (!Uri.IsWellFormedUriString(fullEndpoint, UriKind.Absolute))
                throw new ArgumentException("The fileID format is invalid");

            var response = await client.GetAsync(fullEndpoint);
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"The HTTP request failed with status code: {response.StatusCode}.");

            var responseContent = await response.Content.ReadAsStringAsync();
            FileObject? parsed_response = JsonSerializer.Deserialize<FileObject>(responseContent, _serializerOptions);

            if (parsed_response == null)
                throw new JsonException("Parsing the response did not produce a valid object.");

            return parsed_response;
        }
    }


    /// <summary>
    /// Deletes a specific file by its ID from the OpenAI API.
    /// </summary>
    /// <param name="fileID">The ID of the file to delete.</param>
    /// <returns>True if the file is successfully deleted, otherwise false.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided file ID is not in a valid format.</exception>
    /// <exception cref="HttpRequestException">Thrown when the HTTP request to the OpenAI API fails.</exception>
    public async Task<bool> Delete(string fileID) {
        using (var client = new HttpClient()) {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apikey);

            string fullEndpoint = $"{Endpoints.FILES}/{fileID}";

            if (!Uri.IsWellFormedUriString(fullEndpoint, UriKind.Absolute))
                throw new ArgumentException("The fileID format is invalid");

            var response = await client.DeleteAsync(fullEndpoint);
            return response.IsSuccessStatusCode;
        }
    }


    /// <summary>
    /// Retrieves the content of a specific file by its ID from the OpenAI API.
    /// </summary>
    /// <param name="fileID">The ID of the file whose content is to be retrieved.</param>
    /// <returns>A string containing the content of the file.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided file ID is not in a valid format.</exception>
    /// <exception cref="HttpRequestException">Thrown when the HTTP request to the OpenAI API fails.</exception>
    public async Task<string> Content(string fileID) {
        using (var client = new HttpClient()) {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apikey);

            string fullEndpoint = $"{Endpoints.FILES}/{fileID}/content";

            if (!Uri.IsWellFormedUriString(fullEndpoint, UriKind.Absolute))
                throw new ArgumentException("The fileID format is invalid");

            var response = await client.GetAsync(fullEndpoint);
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"The HTTP request failed with status code: {response.StatusCode}.");

            string responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;
        }
    }
}
