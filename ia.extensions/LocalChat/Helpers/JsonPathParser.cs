using System.Text.Json;

namespace LocalChat.Helpers;
public class JsonPathParser
{
    public static Dictionary<string, string> ParseJsonToPaths(string json)
    {
        if (string.IsNullOrEmpty(json))
        {
            return new Dictionary<string, string>{ { "Value", "" } };
        }

        try
        {
            var result = new Dictionary<string, string>();
            using JsonDocument document = JsonDocument.Parse(json);
            ProcessElement(document.RootElement, "", result);
            return result;
        }
        catch (JsonException)
        {
            return new Dictionary<string, string>{ { "Value", json } };
        }
        
    }

    private static void ProcessElement(JsonElement element, string currentPath, Dictionary<string, string> result)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                foreach (JsonProperty prop in element.EnumerateObject())
                {
                    string newPath = string.IsNullOrEmpty(currentPath) 
                        ? prop.Name 
                        : $"{currentPath}.{prop.Name}";
                    ProcessElement(prop.Value, newPath, result);
                }
                break;
                
            case JsonValueKind.Array:
                int index = 0;
                foreach (JsonElement item in element.EnumerateArray())
                {
                    string newPath = $"{currentPath}[{index}]";
                    ProcessElement(item, newPath, result);
                    index++;
                }
                break;
                
            default:
                result[currentPath] = GetValueAsString(element);
                break;
        }
    }

    private static string GetValueAsString(JsonElement element)
    {
        return element.ValueKind switch
        {
            JsonValueKind.String => element.GetString()!,
            JsonValueKind.Number => element.GetRawText(),
            JsonValueKind.True => "true",
            JsonValueKind.False => "false",
            JsonValueKind.Null => "null",
            _ => element.ToString()
        };
    }
}