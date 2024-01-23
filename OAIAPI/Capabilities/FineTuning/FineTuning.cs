using Promezio.OAIAPI.Utils;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Promezio.OAIAPI.Capabilities.FineTuning;

/// <summary>
/// The FineTuning class provides methods to interact with the Fine Tuning capability of the OpenAI API.
/// This capability allows for creating, listing, retrieving, and canceling fine-tuning jobs for machine learning models.
/// </summary>
public class FineTuning : Capability {

    /// <summary>
    /// Initializes a new instance of the FineTuning class with the provided API key and logger.
    /// </summary>
    /// <param name="apikey">The API key used for authentication.</param>
    /// <param name="logger">An instance of the logger for capturing log messages.</param>
    public FineTuning(string apikey, Logger logger) : base(apikey, logger) { }

    /// <summary>
    /// Creates a fine-tuning job for a machine learning model with the specified parameters.
    /// </summary>
    /// <param name="trainingFileID">The ID of the training file.</param>
    /// <param name="modelName">The name of the model to be fine-tuned.</param>
    /// <param name="batch_size">Optional. The batch size for training.</param>
    /// <param name="learning_rate_multiplier">Optional. The learning rate multiplier.</param>
    /// <param name="n_epochs">Optional. The number of training epochs.</param>
    /// <param name="suffix">Optional. A suffix for the fine-tuning job.</param>
    /// <param name="validationFileID">Optional. The ID of the validation file.</param>
    /// <returns>A <see cref="FineTuningObject"/> representing the created fine-tuning job.</returns>
    public async Task<FineTuningObject> Create(string trainingFileID,
                                            string modelName,
                                            decimal? batch_size = null,
                                            decimal? learning_rate_multiplier = null,
                                            decimal? n_epochs = null,
                                            string? suffix = null,
                                            string? validationFileID = null) {

        _logger.Info("[FineTuning.Create] New request.");

        // Vallidation
        if (!Models.IsValid(modelName)) {
            _logger.Error($"[FineTuning.Create] The parameter {nameof(modelName)} is not valid");
            throw new ArgumentException($"The parameter {nameof(modelName)} is not valid");
        }

        // Request
        using (var client = new HttpClient()) {

            var requestBody = new {
                training_file = trainingFileID,
                model = modelName,
                hyperparameters = new {
                    batch_size = batch_size,
                    learning_rate_multiplier = learning_rate_multiplier,
                    n_epochs = n_epochs
                },
                suffix = suffix,
                validation_file = validationFileID
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apikey);

            var response = await client.PostAsync(Endpoints.FINE_TUNING, content);
            if (!response.IsSuccessStatusCode) {
                string message = await response.Content.ReadAsStringAsync();
                _logger.Error($"[FineTuning.Create] The HTTP request failed with status code: {message}.");
                throw new HttpRequestException($"The HTTP request failed with status code: {message}.");
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            FineTuningObject? parsed_response = JsonSerializer.Deserialize<FineTuningObject>(responseContent, _serializerOptions);

            if (parsed_response == null) {
                _logger.Error("[FineTuning.Create] Parsing the response did not produce a valid object.");
                throw new JsonException("Parsing the response did not produce a valid object.");
            }

            return parsed_response;
        }
    }

    /// <summary>
    /// Lists all fine-tuning jobs.
    /// </summary>
    /// <returns>An array of <see cref="FineTuningObject"/> representing the list of fine-tuning jobs.</returns>
    public async Task<FineTuningObject[]?> ListJobs() {

        using (var client = new HttpClient()) {

            _logger.Info("[FineTuning.ListJobs] new request.");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apikey);

            var response = await client.GetAsync(Endpoints.FINE_TUNING);
            if (!response.IsSuccessStatusCode) {
                string message = await response.Content.ReadAsStringAsync();
                _logger.Error($"[FineTuning.ListJobs] The HTTP request failed with status code: {message}.");
                throw new HttpRequestException($"The HTTP request failed with status code: {message}.");
            }


            var responseContent = await response.Content.ReadAsStringAsync();
            JsonDocument jsonDoc = JsonDocument.Parse(responseContent);
            JsonElement dataElement = jsonDoc.RootElement.GetProperty("data");

            FineTuningObject[]? retrievedTasks = JsonSerializer.Deserialize<FineTuningObject[]>(dataElement.ToString(), _serializerOptions);

            return retrievedTasks;
        }
    }

