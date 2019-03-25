using Newtonsoft.Json;

namespace SiirGezgini.Shared
{
    public class HttpJsonConverter
    {
        public static T ConvertIt<T>(string JsonString)
        {
           return JsonConvert.DeserializeObject<T>(JsonString);
        }
    }
}
