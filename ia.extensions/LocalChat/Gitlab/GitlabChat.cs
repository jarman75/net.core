using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LocalChat.Gitlab;

public class GitlabChat
{
    private readonly GitlabClient _gitlabClient;
    private readonly IOptions<OllamaOptions> _ollamaOptions;
     private readonly ILogger<GitlabChat> _logger;
    private readonly ILoggerFactory _loggerFactory;

    public GitlabChat(GitlabClient gitlabClient, ILogger<GitlabChat> logger, ILoggerFactory loggerFactory, IOptions<OllamaOptions> ollamaOptions)
    {
        _gitlabClient = gitlabClient;
        _logger = logger;
        _loggerFactory = loggerFactory;
        _ollamaOptions = ollamaOptions;
    }

    public async Task StartAsync()
    {
        _logger.LogDebug("Starting the chat");
        
        IChatClient client =
            new ChatClientBuilder(new OllamaChatClient(new Uri(_ollamaOptions.Value.BaseUrl), _ollamaOptions.Value.Model))
            .UseFunctionInvocation()
            .UseLogging(_loggerFactory)
            .Build();

        var chatOptions = new ChatOptions
        {
            Tools = [AIFunctionFactory.Create(async (string search) =>
                {
                    // Call the GitlabClient to get the projects for the search
                    if (string.IsNullOrEmpty(search))
                    {
                        return [];
                    }
                    return await _gitlabClient.GetProyectsAsync(search);
                },
                "get_projects",
                "Get the projects from Gitlab"),
                AIFunctionFactory.Create(async (int projectId, string? key = null) =>
                {
                    // Call the GitlabClient to get the variables for the project
                    return await _gitlabClient.GetProyectVariablesAsync(projectId, key);
                },
                "get_project_variables",
                "Get the variables from a project in Gitlab"),
                AIFunctionFactory.Create(async (int projectId, string variable, string? environment = null) =>
                {                    
                    // Check if projectId is provided
                    if (projectId == 0)
                    {
                        return "Please provide the project ID.";
                    }
                    //Check if variable is provided
                    if (string.IsNullOrEmpty(variable))
                    {
                        return "Please provide the variable name.";
                    }
                    return await _gitlabClient.GetProyectValueVariableAsync(projectId, variable, environment ?? "production");
                },
                "get_project_variable_values",
                "Get the values of a variable from a project in Gitlab")
            ]
        };

        // System prompt to provide context
        //List<ChatMessage> chatHistory = [new(ChatRole.System, """
        //You are a developer who helps people find projects in Gitlab.
        //""")];
        
        List<ChatMessage> chatHistory = [new(ChatRole.System, """
        You are a highly skilled developer assistant specialized in helping users interact with Gitlab. 
        You can perform the following tasks:
        1. Retrieve a list of projects based on a search query. Always include the project ID, web URL, and name in the response.
        2. Get the variables for a specific project. If the project ID is not provided, ask the user for it.
        3. Get the value of a specific variable from a project in a given environment.
        
        Please provide clear and concise responses to user queries, and ask for any additional information if needed.
        """)];
        
        

        while(true)
        {
            var userRequest = Console.ReadLine();
            chatHistory.Add(new ChatMessage(ChatRole.User, userRequest));
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{chatHistory.Last().Role} >>> {chatHistory.Last()}");

            await foreach (var update in client.GetStreamingResponseAsync(chatHistory, chatOptions))
            {
                chatHistory.Add(new ChatMessage(ChatRole.Assistant, update.Contents));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{update.Role} >>> {update}"); 
            }
        }
        
        
    }
}