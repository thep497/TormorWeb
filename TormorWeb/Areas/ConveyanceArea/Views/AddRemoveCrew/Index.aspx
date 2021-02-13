<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Tormor.DomainModel.AddRemoveCrew>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<% ViewData["PageTitle"] = Html.ShowTitle("รายละเอียดการแจ้ง" + Tormor.Web.Models.AddRemoveCrewHelper.AddRemoveTypeString(ViewData["AddRemoveType"]) + "คนประจำพาหนะ (เรือ)", ViewData["dtpstartdate"], ViewData["dtpenddate"], Globals.DateFormat); %>
<%: ViewData["PageTitle"] %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.Telerik().Grid(Model)
                     .Name("Grid")
                     .DataKeys(keys => keys.Add(c => c.Id))
                     .DataBinding(dataBinding =>
                     {
                         dataBinding.Server()
                             .Select("Index", "AddRemoveCrew", new { addRemoveType = (ViewData["AddRemoveType"] ?? 1) });
                     })
                     .Columns(columns =>
                     {
                         columns.Bound(x => x.Id).Title("").Encoded(false).Width(100).Sortable(false).Filterable(false)
                             .Format(Html.ActionLink("Edit", "Edit", new { id = "{0}", addRemoveType = (ViewData["AddRemoveType"] ?? 1) }, new { @class = "btn-edit-master t-button", style = "margin-left:0" }).ToHtmlString());
                         columns.Bound(c => c.Code).Width(60)
                             .Encoded(false)
                             .Template(c =>
                             { 
                                 %>
                                 <%: Html.ActionLink(string.IsNullOrWhiteSpace(c.Code) ? "-" : c.Code,
                                            "Edit", new { id = c.Id, addRemoveType = (ViewData["AddRemoveType"] ?? 1) })%>
                                 <%
                             })
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.SubCode).Width(60)
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.RequestDate).Width(90)
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.Alien.Name.FullName).Width(150);
                         columns.Bound(c => c.AlienAge).Width(40)
                             .Format("{0:0;-0; }")
                             .HtmlAttributes(new { @style = "text-align:right" });
                         columns.Bound(c => c.Alien.Sex).Width(60);
                         columns.Bound(c => c.Alien.Nationality).Width(80);
                         columns.Template(c => c.InDetail.Detail(Globals.DateFormat)).Width(95).Title("พาหนะเข้าเมื่อ")
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.Company).Width(140);
                         columns.Template(c => c.OutDetail.Detail(Globals.DateFormat)).Width(95).Title("พาหนะออกเมื่อ")
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.IsCancel).Width(40).Visible(false);
                         columns.Bound(c => c.UpdateInfo.AddedBy).Width(90).ReadOnly()
                             .HtmlAttributes(new { @style = "text-align:center" });
                         //columns.Bound(c => c.UpdateInfo.AddedDate).Width(90).ReadOnly()
                         //    .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Template(c => {});//อันว่างเปล่าแสดงหลังสุดกรณีที่ grid มีที่ว่างเหลือ เพื่อที่ column อื่นจะได้มีความกว้างคงที่
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
