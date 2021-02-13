using System;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Security;
using MvcMembership;
using MvcMembership.Settings;
using NNS.Web.Areas.UserAdministration.Models.UserAdministration;
using System.Web.Configuration;
using System.Net.Configuration;
using System.Configuration;
using NNS.Config;
using NNS.MVCHelpers;
using NNS.GeneralHelpers;

namespace NNS.Web.Areas.UserAdministration.Controllers
{
    [Authorize]
    public class UserAdministrationController : Controller
	{
		private const int PageSize = 10;
		private const string ResetPasswordBody = "Dear {0},\n\r\n\rYour new password is: {1}\n\r\n\rSee you online at {2} !";
		private const string ResetPasswordSubject = "Your New Password";
        private string ResetPasswordFromAddress = "from@domain.com";
        private readonly IRolesService _rolesService;
		private readonly ISmtpClient _smtpClient;
		private readonly IMembershipSettings _membershipSettings;
		private readonly IUserService _userService;
		private readonly IPasswordService _passwordService;

		public UserAdministrationController()
			: this(
				new AspNetMembershipProviderSettingsWrapper(Membership.Provider),
				new AspNetMembershipProviderWrapper(Membership.Provider),
				new AspNetMembershipProviderWrapper(Membership.Provider),
				new AspNetRoleProviderWrapper(Roles.Provider),
				new SmtpClientProxy(new SmtpClient()))
		{
            _InitialMailSettings();
		}

		public UserAdministrationController(
			IMembershipSettings membershipSettings,
			IUserService userService,
			IPasswordService passwordService,
			IRolesService rolesService,
			ISmtpClient smtpClient)
		{
			_membershipSettings = membershipSettings;
			_userService = userService;
			_passwordService = passwordService;
			_rolesService = rolesService;
			_smtpClient = smtpClient;
        }

        private void _InitialMailSettings()
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~/web.config");
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            ResetPasswordFromAddress = settings.Smtp.From;
        }

        [Authorize(Roles = "Administrator, GODS")]
        public ViewResult Index(bool? isShowRow)
		{
            ToolbarMenuHelpers.SetToolBar(ViewData, new { New = "Register" }, "Account", new { area = "" });

            return View(new IndexViewModel
							{
								Users = _userService.FindAll(0,100000),
								Roles = _rolesService.FindAll(),
                                IsShowRow = isShowRow ?? false
							});
		}

