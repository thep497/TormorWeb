<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NNS.Web.Areas.UserAdministration.Models.UserAdministration.RoleViewModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
<%  ViewData["PageTitle"] = "Roles: "+Model.Role; %>
<%: ViewData["PageTitle"]%>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">
	<link href='<% =Url.Content("~/Content/MvcMembership.css") %>' rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="mvcMembershipall">
	<h2 class="mvcMembership">Role: <%: Model.Role %></h2>
	<div class="mvcMembership-roleUsers">
		<% if(Model.Users.Count() > 0){ %>
			<div class="mvcMembership">
                <table>
				<% foreach (var user in Model.Users) {
                     if (user != null) {%>
				<tr>
                <td><% =Html.ActionLink(user.UserName, "Details", new { id = user.ProviderUserKey })%></td>

                <td><% if (user.UserName != HttpContext.Current.User.Identity.Name) { %>
					    <% using (Html.BeginForm("RemoveFromRole", "UserAdministration", new { id = user.ProviderUserKey, role = Model.Role })) { %>
						    <input type="submit" value="Remove From" />
					    <% } %>
                    <% } %>
				</td></tr>
				<%   }
                } %>
                </table>
			</div>
		<% }else{ %>
		<p>No users are in this role.</p>
		<% } %>
	</div>
</div>
</asp:Content>