<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.DomainModel.VisaDetail>" %>

   <div class="generaleditform visa-edit">
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
                <%--<%: Html.EditorFor(model => model.Alien.Name.FullName) %>--%>
                <%: Html.ValidationMessageFor(model => model.Code) %>
            </td>  
        </tr>
        <tr>
            <%--Test Decimal--%>
<%--            <div class="editor-label">
                <%: Html.LabelFor(model => model.Invoice.Charge) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.Invoice.Charge)%>
                <%: Html.ValidationMessageFor(model => model.Invoice.Charge)%>
            </div>
--%>            <%--Test Decimal--%>

            <td class="editor-label" style="width:100px">
                <%: Html.LabelFor(model => model.RequestDate) %>
            </td>
            <td class="editor-field" style="width:100px">
                <%: Html.EditorFor(model => model.RequestDate)%>
                <% ViewData["CalcAgeDate"] = Model.RequestDate; %>
                <%: Html.ValidationMessageFor(model => model.RequestDate) %>
            </td>

            <td class="editor-label" style="width:80px">
                <%: Html.LabelFor(model => model.ResultAppointmentDate) %>
            </td>
            <td class="editor-field">
                <%: Html.EditorFor(model => model.ResultAppointmentDate)%>
                <%: Html.ValidationMessageFor(model => model.ResultAppointmentDate) %>
            </td>
        </tr>
        <tr>
            <td class="editor-label editbreak" style="vertical-align:top">
                <%: Html.LabelFor(model => model.AlienId) %>
            </td>
            <td class="editor-field editbreak" colspan="3">
<%--
                <% if ((Model != null) && (Model.Alien != null)) { %>
                <%: Model.Alien.Name.FullName + " " + Model.Alien.PassportCard.DocNo%>
                <% } %>
--%>            
                <% 
                    if ((ViewData["AlienSearch_Passport"] == null) && (Model != null) && (Model.Alien != null))
                        ViewData["AlienSearch_Passport"] = Model.Alien.PassportCard.DocNo;
                    if ((ViewData["AlienSearch_Name"] == null) && (Model != null) && (Model.Alien != null))
                        ViewData["AlienSearch_Name"] = Model.Alien.Name.FullName;
                %>
                <%: Html.HiddenFor(model => model.AlienId) %>
                <%--<%: Html.EditorFor(model => model.Alien) %>--%>
                <%  Html.RenderPartial("AlienEdit", new Tormor.Web.Models.AlienViewModel(Model.Alien)); %>
                <%: Html.ValidationMessageFor(model => model.AlienId) %>
            </td>
            
        </tr>
        <tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.StayPeriod) %>
            </td>
            <td class="editor-field">
                <%: Html.DropDownListRef_NameFor(model => model.StayPeriod, 2, new { OnChange = "onStayPeriodChange" })%>
<%--                <%: Html.Telerik().DropDownListFor(model => model.StayPeriod)
                        .DataBinding(bind => bind.Ajax().Select("_getReferenceComboDropDownAjax", "Reference", new { area = "reference", reftypeid = 2, value = Model.StayPeriod }))
                        .ClientEvents(events => events.OnChange("onStayPeriodChange")
                                                      .OnLoad("DSDropDownList_onLoad")
                                                      .OnDataBound("DSDropDownList_onDataBound"))
                %>
--%>
                <%: Html.ValidationMessageFor(model => model.StayPeriod) %>
            </td>
            <td class="editor-field" colspan="2">
                <span id="StayType-Str"> </span>
                <%: Html.HiddenFor(model => model.StayType) %>
                <%: Html.ValidationMessageFor(model => model.StayType) %>
            </td>
            
        </tr>
        <tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.StayReason) %>
            </td>
            <td class="editor-field">
                <%: Html.DropDownListRef_NameFor(model => model.StayReason, 3, new { OnChange = "onStayReasonChange" })%>
<%--                <%: Html.Telerik().DropDownListFor(model => model.StayReason)
                        .DataBinding(bind => bind.Ajax().Select("_getReferenceComboDropDownAjax", "Reference", new { area = "reference", reftypeid = 3, value = Model.StayReason }))
                        .ClientEvents(events => events.OnChange("onStayReasonChange")
                                                      .OnLoad("DSDropDownList_onLoad")
                                                      .OnDataBound("DSDropDownList_onDataBound"))
                %>
