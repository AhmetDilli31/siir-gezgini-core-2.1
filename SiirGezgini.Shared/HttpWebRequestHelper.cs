using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Principal;
using System.Text;
using Newtonsoft.Json;

namespace SiirGezgini.Shared
{
    public class HttpWebRequestHelper
    {
        private static string GetResponse(string url, string method, Dictionary<string, string> headers, object data = null)
        {
            try
            {
                var req = WebRequest.Create(url);
                req.Timeout = int.MaxValue;
                req.Method = method;
                req.ContentType = "application/json";
                req.UseDefaultCredentials = true;
                req.ImpersonationLevel = TokenImpersonationLevel.Delegation;

                if (headers != null)
                {
                    foreach (var item in headers)
                        req.Headers.Add(item.Key, item.Value);
                }

                if (data != null)
                {
                    var reqStream = req.GetRequestStream();

                    var json = JsonConvert.SerializeObject(data);
                    var buffer = Encoding.UTF8.GetBytes(json);

                    reqStream.Write(buffer, 0, buffer.Length);
                }

                var res = req.GetResponse();
                var stream = res.GetResponseStream();
                var sr = new StreamReader(stream ?? throw new InvalidOperationException());
                return sr.ReadToEnd();
            }
            catch
            {
                // ignored
            }

            return String.Empty;
        }

        public static T GetDataWithResult<T>(string url, Dictionary<string, string> headers = null) where T : new()
        {
            try
            {
                var response = GetResponse(url, "GET", headers);
                T result = HttpJsonConverter.ConvertIt<T>(response);

                return result;
            }
            catch (Exception)
            {
                // ignored
            }

            return new T();
        }

    }
}
