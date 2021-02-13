<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Tormor.Web.Models.ReferenceViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%  ViewData["PageTitle"] = "Reference - " + Model.RefTypeName; %>
<%: ViewData["PageTitle"] %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%: Html.Telerik().Grid<Tormor.DomainModel.zz_Reference>(Model.References)
                     .Name("Grid")
                     .DataKeys(keys => keys.Add(c => c.Id))
                     //.ToolBar(commands => 
                     //{
                     //    if (Model.RefTypeId != 0)
                     //        commands.Insert().ButtonType(GridButtonType.ImageAndText)
                     //           .ImageHtmlAttributes(new { style = "margin-left:0" });
                     //})
                     .DataBinding(dataBinding =>
                     {
                         dataBinding.Server()
                            .Select("Index",  "Reference")
                            .Insert("Insert", "Reference")
                            .Update("Update", "Reference")
                            .Delete("Delete", "Reference");
                     })
                     .Columns(columns =>
                     {
                         columns.Command(commands =>
                         {
                             commands.Edit().ButtonType(GridButtonType.ImageAndText).HtmlAttributes(new { @class = "t-button" }); //, style = "min-width:60px;" 
                             if (Model.RefTypeId != 0)
                                 commands.Delete().ButtonType(GridButtonType.ImageAndText).HtmlAttributes(new { @class = "t-button" }); //, style = "min-width:70px;width:70px;"
                         }).Width(105); 
                         columns.Bound(c => c.Code).Width(80)
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.RefName).Title(Model.RefTypeName).Width(120);
                         columns.Bound(c => c.RefDesc).Width(120);
                         if (Model.HasReference)
                             columns.Bound(c => c.RefRefName).Width(120);
                         columns.Bound(c => c.UpdateInfo.AddedBy).Width(90).ReadOnly()
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.UpdateInfo.AddedDate).Width(100).ReadOnly()
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.UpdateInfo.UpdatedBy).Width(90).ReadOnly()
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Bound(c => c.UpdateInfo.UpdatedDate).Width(100).ReadOnly()
                             .HtmlAttributes(new { @style = "text-align:center" });
                         columns.Template(c => {});//อันว่างเปล่าแสดงหลังสุดกรณีที่ grid มีที่ว่างเหลือ เพื่อที่ column อื่นจะได้มีความกว้างคงที่
                     })
                     .Editable(editing => editing.Mode(GridEditMode.InLine))
                     .Pageable(paging => paging
                         .PageSize(Globals.PageSize)
                         .Style(Globals.GridPagerStyle)
                     )
                     .Scrollable(scrolling => scrolling.Height(Globals.MainScreenHeight))
                     .Sortable(sorting => sorting.SortMode(Globals.GridSortMode))
                     .Groupable(grouping => grouping.Enabled(true))
                     .Resizable(resizing => resizing.Columns(true))
                     .Filterable(filtering => filtering.Filters(filter => filter.Add(c => c.IsCancel).IsEqualTo(false)))
                     //.ClientEvents(events => events.OnEdit("onEdit"))
                     .Selectable(selecting => selecting.Enabled(true))
    %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            setTimeout(function () {
                $(':button').removeClass('submitbutton not-submitbutton');
                //$(".gridbtn").width("56px");
                //$(":button").width("70px");
            }, 200);
        });
    </script>
<%--<%= Html.DefineEditForm() %>--%>
</asp:Content>
