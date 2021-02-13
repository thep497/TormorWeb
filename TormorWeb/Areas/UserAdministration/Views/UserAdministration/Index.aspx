<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NNS.Web.Areas.UserAdministration.Models.UserAdministration.IndexViewModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	User Administration
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
	<link href='<% =Url.Content("~/Content/MvcMembership.css") %>' rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.Telerik().Grid(Model.Users)
                             .Name("Grid")
                             .DataKeys(keys => keys.Add(c => c.ProviderUserKey))
                             //.ToolBar(commands =>
                             //{
                             //    commands.Custom().Text("Insert").Action("Register", "Account", new { area = "" });
                             //})
                             .DataBinding(dataBinding =>
                             {
                                 dataBinding.Server()
                                     .Select("Index", "UserAdministration");
                             })
                             .Columns(columns =>
                             {
                                 columns.Bound(x => x.ProviderUserKey).Title("").Encoded(false).Width(100).Sortable(false).Filterable(false)
                                     .Format(Html.ActionLink("Edit", "Details", new { id = "{0}" }, new { @class = "t-grid-action t-button t-state-default t-grid-edit", style = "margin-left:0" }).ToHtmlString());
                                 columns.Bound(c => c.UserName).Title("ชื่อผู้ใช้").Width(100);
                                 columns.Bound(c => c.Email).Title("E-Mail").Width(180);
                                 columns.Bound(c => c.Comment).Title("หมายเหตุ").Width(150)
                                     .HtmlAttributes(new { @style = "text-align:left" });
                                 columns.Bound(c => c.IsApproved).Width(70).Title("ใช้งานได้")
                                     .HtmlAttributes(new { @style = "text-align:center" });
                                 columns.Bound(c => c.IsOnline).Width(70).Title("กำลังใช้งาน")
                                     .HtmlAttributes(new { @style = "text-align:center" });
                                 columns.Bound(c => c.IsLockedOut).Width(70).Title("Locked")
                                     .HtmlAttributes(new { @style = "text-align:center" });
                                 columns.Bound(c => c.CreationDate).Title("วันที่สร้าง").Width(100).ReadOnly()
                                     .HtmlAttributes(new { @style = "text-align:center" });
                                 columns.Bound(c => c.LastActivityDate).Title("วันที่ใช้งานล่าสุด").Width(100).ReadOnly()
                                     .HtmlAttributes(new { @style = "text-align:center" });
                                 columns.Template(c => {});//อันว่างเปล่าแสดงหลังสุดกรณีที่ grid มีที่ว่างเหลือ เพื่อที่ column อื่นจะได้มีความกว้างคงที่
                             })
                             .Editable(editing => editing.Enabled(false))
                             .Pageable(paging => paging
                                 .PageSize(Globals.PageSize)
                                 .Style(Globals.GridPagerStyle)
                             )
                             .Scrollable(scrolling => scrolling.Height(Globals.MainScreenHeight))
                             .Sortable(sorting => sorting.SortMode(Globals.GridSortMode)
                             )
                             .Groupable()
                             .Resizable(resizing => resizing.Columns(true))
                             .Filterable()
                             .Selectable(selecting => selecting.Enabled(true))
    %>

	<% if(Model.IsShowRow){ %>
	    <h3 class="mvcMembership">Roles</h3>
	    <div class="mvcMembership-allRoles">
	    <% if(Model.Roles.Count() > 0 ){ %>
		    <ul class="mvcMembership">
			    <% foreach(var role in Model.Roles){ %>
			    <li>
				    <% =Html.ActionLink(role, "Role", new{id = role}) %>
				    <% using(Html.BeginForm("DeleteRole", "UserAdministration", new{id=role})){ %>
				    <input type="submit" value="Delete" />
				    <% } %>
			    </li>
			    <% } %>
		    </ul>
	    <% }else{ %>
		    <p>No roles have been created.</p>
	    <% } %>
	    <% using(Html.BeginForm("CreateRole", "UserAdministration")){ %>
		    <fieldset>
			    <label for="id">Role:</label>
			    <% =Html.TextBox("id") %>
			    <input type="submit" value="Create Role" />
		    </fieldset>
	    <% } %>
    <% } %>
	</div>

</asp:Content>