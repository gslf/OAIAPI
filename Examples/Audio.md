# OAIAPI - Chat Capability

## Namespace
`Promezio.OAIAPI.Capabilities.Audio`

## Summary
The Audio class represents the Audio capability in OpenAI’s API. 

## Example SPEECH
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

bool result = await api.Audio.Speech(
            model: new Models(AvailableModels.TTS_1),
            text: "Hello Testers!", 
            outputFile: "path/to/file.aac",
            voice: new Voices(AvailableVoices.FABLE),
            responseFormat: new SpeechResponseFormats(AvailableSpeechFormats.AAC),
            speed: 2.5);
```

## Example TRANSCRIPTION
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

TranscriptionObject? result = await api.Audio.Transcription(
    audioURL: "path/to/file.mp3",
    prompt: "",
    temperature: 0);
```

## Example SPEECH
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

string result = await api.Audio.Translation(
            audioURL: "path/to/file.mp3",
            prompt: "",
            temperature: 0);
```
