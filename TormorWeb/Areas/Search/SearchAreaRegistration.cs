using System.Web.Mvc;

namespace Tormor.Web.Areas.Search
{
    public class SearchAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Search";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Search_default",
                "Search/{controller}/{action}/{ttype}/{id}",
                new { action = "Index", ttype = UrlParameter.Optional, id = UrlParameter.Optional }
            );
        }
    }
}
