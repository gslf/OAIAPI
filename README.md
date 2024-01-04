# OAIAPI
Unofficial OpenAI API interface in C# 

---


## API key management
In this project the API key is managed via user secret.
To add your API key to the project use this commands inside the project folder.

```powershell
dotnet user-secrets init
dotnet user-secrets set "apikey" "YOUR-OPEN-AI-API-KEY"
```

You can read more about user secrete [here](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=windows).
You can read more about OpenAI API Key [here](https://help.openai.com/en/articles/4936850-where-do-i-find-my-api-key).

#### Available API
- Chat Completion ([DOCS](Docs/Chat.md))

#### API integration in progress
- Speech
- Transcription




---

[Gioele SL Fierro](https://gslf.it)

Sponsored by [Promezio](https://promezio.it).
