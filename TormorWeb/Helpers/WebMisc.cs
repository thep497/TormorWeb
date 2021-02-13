using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NNS.Config;
using System.Globalization;

namespace NNS.MVCHelpers
{
    public static class WebMisc
    {
        public static DateTime? ToDate(this string strdate)
        {
            DateTime? resultDate = null;
            if (string.IsNullOrEmpty(strdate))
                return resultDate;

            try
            {
                //th20110622 vvv ตรวจสอบว่าค่าวันที่ที่ได้รับเป็นปี พ.ศ. หรือเปล่า ถ้าเป็น ต้องเปลี่ยนเป็นค.ศ.ก่อน
                var dateArr = strdate.Split(new char[] { '/', '-' });
                var fmtArr = Globals.DateFormat.Split(new char[] { '/', '-' });

                bool hasChange = false;
                for (var i = 0; (i < fmtArr.Count()) && (i < dateArr.Count()); i++)
                {
                    if (fmtArr[i] == "yyyy")
                    {
                        int yr = Convert.ToInt32(dateArr[i]);
                        if (yr > DateTime.Today.Year + 200)
                        {
                            dateArr[i] = Convert.ToString(yr - 543);
                            hasChange = true;
                        }
                        break;
                    }
                }
                var convStr = strdate;
                if (hasChange)
                {
                    var dateSep = Globals.DateFormat.IndexOf('/') > 0 ? '/' : '-';
                    convStr = dateArr[0];
                    for (var i = 1; i < dateArr.Count(); i++)
                    {
                        convStr += dateSep + dateArr[i];
                    }
                }
                //th ^^^

                resultDate = DateTime.ParseExact(convStr, Globals.DateFormat, CultureInfo.InvariantCulture);
            }
            catch
            {
                resultDate = null;
            }
            return resultDate;
        }
    }
}