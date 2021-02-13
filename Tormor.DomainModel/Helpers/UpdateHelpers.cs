using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using NNS.GeneralHelpers;

namespace NNS.ModelHelpers
{
    public static class UpdateHelpers
    {
        public static void ClearNullDate(this object obj)
        {
            foreach (var prop in obj.GetType().GetProperties().Where(p => p.PropertyType.Name.Contains("Date")))
            {
                if (prop.CanWrite && prop.CanRead)
                {
                    var dt = (DateTime)(prop.GetValue(obj, null) ?? DateTime.MinValue);
                    //if (dt <= DateTime.MinValue)
                    if (dt <= DateExtension.SQLDateMinValue)
                        prop.SetValue(obj, null, null);

                    //th20110206 ถ้าวันที่มากกว่า 400 ปี แสดงว่า Convert ผิด ให้แปลงจากค.ศ.เป็นพ.ศ.
                    if (dt > DateTime.Today.AddYears(400))
                    {
                        var newDate = dt.AddYears(-543);
                        var datediff = Math.Abs(newDate.CalcAgeYear());
                        if (datediff < 10)
                            prop.SetValue(obj, newDate, null);
                    }
                }
            }
        }
    }
}
