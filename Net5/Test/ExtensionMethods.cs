using System.Security;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace Test
{
    public static class Extensions {
        public static T DeepClone<T>(this T obj)
        {
                        
            var aux = JsonSerializer.Serialize<T>(obj);
            
            return JsonSerializer.Deserialize<T>(aux);
           
        }

    }
}