<%@ Page Title="" Language="C#" MasterPageFile="../Shared/UserAdmin.Master" Inherits="System.Web.Mvc.ViewPage<NNS.Web.Areas.UserAdministration.Models.UserAdministration.DetailsViewModel>" %>
<%@ Import Namespace="System.Globalization" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
<%  ViewData["PageTitle"] = "Password: "+Model.DisplayName +" ["+Model.Status+"]"; %>
<%: ViewData["PageTitle"]%>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="mvcMembership">Password</h3>
	<div class="mvcMembership-password">
		<% if(Model.User.IsLockedOut){ %>
			<p>Locked out since <% =Model.User.LastLockoutDate.ToString(Globals.DateTimeFormat) %></p>
			<% using(Html.BeginForm("Unlock", "UserAdministration", new{ id = Model.User.ProviderUserKey })){ %>
			<input type="submit" value="Unlock Account" />
			<% } %>
		<% }else{ %>

			<% if(Model.User.LastPasswordChangedDate == Model.User.CreationDate){ %>
			<dl class="mvcMembership">
				<dt>Last Changed:</dt>
				<dd><em>Never</em></dd>
			</dl>
			<% }else{ %>
			<dl class="mvcMembership">
				<dt>Last Changed:</dt>
				<dd><% =Model.User.LastPasswordChangedDate.ToString(Globals.DateTimeFormat)%></dd>
			</dl>
			<% } %>

			<% if(Model.CanResetPassword && Model.RequirePasswordQuestionAnswerToResetPassword){ %>
				<% using(Html.BeginForm("ResetPasswordWithAnswer", "UserAdministration", new{ id = Model.User.ProviderUserKey })){ %>
				<fieldset>
					<p>
						<dl class="mvcMembership">
							<dt>Password Question:</dt>
							<% if(string.IsNullOrEmpty(Model.User.PasswordQuestion) || string.IsNullOrEmpty(Model.User.PasswordQuestion.Trim())){ %>
							<dd><em>No password question defined.</em></dd>
							<% }else{ %>
							<dd><%: Model.User.PasswordQuestion %></dd>
							<% } %>
						</dl>
					</p>
					<p>
						<label for="answer">Password Answer:</label>
						<% =Html.TextBox("answer") %>
					</p>
					<input type="submit" value="Reset to Random Password and Email User" />
				</fieldset>
				<% } %>
			<% }else if(Model.CanResetPassword){ %>
				<% using(Html.BeginForm("SetPassword", "UserAdministration", new{ id = Model.User.ProviderUserKey })){ %>
				<fieldset>
					<p>
						<label for="password">New Password:</label>
						<% =Html.TextBox("password") %>
					</p>
					<input type="submit" value="Change Password" />
				</fieldset>
				<% } %>
				<% using(Html.BeginForm("ResetPassword", "UserAdministration", new{ id = Model.User.ProviderUserKey })){ %>
				<fieldset>
					<input type="submit" value="Reset to Random Password and Email User" />
				</fieldset>
				<% } %>
			<% } %>

		<% } %>
	</div>
</asp:Content>