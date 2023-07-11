using System.Reflection;

namespace Dependences.Api.Services;

public interface IExampleService
{
    bool IsEnabled();
}
public interface IExampleService2
{
    bool IsBroken();
}

public class ExampleService : IExampleService
{
    public bool IsEnabled()
    {
        return true;
    }
}
public class ExampleService2 : IExampleService2
{
    public bool IsBroken()
    {
        return true;
    }
}

public static class ExampleServiceExtensions
{
    public static IServiceCollection AddExampleServices(this IServiceCollection services)
    {
        //add services from assambly
        var assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType)
            .Where(t => t.GetInterfaces().Any(i => i.Name == $"I{t.Name}"))
            .ToList();
        
        foreach (var type in types)
        {
            var interfaceType = type.GetInterfaces().First(i => i.Name == $"I{type.Name}");
            services.AddSingleton(interfaceType, type);
        }        
        
        return services;
    }
}
