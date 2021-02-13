using System.Web.Mvc;
using NNS.Web.Areas.UserAdministration.Controllers;

namespace NNS.Web.Areas.UserAdministration
{
	public class UserAdministrationAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "UserAdministration";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"UserAdministration_default",
				"UserAdministration/{controller}/{action}/{id}",
				new { area="UserAdministration", action = "Index", id = UrlParameter.Optional },
				new [] { typeof(UserAdministrationController).Namespace }
			);
		}
	}
}
