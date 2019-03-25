namespace SiirGezgini.Shared.HttpRequestResults
{
   public class HttpWebRequestPoetResult<T>
    {
        public T PoetResponseList { get; set; }
        public int TotalPoetNumber { get; set; }
        public int TotalPageNumber { get; set; }
    }
}
