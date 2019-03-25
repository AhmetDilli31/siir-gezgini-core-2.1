using System.Collections.Generic;
using SairGezgini.Models;

namespace SiirGezgini.Business.Interfaces
{
    public interface ISearchBusiness
    {
        List<SearchModel> Search(string value);
    }
}
