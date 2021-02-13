<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Tormor.Web.Models.AlienLite>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<% ViewData["PageTitle"] = "แสดงรายการบุคคลต่างด้าว"; %>
<%: ViewData["PageTitle"] %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% Html.Telerik().Grid(Model)
                     .Name("Grid")
                     .DataKeys(keys => keys.Add(c => c.Id))
                     .DataBinding(dataBinding =>
                     {
                         //ไม่สมควรใช้ ajax เพราะใน client format ได้ไม่เหมือน server
                         //dataBinding.Ajax()
                         //    .Select("_SelectAjaxEditing", "Alien");
                         dataBinding.Server()
                             .Select("Index", "Alien");
                         //    .Update("Edit", "Alien")
                         //    .Delete("Delete", "Alien");
                     })
                     .Columns(columns =>
                     {
                         columns.Bound(x => x.Id).Title("").Encoded(false).Width(95).Sortable(false).Filterable(false)
                             .Format(Html.ActionLink("Edit", "Edit", new { id = "{0}" }, new { @class = "t-grid-action t-button t-state-default t-grid-edit", style = "margin-left:0" }).ToHtmlString());
                         //columns.Bound(c => c.PassportCard.DocNo).Width(100).Title("เลขที่ Passport");
                         columns.Bound(c => c.PassportCard.DocNo).Width(100).Title("เลขที่ Passport")
                             .Encoded(false)
                             .Template(c =>
                             { 
                                 %>
                                 <%: Html.ActionLink(string.IsNullOrWhiteSpace(c.PassportCard.DocNo) ? "-" : c.PassportCard.DocNo,
                                     "Edit", new { id = c.Id })%>
                                 <%
                             });
                         columns.Bound(c => c.Name.FullName);
                         columns.Bound(c => c.Nationality).Width(100);
                         columns.Bound(c => c.DateOfBirth).Width(100)
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.UpdateInfo.AddedBy).Width(100).ReadOnly()
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.UpdateInfo.AddedDate).Width(100).ReadOnly()
                             .HtmlAttributes(new { @style = "text-align:center" });
                     })
                     .Editable(editing => editing.Enabled(false))
                     .Pageable(paging => paging.PageSize(Globals.PageSize).Style(Globals.GridPagerStyle))
                     .Scrollable(scrolling => scrolling.Height(Globals.MainScreenHeight))
                     .Sortable(sorting => sorting.SortMode(Globals.GridSortMode))
                     .Groupable()
                     .Resizable(resizing => resizing.Columns(true))
                     .Filterable(filtering => filtering.Filters(filter => filter.Add(c => c.IsCancel).IsEqualTo(false)))
                     .Selectable(selecting => selecting.Enabled(true)).Render();
    %>
    
</asp:Content>

