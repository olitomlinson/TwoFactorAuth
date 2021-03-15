using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoFactorAuth.Domain.Tests.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime Fathom(string time, string date)
        {
            DateTime outtime = DateTime.UtcNow;

            if (date == "today")
            {
                var now = DateTime.UtcNow;
                outtime = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            }

            if (time == "noon")
            {
                outtime = outtime.AddHours(12);
            }

            return outtime;
        }
    }
}
