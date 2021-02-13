<%@ Page Title="" Language="C#" MasterPageFile="../Shared/UserAdmin.Master" Inherits="System.Web.Mvc.ViewPage<NNS.Web.Areas.UserAdministration.Models.UserAdministration.DetailsViewModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
<%  ViewData["PageTitle"] = "User Roles: "+Model.DisplayName +" ["+Model.Status+"]"; %>
<%: ViewData["PageTitle"]%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   	<h3 class="mvcMembership">Roles</h3>
	<div class="mvcMembership-userRoles">
		<div class="mvcMembership">
            <table>
			<% foreach(var role in Model.Roles){ %>
			<tr>
            <td><% =Html.ActionLink(role.Key, "Role", new{id = role.Key}) %></td>

			<td><% if (Model.User.UserName != HttpContext.Current.User.Identity.Name){ %>
				    <% if (role.Value) { %>
					    <% using(Html.BeginForm("RemoveFromRole", "UserAdministration", new{id = Model.User.ProviderUserKey, role = role.Key})){ %>
					    <input type="submit" value="Remove From" />
					    <% } %>
				    <% }else{ %>
					    <% using(Html.BeginForm("AddToRole", "UserAdministration", new{id = Model.User.ProviderUserKey, role = role.Key})){ %>
					    <input type="submit" value="Add To" />
					    <% } %>
				    <% } %>
				<% } %>
			</td></tr>
			<% } %>
            </table>
        </div>
	</div>
</asp:Content>