using System.Text.Json.Serialization;

namespace LocalChat.Gitlab;

public class ProyectVariable
{
    [JsonPropertyName("variable_type")]
    public required string VariableType { get; set; }
    [JsonPropertyName("key")]
    public required string Key { get; set; }
    [JsonPropertyName("value")]
    public string? Value { get; set; }
    [JsonPropertyName("protected")]
    public bool Protected { get; set; }
    [JsonPropertyName("masked")]
    public bool Masked { get; set; }
    [JsonPropertyName("raw")]
    public bool Raw { get; set; }
    [JsonPropertyName("environment_scope")]
    public required string EnvironmentScope { get; set; }
}

