using System.Collections.Generic;
using System.Linq;
using SairGezgini.Core.Entities;
using SiirGezgini.Business.Interfaces;
using SiirGezgini.Repository;

namespace SiirGezgini.Business
{
    public class PoetBusiness : IPoetBusiness
    {
        private readonly IPoetServices _poetServices;

        public PoetBusiness(IPoetServices poetServices)
        {
            _poetServices = poetServices;
        }

        public List<Poet> GetPoet(string letter, int pageIndex, int pageSize)
        {
            List<Poet> poets = _poetServices.GetPoet(letter,pageIndex,pageSize).ToList();
            return poets;
        }
    }
}
