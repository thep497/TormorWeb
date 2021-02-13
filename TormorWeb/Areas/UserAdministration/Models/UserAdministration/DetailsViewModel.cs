using System.Collections.Generic;
using System.Web.Security;
using NNS.Config;

namespace NNS.Web.Areas.UserAdministration.Models.UserAdministration
{
	public class DetailsViewModel
	{
		#region StatusEnum enum

		public enum StatusEnum
		{
			Offline,
			Online,
			LockedOut,
			Unapproved
		}

		#endregion

		public string DisplayName { get; set; }
		public StatusEnum Status { get; set; }
		public MembershipUser User { get; set; }
		public bool CanResetPassword { get; set; }
		public bool RequirePasswordQuestionAnswerToResetPassword { get; set; }
        public ProfileCommon Profile { get; set; }
        public IDictionary<string, bool> Roles { get; set; }

        public string[] profileCultureList = new string[] { "-", "en-US" }; // ให้มีแต่ en เนื่องจาก user ต้องการวันที่เป็นค.ศ. PD18-540102 Req5
        //public string[] profileCultureList = new string[] { "-", "th-TH" };
    }
}