using System.Globalization;

namespace SiirGezgini.Shared
{
    public static class StringExtenisons
    {
        public static string ReplaceForUrl(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return value.Trim().ToLower(CultureInfo.CurrentCulture)
                    .Replace(".", "-")
                    .Replace(' ', '-')
                    .Replace('ş', 's')
                    .Replace('ç', 'c')
                    .Replace('ı', 'i')
                    .Replace('ğ', 'g')
                    .Replace('ü', 'u')
                    .Replace('ö', 'o')
                    .Replace("?", "")
                    .Replace("!", "")
                    .Replace("'", "")
                    .Replace('"', '-')
                    .Replace("--", string.Empty)
                    .Replace("---", string.Empty)
                    .Replace("/", "-")
                    .Replace(@"//", "-")
                    .Replace(@"\", "-")
                    .Replace("\\", "-")
                    .Replace(@"\\", "-")
                    .Replace("\\\\", string.Empty)
                    .Replace("\r", string.Empty)
                    .Replace("\n", string.Empty)
                    .Replace(@"'", string.Empty)
                    .Replace(@"(", string.Empty)
                    .Replace(@")", string.Empty)
                    .Replace(@"*", string.Empty)
                    .Replace(@"=", "-");
            }

            return "";
        }
    }
}
