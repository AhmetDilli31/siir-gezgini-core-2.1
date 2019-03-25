using System.Collections.Generic;
using SairGezgini.Core.Entities;

namespace SiirGezgini.Business.Interfaces
{
    public interface IPoetBusiness
    {
        List<Poet> GetPoet(string letter, int pageIndex, int pageSize);
    }
}
