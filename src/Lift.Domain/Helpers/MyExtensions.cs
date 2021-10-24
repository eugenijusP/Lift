using System;
using System.Collections.Generic;
using System.Linq;

namespace Lift.Domain.Helpers
{
    public static class MyExtensions
    {

        public static string NullSafeToString(this object obj) => obj != null ? obj.ToString() : string.Empty;

        public static string NullSafeToUpper(this object obj) => obj.NullSafeToString().ToUpper();

        public static string AddIfNotNullOrEmpty(this string original, string text, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                original = $"{original}{text}{value}";
            }
            return original;
        }

        public static string AddZeroes(this string original, int lenth)
        {

            while (original.Length < lenth)
            {
                original = $"0{original}";
            }
            return original;
        }

        public static DateTime SafeToDateTime(this object obj) =>
            obj != null && obj != DBNull.Value ? Convert.ToDateTime(obj) : DateTime.Now;

        public static DateTime SafeToTime(this object obj)
        {
            var tm = obj.NullSafeToString().Split(':');
            var hour = tm[0].SafeToInt32();
            if (hour == 24)
            {
                hour = 0;
            }
            var min = tm[1].SafeToInt32();
            var sec = tm[2].SafeToInt32();

            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, min, sec);
        }

        public static int SafeToInt32(this object obj)
        {
            var tmp = obj.NullSafeToString();
            var t = tmp.SafeToDouble();
            var rez = (int)t;
            return rez;
        }

        public static long SafeToInt64(this object obj)
        {
            var tmp = obj.NullSafeToString();
            _ = long.TryParse(tmp, out var rez);
            return rez;
        }

        public static double SafeToDouble(this object obj)
        {
            var tmp = obj.NullSafeToString();
            _ = double.TryParse(tmp, out var rez);
            return rez;
        }

        public static bool SafeToBool(this object obj)
        {
            var rez = false;
            try
            {
                rez = Convert.ToBoolean(obj);
            }
            catch
            {
                //If something happens return false
            }
            return rez;
        }

        public static decimal SafeToDecimal(this object obj)
        {
            var tmp = obj.NullSafeToString();
            _ = decimal.TryParse(tmp, out var rez);
            return rez;
        }

        public static double Round(this double obj, int decimalPlaces)
        {
            var res = Math.Round(obj, decimalPlaces, MidpointRounding.AwayFromZero);
            return res;
        }

        public static DateTime StringToDate(this string str) =>
            new DateTime(str.Substring(0, 4).SafeToInt32(),
                         str.Substring(4, 2).SafeToInt32(),
                         str.Substring(6, 2).SafeToInt32());

        public static DateTime StartOfWeek(this DateTime data)
        {
            while (data.DayOfWeek != DayOfWeek.Monday)
            {
                data = data.AddDays(-1);
            }
            data = new DateTime(data.Year, data.Month, data.Day, 0, 0, 0);
            return data;
        }

        public static DateTime EndOfWeek(this DateTime data)
        {
            while (data.DayOfWeek != DayOfWeek.Sunday)
            {
                data = data.AddDays(1);
            }
            data = new DateTime(data.Year, data.Month, data.Day, 23, 59, 59);
            return data;
        }

        public static DateTime StartOfWeekWithTime(this DateTime data)
        {
            while (data.DayOfWeek != DayOfWeek.Monday)
            {
                data = data.AddDays(-1);
            }
            data = data.Date;
            return data;
        }

        public static DateTime StartOfMonth(this DateTime data)
        {
            data = new DateTime(data.Year, data.Month, 1, 0, 0, 0);
            return data;
        }

        public static DateTime EndOfMonth(this DateTime data)
        {
            data = new DateTime(data.Year, data.Month, DateTime.DaysInMonth(data.Year, data.Month), 23, 59, 59);
            return data;
        }

        public static DateTime StartOfDay(this DateTime data)
        {
            data = new DateTime(data.Year, data.Month, data.Day, 0, 0, 0);
            return data;
        }

        public static DateTime EndOfDay(this DateTime data)
        {
            data = new DateTime(data.Year, data.Month, data.Day, 23, 59, 59);
            return data;
        }

        public static DateTime UnixToDateTime(this long? timeStamp)
        {
            var utc = DateTimeOffset.FromUnixTimeSeconds(timeStamp ?? 0).UtcDateTime;
            return TimeZoneInfo.ConvertTimeFromUtc(utc, TimeZoneInfo.Local);
        }

        public static List<T> Clone<T>(this List<T> listToClone) where T : ICloneable =>
            listToClone.Select(item => (T)item.Clone()).ToList();
    }
}
