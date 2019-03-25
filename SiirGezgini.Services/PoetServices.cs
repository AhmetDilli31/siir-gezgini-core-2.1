using System.Collections.Generic;
using SairGezgini.Core.Configuration;
using SairGezgini.Core.Entities;
using SiirGezgini.Shared;
using SiirGezgini.Shared.HttpRequestResults;

namespace SiirGezgini.Repository
{
    public class PoetServices : IPoetServices
    {
        private readonly IConfigurationManager _configurationManager;

        public PoetServices(IConfigurationManager configurationManager)
        {
            _configurationManager = configurationManager;
        }

        public IList<Poet> GetPoet(string letter, int pageIndex, int pageSize)
        {
            var serviceUrl = _configurationManager.Get("ApiEndPoints:GetPoetsByLetter")
                .Replace("{letter}", letter)
                .Replace("{pageIndex}", pageIndex.ToString())
                .Replace("{pageSize}", pageSize.ToString());

            HttpWebRequestPoetResult<IList<Poet>> data = HttpWebRequestHelper.GetDataWithResult<HttpWebRequestPoetResult<IList<Poet>>>(serviceUrl);
            if (data != null) return data.PoetResponseList;
            return new List<Poet>();
        }

        public PoemOfPoetItem GetPoetOfPoems(int poetId, int pageIndex, int pageSize)
        {
            var serviceUrl = _configurationManager.Get("ApiEndPoints:GetPoetsOfPoem")
                .Replace("{poetId}", poetId.ToString())
                .Replace("{pageIndex}", pageIndex.ToString())
                .Replace("{pageSize}", pageSize.ToString());

            var serviceResult = HttpWebRequestHelper.GetDataWithResult<HttpWebRequestBaseResult<PoemOfPoetItem>>(serviceUrl);

            if (serviceResult != null)
            {
                return serviceResult.Item;
            }

            return new PoemOfPoetItem
            {
                PoetName = "",
                PoetId = 0,
                PoemOfPoetResponse = new List<PoemOfPoetInformaton>(),
                TotalPageNumber = 0,
                TotalPoemNumber = 0
            };
        }
    }
}
