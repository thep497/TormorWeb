<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.DomainModel.Crew>" %>

<div id='detailwindow-content'>
<div class="generaleditsubform conveyancesinout-edit">
<% 
    ViewData["InCrewPage"] = true; //th20110408 PD18-540102 Req 2 เอาไว้ตรวจสอบว่าจะแสดง IDCard/Seaman หรือไม่ ในหน้า AlienEditDetail
    var vmIsCrew = (bool)(ViewData["isCrew"] ?? true); 
    using (Ajax.BeginForm("_SaveCrew", "Crew",
                         new { isCreate = ViewData["isCreate"], isCrew = vmIsCrew },
                         new AjaxOptions { UpdateTargetId = "detailwindow-content", OnSuccess = "SuccessUpdateDetailGrid" + Tormor.Web.Models.CrewHelper.IsCrewStr(ViewData["isCrew"]) }))
   {%>
    <%: Html.ValidationSummary(true)%>
    <table class="generaleditform">
    <tr>
        <td class="editor-label" style="width:80px">
            <%: Html.LabelFor(model => model.Code)%>
        </td>
        <td class="editor-field" style="width:700px">
            <%: Html.HiddenFor(model => model.ConveyanceInOutId)%>
            <%: Html.HiddenFor(model => model.Id)%>
            <%: Html.TextBoxFor(model => model.Code, new { style = "width:50px" })%>
            <%: Html.ValidationMessageFor(model => model.Code)%>
        </td>  
    </tr>

    <tr>
        <td class="editor-label editbreak" style="vertical-align:top">
            <%: Html.LabelFor(model => model.AlienId)%>
        </td>
        <td class="editor-field editbreak" colspan="3">
    <% 
        if ((ViewData["AlienSearch_Passport"] == null) && (Model != null) && (Model.Alien != null))
            ViewData["AlienSearch_Passport"] = Model.Alien.PassportCard.DocNo;
        if ((ViewData["AlienSearch_Name"] == null) && (Model != null) && (Model.Alien != null))
            ViewData["AlienSearch_Name"] = Model.Alien.Name.FullName;
    %>
            <%: Html.HiddenFor(model => model.AlienId)%>
            <%  Html.RenderPartial("AlienEdit", new Tormor.Web.Models.AlienViewModel(Model.Alien)); %>
            <%: Html.ValidationMessageFor(model => model.AlienId)%>
        </td>
    </tr>

    <%--//th20110408 PD18-540102 Req 8.5 เพิ่มช่องหมายเหตุ--%>
    <% if (vmIsCrew) { %>
    <tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.Remark) %>
            </td>
            <td class="editor-field" colspan="3">
                <%: Html.TextAreaFor(model => model.Remark, new { rows=3, cols=85 })%>
            </td>
    </tr>
    <% } %>

    </table>

<p>
    <input id="btnDetlSave" type="submit" value="Save" style="display:none;" />
</p>
<%--<p></p><p></p><p></p><p></p><p>.</p>--%>

<% } %>

<% if ((bool)ViewData["isCreate"] == false) { %>
<% using (Ajax.BeginForm("_DeleteCrew", "Crew", null, new AjaxOptions { UpdateTargetId = "detailwindow-content", OnSuccess = "SuccessUpdateDetailGrid" + Tormor.Web.Models.CrewHelper.IsCrewStr(ViewData["isCrew"]) }))
   {%>
            <input name="conveyanceInOutId" type="hidden" value="<%: Model.ConveyanceInOutId %>" />
            <input name="isCrew" type="hidden" value="<%: ((bool?)ViewData["isCrew"] ?? true) %>" />
            <input name="id" type="hidden" value="<%: Model.Id %>" />
            <%= Html.AntiForgeryToken()%>
            <input id="btnDetlDelete" type="submit" value="Delete" style="display:none;"/>
<% } %>
<% } %>
</div>
</div>

<%--ต้องใส่ js ตัวนี้เข้าไปเพื่อให้จัดการปุ่ม menu ด้านบนได้ถูกต้อง--%>
<%= Html.JavascriptTag("nns/NNSModalDetailClient",true)  %>

<script type="text/javascript">
    $(document).ready(function () {
        SetShowOnly();
    });

    function SetShowOnly() {
        $('.showonly-crew').show();
    }
</script>

