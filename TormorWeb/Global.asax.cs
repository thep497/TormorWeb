using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NNS.Web.Infrastructure;
using Telerik.Web.Mvc;
using System.Web.Security;
using System.Threading;
using System.Globalization;
using NNS.Config;

namespace Tormor.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        #region private functions...
        private void _bindModels()
        {
            ModelBinders.Binders.Add(typeof(DateTime?), new LocaleDateTimeNModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime), new LocaleDateTimeModelBinder());
        }

        private void _setSiteMap()
        {
            if (SiteMapManager.SiteMaps.ContainsKey("siteMap"))
                SiteMapManager.SiteMaps.Remove("siteMap");

            if (!SiteMapManager.SiteMaps.ContainsKey("siteMap"))
            {
                SiteMapManager.SiteMaps.Register<XmlSiteMap>("siteMap", sitmap => sitmap.Load());
            }
        }

        #endregion

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            _bindModels();
            _setSiteMap();
        }

        protected void Application_AuthenticateRequest()
        {
            //ที่นี่ Session ยังไม่ถูก Initialize ใน HttpContext จึงเรียกใช้ profile จาก session ไม่ได้ ให้ไปเรียกใช้ใน Application_AcquireRequestState แทน
            //if (HttpContext.Current.Session != null)
            //    ProfileRepository.ReadProfileToGlobals();
            //else if (HttpContext.Current.User != null)
            //{
            //    MembershipUser user = Membership.GetUser(true);
            //    Globals.ReadProfileToGlobals(user.UserName);
            //}
        }

        protected void Application_BeginRequest()
        {
            Thread.CurrentThread.CurrentCulture = Globals.UserCulture;
        }

        protected void Application_AcquireRequestState()
        {
            ProfileRepository.DoReadProfileToGlobals();
        }
    }

}