--%>
                <%: Html.ValidationMessageFor(model => model.StayReason) %>
            </td>
            <td class="editor-field" colspan="2">
                <%: Html.AutoCompleteRef_NameFor(model => model.StayReasonDetail, 4, Model.StayReason,300,15) %>
<%--                <%: Html.Telerik().AutoCompleteFor(model => model.StayReasonDetail)
                        .DataBinding(bind => bind.Ajax().Select("_getReferenceAutoCompleteAjax", "Reference", new { area = "reference", reftypeid = 4, refRefName = Model.StayReason }))
                        .AutoFill(true)
                        .Filterable(filter => filter.FilterMode(AutoCompleteFilterMode.Contains))
                        .HighlightFirstMatch(false)
                        .HtmlAttributes(new { style = "background-color:white; width:300px", value = Model.StayReasonDetail })
                %>
--%>
                <%: Html.ValidationMessageFor(model => model.StayReasonDetail) %>
            </td>
<%--                        //.ClientEvents(events => events.OnLoad("DSDropDownList_onLoad")
                        //                              .OnDataBound("DSDropDownList_onDataBound"))
--%>            
        </tr>
        <tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.DateArrive) %>
            </td>
            <td class="editor-field" colspan="3">
                <%: Html.EditorFor(model => model.DateArrive)%>
                <%: Html.ValidationMessageFor(model => model.DateArrive) %>
            </td>
        </tr>
        <tr>
            
            <td class="editor-label">
                <%: Html.LabelFor(model => model.IsPermit) %>
            </td>
            <td class="editor-field">
                <%: Html.EditorFor(model => model.IsPermit)%>
                <%: Html.ValidationMessageFor(model => model.IsPermit) %>
            </td>
            
            <td class="editor-label">
                <%: Html.LabelFor(model => model.PermitToDate) %>
            </td>
            <td class="editor-field">
                <%: Html.EditorFor(model => model.PermitToDate)%>
                <%: Html.ValidationMessageFor(model => model.PermitToDate) %>
            </td>
        </tr>

        <tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.Invoice) %>
            </td>
            <td class="editor-field" colspan="3">
                <%: Html.EditorFor(model => model.Invoice.InvoiceNo)%>
                <%: Html.ValidationMessageFor(model => model.Invoice.InvoiceNo)%>
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
        <strong>ที่อยู่:</strong> <%: Model.CurrentAddress.FullAddress %>
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
            checkDupCode('<%= Url.Action("_GetVisaCodeDetail") %>', '<%= Url.Action("ReEdit") %>');
        });
        SetShowOnly();
    });

    function SetShowOnly() {
        $('.showonly-visa').show();
    }

//for stayperiod
function refreshStayPeriod(stayPeriod) {
    if ((stayPeriod != null) && (stayPeriod != "")) {
        $.post('<%= Url.Action("_GetStayType") %>',
        { text: stayPeriod },
        function (data) {
            $("#StayType").val(data);
            $("#StayType-Str").text(data);
        });
    }
}
function onStayPeriodChange() {
    DSDropDownList_Change($(this));
    var stayPeriod = $(this).data("tDropDownList").value();
    refreshStayPeriod(stayPeriod);
}

//for stayreason
function refreshStayReasonDetail(stayReason) {
    var oldDetail = $("#StayReasonDetail").val();

    $.post('<%= Url.Action("_GetStayReasonDetail") %>',
        { text: stayReason },
        function (data) {
            //ค่าจะหายตอน databind เลยต้อง set อย่างนี้จะได้ไม่ขึ้น modify
            $("#StayReasonDetail").data('oldVal', "");
            $("#StayReasonDetail").data("tAutoComplete").dataBind(data); //เรียกตัวนี้เป็นหลัก
            $("#StayReasonDetail").data('oldVal', oldDetail);

            $("#StayReasonDetail").val(oldDetail);
        });
}
function onStayReasonChange() {
    DSDropDownList_Change($(this));
    var stayReason = $(this).data("tDropDownList").value();
    refreshStayReasonDetail(stayReason);
}

</script>

