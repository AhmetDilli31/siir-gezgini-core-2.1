using System.Collections.Generic;
using SairGezgini.Core.Entities;

namespace SiirGezgini.Repository
{
    public interface IPoetServices
    {
        IList<Poet> GetPoet(string letter, int pageIndex, int pageSize);

        PoemOfPoetItem GetPoetOfPoems(int poetId,int pageIndex, int pageSize);
    }
}
