<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.Web.Models.CrewViewModel>" %>

<%
    string headerText = "แสดงข้อมูล"+(Model.vmIsCrew ? "คนประจำเรือ" : "ผู้โดยสาร")+"ของเรือ '";
    headerText += Model.vmConveyanceName + "' วันที่ " + Model.vmInOutDate.ToString(Globals.DateFormat);
%>
<h3>
<%:headerText %>
</h3><br />
<div class="generaleditform conveyancesinout-edit">
<% Html.Telerik().Grid(Model.vmCrews)
                    .Name("GridDetail" + Model.vmIsCrewStr)
                    .DataKeys(keys => keys.Add(c => c.Id))
                    .DataBinding(dataBinding =>
                    {
                        //th20110409 ตัด ajax ออก และไม่ให้เรียง น่าจะไม่มีการเรียกใช้ ajax แล้ว
                        //th20110411 การตัด ajax ออกทำให้ list นี้ไม่ update จากการเพิ่มหรือแก้ไข ต้องสร้าง ClientTemplate แล้วเปิด AJAX เอาไว้
                        if (!Model.vmIsAddRemove)
                        {
                            dataBinding.Ajax()
                                .Select("_GetCrews", "Crew", new { conveyanceInOutId = Model.vmConveyanceInOutId, isCrew = Model.vmIsCrew });
                        }
                    })
                    .Columns(columns =>
                    {
                        if (!Model.vmIsAddRemove)
                            columns.Bound(x => x.Id).Title("").Encoded(false).Width(60).Sortable(false).Filterable(false)
                                .HeaderTemplate(() =>
                                {%>
                                    <%: Html.ActionLink("เพิ่ม", "_InsertCrew", "Crew", new { conveyanceInOutid = Model.vmConveyanceInOutId, isCrew = Model.vmIsCrew },
                                                                              new { @class = "btn-insert-detail t-button", style = "min-width:40px;", id = "btn-insert-detail" + Model.vmIsCrewStr, title = "เพิ่มข้อมูล" })
                                    %>
                                <%})
                                .Template(o =>
                                {%>
                                    <% if (o.AddRemoveType == ModelConst.ADDREMOVETYPE_NONE) { %>
                                        <%: Html.ActionLink("แก้ไข", "_EditCrew", "Crew", new { conveyanceInOutid = Model.vmConveyanceInOutId, isCrew = Model.vmIsCrew, crewId = o.Id },
                                                new { @class = "btn-edit-detail t-button", style = "min-width:40px;", id = "btn-edit-detail" + Model.vmIsCrewStr })
                                        %>
                                    <% } else { %>
                                        <%: o.AddRemoveTypeStr %>
                                    <% } %>
                                <%})
                                //th20110411 เพิ่ม clienttemplate เพื่อให้ใช้ ajax ในการ update การเปลี่ยนแปลงได้
                                .ClientTemplate(
                                    "<# if(AddRemoveType == 0) { #>"+
                                        Html.ActionLink("แก้ไข", "_EditCrew", "Crew", new { conveyanceInOutid = Model.vmConveyanceInOutId, isCrew = Model.vmIsCrew, crewId = "<#= Id #>" }, 
                                                new { @class = "btn-edit-detail t-button", style = "min-width:40px;", id = "btn-edit-detail" + Model.vmIsCrewStr }).ToHtmlString() +
                                    "<# } else {#>"+
                                        "<#= AddRemoveTypeStr #>"+
                                    "<# } #>"
                                 )  
                                .HtmlAttributes(new { @style = "text-align:center" });
                        //.Format(.ToHtmlString());
                        else
                            columns.Bound(c => c.AddRemoveTypeStr).Width(60)
                                .HtmlAttributes(new { @style = "text-align:center" });
                        
                        columns.Bound(c => c.Code).Width(60)
                            .HtmlAttributes(new { @style = "text-align:center" });
                        columns.Bound(c => c.FullName).Width(150);
                        columns.Bound(c => c.Sex).Width(60)
                            .HtmlAttributes(new { @style = "text-align:center" });
                        columns.Bound(c => c.Age).Width(40).Format("{0:0;-0; }")
                            .HtmlAttributes(new { style = "text-align:right;" });
                        columns.Bound(c => c.Nationality).Width(100);
                        columns.Bound(c => c.PassportCard.DocNo).Width(100).Title("เลขที่ Passport");
                        columns.Bound(c => c.IDCardNo).Width(100);
                        columns.Bound(c => c.SeamanCardNo).Width(100);
                        columns.Bound(c => c.DateOfBirth).Width(100)
                            .Format("{0:" + Globals.DateFormat + "}");
                        if (Model.vmIsCrew) //th20110408 PD18-540102 Req 8.3 เพิ่มวันที่แจ้ง (เพิ่มทั้งกรณีลูกเรือ และ เพิ่มลด)
                            columns.Bound(c => c.RequestDate).Width(100)
                                .Format("{0:" + Globals.DateFormat + "}");
                        if (Model.vmIsCrew) //th20110408 PD18-540102 Req 8.4 / 8.6 เพิ่มช่องหมายเหตุ
                            columns.Bound(c => c.Remark).Width(200);
                        columns.Template(c => { }).ClientTemplate(" ");
                    })
                    .Pageable(paging => paging.PageSize(1000).Style(Globals.GridPagerStyle))
                    .Resizable(resizing => resizing.Columns(true))
                    .Sortable(sort => sort.SortMode(Globals.GridSortMode).Enabled(!Model.vmIsAddRemove))
                    .Filterable(filtering => filtering.Filters(filter => filter.Add(c => c.IsCancel).IsEqualTo(false)).Enabled(false))
                    .Selectable(selecting => selecting.Enabled(true))
                    .Footer(!Model.vmIsAddRemove)
                    .Render();
%>
</div>
<p></p><p></p><p></p>

<script type="text/javascript">
    $(document).ready(function () {
        $('#btn-insert-detail<%: Model.vmIsCrewStr %>,#btn-edit-detail<%: Model.vmIsCrewStr %>').live('click', function () {
            AjaxGetActionToDetailWindow($(this), true, 850, 450);
            return false;
        });
    });
    function SuccessUpdateDetailGrid<%= Model.vmIsCrewStr %>(status) {
        if (status.get_response().get_responseData() == glo_ModalDetailUpdateOK) {
            doCloseWindow("<%: Model.vmIsCrewStr %>");
        }
    }
</script> 
