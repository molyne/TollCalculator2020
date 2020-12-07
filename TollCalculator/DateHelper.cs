using System;
using System.Collections.Generic;
using System.Text;

namespace TollCalculator
{
    public static class DateHelper
    {

        public static DateTime GetNextWeekday(DayOfWeek day)
        {
            DateTime start = DateTime.Now;
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd).Date.AddHours(8);
        }

        public static DateTime GetLastWeekday(DayOfWeek day)
        {
            DateTime lastWeekDay = DateTime.Now.AddDays(-1);
            while (lastWeekDay.DayOfWeek != day)
                lastWeekDay = lastWeekDay.AddDays(-1);

            return lastWeekDay.Date.AddHours(8);
        }
    }
}
