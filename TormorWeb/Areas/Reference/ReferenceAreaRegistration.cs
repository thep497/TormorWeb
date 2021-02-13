using System.Web.Mvc;

namespace NNS.Web.Areas.Reference
{
    public class ReferenceAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Reference";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Reference_default",
                "Reference/{controller}/{action}/{refTypeId}/{id}",
                new { action = "Index", refTypeId = UrlParameter.Optional, id = UrlParameter.Optional }
            );
        }
    }
}
