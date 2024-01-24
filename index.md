# OAIAPI
Unofficial OpenAI API interface in C# 

---

Welcome to OAIAPI, the go-to C# library for interacting with OpenAI's API in a smooth and efficient way. Designed with simplicity and extendability in mind, this unofficial interface can be the right choice in many kind of projects.

## Getting Started
Getting started with OAIAPI is a breeze.

### STEP 1: Clone the repository
```powershell
git clone https://github.com/gslf/OAIAPI.git
```

### STEP 3: Reference the project
**For Visual Studio users:** right-click on the References or Dependencies in your main project, select Add Reference..., and then choose the project you just added.

**For Visual Studio Code and other editors users:** open yout project file and add a reference tag that look like this:
```xml
<ItemGroup>
  <ProjectReference Include="..\path\to\cloned\project.csproj" />
</ItemGroup>
```

### STEP 3: Manage the API key
In this project the API key is managed via user secret.
To add your API key to the project use this commands inside the project folder.

```powershell
dotnet user-secrets init
dotnet user-secrets set "apikey" "YOUR-OPEN-AI-API-KEY"
```

You can read more about user secret [here](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=windows).

You can read more about OpenAI API Key [here](https://help.openai.com/en/articles/4936850-where-do-i-find-my-api-key).

### STEP 4: Make Your First API Call
```csharp
// Retrieve the API Key
 Configuration = new ConfigurationBuilder()
    .AddUserSecrets<Test>()
    .Build();

if (Configuration == null) {
    throw new NullReferenceException("User Secrets configuration not found."); 
}

string myapikey = Configuration["OpenAIApiKey"] ?? throw new InvalidOperationException("API Key not found");


// Call the Chat API
OAIAPI api = new OAIAPI(myapikey);
Config config = new Config("gpt-3.5-turbo");
api.Chat.Init(config);

ChatResponse? result = await api.Chat.Dispatch("Hello from space: the final frontier.");
Console.WriteLine(result?.GetMessage());
```



---

##### [Gioele SL Fierro](https://gslf.it)

OAIAPI is an unofficial interface and is not endorsed by or affiliated with OpenAI. 
The project is sponsored by [Promezio](https://promezio.it).
