using System.Web.Mvc;

namespace Tormor.Web.Areas.AlienArea
{
    public class AlienAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AlienArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AlienArea_default",
                "AlienArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
