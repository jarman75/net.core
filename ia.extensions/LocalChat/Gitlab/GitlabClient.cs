using System.Text;
using System.Text.Json;
using LocalChat.Helpers;
using Microsoft.Extensions.Logging;

namespace LocalChat.Gitlab;

public class GitlabClient
{
    private readonly HttpClient _httpClient;    

    public GitlabClient(HttpClient httpClient)
    {
        _httpClient = httpClient;                
    }

    public async Task<List<string>> GetProyectsAsync(string search) 
    {        
        Console.WriteLine($"Getting projects for search '{search}'");
        
        var url = $"api/v4/projects?pagination=keyset&per_page=200&order_by=id&sort=desc&simple=true&archived=false&search={search}";        
        var response = await _httpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode)
        {
            return [];
        }

        var content = await response.Content.ReadAsStringAsync();
        if (content is null)
        {
            return [];
        }
        
        var data = JsonSerializer.Deserialize<List<Proyect>>(content);   
        if (data is null || data.Count == 0)
        {
            return [];
        }

        return [.. data.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).Select(s => $"1. Name: {s.Name}. Id: {s.Id}" )];
        
    }

    public async Task<List<string>> GetProyectVariablesAsync(int projectId, string? key = null)
    {
        Console.WriteLine($"Getting variables for project '{projectId}' and key '{key}'");

        var url = $"api/v4/projects/{projectId}/variables";
        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode) 
        {
            return [];
        }
        
        var content = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<List<ProyectVariable>>(content);
        if (data is null || data.Count == 0)
        {
            return [];
        }
                
        List<string> result =  key is null ? [.. data.Select(s => s.Key).Distinct()] :
         [.. data.Where(v => (v.EnvironmentScope == "production" || v.EnvironmentScope == "*") && v.Key.Contains(key, StringComparison.OrdinalIgnoreCase)).Select(s => s.Key).Distinct()];
                

        return result;
        
        
        
    }

    public async Task<string> GetProyectValueVariableAsync(int projectId, string variable, string environment)
    {
        Console.WriteLine($"Getting variable value for project '{projectId}' and variable '{variable}' and environment '{environment}'");

        var url = $"api/v4/projects/{projectId}/variables";
        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode) 
        {
            return "";
        }
        
        var content = await response.Content.ReadAsStringAsync();        
        
        var data = JsonSerializer.Deserialize<List<ProyectVariable>>(content);
        if (data is null || data.Count == 0)
        {
            return "";
        }                
        
        var result = data.Where(v => v.EnvironmentScope.Equals(environment, StringComparison.OrdinalIgnoreCase) && v.Key.Equals(variable, StringComparison.OrdinalIgnoreCase)).FirstOrDefault()?.Value ?? "";                
        

        var dic = JsonPathParser.ParseJsonToPaths(result);
        var fmtResult = new StringBuilder();
        foreach (var item in dic)
        {
            fmtResult.AppendLine($"{item.Key}: {item.Value}");
        }        
        return fmtResult.ToString();
        
    }
}
