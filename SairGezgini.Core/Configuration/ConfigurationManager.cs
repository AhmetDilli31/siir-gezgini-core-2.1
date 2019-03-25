using Microsoft.Extensions.Configuration;

namespace SairGezgini.Core.Configuration
{
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly IConfiguration _configuration;

        public ConfigurationManager()
        {
            _configuration = IoCResolver.Current.GetService<IConfiguration>();
        }

        public string Get(string key)
        {
            return _configuration[key];
        }
    }
}
