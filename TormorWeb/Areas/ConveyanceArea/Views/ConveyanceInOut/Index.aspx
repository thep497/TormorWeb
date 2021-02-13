<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Tormor.Web.Models.ConveyanceSearchViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<% ViewData["PageTitle"] = Html.ShowTitle("รายการเข้าออกพาหนะ (เรือ)", ViewData["dtpstartdate"], ViewData["dtpenddate"], Globals.DateFormat); %>
<%: ViewData["PageTitle"] %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.RenderPartial("SearchAllPanel",Model.SearchInfo); %>
    <% Html.Telerik().Grid(Model.ConvInOuts)
                     .Name("Grid")
                     .DataKeys(keys => 
                     {
                         keys.Add(c => c.Id);
                     })
                     .DataBinding(dataBinding =>
                     {
                         dataBinding.Server()
                             .Select("Index", "ConveyanceInOut");
                     })
                     .Columns(columns =>
                     {
                         columns.Bound(c => c.Code).Title("").Width(100).Sortable(false).Filterable(false).Encoded(false)
                             .Template(c =>
                             { 
                                 %>
                                 <%: Html.ActionLink("Edit", "Edit", new { id = c.Id}, new { @class = "btn-edit-master t-button" })%>
                                 <%
                             })
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.InOutTypeName).Width(50)
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.RequestDate).Width(85)
                             .HtmlAttributes(new { @style = "text-align:center" });
                         //th20110407 PD18-540102 Req 7.3
                         columns.Bound(c => c.InOutTime).Width(145)
                             .HtmlAttributes(new { @style = "text-align:center" })
                             .Aggregate(aggregates => aggregates.Count())
                             .FooterTemplate(result => 
                               {
                                   if (result != null && result.Count != null)
                                   {
                                        %><center>จำนวนเรือทั้งหมด <%=  result.Count.Format("{0:#,##0}")%> ลำ</center><%     
                                   }
                               })
                             .GroupFooterTemplate(result =>
                               {
                                   if (result != null && result.Count != null)
                                   {
                                        %><center>รวม <%= result.Count.Format("{0:#,##0}")%> ลำ</center><%
                                   }
                               });
                         columns.Bound(c => c.Conveyance.Name).Width(130)
                             .Encoded(false)
                             .Template(c =>
                             { 
                                 %>
                                 <%: Html.ActionLink(string.IsNullOrWhiteSpace(c.Code) ? "-" : c.Conveyance.Name,
                                     "Edit", new { id = c.Id })%>
                                 <%
                             })
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.Conveyance.OwnerName).Width(180);
                         columns.Bound(c => c.PortForm).Width(150);
                         columns.Bound(c => c.PortTo).Width(150);
                         //th20110407 PD18-540102 Req 7.3
                         columns.Bound(c => c.NumCrew).Width(30)
                             .HtmlAttributes(new { @style = "text-align:right" })
                             .Aggregate(aggregates => aggregates.Sum())
                             .FooterTemplate(result =>
                               {
                                   if (result != null && result.Sum != null)
                                   {
                                        %><div style="float:right"><%= result.Sum.Format("{0:#,##0}")%></div><%     
                                   }
                               })
                             .GroupFooterTemplate(result =>
                               {
                                   if (result != null && result.Sum != null)
                                   {
                                        %><div style="float:right"><%= result.Sum.Format("{0:#,##0}")%></div><%     
                                   }
                               });
                         columns.Bound(c => c.IsCancel).Width(40).Visible(false);
                         columns.Bound(c => c.UpdateInfo.AddedBy).Width(80).ReadOnly()
                             .HtmlAttributes(new { @style = "text-align:center" });
                         //columns.Bound(c => c.UpdateInfo.AddedDate).Width(90).ReadOnly()
                         //    .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Template(c => {});//อันว่างเปล่าแสดงหลังสุดกรณีที่ grid มีที่ว่างเหลือ เพื่อที่ column อื่นจะได้มีความกว้างคงที่
                     })
                     .Editable(editing => editing.Enabled(false))
                     .Pageable(paging => paging.PageSize(Globals.PageSize).Style(Globals.GridPagerStyle))
                     .Scrollable(scrolling => scrolling.Height(Globals.MainScreenHeight))
                     .Sortable(sorting => sorting.SortMode(Globals.GridSortMode))
                     .Groupable()
                     .Resizable(resizing => resizing.Columns(true))
                     .Filterable(filtering => filtering.Filters(filter => filter.Add(c => c.IsCancel).IsEqualTo(false)).Enabled(false))
                     .Selectable(selecting => selecting.Enabled(true))
                     .Render();
    %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
