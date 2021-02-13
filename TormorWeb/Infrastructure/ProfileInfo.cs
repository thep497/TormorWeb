using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NNS.Config;
using System.Globalization;
using System.Web.Security;
using System.Threading;

namespace NNS.Config
{
    public class ProfileInfo
    {
        public string CurrentUserName { get; set; }
        public CultureInfo UserCulture { get; set; }
        public int PageSize { get; set; }
        public int MainScreenHeight { get; set; }
    }

    public static class ProfileRepository
    {
        private static ProfileInfo currentProfile
        {
            get
            {
                return (ProfileInfo)HttpContext.Current.Session["CurrentProfile"];
            }
            set
            {
                HttpContext.Current.Session["CurrentProfile"] = value;
            }
        }

        public static void ClearCurrentProfile()
        {
            currentProfile = null;
            DoReadProfileToGlobals();
        }

        public static void DoReadProfileToGlobals()
        {
            if (HttpContext.Current.Session != null)
            {
                if ((currentProfile == null) || 
                    (string.IsNullOrEmpty(currentProfile.CurrentUserName)) ||
                    ((HttpContext.Current.User != null) && (HttpContext.Current.User.Identity.Name != currentProfile.CurrentUserName)))
                {
                    currentProfile = new ProfileInfo();
                    if (HttpContext.Current.User != null)
                    {
                        //MembershipUser user = Membership.GetUser(true);
                        if (!string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
                        {
                            var userName = HttpContext.Current.User.Identity.Name;
                            Globals.ReadProfileToGlobals(userName, true);

                            currentProfile.CurrentUserName = userName;
                            currentProfile.UserCulture = Globals.UserCulture;
                            currentProfile.PageSize = Globals.PageSize;
                            currentProfile.MainScreenHeight = Globals.MainScreenHeight;
                        }
                    }
                }
                else
                {
                    Globals.CurrentUserProfile = currentProfile.CurrentUserName;

                    Globals.UserCulture = currentProfile.UserCulture;
                    Thread.CurrentThread.CurrentCulture = Globals.UserCulture;

                    Globals.PageSize = currentProfile.PageSize;
                    Globals.MainScreenHeight = currentProfile.MainScreenHeight;
                }
            }
            else //ยังไม่มี session
            {
                if (HttpContext.Current.User != null)
                {
                    Globals.ReadProfileToGlobals(HttpContext.Current.User.Identity.Name, true);
                }
            }
        }
    }
}