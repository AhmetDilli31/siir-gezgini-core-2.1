using System.Collections.Generic;
using SairGezgini.Core.Entities;

namespace SiirGezgini.Business.Interfaces
{
    public interface IPoetOfPoemsBusiness
    {
        IList<PoemOfPoetInformaton> GetPoetPoems(int poetId, int pageIndex, int pageSize);
    }
}
