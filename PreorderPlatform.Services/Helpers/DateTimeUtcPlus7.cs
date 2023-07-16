using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.Helpers
{
    public static class DateTimeUtcPlus7
    {
        private static readonly TimeZoneInfo UtcPlus7TimeZone;

        static DateTimeUtcPlus7()
        {
            // Note: The time zone ID can vary depending on the system. 
            // This is for Windows. For Unix-based systems (like Linux or macOS), 
            // you might need to use "Asia/Bangkok" or "Asia/Ho_Chi_Minh" instead.
            UtcPlus7TimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        }

        public static DateTime Now
        {
            get
            {
                var nowUtc = DateTime.UtcNow;
                return TimeZoneInfo.ConvertTimeFromUtc(nowUtc, UtcPlus7TimeZone);
            }
        }
    }
}
