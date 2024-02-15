# OAIAPI - Files Management Capability

## Namespace
`Promezio.OAIAPI.Capabilities.Files`

## Summary
The `Files` class provides functionality to send, delete and list files on OpenAI server.

## Examples

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

// File upload
FileObject? uploadResult = await api.Files.Upload("path/file.jsonl", new Purposes());

// List available files
FileObject[]? listResults = await api.Files.List();

// Retrieve information about a file
FileObject? retrieveResult = await api.Files.Retrieve("theFileID");

// Retrieve the file content
string? contentResult = await api.Files.Content("theFileID");

// File removal
bool deleteResult = await api.Files.Delete("theFileID");

```

## Documentation Links
#### [FileObject documentation](/api/Promezio.OAIAPI.Capabilities.Files.FileObject.html)
#### [Purposes documentation](/api/Promezio.OAIAPI.Capabilities.Files.Purposes.html)