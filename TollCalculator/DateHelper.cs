using System;
using System.Collections.Generic;
using System.Text;

namespace TollCalculator
{
    public static class DateHelper
    {
        public static bool IsHoliday(DateTime date)
        {
            return IsChristmasEve(date) || IsNewYearsDay(date) || IsNewYearsEve(date)
                   || IsThirteenEvening(date) || IsThirteenDayChristmas(date) || IsChristmasDay(date)
                   || IsAllSaintsDay(date) || IsBoxingDay(date) || IsFirstMay(date) || IsAllSaintsEve(date)
                   || IsAscensionDay(date) || IsDayBeforeWhitSunday(date) || IsWhitSunday(date) || IsAllSaintsEve(date)
                   || IsEasterMonday(date) || IsEasterSunday(date) || IsGoodFriday(date) || IsMidsummerDay(date)
                   || IsMidsummerEve(date) || IsSwedishNationalDay(date);
        }

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

        public static bool IsChristmasEve(DateTime date)
        {
            return date.Date == GetChristmasEve(date).Date;
        }

        public static bool IsChristmasDay(DateTime date)
        {
            return date.Date == GetChristmasDay(date).Date;
        }

        public static bool IsBoxingDay(DateTime date)
        {
            return date.Date == GetBoxingDay(date).Date;
        }

        public static bool IsNewYearsDay(DateTime date)
        {
            return date.Date == GetNewYearsDay(date).Date;
        }

        public static bool IsNewYearsEve(DateTime date)
        {
            return date.Date == GetNewYearsEve(date).Date;
        }

        public static bool IsThirteenEvening(DateTime date)
        {
            return date.Date == GetThirteenEvening(date).Date;
        }

        public static bool IsThirteenDayChristmas(DateTime date)
        {
            return date.Date == GetThirteenDayChristmas(date).Date;
        }

        public static bool IsEasterSunday(DateTime date)
        {
            return date.Date == GetEasterSunday(date.Year).Date;
        }

        public static bool IsGoodFriday(DateTime date)
        {
            return date.Date == GetGoodFriday().Date;
        }

        public static bool IsEasterMonday(DateTime date)
        {
            return date.Date == GetEasterMonday().Date;
        }

        public static bool IsMidsummerDay(DateTime dt)
        {
            return dt.DayOfWeek == DayOfWeek.Saturday
                   && dt.Month == 6 && dt.Day >= 20 && dt.Day <= 26;
        }

        public static bool IsMidsummerEve(DateTime dt)
        {
            return dt.DayOfWeek == DayOfWeek.Friday
                   && dt.Month == 6 && dt.Day >= 19 && dt.Day <= 25;
        }

        public static bool IsValborg(DateTime date)
        {
            return date.Date == GetValborg(date).Date;
        }

        public static bool IsFirstMay(DateTime date)
        {
            return date.Date == GetFirstMay(date).Date;
        }

        public static bool IsSwedishNationalDay(DateTime date)
        {
            return date.Date == GetSwedishNationalDay(date).Date;
        }

        public static bool IsWhitSunday(DateTime date)
        {
            return date.Date == GetWhitSunday(date).Date;
        }

        public static bool IsDayBeforeWhitSunday(DateTime date)
        {
            return date.Date == GetDayBeforeWhitSunday(date).Date;
        }

        public static bool IsAscensionDay(DateTime date)
        {
            return date.Date == GetAscensionDay(date).Date;
        }

        public static bool IsAllSaintsDay(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday
                   && date.Month == 10 && date.Day >= 31 || date.Month == 11 && date.Day >= 1 && date.Day <= 6;
        }

        public static bool IsAllSaintsEve(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Friday
                   && date.Month == 10 && date.Day >= 30 || date.Month == 11 && date.Day >= 1 && date.Day <= 5;
        }

        public static bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        public static DateTime GetChristmasEve(DateTime date)
        {
            DateTime christmasEve = new DateTime(date.Year, 12, 24);

            return christmasEve.Date.AddHours(8);
        }

        public static DateTime GetChristmasDay(DateTime date)
        {
            DateTime christmasDay = new DateTime(date.Year, 12, 25);

            return christmasDay.Date.AddHours(8);
        }

        public static DateTime GetBoxingDay(DateTime date)
        {
            DateTime boxingDay = new DateTime(date.Year, 12, 26);

            return boxingDay.Date.AddHours(8);
        }

        public static DateTime GetNewYearsDay(DateTime date)
        {
            DateTime newYearsDay = new DateTime(date.Year, 01, 01);

            return newYearsDay.Date.AddHours(8);
        }

        public static DateTime GetNewYearsEve(DateTime date)
        {
            DateTime newYearsEve = new DateTime(date.Year, 01, 01);

            return newYearsEve.Date.AddHours(8);
        }

        public static DateTime GetThirteenEvening(DateTime date)
        {
            DateTime thirteenEvening = new DateTime(date.Year, 01, 05);

            return thirteenEvening.Date.AddHours(8);
        }

        public static DateTime GetThirteenDayChristmas(DateTime date)
        {
            DateTime thirteenDayChristmas = new DateTime(date.Year, 01, 06);

            return thirteenDayChristmas.Date.AddHours(8);
        }

        public static DateTime GetValborg(DateTime date)
        {
            DateTime valborg = new DateTime(date.Year, 04, 30);

            return valborg.Date.AddHours(8);
        }

        public static DateTime GetFirstMay(DateTime date)
        {
            DateTime firstMay = new DateTime(date.Year, 05, 01);

            return firstMay.Date.AddHours(8);
        }

        public static DateTime GetSwedishNationalDay(DateTime date)
        {
            DateTime nationalDay = new DateTime(date.Year, 06, 06);

            return nationalDay.Date.AddHours(8);
        }

        public static DateTime GetWhitSunday(DateTime date)
        {
            return GetEasterSunday(date.Year).AddDays(49);
        }

        public static DateTime GetDayBeforeWhitSunday(DateTime date)
        {
            return GetEasterSunday(date.Year).AddDays(48);
        }

        public static DateTime GetAscensionDay(DateTime date)
        {
            return GetEasterSunday(date.Year).AddDays(39);
        }

        public static DateTime GetEasterSunday(int year)
        {
            int day = 0;
            int month = 0;

            int g = year % 19;
            int c = year / 100;
            int h = (c - (int)(c / 4) - (int)((8 * c + 13) / 25) + 19 * g + 15) % 30;
            int i = h - (int)(h / 28) * (1 - (int)(h / 28) * (int)(29 / (h + 1)) * (int)((21 - g) / 11));

            day = i - ((year + (int)(year / 4) + i + 2 - c + (int)(c / 4)) % 7) + 28;
            month = 3;

            if (day > 31)
            {
                month++;
                day -= 31;
            }

            return new DateTime(year, month, day).Date.AddHours(8);
        }

        public static DateTime GetGoodFriday()
        {
            return GetEasterSunday(DateTime.Now.Year).AddDays(-2).Date.AddHours(8);
        }

        public static DateTime GetEasterMonday()
        {
            return GetEasterSunday(DateTime.Now.Year).AddDays(1).Date.AddHours(8);
        }
    }
}
