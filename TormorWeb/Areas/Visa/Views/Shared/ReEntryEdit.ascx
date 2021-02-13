<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.DomainModel.ReEntry>" %>

   <div class="generaleditform reentry-edit">
   <%-- <% Html.EnableClientValidation(); %>--%>
     <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        
        <table class="generaleditform">
        <tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.Code) %>
            </td>
            <td class="editor-field" colspan="3">
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

            <td class="editor-label" style="width:80px">
            </td>
            <td class="editor-field">
            </td>
        </tr>
        <tr>
            <td class="editor-label editbreak" style="vertical-align:top">
                <%: Html.LabelFor(model => model.AlienId) %>
            </td>
            <td class="editor-field editbreak" colspan="3">
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
            <td class="editor-label">
                <%: Html.LabelFor(model => model.ReEntryCode) %>
            </td>
            <td class="editor-field">
                <%: Html.ComboBoxRef_NameFor(model => model.ReEntryCode,9) %>
                <%: Html.ValidationMessageFor(model => model.ReEntryCode)%>
            </td>

            <td class="editor-label">
                <%: Html.LabelFor(model => model.SMTime) %>
            </td>
            <td class="editor-field">
            <%: Html.DropDownListRef_NameFor(model => model.SMTime, 10, new { OnChange = "onSMTimeChange" }, new { style = "width:100px" }) %>
            <%: Html.ValidationMessageFor(model => model.SMTime)%>
            </td>
        </tr>

        <tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.Invoice.InvoiceNo) %>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.Invoice.InvoiceNo, new { style = "width:150px" })%>
                <%: Html.ValidationMessageFor(model => model.Invoice.InvoiceNo)%>
            </td>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.Invoice.Charge) %>
            </td>
            <td class="editor-field">
                <%-- ต้องใช้ textbox เพราะตอนให้ค่ากลับจาก reference เราใช้ val() --%>
                <%: Html.TextBoxFor(model => model.Invoice.Charge, new { style = "width:80px;text-align:left;", @readonly = "true" })%>  
                <%: Html.ValidationMessageFor(model => model.Invoice.Charge)%>
            </td>
        </tr>
        <tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.PermitToDate) %>
            </td>
            <td class="editor-field" colspan="3">
                <%: Html.EditorFor(model => model.PermitToDate)%>
                <%: Html.ValidationMessageFor(model => model.PermitToDate) %>
            </td>
        </tr>
        <tr>
            <td class="editor-label">
                หมายเหตุ 
            </td>
            <td class="editor-field" colspan="3">
                <%: Html.TextAreaFor(model => model.ExtendedData.Custom1, new { rows=3, cols=75 })%>
                <%: Html.ValidationMessageFor(model => model.ExtendedData.Custom1)%>
            </td>
        </tr>

        <%--แสดงข้อมูล passport และที่อยู่เก่า--%>
        <tr>
        <td class="editor-field" style="border-width:1px;" colspan="4">
        <em>ข้อมูล ณ วันรับคำร้อง</em><br />
        <% if ((ViewData["CalcAgeDate"] != null) && (Model != null) && (Model.Alien != null) && (Model.Alien.DateOfBirth != null)) { %>
        <strong>อายุ:</strong> <%: (Model.Alien.DateOfBirth ?? DateTime.Today).CalcAgeYear((DateTime)ViewData["CalcAgeDate"]) %> ปี (ณ วันรับคำร้อง)<br />
        <% } %>
        <strong>เลขที่ Passport:</strong> <%: Model.PassportCard.FullDetail(Globals.DateFormat)%> <br />
        </td>
        </tr>
        </table>
        <input id="btnSave" type="submit" value="Save" style="visibility:hidden;" />
        <p></p><p></p>
    <% } %>
  </div>
<%-- กรณีมี Detail ต้องเรียก RenderPartial ModalDetailWindow และต้องอยู่นอกฟอร์มด้วย --%>
<% Html.RenderPartial("ModalDetailWindow"); %>
<%= Html.DefineEditForm() %> 

<script type="text/javascript">
    $(document).ready(function () {
        $('#Code').bind('change', function () {
            checkDupCode('<%= Url.Action("_GetReEntryCodeDetail") %>', '<%= Url.Action("ReEdit") %>');
        });
        SetShowOnly();
    });

    function SetShowOnly() {
        $('.showonly-reentry').show();
    }


//for SMTime
function refreshSMTime(sMType) {
    if ((sMType != null) && (sMType != "")) {
        $.post('<%= Url.Action("_GetSMMoney") %>',
        { text: sMType },
        function (data) {
            $("#Invoice_Charge").val(data);
            //$("#Invoice_Charge").text(data);
        });
    }
}
function onSMTimeChange() {
    DSDropDownList_Change($(this));
    var sMType = $(this).data("tDropDownList").value();
    refreshSMTime(sMType);
}

//เอาค่า PermitToDate จาก Visa มาใส่ใน re-entry
function _doOtherSearchAction(valienid) {
    var drdate = $('#RequestDate').data('tDatePicker');
    var dpdate = $('#PermitToDate').data('tDatePicker');
    var vpdate, vrdate;

    if ((drdate != null) && (drdate.value() != null))
        vrdate = drdate.value().f(glo_DateFormat);
    if ((dpdate != null) && (dpdate.value() != null))
        vpdate = dpdate.value().f(glo_DateFormat);

    //ถ้าเงื่อนไขเหมาะสม ให้ post ajax ถามไปที่ server
    if ((valienid > 0) && (vrdate == vpdate)) {
        $.post('<%= Url.Action("_GetLastPermitDate") %>',
            { alienid: valienid, rdate: vrdate, pdate: vpdate },
            function (data) {
                var newdate = parseMSJsonDateTime(data.permittodate);
                $('#PermitToDate').data('tDatePicker').value(newdate);
            });
    }
}
</script>