		[AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "Administrator, GODS")]
        public RedirectToRouteResult CreateRole(string id)
		{
			_rolesService.Create(id);
			return RedirectToAction("Index");
		}

		[AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "Administrator, GODS")]
        public RedirectToRouteResult DeleteRole(string id)
		{
			_rolesService.Delete(id);
			return RedirectToAction("Index");
		}

        [Authorize(Roles = "Administrator, GODS")]
        public ViewResult Role(string id)
		{
            ViewData["__tbMessage"] = "User Management";
			return View(new RoleViewModel
							{
								Role = id,
								Users = _rolesService.FindUserNamesByRole(id).Select(username => _userService.Get(username))
							});
		}

        [Authorize(Roles = "Administrator, GODS")]
        public ViewResult Details(Guid id)
		{
            ViewData["__tbMessage"] = "User Management";
            
            var user = _userService.Get(id);
			var userRoles = _rolesService.FindByUser(user);
			return View(new DetailsViewModel
							{
								CanResetPassword = _membershipSettings.Password.ResetOrRetrieval.CanReset,
								RequirePasswordQuestionAnswerToResetPassword = _membershipSettings.Password.ResetOrRetrieval.RequiresQuestionAndAnswer,
								DisplayName = user.UserName,
								User = user,
								Roles = _rolesService.FindAll().ToDictionary(role => role, role => userRoles.Contains(role)),
								Status = user.IsOnline
											? DetailsViewModel.StatusEnum.Online
											: !user.IsApproved
												? DetailsViewModel.StatusEnum.Unapproved
												: user.IsLockedOut
													? DetailsViewModel.StatusEnum.LockedOut
													: DetailsViewModel.StatusEnum.Offline
							});
		}

        [Authorize(Roles = "Administrator, GODS")]
        public ViewResult Password(Guid id)
		{
            ViewData["__tbMessage"] = "User Management";
            
            var user = _userService.Get(id);
			var userRoles = _rolesService.FindByUser(user);
			return View(new DetailsViewModel
			{
				CanResetPassword = _membershipSettings.Password.ResetOrRetrieval.CanReset,
				RequirePasswordQuestionAnswerToResetPassword = _membershipSettings.Password.ResetOrRetrieval.RequiresQuestionAndAnswer,
				DisplayName = user.UserName,
				User = user,
				Roles = _rolesService.FindAll().ToDictionary(role => role, role => userRoles.Contains(role)),
				Status = user.IsOnline
							? DetailsViewModel.StatusEnum.Online
							: !user.IsApproved
								? DetailsViewModel.StatusEnum.Unapproved
								: user.IsLockedOut
									? DetailsViewModel.StatusEnum.LockedOut
									: DetailsViewModel.StatusEnum.Offline
			});
		}

        [Authorize(Roles = "Administrator, GODS")]
        public ViewResult UsersRoles(Guid id)
		{
            ViewData["__tbMessage"] = "User Management";
            
            var user = _userService.Get(id);
			var userRoles = _rolesService.FindByUser(user);
			return View(new DetailsViewModel
			{
				CanResetPassword = _membershipSettings.Password.ResetOrRetrieval.CanReset,
				RequirePasswordQuestionAnswerToResetPassword = _membershipSettings.Password.ResetOrRetrieval.RequiresQuestionAndAnswer,
				DisplayName = user.UserName,
				User = user,
				Roles = _rolesService.FindAll().ToDictionary(role => role, role => userRoles.Contains(role)),
				Status = user.IsOnline
							? DetailsViewModel.StatusEnum.Online
							: !user.IsApproved
								? DetailsViewModel.StatusEnum.Unapproved
								: user.IsLockedOut
									? DetailsViewModel.StatusEnum.LockedOut
									: DetailsViewModel.StatusEnum.Offline
			});
		}

        public ViewResult UsersProfiles(Guid id)
        {
            ViewData["__tbMessage"] = "User Management";

            var user = _userService.Get(id);
            var userProfiles = ProfileCommon.GetProfile(user.UserName);
            return View(new DetailsViewModel
            { 
                DisplayName = user.UserName,
                User = user,
                Profile = userProfiles,
				Status = user.IsOnline
							? DetailsViewModel.StatusEnum.Online
							: !user.IsApproved
								? DetailsViewModel.StatusEnum.Unapproved
								: user.IsLockedOut
									? DetailsViewModel.StatusEnum.LockedOut
									: DetailsViewModel.StatusEnum.Offline
            });
        }

		[AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "Administrator, GODS")]
        public RedirectToRouteResult Details(Guid id, string email, string comments)
		{
			var user = _userService.Get(id);
			user.Email = email;
			user.Comment = comments;
			_userService.Update(user);

            TempData.AddInfo(Resources.Messages.SaveSuccess);
            return RedirectToAction("Details", new { id });
		}

        [AcceptVerbs(HttpVerbs.Post)]
        public RedirectToRouteResult UsersProfiles(Guid id, string Profile_FirstName, string Profile_LastName,
            string Profile_Position, string Profile_Phone, string Profile_Fax,
            string Profile_Culture, int Profile_PageSize, int Profile_MainScreenHeight)
        {
            var user = _userService.Get(id);
            ProfileCommon profile = ProfileCommon.GetProfile(user.UserName);

            if (ModelState.IsValid)
            {
                try
                {
                    profile.FirstName = Profile_FirstName;
                    profile.LastName = Profile_LastName;
                    profile.Position = Profile_Position;
                    profile.Phone = Profile_Phone;
                    profile.Fax = Profile_Fax;
                    profile.Culture = Profile_Culture == "-" ? "" : Profile_Culture.Trim();
                    profile.PageSize = Profile_PageSize;
                    profile.MainScreenHeight = Profile_MainScreenHeight;
                    profile.Save();

                    TempData.AddInfo(Resources.Messages.SaveSuccess);
                    if (user.UserName == HttpContext.User.Identity.Name)
                    {
                        //Globals.ReadProfileToGlobals(user.UserName, true);
                        ProfileRepository.ClearCurrentProfile();
                    }
                    return RedirectToAction("UsersProfiles", new { id });
                }
                catch (Exception ex)
                {
                    TempData.AddError(Resources.Messages.SaveError+ex.ExMessage());
                }
            }
            return RedirectToAction("UsersProfiles", new { id });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "Administrator, GODS")]
        public RedirectToRouteResult DeleteUser(Guid id)
		{
			_userService.Delete(_userService.Get(id));
			return RedirectToAction("Index");
		}

		[AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "Administrator, GODS")]
        public RedirectToRouteResult ChangeApproval(Guid id, bool isApproved)
		{
			var user = _userService.Get(id);
			user.IsApproved = isApproved;
			_userService.Update(user);

            return RedirectToAction("Details", new { id });
		}

		[AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "Administrator, GODS")]
        public RedirectToRouteResult Unlock(Guid id)
		{
			_passwordService.Unlock(_userService.Get(id));
			return RedirectToAction("Details", new { id });
		}

		[AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "Administrator, GODS")]
        public RedirectToRouteResult ResetPassword(Guid id)
		{
			var user = _userService.Get(id);
			var newPassword = _passwordService.ResetPassword(user);

            var body = string.Format(ResetPasswordBody, new string[] { user.UserName, newPassword, Request.Url.GetLeftPart(UriPartial.Authority) });
			_smtpClient.Send(new MailMessage(ResetPasswordFromAddress, user.Email, ResetPasswordSubject, body));

            TempData.AddInfo(Resources.Messages.ActionSuccess);
			return RedirectToAction("Password", new { id });
		}

		[AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "Administrator, GODS")]
        public RedirectToRouteResult ResetPasswordWithAnswer(Guid id, string answer)
		{
			var user = _userService.Get(id);
			var newPassword = _passwordService.ResetPassword(user, answer);

            var body = string.Format(ResetPasswordBody, new string[] { user.UserName, newPassword, Request.Url.GetLeftPart(UriPartial.Authority) });
            _smtpClient.Send(new MailMessage(ResetPasswordFromAddress, user.Email, ResetPasswordSubject, body));

            TempData.AddInfo(Resources.Messages.ActionSuccess);
            return RedirectToAction("Password", new { id });
		}

		[AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "Administrator, GODS")]
        public RedirectToRouteResult SetPassword(Guid id, string password)
		{
			var user = _userService.Get(id);

            try
            {
                _passwordService.ChangePassword(user, password);

                var body = string.Format(ResetPasswordBody, new string[] { user.UserName, password, Request.Url.GetLeftPart(UriPartial.Authority) });
                _smtpClient.Send(new MailMessage(ResetPasswordFromAddress, user.Email, ResetPasswordSubject, body));

                TempData.AddInfo(Resources.Messages.ActionSuccess);
            }
            catch (Exception ex)
            {
                TempData.AddError(Resources.Messages.SaveError+ex.ExMessage());
            }
            return RedirectToAction("Password", new { id });
		}

		[AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "Administrator, GODS")]
        public RedirectToRouteResult AddToRole(Guid id, string role)
		{
			_rolesService.AddToRole(_userService.Get(id), role);
			return RedirectToAction("UsersRoles", new { id });
		}

		[AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "Administrator, GODS")]
        public RedirectToRouteResult RemoveFromRole(Guid id, string role)
		{
			_rolesService.RemoveFromRole(_userService.Get(id), role);
			return RedirectToAction("UsersRoles", new { id });
		}
	}
}