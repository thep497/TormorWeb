using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNS.GeneralHelpers
{
    public static class TpMisc
    {
        public static string ExMessage(this Exception ex)
        {
            string exMessage = ex.Message;

            Exception inn = ex.InnerException;
            if (inn != null && inn.Message != null)
                exMessage = inn.Message;
            return exMessage;
        }

        /// <summary>
        /// หาว่าทุก field สามารถ match searchcond ที่ split ตาม space ได้ครบถ้วนหรือไม่ ? (แบบ Google)
        /// </summary>
        /// <param name="searchCond"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public static bool IsSearchCondInAllField_ByAnd(string searchCond, string[] fieldValue)
        {
            var sc = searchCond.Split(' ');
            var result = false;
            foreach (var value in sc)
            {
                var v = (value ?? "").Trim().ToLower();
                if (v != "")
                {
                    if (checkSearchCondByAnd(v, fieldValue))
                        result = true;
                    else
                    {
                        result = false;
                        break;
                    }
                }
            }
                return result;
        }

        private static bool checkSearchCondByAnd(string searchData, string[] fieldValue)
        {
            foreach (var value in fieldValue)
            {
                var v = (value ?? "").Trim().ToLower();
                if (v != "")
                {
                    if (v.Contains(searchData))
                        return true;
                }
            }
            return false;
        }

    }
}
