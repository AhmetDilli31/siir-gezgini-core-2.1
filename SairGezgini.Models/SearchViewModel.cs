using System.Collections.Generic;

namespace SairGezgini.Models
{
   public class SearchViewModel
    {
        public SearchViewModel()
        {
            SearchModels = new List<SearchModel>();
        }
        public List<SearchModel> SearchModels { get; set; }
        public string Key { get; set; }
        public int Count { get; set; }
    }
}
