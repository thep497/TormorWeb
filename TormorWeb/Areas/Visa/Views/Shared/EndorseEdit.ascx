<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.DomainModel.Endorse>" %>

   <div class="generaleditform endorse-edit">
   <%-- <% Html.EnableClientValidation(); %>--%>
     <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        
        <table class="generaleditform">
        <tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.Code) %>
            </td>
            <td class="editor-field" colspan="5">
                <%: Html.Hidden("OldId",Model.Id) %>
                <%: Html.TextBoxFor(model => model.Code, new { style = "width:50px" })%>
                <%: Html.ValidationMessageFor(model => model.Code) %>
            </td>  
        </tr>

        <tr>
            <td class="editor-label" style="width:100px">
                <%: Html.LabelFor(model => model.RequestDate) %>
            </td>
            <td class="editor-field" style="width:100px">
                <%: Html.EditorFor(model => model.RequestDate)%>
                <% ViewData["CalcAgeDate"] = Model.RequestDate; %>
                <%: Html.ValidationMessageFor(model => model.RequestDate) %>
            </td>

            <td class="editor-label" style="width:100px">
                <%: Html.LabelFor(model => model.ExpiredDate) %>
            </td>
            <td class="editor-field" style="width:100px">
                <%: Html.EditorFor(model => model.ExpiredDate)%>
                <%: Html.ValidationMessageFor(model => model.ExpiredDate)%>
            </td>

            <td class="editor-label" style="width:80px">
                <%: Html.LabelFor(model => model.TMType) %>
            </td>
            <td class="editor-field">
                <%: Html.ComboBoxRef_NameFor(model => model.TMType,12,null, new { style = "width:100px" })%>
                <%: Html.ValidationMessageFor(model => model.TMType)%>
            </td>
        </tr>

        <tr>
            <td class="editor-label editbreak" style="vertical-align:top">
                <%: Html.LabelFor(model => model.AlienId) %>
            </td>
            <td class="editor-field editbreak" colspan="5">
                <% 
                    if ((ViewData["AlienSearch_Passport"] == null) && (Model != null) && (Model.Alien != null))
                        ViewData["AlienSearch_Passport"] = Model.Alien.PassportCard.DocNo;
                    if ((ViewData["AlienSearch_Name"] == null) && (Model != null) && (Model.Alien != null))
                        ViewData["AlienSearch_Name"] = Model.Alien.Name.FullName;
                %>
                <%: Html.HiddenFor(model => model.AlienId) %>
                <%  Html.RenderPartial("AlienEdit", new Tormor.Web.Models.AlienViewModel(Model.Alien)); %>
                <%: Html.ValidationMessageFor(model => model.AlienId) %>
            </td>
            
        </tr>

        <tr>
            <td class="editor-field editbreak" style="width:100px;padding:0" colspan="6">
                <%: Html.EditorFor(model => model.OutDetail,"OutDetail") %>
            </td>
        </tr>

        <tr>
            <td class="editor-label editbreak" style="vertical-align:top">
                <em><%: Html.LabelFor(model => model.EndorseStamps) %></em>
            </td>
            <td class="editor-field editbreak" colspan="5">
                <% Html.RenderPartial("EndorseStampList", new Tormor.Web.Models.EndorseStampViewModel(Model.Id,Model.EndorseStamps)); %> 
                <%--<% Html.RenderPartial("EndorseStampEdit", new Tormor.DomainModel.EndorseStamp(Model.Id)); %>--%> 
            </td>
        </tr>

<%--        <tr>
            <td class="editor-label">
                หมายเหตุ 
            </td>
            <td class="editor-field" colspan="5">
                <%: Html.TextAreaFor(model => model.ExtendedData.Custom1, new { rows=3, cols=75 })%>
                <%: Html.ValidationMessageFor(model => model.ExtendedData.Custom1)%>
            </td>
        </tr>
--%>
        <%--แสดงข้อมูล passport และที่อยู่เก่า--%>
        <tr>
        <td class="editor-field" style="border-width:1px;" colspan="6">
        <em>ข้อมูล ณ วันรับคำร้อง</em><br />
        <% if ((ViewData["CalcAgeDate"] != null) && (Model != null) && (Model.Alien != null) && (Model.Alien.DateOfBirth != null)) { %>
        <strong>อายุ:</strong> <%: (Model.Alien.DateOfBirth ?? DateTime.Today).CalcAgeYear((DateTime)ViewData["CalcAgeDate"]) %> ปี (ณ วันรับคำร้อง)<br />
        <% } %>
        <strong>เลขที่ Passport:</strong> <%: Model.PassportCard.FullDetail(Globals.DateFormat) %> <br />
        <strong>เลขที่ใบสำคัญถิ่นที่อยู่:</strong> <%: Model.HabitatCard.FullDetail(Globals.DateFormat)%> <br />
        <strong>ที่อยู่:</strong> <%: Model.CurrentAddress.FullAddress %>
        </td>
        </tr>
        </table>
        <input id="btnSave" type="submit" value="Save" style="display:none;" />
        <p></p><p></p>
    <% } %>
  </div>
<%-- กรณีมี Detail ต้องเรียก RenderPartial ModalDetailWindow และต้องอยู่นอกฟอร์มด้วย --%>
<% Html.RenderPartial("ModalDetailWindow"); %>
<%= Html.DefineEditForm() %> 

<script type="text/javascript">
    $(document).ready(function () {
        $('#Code').bind('change', function () {
            checkDupCode('<%= Url.Action("_GetEndorseCodeDetail") %>', '<%= Url.Action("ReEdit") %>'); 
        });
        SetShowOnly();
    });

function SetShowOnly() {
    $('.showonly-endorse').show();
}

//for SMTime ต้องเอามาอยู่ที่ view หลัก เพราะถ้าอยู่ใน window มันจะหา onSMTimeChange ไม่เจอ
function refreshSMTime(sMType) {
    if ((sMType != null) && (sMType != "")) {
        $.post('<%= Url.Action("_GetSMMoney","EndorseStamp") %>',
                { text: sMType },
                function (data) {
                    $("#Invoice_Charge").val(data);
                });
    }
}
function onSMTimeChange() {
    DSDropDownList_Change($(this));
    var sMType = $(this).data("tDropDownList").value();
    refreshSMTime(sMType);
}
</script>

