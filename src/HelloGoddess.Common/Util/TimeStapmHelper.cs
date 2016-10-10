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
            var now = DateTimeOffset.Now;
            var datetimeNowSeconds = now.ToUnixTimeSeconds();
            var seconds = now.Hour * 60 * 60 + now.Minute * 60 + now.Second + now.Millisecond / 1000;

            if (now.Hour >= 16)
            {
                return datetimeNowSeconds - seconds + 57600;//centos docker下时间少16个小时
            }

            return datetimeNowSeconds - seconds - 8 * 60 * 60; // 
        }

        public static long GetCurrentDateTimeStampSeconds()
        {
            return DateTimeOffset.Now.ToUnixTimeSeconds();
        }
    }
}
