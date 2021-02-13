<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.DomainModel.Staying90Day>" %>

   <div class="generaleditform stay-edit">
   <%-- <% Html.EnableClientValidation(); %>--%>
     <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        
        <table class="generaleditform">
        <tr>
<%--            <td class="editor-label">
                <%: Html.LabelFor(model => model.Code) %>
            </td>
--%>
            <td class="editor-field" colspan="4">
                <%: Html.Hidden("OldId",Model.Id) %>
                <%: Html.HiddenFor(model => model.Code, new { style = "width:50px" })%>
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
                <%: Html.LabelFor(model => model.ArrivalDate) %>
            </td>
            <td class="editor-field">
                <%: Html.EditorFor(model => model.ArrivalDate)%>
                <%: Html.ValidationMessageFor(model => model.ArrivalDate)%>
            </td>

            <td class="editor-label">
                <%: Html.LabelFor(model => model.ArrivalBy) %>
            </td>
            <td class="editor-field">
                <%: Html.EditorFor(model => model.ArrivalBy,"CBArrivalBy")%>
                <%: Html.ValidationMessageFor(model => model.ArrivalBy)%>
            </td>
        </tr>

        <tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.ArrivalCard) %>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.ArrivalCard.DocNo, new { style = "width:150px" })%>
                <%: Html.ValidationMessageFor(model => model.ArrivalCard)%>
            </td>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.VisaType) %>
            </td>
            <td class="editor-field">
                <%: Html.DropDownListRef_CodeNameFor(model => model.VisaType, 102)%>
                <%: Html.ValidationMessageFor(model => model.VisaType)%>
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
        SetShowOnly();
    });

    function SetShowOnly() {
        $('.showonly-stay').show();
    }
</script>