    /// <summary>
    /// Lists events associated with a specific fine-tuning job.
    /// </summary>
    /// <param name="jobID">The ID of the fine-tuning job.</param>
    /// <returns>An array of <see cref="FineTuningEvent"/> representing the list of events for the specified job.</returns>
    public async Task<FineTuningEvent[]?> ListEvents(string jobID) {
        using (var client = new HttpClient()) {

            _logger.Info("[FineTuning.ListEvents] new request.");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apikey);

            string fullEndpoint = $"{Endpoints.FINE_TUNING}/{jobID}/events";

            if (!Uri.IsWellFormedUriString(fullEndpoint, UriKind.Absolute)) {
                _logger.Error("[FineTuning.ListEvents] The jobID format is invalid");
                throw new ArgumentException("The jobID format is invalid");
            }

            var response = await client.GetAsync(fullEndpoint);
            if (!response.IsSuccessStatusCode) {
                string message = await response.Content.ReadAsStringAsync();
                _logger.Error($"[FineTuning.ListEvents] The HTTP request failed with status code: {message}.");
                throw new HttpRequestException($"The HTTP request failed with status code: {message}.");
            }


            var responseContent = await response.Content.ReadAsStringAsync();
            JsonDocument jsonDoc = JsonDocument.Parse(responseContent);
            JsonElement dataElement = jsonDoc.RootElement.GetProperty("data");

            FineTuningEvent[]? retrievedTasks = JsonSerializer.Deserialize<FineTuningEvent[]>(dataElement.ToString(), _serializerOptions);

            return retrievedTasks;
        }
    }

    /// <summary>
    /// Retrieves details of a specific fine-tuning job.
    /// </summary>
    /// <param name="jobID">The ID of the fine-tuning job.</param>
    /// <returns>A <see cref="FineTuningObject"/> representing the details of the specified fine-tuning job.</returns>
    public async Task<FineTuningObject?> Retrieve(string jobID) {
        using (var client = new HttpClient()) {

            _logger.Info("[FineTuning.Retrieve] new request.");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apikey);

            string fullEndpoint = $"{Endpoints.FINE_TUNING}/{jobID}";

            if (!Uri.IsWellFormedUriString(fullEndpoint, UriKind.Absolute)) {
                _logger.Error("[FineTuning.Retrieve] The jobID format is invalid");
                throw new ArgumentException("The jobID format is invalid");
            }

            var response = await client.GetAsync(fullEndpoint);
            if (!response.IsSuccessStatusCode) {
                string message = await response.Content.ReadAsStringAsync();
                _logger.Error($"[FineTuning.Retrieve] The HTTP request failed with status code: {message}.");
                throw new HttpRequestException($"The HTTP request failed with status code: {message}.");
            }


            var responseContent = await response.Content.ReadAsStringAsync();
            FineTuningObject? retrievedTasks = JsonSerializer.Deserialize<FineTuningObject>(responseContent, _serializerOptions);

            return retrievedTasks;
        }
    }

    /// <summary>
    /// Cancels a specific fine-tuning job.
    /// </summary>
    /// <param name="jobID">The ID of the fine-tuning job to be canceled.</param>
    /// <returns>A <see cref="FineTuningObject"/> representing the canceled fine-tuning job.</returns>
    public async Task<FineTuningObject?> Cancel(string jobID) {
        using (var client = new HttpClient()) {

            _logger.Info("[FineTuning.Cancel] new request.");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apikey);

            string fullEndpoint = $"{Endpoints.FINE_TUNING}/{jobID}/cancel";

            if (!Uri.IsWellFormedUriString(fullEndpoint, UriKind.Absolute)) {
                _logger.Error("[FineTuning.Cancel] The jobID format is invalid");
                throw new ArgumentException("The jobID format is invalid");
            }

            var response = await client.PostAsync(fullEndpoint, null);
            if (!response.IsSuccessStatusCode) {
                string message = await response.Content.ReadAsStringAsync();
                _logger.Error($"[FineTuning.Cancel] The HTTP request failed with status code: {message}.");
                throw new HttpRequestException($"The HTTP request failed with status code: {message}.");
            }


            var responseContent = await response.Content.ReadAsStringAsync();
            FineTuningObject? retrievedTasks = JsonSerializer.Deserialize<FineTuningObject>(responseContent, _serializerOptions);

            return retrievedTasks;
        }
    }
}

