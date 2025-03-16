using System.Text.Json.Serialization;

namespace LocalChat.Gitlab;

public class Proyect 
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }   
    [JsonPropertyName("description")]    
    public string? Description { get; set; }    
    [JsonPropertyName("web_url")]
    public string? WebUrl { get; set; }
    
}

