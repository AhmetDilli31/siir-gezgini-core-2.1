using System.Collections.Generic;
using SairGezgini.Core.Entities;
using SiirGezgini.Business.Interfaces;
using SiirGezgini.Repository;

namespace SiirGezgini.Business
{
    public class PoetOfPoemsBusiness : IPoetOfPoemsBusiness
    {
        private readonly IPoetServices _poetServices;

        public PoetOfPoemsBusiness(IPoetServices poetServices)
        {
            _poetServices = poetServices;
        }

        public IList<PoemOfPoetInformaton> GetPoetPoems(int poetId, int pageIndex, int pageSize)
        {
            var data = _poetServices.GetPoetOfPoems(poetId, pageIndex, pageSize);
            return data.PoemOfPoetResponse;
        }
    }
}
