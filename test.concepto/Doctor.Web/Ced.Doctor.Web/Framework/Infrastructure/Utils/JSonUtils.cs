using Framework.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Framework.Infrastructure.Utils
{
    public static class JSonUtils
    {

        /// <summary>
        /// Gets the data from json.
        /// </summary>
        /// <param name="JSonData">The j son data.</param>
        /// <returns></returns>
        /// TODO Edit XML Comment Template for GetDataFromJSon
        public static IEnumerable<T> GetDataFromJSon<T>(string JSonData) where T: class
        {

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,                
            };


            var stream = JSonData.ToStream();
            StreamReader reader = new StreamReader(stream);
            var locEntities = JsonConvert.DeserializeObject<ICollection<T>>(JSonData, settings);

            string valor = string.Empty;

            return locEntities.AsEnumerable<T>();
        }


        
    }
}
