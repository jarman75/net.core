using LocalChat;
using LocalChat.Gitlab;
using LocalChat.Helpers;
using LocalChat.Wheather;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;

ServiceCollection services = new();
ConfigureServices(services);

var serviceProvider = services.BuildServiceProvider();
var chatGitlab = serviceProvider.GetService<GitlabChat>();
await chatGitlab!.StartAsync();

static void ConfigureServices(ServiceCollection services)
{
    // Load configuration from appsettings.json
    var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddUserSecrets<Program>()
        .Build();

    services.AddSingleton<IConfiguration>(configuration);

    // Add logging services to the builder
    services.AddLogging();
    services.AddSingleton<ILoggerFactory>(provider => LoggerFactory.Create(builder => builder.AddConsole()));
    services.AddSingleton<IMemoryCache, MemoryCache>();
    

    var gitlabOptions = configuration.GetSection("Gitlab").Get<GitlabOptions>() ?? throw new ArgumentNullException("Gitlab options not found in appsettings.json");
    //gitlab token from user secrets
    var gitlabToken = configuration["GitlabSmsDataToken"] 
        ?? throw new ArgumentNullException("Secret key 'GitlabSmsDataToken' not found in user serets. Please add it to your user secrets [dotnet user-secrets set 'GitlabSmsDataToken' 'your-gitlab-token'].");    
    
    services.AddHttpClient<GitlabClient> (options => 
    {
        options.BaseAddress = new Uri(gitlabOptions.BaseUrl);
        options.DefaultRequestHeaders.Add(gitlabOptions.PrivateTokenName, gitlabToken);
    });
    
    //Ollama
    services.Configure<OllamaOptions>(configuration.GetSection("Ollama"));
    services.AddTransient<GitlabChat>();
}



