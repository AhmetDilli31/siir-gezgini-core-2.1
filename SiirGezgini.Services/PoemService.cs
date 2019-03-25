using SairGezgini.Core.Configuration;
using SairGezgini.Core.Entities;
using SiirGezgini.Shared;
using SiirGezgini.Shared.HttpRequestResults;

namespace SiirGezgini.Repository
{
    public class PoemService : IPoemService
    {
        private readonly IConfigurationManager _configurationManager;

        public PoemService(IConfigurationManager configurationManager)
        {
            _configurationManager = configurationManager;
        }

        public Poem GetPoem(int poemId)
        {
            var serviceUrl = _configurationManager.Get("ApiEndPoints:GetPoem").Replace("{poemId}", poemId.ToString());

            var serviceResult = HttpWebRequestHelper.GetDataWithResult<HttpWebRequestBaseResult<Poem>>(serviceUrl);

            if (serviceResult == null || !serviceResult.Success)
                return new Poem();

            return serviceResult.Item;
        }
    }
}
