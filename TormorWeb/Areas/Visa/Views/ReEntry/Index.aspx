<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Tormor.DomainModel.ReEntry>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<% ViewData["PageTitle"] = Html.ShowTitle("รายละเอียดการแจ้ง Re-Entry", ViewData["dtpstartdate"], ViewData["dtpenddate"], Globals.DateFormat); %>
<%: ViewData["PageTitle"] %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.Telerik().Grid(Model)
                     .Name("Grid")
                     .DataKeys(keys => keys.Add(c => c.Id))
                     .DataBinding(dataBinding =>
                     {
                         dataBinding.Server()
                             .Select("Index", "ReEntry");
                     })
                     .Columns(columns =>
                     {
                         columns.Bound(x => x.Id).Title("").Encoded(false).Width(95).Sortable(false).Filterable(false)
                             .Format(Html.ActionLink("Edit", "Edit", new { id = "{0}" }, new { @class = "t-grid-action t-button t-state-default t-grid-edit", style = "margin-left:0" }).ToHtmlString());
                         columns.Bound(c => c.Code).Width(60)
                             .Encoded(false)
                             .Template(c =>
                             { 
                                 %>
                                 <%: Html.ActionLink(string.IsNullOrWhiteSpace(c.Code) ? "-" : c.Code,
                                     "Edit", new { id = c.Id })%>
                                 <%
                             })
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.RequestDate).Width(90)
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.Alien.Name.FullName);
                         columns.Bound(c => c.Alien.Nationality).Width(80);
                         columns.Bound(c => c.ReEntryCode).Width(50)
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.SMTime).Width(50)
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.Invoice.InvoiceNo).Width(80)
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.Invoice.Charge).Width(80)
                             .HtmlAttributes(new { @style = "text-align:right" });
                         columns.Bound(c => c.PermitToDate).Width(90)
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.IsCancel).Width(40).Visible(false);
                         columns.Bound(c => c.UpdateInfo.AddedBy).Width(90).ReadOnly()
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.UpdateInfo.AddedDate).Width(90).ReadOnly()
                             .HtmlAttributes(new { @style = "text-align:center" });
                     })
                     .Editable(editing => editing.Enabled(false))
                     .Pageable(paging => paging.PageSize(Globals.PageSize).Style(Globals.GridPagerStyle))
                     .Scrollable(scrolling => scrolling.Height(Globals.MainScreenHeight))
                     .Sortable(sorting => sorting.SortMode(Globals.GridSortMode).OrderBy(sortOrder => sortOrder.Add("Code")))
                     .Groupable()
                     .Resizable(resizing => resizing.Columns(true))
                     .Filterable(filtering => filtering.Filters(filter => filter.Add(c => c.IsCancel).IsEqualTo(false)))
                     .Selectable(selecting => selecting.Enabled(true))
                     .Render();
    %>
</asp:Content>
