using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace HelloGoddess.Common.Util
{
    public static class HttpHelper
    {
        public static T DoGet<T>(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string result = httpClient.GetStringAsync(url).Result;
                return result.ToObj<T>();
            }
        }
        public static T DoPost<T>(string url, Dictionary<string, string> data)
        {
            HttpContent httpContent = new FormUrlEncodedContent(data.ToArray());
            using (HttpClient httpClient = new HttpClient())
            {
                var rsp = httpClient.PostAsync(url, httpContent).Result;
                string result = rsp.Content.ReadAsStringAsync().Result;
                return result.ToObj<T>();
            }
        }
        public static T DoPost<T>(string url, string data)
        {
            HttpContent httpContent = new StringContent(data);
            using (HttpClient httpClient = new HttpClient())
            {
                var rsp = httpClient.PostAsync(url, httpContent).Result;
                string result = rsp.Content.ReadAsStringAsync().Result;
                return result.ToObj<T>();
            }
        }
    }
}