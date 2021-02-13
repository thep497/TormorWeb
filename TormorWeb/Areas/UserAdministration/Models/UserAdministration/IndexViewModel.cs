using System.Collections.Generic;
using System.Web.Security;

namespace NNS.Web.Areas.UserAdministration.Models.UserAdministration
{
	public class IndexViewModel
	{
        public IEnumerable<MembershipUser> Users { get; set; }
		public IEnumerable<string> Roles { get; set; }
        public bool IsShowRow { get; set; }
	}
}