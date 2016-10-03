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
    }
}