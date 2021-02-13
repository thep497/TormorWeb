<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Tormor.Web.Models.AlienSearchViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<% ViewData["PageTitle"] = Html.ShowTitle("รายการติดต่อของบุคคลต่างด้าว", ViewData["dtpstartdate"], ViewData["dtpenddate"], Globals.DateFormat); %>
<%: ViewData["PageTitle"] %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.RenderPartial("SearchAllPanel",Model.SearchInfo); %>
    <% Html.Telerik().Grid(Model.AlienTrans)
                     .Name("Grid")
                     .DataKeys(keys => 
                     {
                         keys.Add(c => c.Id);
                         keys.Add(c => c.TType);
                     })
                     .DataBinding(dataBinding =>
                     {
                         dataBinding.Server()
                             .Select("Index", "Search");
                     })
                     .Columns(columns =>
                     {
                         columns.Bound(c => c.Code).Title("").Width(100).Sortable(false).Filterable(false).Encoded(false)
                             .Template(c =>
                             { 
                                 %>
<%--                                 <%: Html.ActionLink("Edit", "Edit", new { id = c.Id, ttype = c.TType }, new { @class = "t-grid-action t-button t-state-default t-grid-edit" })%> --%>
                                 <%: Html.ActionLink("Edit", "Edit", new { id = c.Id, ttype = c.TType }, new { @class = "btn-edit-master t-button" })%>
                                 <%
                             })
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.TypeName).Width(115)
                             .HtmlAttributes(new { @style = "text-align:center" });
                         //th20110407 PD18-540102 Req 7.4 เพิ่มพาหนะ
                         if (Model.SearchInfo.WantShip)
                         {
                             columns.Bound(c => c.ConveyanceName).Width(100)
                                 .HtmlAttributes(new { @style = "text-align:center" });
                         }
                         columns.Bound(c => c.Code).Width(60)
                             .Encoded(false)
                             .Template(c =>
                             { 
                                 %>
                                 <%: Html.ActionLink(string.IsNullOrWhiteSpace(c.Code) ? "-" : c.Code,
                                     "Edit", new { id = c.Id, ttype = c.TType })%>
                                 <%
                             })
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.RequestDate).Width(85)
                             .HtmlAttributes(new { @style = "text-align:center" });
                         //th20110407 PD18-540102 Req 3.4    
                         columns.Bound(c => c.Alien.Name.FullName).Width(170)
                             .Aggregate(aggregates => aggregates.Count())
                             .FooterTemplate(result =>
                               {
                                   if (result != null && result.Count != null)
                                   {
                                        %><center>รวมทั้งหมด <%= result.Count.Format("{0:#,##0}")%> คน</center><%     
                                   }
                               })
                             .GroupFooterTemplate(result =>
                               {
                                   if (result != null && result.Count != null)
                                   {
                                        %><center>รวม <%= result.Count.Format("{0:#,##0}")%> คน</center><%     
                                   }
                               });
                         columns.Bound(c => c.Age).Width(40)
                             .Format("{0:0;-0; }")
                             .HtmlAttributes(new { @style = "text-align:right" });
                         columns.Bound(c => c.Alien.Nationality).Width(80);
                         columns.Bound(c => c.PassportCard.DocNo).Width(100).Title("เลขที่ Passport");
                         //th20110407 PD18-540102 Req 3.2
                         if (Model.SearchInfo.WantVisa)
                             columns.Bound(c => c.StayReason).Width(150);
                         columns.Bound(c => c.DateArrive).Width(85)
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.PermitToDate).Width(85)
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.Invoice.InvoiceNo).Width(100);
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

