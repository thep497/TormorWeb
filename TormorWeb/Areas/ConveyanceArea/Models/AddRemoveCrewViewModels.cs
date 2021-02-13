using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tormor.DomainModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using NNS.GeneralHelpers;

namespace Tormor.Web.Models
{
    public static class AddRemoveCrewHelper
    {
        public static string AddRemoveTypeStr(int addRemoveType)
        {
            if (addRemoveType == 1) return "A";
            return "R";
        }
        public static string AddRemoveTypeStr(object addRemoveType)
        {
            try
            {
                return AddRemoveTypeStr((int?)addRemoveType ?? 1);
            }
            catch
            {
                return "A";
            }
        }
        public static string AddRemoveTypeString(object addRemoveType)
        {
            int art;
            if (addRemoveType == null)
                art = 1;
            else
                art = (int)addRemoveType;

            if (art == 1) return "เพิ่ม";
            return "ลด";
        }
    }
} 