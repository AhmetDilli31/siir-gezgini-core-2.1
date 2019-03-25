namespace SiirGezgini.Shared.HttpRequestResults
{
    public class HttpWebRequestBaseResult<T>
    {
        public bool Success { get; set; }
        public T Item { get; set; }
    }
}
