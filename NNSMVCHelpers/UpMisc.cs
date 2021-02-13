using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNS.MVCHelpers
{
    public static class UpMisc
    {
        public static string SplitActionString(object actionname, out string str_controller, out string str_area)
        {
            string result="";
            str_controller = "";
            str_area = "";
            if (actionname != null)
            {
                try
                {
                    var action = ((string)actionname.ToString()).Split(new Char[] { '/' });
                    if (action.Length == 3)
                    {
                        result = action[2];
                        str_controller = action[1];
                        str_area = action[0];
                    }
                    else if (action.Length == 2)
                    {
                        result = action[1];
                        str_controller = action[0];
                    }
                    else if (action.Length == 1)
                    {
                        result = action[0];
                    }
                }
                catch { 
                    // do nothing
                }
            }
            return result;
        }
    }
}
