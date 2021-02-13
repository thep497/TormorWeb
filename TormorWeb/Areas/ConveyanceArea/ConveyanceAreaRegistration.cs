using System.Web.Mvc;

namespace Tormor.Web.Areas.ConveyanceArea
{
    public class ConveyanceAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ConveyanceArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Conveyance_default",
                "ConveyanceArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
