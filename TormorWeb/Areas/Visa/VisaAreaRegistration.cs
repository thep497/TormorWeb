using System.Web.Mvc;

namespace Tormor.Web.Areas.Visa
{
    public class VisaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Visa";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Visa_default",
                "Visa/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
