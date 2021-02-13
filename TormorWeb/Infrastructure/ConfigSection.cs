using System;
using System.Data;
using System.Configuration;
using System.Web.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.Mvc.UI;

namespace NNS.Config
{
    public class NNSConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("dateFormat", DefaultValue = "yyyy/MM/dd")]
        public string DateFormat
        {
            get { return (string)base["dateFormat"]; }
            set { base["dateFormat"] = value; }
        }

        [ConfigurationProperty("timeFormat", DefaultValue = "HH:mm:ss")]
        public string TimeFormat
        {
            get { return (string)base["timeFormat"]; }
            set { base["timeFormat"] = value; }
        }

        [ConfigurationProperty("gridSortMode", DefaultValue = GridSortMode.SingleColumn)]
        public GridSortMode GridSortMode
        {
            get { return (GridSortMode)base["gridSortMode"]; }
            set { base["gridSortMode"] = value; }
        }

        [ConfigurationProperty("pageSize", DefaultValue = 10)]
        public int PageSize
        {
            get { return (int)base["pageSize"]; }
            set { base["pageSize"] = value; }
        }

        [ConfigurationProperty("defaultMainScreenWidth", DefaultValue = 350)]
        public int DefaultMainScreenWidth
        {
            get { return (int)base["defaultMainScreenWidth"]; }
            set { base["defaultMainScreenWidth"] = value; }
        }

        [ConfigurationProperty("defaultMainScreenHeight", DefaultValue = 380)]
        public int DefaultMainScreenHeight
        {
            get { return (int)base["defaultMainScreenHeight"]; }
            set { base["defaultMainScreenHeight"] = value; }
        }
    }
}
