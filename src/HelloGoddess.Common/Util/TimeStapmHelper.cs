using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloGoddess.Common.Util
{
    public class TimeStampHelper
    {
        public static long GetTodayTimeStampSeconds()
        {
            return DateTimeOffset.Parse(DateTime.Today.ToString()).ToUnixTimeSeconds();
        }

        public static long GetCurrentDateTimeStampSeconds()
        {
            return DateTimeOffset.Now.ToUnixTimeSeconds();
        }
    }
}
