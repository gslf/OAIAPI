# OAIAPI - Fine Tuning Capability

## Namespace
`Promezio.OAIAPI.Capabilities.FineTuning`

## Summary
The `FineTuning` class provides functionality to create and manage a fine tuning job with OpenAI models.

## Examples

#### Fine tuning methods
```csharp
// Retrieve the API Key
 Configuration = new ConfigurationBuilder()
    .AddUserSecrets<Test>()
    .Build();

if (Configuration == null) {
    throw new NullReferenceException("User Secrets configuration not found."); 
}

string myapikey = Configuration["OpenAIApiKey"] ?? throw new InvalidOperationException("API Key not found");


// Init OAIAPI Class
OAIAPI api = new OAIAPI(myapikey);

// Create a fine tuning job
FineTuningObject? fineTuningResult = await api.FineTuning.Create(
            "theTrainigFileID",
            new Models(),
            batch_size: 5, //optional
            learning_rate_multiplier: 2, //optional
            n_epochs: 10, //optional
            suffix: "test", //optional
            validationFileID: "theValidationFileID");

// Cancel a fine tuning job
FineTuningObject? fineTuningCancelResult = await api.FineTuning.Cancel("theFineTuningID");

// List all fine tuning jobs
FineTuningObject[]? fineTuningList = await api.FineTuning.ListJobs();

// Retrieve a fine tunning job
FineTuningObject? fineTuningRetrieveResult = await api.FineTuning.Retrieve(fineTuningResult.Id);

// Retrieve all events of a fine tuning job
FineTuningEvent[]? fineTuningEvents = await api.FineTuning.ListEvents(fineTuningResult.Id);
```

#### Upload & tune
```csharp
// Retrieve the API Key
 Configuration = new ConfigurationBuilder()
    .AddUserSecrets<Test>()
    .Build();

if (Configuration == null) {
    throw new NullReferenceException("User Secrets configuration not found."); 
}

string myapikey = Configuration["OpenAIApiKey"] ?? throw new InvalidOperationException("API Key not found");


// Init OAIAPI Class
OAIAPI api = new OAIAPI(myapikey);

// Upload a file
FileObject? uploadResult = await api.Files.Upload("/path/to/tuningFile.jsonl", Purposes.FINE_TUNE);

if( ! string.IsNullOrEmpty(uploadResult.Id) ){

    // Create a fine tuning job
    FineTuningObject? fineTuningResult = await api.FineTuning.Create(
                uploadResult.Id,
                new Models());
}
```

## Documentation Links
#### [FineTuningObject](/api/Promezio.OAIAPI.Capabilities.FineTuning.FineTuningObject.html)
#### [Create job parameters](/api/Promezio.OAIAPI.Capabilities.FineTuning.FineTuning.html#parameters-2)
