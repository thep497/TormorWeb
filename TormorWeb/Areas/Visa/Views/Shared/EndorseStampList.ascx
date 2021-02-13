<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.Web.Models.EndorseStampViewModel>" %>

<% Html.Telerik().Grid(Model.vmEndorseStamps)
                    .Name("GridDetail")
                    .DataKeys(keys => keys.Add(c => c.Id))
                    .DataBinding(dataBinding =>
                    {
                        dataBinding.Ajax()
                            .Select("_GetEndorseStamps", "EndorseStamp", new { endorseId = Model.vmEndorseId });
                    })
                    .Columns(columns =>
                    {
                        columns.Bound(x => x.Id).Title("").Encoded(false).Width(60).Sortable(false).Filterable(false)
                             .HeaderTemplate(() =>
                              {%>
                                <%: Html.ActionLink("เพิ่ม", "_InsertEndorseStamp", "EndorseStamp", new { endorseid = Model.vmEndorseId },
                                                                                                              new { @class = "btn-insert-detail t-button", style = "min-width:40px;", id = "btn-insert-detail", title = "เพิ่มข้อมูลการตรวจลงตรา" })%>
                              <%})
                            .Format(Html.ActionLink("แก้ไข", "_EditEndorseStamp", "EndorseStamp", new { endorseid = Model.vmEndorseId, endorsestampid = "{0}" },
                                        new { @class = "btn-edit-detail t-button", style = "min-width:40px;", id = "btn-edit-detail" }).ToHtmlString());
                        columns.Bound(c => c.Code).Width(80);
                        columns.Bound(c => c.StampDate).Width(100)
                            .Format("{0:" + Globals.DateFormat + "}");
                        columns.Bound(c => c.StampExpiredDate).Width(100)
                            .Format("{0:" + Globals.DateFormat + "}");
                        columns.Bound(c => c.Invoice.InvoiceNo).Width(100)
                            .HtmlAttributes(new { @style = "text-align:left" });
                        columns.Bound(c => c.Invoice.Charge).Width(100)
                            .Format("{0:#,##0}")
                            .HtmlAttributes(new { @style = "text-align:right" });
                        columns.Bound(c => c.SMTime).Width(60);
                        columns.Template(c => { }).ClientTemplate(" ");
                    })
                    .Pageable(paging => paging.PageSize(1000).Style(Globals.GridPagerStyle))
                    .Resizable(resizing => resizing.Columns(true))
                    .Filterable(filtering => filtering.Filters(filter => filter.Add(c => c.IsCancel).IsEqualTo(false)).Enabled(false))
                    .Selectable(selecting => selecting.Enabled(true))
                    .HtmlAttributes(new { @style = "text-align:center" })
                    .Render();
%>

<script type="text/javascript">
    $(document).ready(function () {
        $('#btn-insert-detail,#btn-edit-detail').live('click', function () {
            AjaxGetActionToDetailWindow($(this),true,700,240);
            return false;
        });
    });
</script> 
