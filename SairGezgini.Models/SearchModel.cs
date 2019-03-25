namespace SairGezgini.Models
{
    public class SearchModel
    {
        public SearchModel()
        {
            PoemName = string.Empty;
            Title = string.Empty;
            Url = string.Empty;
        }
        public string PoemName { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
