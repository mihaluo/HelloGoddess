using System.Linq;
using System.Net;

namespace HelloGoddess.Common.Util
{
    public static class IpHelper
    {
        public static string GetIp(string hostName)
        {
            var dns = Dns.GetHostAddressesAsync(hostName).Result;
            var ipAddess = dns.FirstOrDefault();
            return ipAddess?.ToString();
        }
    }
}