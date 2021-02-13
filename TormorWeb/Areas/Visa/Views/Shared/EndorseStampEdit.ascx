<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.DomainModel.EndorseStamp>" %>

<div id='detailwindow-content'>
<div class="generaleditsubform endorse-edit">
<% using (Ajax.BeginForm("_SaveEndorseStamp", "EndorseStamp", 
                         new { isCreate = ViewData["isCreate"] },
                         new AjaxOptions { UpdateTargetId = "detailwindow-content", OnSuccess = "SuccessUpdateDetail" }))
   {%>
    <%: Html.ValidationSummary(true)%>
    <table class="generaleditform">
    <tr>
        <td class="editor-label" style="width:80px">
            <%: Html.LabelFor(model => model.Code)%>
        </td>
        <td class="editor-field" style="width:80px">
            <%: Html.HiddenFor(model => model.EndorseId)%>
            <%: Html.HiddenFor(model => model.Id)%>
            <%: Html.TextBoxFor(model => model.Code, new { style = "width:50px" })%>
            <%: Html.ValidationMessageFor(model => model.Code)%>
        </td>  

        <td class="editor-label" style="width:100px">
            <%: Html.LabelFor(model => model.StampDate)%>
        </td>
        <td class="editor-field" style="width:100px">
            <%: Html.EditorFor(model => model.StampDate)%>
            <%: Html.ValidationMessageFor(model => model.StampDate)%>
        </td>

        <td class="editor-label" style="width:100px">
            <%: Html.LabelFor(model => model.StampExpiredDate)%>
        </td>
        <td class="editor-field" style="width:100px">
            <%: Html.EditorFor(model => model.StampExpiredDate)%>
            <%: Html.ValidationMessageFor(model => model.StampExpiredDate)%>
        </td>

    </tr>
    <tr>
        <td class="editor-label">
            <%: Html.LabelFor(model => model.SMTime)%>
        </td>
        <td class="editor-field">
            <%: Html.DropDownListRef_NameFor(model => model.SMTime, 13, new { OnChange = "onSMTimeChange" }, new { style = "width:100px" })%>
            <%: Html.ValidationMessageFor(model => model.SMTime)%>
        </td>
        <td class="editor-label">
            <%: Html.LabelFor(model => model.Invoice.InvoiceNo)%>
        </td>
        <td class="editor-field">
            <%: Html.TextBoxFor(model => model.Invoice.InvoiceNo, new { style = "width:150px" })%>
            <%: Html.ValidationMessageFor(model => model.Invoice.InvoiceNo)%>
        </td>
        <td class="editor-label">
            <%: Html.LabelFor(model => model.Invoice.Charge)%>
        </td>
        <td class="editor-field">
            <%: Html.TextBoxFor(model => model.Invoice.Charge, new { style = "width:80px;text-align:left;", @class="readonly", @readonly = "true" })%>  
            <%: Html.ValidationMessageFor(model => model.Invoice.Charge)%>
        </td>
    </tr>
    </table>

<p>
    <input id="btnDetlSave" type="submit" value="Save" style="display:none;" />
</p>
<%--<p></p><p></p><p></p><p></p><p>.</p>--%>

<% } %>

<% if ((bool)ViewData["isCreate"] == false) { %>
<% using (Ajax.BeginForm("_DeleteEndorseStamp", "EndorseStamp", null, new AjaxOptions { UpdateTargetId = "detailwindow-content", OnSuccess = "SuccessUpdateDetail" }))
   {%>
            <input name="endorseid" type="hidden" value="<%: Model.EndorseId %>" />
            <input name="id" type="hidden" value="<%: Model.Id %>" />
            <%= Html.AntiForgeryToken()%>
            <input id="btnDetlDelete" type="submit" value="Delete" style="display:none;"/>
<% } %>
<% } %>
</div>
</div>

<%--ต้องใส่ js ตัวนี้เข้าไปเพื่อให้จัดการปุ่ม menu ด้านบนได้ถูกต้อง--%>
<%= Html.JavascriptTag("nns/NNSModalDetailClient",true)  %>
