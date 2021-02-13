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
    public class NNSConfigAppSection : ConfigurationSection
    {
        [ConfigurationProperty("searchVisa", DefaultValue = true)]
        public bool SearchVisa
        {
            get { return (bool)base["searchVisa"]; }
            set { base["searchVisa"] = value; }
        }

        [ConfigurationProperty("searchReEntry", DefaultValue = true)]
        public bool SearchReEntry
        {
            get { return (bool)base["searchReEntry"]; }
            set { base["searchReEntry"] = value; }
        }

        [ConfigurationProperty("searchEndorse", DefaultValue = true)]
        public bool SearchEndorse
        {
            get { return (bool)base["searchEndorse"]; }
            set { base["searchEndorse"] = value; }
        }

        [ConfigurationProperty("searchStay", DefaultValue = true)]
        public bool SearchStay
        {
            get { return (bool)base["searchStay"]; }
            set { base["searchStay"] = value; }
        }

        [ConfigurationProperty("searchShip", DefaultValue = true)]
        public bool SearchShip
        {
            get { return (bool)base["searchShip"]; }
            set { base["searchShip"] = value; }
        }

    }
}
