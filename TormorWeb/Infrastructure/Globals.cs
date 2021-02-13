using System.Globalization;
using System.Web;
using System.Web.Configuration;
using Telerik.Web.Mvc.UI;
using System.Threading;

namespace NNS.Config
{
    /// <summary>
    /// Summary description for Globals Class
    /// </summary>
    public static class Globals
    {
        private readonly static NNSConfigSection settings = (NNSConfigSection)WebConfigurationManager
                                                                .GetSection("NNSConfig");
        //public static string CurrentUserName = "unidentified";
        public static string CurrentUserProfile = "";
        public static CultureInfo UserCulture = CultureInfo.CreateSpecificCulture("en-US"); //th20110407 ให้มีแต่ en เนื่องจาก user ต้องการวันที่เป็นค.ศ. PD18-540102 Req5
        public static int PageSize = settings.PageSize;
        public static int MainScreenHeight = settings.DefaultMainScreenHeight;

        private readonly static NNSConfigAppSection appsettings = (NNSConfigAppSection)WebConfigurationManager
                                                                    .GetSection("NNSConfigApp");
        public static bool WantVisa = appsettings.SearchVisa;
        public static bool WantReEntry = appsettings.SearchReEntry;
        public static bool WantEndorse = appsettings.SearchEndorse;
        public static bool WantStay = appsettings.SearchStay;
        public static bool WantShip = appsettings.SearchShip;


        #region Not set in profile
        public static GridPagerStyles GridPagerStyle = GridPagerStyles.NextPreviousAndNumeric | GridPagerStyles.PageInput;
        public static GridSortMode GridSortMode = settings.GridSortMode;
        public static string DateFormat = settings.DateFormat;
        public static string TimeFormat = settings.TimeFormat;
        public static string DateTimeFormat
        {
            get
            {
                if (DateFormat == "")
                    return "";
                return DateFormat + " " + TimeFormat;
            }
        }
        public static string ModalDetailUpdateOK = "OK";
        #endregion

        public static void ReadProfileToGlobals(string username, bool forceRead = false)
        {
            if (forceRead || (Globals.CurrentUserProfile != username))
            {
                Globals.CurrentUserProfile = username;

                ProfileCommon profile = ProfileCommon.GetProfile(username);

                //Globals.CurrentUserName = HttpContext.Current.User.Identity.Name;
                if (profile.Culture != "")
                    Globals.UserCulture = CultureInfo.CreateSpecificCulture(profile.Culture);
                else
                    Globals.UserCulture = System.Globalization.CultureInfo.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = Globals.UserCulture;

                if (profile.PageSize > 0)
                    Globals.PageSize = profile.PageSize;
                else
                    Globals.PageSize = Globals.settings.PageSize;

                if (profile.MainScreenHeight >= 0) //ค่า 0 ใน profile หมายถึงต้องการให้โปรแกรมคำนวณความสูง grid อัตโนมัติ
                    Globals.MainScreenHeight = profile.MainScreenHeight;
                else
                    Globals.MainScreenHeight = Globals.settings.DefaultMainScreenHeight;
            }
        }

    }
}