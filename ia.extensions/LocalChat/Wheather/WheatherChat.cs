using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;

namespace LocalChat.Wheather;
public class WheatherChat 
{
    private readonly WeatherClient _weatherClient;
    private readonly ILogger<WheatherChat> _logger;
    private readonly ILoggerFactory _loggerFactory;

    public WheatherChat(WeatherClient weatherClient, ILogger<WheatherChat> logger, ILoggerFactory loggerFactory)
    {
        _weatherClient = weatherClient;
        _logger = logger;
        _loggerFactory = loggerFactory;
    }

    public async Task StartAsync()
    {

        _logger.LogInformation("Starting the chat");

        IChatClient client =
            new ChatClientBuilder(new OllamaChatClient(new Uri("http://localhost:11434/"), "llama3.2:1b"))            
            .UseFunctionInvocation()
            .UseLogging(_loggerFactory)
            .Build();

        var chatOptions = new ChatOptions
        {
            Tools = [AIFunctionFactory.Create(async (string location, string unit) =>
                {
                    // Call the WeatherClient to get the weather for the location
                    return await _weatherClient.GetCurrentWeatherAsync(location, unit);
                },
                "get_current_weather",
                "Get the current weather in a given location"),
                AIFunctionFactory.Create(() =>
                {
                    // return the time
                    return DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                },
                "get_current_date_time",
                "Get the current date time")
            ]
        };

        // System prompt to provide context
        List<ChatMessage> chatHistory = [new(ChatRole.System, """
        You are a hiking enthusiast who helps people discover fun hikes in their area. You are upbeat and friendly.
        """)];

        var location = Console.ReadLine();

        // Weather conversation relevant to the registered function
        chatHistory.Add(new ChatMessage(ChatRole.User,
            $"I live in {location} and I'm looking for a moderate intensity hike. What's the current weather like? "));
        Console.WriteLine($"{chatHistory.Last().Role} >>> {chatHistory.Last()}");

        var response = await client.GetResponseAsync(chatHistory, chatOptions);
        chatHistory.Add(new ChatMessage(ChatRole.Assistant, response.Message.Contents));
        Console.WriteLine($"{chatHistory.Last().Role} >>> {chatHistory.Last()}");

        // Answer for the time
        chatHistory.Add(new ChatMessage(ChatRole.User,
            $"What's time it's now? "));
        Console.WriteLine($"{chatHistory.Last().Role} >>> {chatHistory.Last()}");

        response = await client.GetResponseAsync(chatHistory, chatOptions);
        chatHistory.Add(new ChatMessage(ChatRole.Assistant, response.Message.Contents));
        Console.WriteLine($"{chatHistory.Last().Role} >>> {chatHistory.Last()}");
    }
}