using System;

namespace NNS.GeneralHelpers
{
    public static class DayExtensions
    {
        /// <summary>
        /// Gets a DateTime representing the first day in the current month
        /// </summary>
        /// <param name="current">The current date</param>
        /// <returns></returns>
        public static DateTime First(this DateTime current)
        {
            DateTime first = current.AddDays(1 - current.Day);
            return first;
        }

        /// <summary>
        /// หาวันแรกของเดือนจากวันที่ที่กำหนด
        /// </summary>
        /// <param name="current">วันที่ที่กำหนด</param>
        /// <returns></returns>
        public static DateTime StartOfTheMonth(this DateTime current)
        {
            return current.First();
        }

        /// <summary>
        /// หาวันแรกของปี
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static DateTime StartOfTheYear(this DateTime current)
        {
            DateTime dt = current.AddMonths(1 - current.Month);
            return dt.First();
        }

        /// <summary>
        /// Gets a DateTime representing the first specified day in the current month
        /// </summary>
        /// <param name="current">The current day</param>
        /// <param name="dayOfWeek">The current day of week</param>
        /// <returns></returns>
        public static DateTime First(this DateTime current, DayOfWeek dayOfWeek)
        {
            DateTime first = current.First();

            if (first.DayOfWeek != dayOfWeek)
            {
                first = first.Next(dayOfWeek);
            }

            return first;
        }

        /// <summary>
        /// Gets a DateTime representing the last day in the current month
        /// </summary>
        /// <param name="current">The current date</param>
        /// <returns></returns>
        public static DateTime Last(this DateTime current)
        {
            int daysInMonth = DateTime.DaysInMonth(current.Year, current.Month);

            DateTime last = current.First().AddDays(daysInMonth - 1);
            return last;
        }

        /// <summary>
        /// หาวันที่สิ้นเดือนจากวันที่ที่กำหนด
        /// </summary>
        /// <param name="current">วันที่ที่กำหนด</param>
        /// <returns></returns>
        public static DateTime EndOfTheMonth(this DateTime current)
        {
            return current.Last();
        }

        /// <summary>
        /// หาวันสุดท้ายของปี
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static DateTime EndOfTheYear(this DateTime current)
        {
            DateTime last = current.First().AddMonths(12 - current.Month);
            return last.Last();
        }

        /// <summary>
        /// Gets a DateTime representing the last specified day in the current month
        /// </summary>
        /// <param name="current">The current date</param>
        /// <param name="dayOfWeek">The current day of week</param>
        /// <returns></returns>
        public static DateTime Last(this DateTime current, DayOfWeek dayOfWeek)
        {
            DateTime last = current.Last();

            last = last.AddDays(Math.Abs(dayOfWeek - last.DayOfWeek) * -1);
            return last;
        }

        /// <summary>
        /// Gets a DateTime representing the first date following the current date which falls on the given day of the week
        /// </summary>
        /// <param name="current">The current date</param>
        /// <param name="dayOfWeek">The day of week for the next date to get</param>
        public static DateTime Next(this DateTime current, DayOfWeek dayOfWeek)
        {
            int offsetDays = dayOfWeek - current.DayOfWeek;

            if (offsetDays <= 0)
            {
                offsetDays += 7;
            }

            DateTime result = current.AddDays(offsetDays);
            return result;
        }


    }
}
