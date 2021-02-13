using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNS.GeneralHelpers
{
    public static class DateExtension
    {
        public readonly static DateTime SQLDateMinValue = DateTime.Parse("1753/1/1");

        /// <summary>
        /// th20110804 คำนวณวันเกิดจากอายุ
        /// </summary>
        /// <param name="age"></param>
        /// <param name="refDate"></param>
        /// <returns></returns>
        public static DateTime CalcDateOfBirth(this int age, DateTime? refDate = null)
        {
            var birthYear = (refDate ?? DateTime.Today).Year - age;
            return new DateTime(birthYear, 1, 1);
        }

        public static string ToString(this DateTime? dt,string dateFormat)
        {
            return dt == null ? "" : (dt ?? DateTime.MinValue).ToString(dateFormat);
        }

        public static DateTime AddTime(this DateTime dt,DateTime? tm)
        {
            if (tm == null)
                return dt;

             //เอาค่าวันใส่ใน InOutTime
            var outTime = tm ?? DateTime.Today;
            return dt.SetTime(outTime.Hour,outTime.Minute);
        }

        public static DateTime? AddTime(this DateTime? dt, DateTime? tm)
        {
            if (dt==null)
                return tm;
            return (dt ?? DateTime.Today).AddTime(tm);
        }

        public static bool IsNull(this DateTime? dt)
        {
            try
            {
                return (dt == null) || (dt == DateTime.MinValue) || (dt == SQLDateMinValue);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsNotDefined(this DateTime? dt)
        {
            try
            {
                return (dt == null) || (dt == DateTime.MinValue);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// หาช่วงอายุจากวันที่กำหนด
        /// </summary>
        /// <param name="Am"></param>
        /// <param name="argToDate"></param>
        /// <returns></returns>
        public static AgeSpan CalcAge(this DateTime Am, DateTime argToDate)
        {
            DateTime FromDate = Am;
            DateTime ToDate = argToDate;
            DateTime tempDate = DateTime.Now;

            AgeSpan retAge = new AgeSpan();

            int CarryFlag = 0;

            if (Am > argToDate)
            {
                ToDate = Am;
                FromDate = argToDate;
            }
            else
            {
                ToDate = argToDate;
                FromDate = Am;
            }

            // Day calc
            if (FromDate.Day > ToDate.Day)
            {
                // Get last day of month from previous month
                // even if leapyear system will automate return correctly
                tempDate = new DateTime(FromDate.Year, FromDate.Month, 1);
                tempDate = (tempDate.AddMonths(1)).AddDays(-1);

                CarryFlag = tempDate.Day;
                retAge.Day = (ToDate.Day + CarryFlag) - FromDate.Day;
                CarryFlag = 1;
            }
            else
                retAge.Day = ToDate.Day - FromDate.Day;

            //month calc
            if ((FromDate.Month + CarryFlag) > ToDate.Month)
            {
                retAge.Month = (ToDate.Month + 12) - (FromDate.Month + CarryFlag);
                CarryFlag = 1;
            }
            else
            {
                retAge.Month = ToDate.Month - (FromDate.Month + CarryFlag);
                CarryFlag = 0;
            }

            retAge.Year = ToDate.Year - (FromDate.Year + CarryFlag);

            return retAge;

        }

        /// <summary>
        /// หาช่วงอายุจากวันปัจจุบัน
        /// </summary>
        /// <param name="Am"></param>
        /// <returns></returns>
        public static AgeSpan CalcAge(this DateTime Am)
        {
            return Am.CalcAge(DateTime.Now);
        }

        /// <summary>
        /// หาช่วงอายุโดยเทียบเฉพาะปีเกิด
        /// </summary>
        /// <param name="Am"></param>
        /// <param name="argToDate"></param>
        /// <returns></returns>
        public static int CalcAgeYear(this DateTime Am, DateTime argToDate)
        {
            return Math.Abs(argToDate.Year - Am.Year);
        }

        /// <summary>
        /// หาช่วงอายุโดยเทียบปีเกิดกับปีปัจจุบัน
        /// </summary>
        /// <param name="Am"></param>
        /// <returns></returns>
        public static int CalcAgeYear(this DateTime Am)
        {
            return Am.CalcAgeYear(DateTime.Now);
        }
        
    }

    /// <summary>
    /// แสดงช่วงวันที่ที่ต่างกัน
    /// </summary>
    public class AgeSpan
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public AgeSpan() { this.Year = 0; this.Month = 0; this.Day = 0; }

        public override string ToString()
        {
            return this.ToString("Year(s)", "Month(s)", "Day(s)", " ,");
        }

        public string ToString(string argYearCaption, string argMonthCaption, string argDayCaption, string argSeparator)
        {
            string strRet = string.Empty;

            if (Year > 0)
                strRet = string.Format("{0} {1}", this.Year, argYearCaption, argSeparator);
            if (Month > 0)
            {
                if (strRet.Length > 0) strRet = strRet + argSeparator;
                strRet = strRet + string.Format("{0} {1}", this.Month, argMonthCaption, argSeparator);
            }
            if (Day > 0)
            {
                if (strRet.Length > 0) strRet = strRet + argSeparator;
                strRet = strRet + string.Format("{0} {1}", this.Day, argDayCaption);
            }
            return strRet;
        }

        /// <summary>
        /// แสดงเป็นปี ตามจำนวนปี โดยปัดเศษ
        /// </summary>
        /// <returns></returns>
        public int RoundAge
        {
            get
            {
                if (Age - Year >= 0.50m)
                    return Year + 1;
                return Year;
            }
        }

        public Decimal Age
        {
            get { return Year + (Month / 12) + (Day / 365); }
        }
    }
}
