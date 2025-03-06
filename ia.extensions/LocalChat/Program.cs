using LocalChat.Wheather;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


ServiceCollection services = new();
ConfigureServices(services);

var serviceProvider = services.BuildServiceProvider();
var chat = serviceProvider.GetService<WheatherChat>();
await chat!.StartAsync();
    
static void ConfigureServices(ServiceCollection services)
{
    // Add logging services to the builder
    services.AddLogging(b => b.AddConsole().SetMinimumLevel(LogLevel.Trace));
    services.AddSingleton<ILoggerFactory>(provider => LoggerFactory.Create(builder => builder.AddConsole()));
    services.AddSingleton<IMemoryCache, MemoryCache>();
    
    services.AddHttpClient<WeatherClient>();
    services.AddTransient<WheatherChat>();    
}



