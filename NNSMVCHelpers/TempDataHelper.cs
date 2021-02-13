using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace NNS.MVCHelpers
{
    public static class TempDataHelper
    {
        public static string ShowTempData(this TempDataDictionary tempData,string idx)
        {
            if (tempData[idx] != null)
                return "<div class='"+idx+"'>"+tempData[idx].ToString()+"</div>";
            return "";
        }

        public static string ShowInfo(this TempDataDictionary tempData)
        {
            return tempData.ShowTempData("message");
        }

        public static string ShowError(this TempDataDictionary tempData)
        {
            return tempData.ShowTempData("errormessage");
        }

        public static string ShowWarning(this TempDataDictionary tempData)
        {
            return tempData.ShowTempData("warningmessage");
        }

        public static void AddInfo(this TempDataDictionary tempData, string message)
        {
            tempData["message"] = message;
        }

        public static void AddError(this TempDataDictionary tempData, string message)
        {
            tempData["errormessage"] = message;
        }

        public static void AddWarning(this TempDataDictionary tempData, string message)
        {
            tempData["warningmessage"] = message;
        }

    }
}
