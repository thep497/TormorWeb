<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.DomainModel.AddRemoveCrew>" %>

<% 
    ViewData["InCrewPage"] = true;  //th20110408 PD18-540102 Req 2 เอาไว้ตรวจสอบว่าจะแสดง IDCard/Seaman หรือไม่ ในหน้า AlienEditDetail
    string CrewInEditor, CrewOutEditor;
    if ((int?)ViewData["AddRemoveType"] == 1)
    {
        CrewInEditor = "CrewInOut";
        CrewOutEditor = "CrewInOutShip";
    }
    else
    {
        CrewInEditor = "CrewInOutShip";
        CrewOutEditor = "CrewInOut";
    }
%>
   <div class="generaleditform addremovecrew<%= Tormor.Web.Models.AddRemoveCrewHelper.AddRemoveTypeStr(ViewData["AddRemoveType"]) %>-edit">
   <%-- <% Html.EnableClientValidation(); %>--%>
     <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        
        <table class="generaleditform">
        <tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.Code) %>
            </td>
            <td class="editor-field">
                <%: Html.Hidden("OldId",Model.Id) %>
                <%: Html.Hidden("addRemoveType",ViewData["AddRemoveType"]) %>
                <%: Html.TextBoxFor(model => model.Code, new { style = "width:50px" })%>
                <%: Html.ValidationMessageFor(model => model.Code) %>
            </td>  
            <td class="editor-label">
                <%: Html.LabelFor(model => model.RequestDate) %>
            </td>
            <td class="editor-field">
                <%: Html.EditorFor(model => model.RequestDate)%>
                <% ViewData["CalcAgeDate"] = Model.RequestDate; %>
                <%: Html.ValidationMessageFor(model => model.RequestDate) %>
            </td>

        </tr>
        <tr>
            <td class="editor-label" style="width:100px">
                <%: Html.LabelFor(model => model.SubCode) %>
            </td>
            <td class="editor-field" style="width:80px">
                <%: Html.TextBoxFor(model => model.SubCode, new { style = "width:40px" })%>
                <%: Html.ValidationMessageFor(model => model.SubCode) %>
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
                <%: Html.LabelFor(model => model.Company) %>
            </td>
            <td class="editor-field" colspan="3">
                <%: Html.TextBoxFor(model => model.Company, new { style = "width:300px" })%>
                <%: Html.ValidationMessageFor(model => model.Company)%>
            </td>
        </tr>

        <tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.InDetail) %>
            </td>
            <td class="editor-field" colspan="3">
                <%: Html.EditorFor(model => model.InDetail,CrewInEditor)%>
                <%: Html.ValidationMessageFor(model => model.InDetail)%>
            </td>
        </tr>

        <tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.OutDetail) %>
            </td>
            <td class="editor-field" colspan="3">
                <%: Html.EditorFor(model => model.OutDetail,CrewOutEditor)%>
                <%: Html.ValidationMessageFor(model => model.OutDetail)%>
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
        //ไม่มีการเช็ค code ซ้ำ เพราะมี code ย่อยด้วย แต่จะไปตรวจสอบใน validate
        SetShowOnly();
    });

    function SetShowOnly() {
        $('.showonly-addremovecrew').show();
    }
</script>

