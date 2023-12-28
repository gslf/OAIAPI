using Microsoft.Extensions.Configuration;

namespace Promezio.OAIAPI.Test;
public abstract class Test {
    protected IConfiguration Configuration { get; set; }
    protected string _apikey;

    protected Test() {
        // Retrieve Api Key from user secrets
        Configuration = new ConfigurationBuilder()
            .AddUserSecrets<Test>()
            .Build();

        if (Configuration == null) {
            throw new NullReferenceException("Missing API Key, user secrets setup configuration instruction are in README.md"); 
        }

        _apikey = Configuration["OpenAIApiKey"] ?? throw new InvalidOperationException("API Key not found");
    }
}

