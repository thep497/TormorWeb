<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.DomainModel.ConveyanceInOut>" %>

   <div class="generaleditform conveyancesinout-edit">
     <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(false) %>
        
        <table class="generaleditform">
        <tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.InOutType) %>
            </td>
            <td class="editor-field">
                <%: Html.Hidden("OldId",Model.Id) %>
                <%: Html.HiddenFor(model => model.Code, new { style = "width:50px" })%>
                <%: Html.Hidden("InOutType_Old", Model.InOutType)%>
                <%: Html.ComboBoxRef_CodeNameFor(model => model.InOutType, ModelConst.REF_CONVINOUT, new { OnChange = "ConveyanceInOutChange" }, new { style="width:90px"})%>
                <%: Html.ValidationMessageFor(model => model.InOutType) %>
            </td>
            
            <td class="editor-label">
                <%: Html.LabelFor(model => model.RequestDate) %>
            </td>
            <td class="editor-field">
                <%: Html.EditorFor(model => model.RequestDate) %>
                <%: Html.ValidationMessageFor(model => model.RequestDate) %>
            </td>

            </tr><tr>

            <td class="editor-label editbreak" style="vertical-align:top">
                <%: Html.LabelFor(model => model.ConveyanceId) %>
            </td>
            <td class="editor-field editbreak" colspan="3">
                <% 
                    if ((ViewData["ConveyanceSearch_OwnerName"] == null) && (Model != null) && (Model.Conveyance != null))
                    {
                        ViewData["ConveyanceSearch_OwnerName"] = Model.Conveyance.OwnerName;
                    }
                    if ((ViewData["ConveyanceSearch_Name"] == null) && (Model != null) && (Model.Conveyance != null))
                    {
                        ViewData["ConveyanceSearch_Name"] = Model.Conveyance.Name;
                    } 
                %>
                <%: Html.HiddenFor(model => model.ConveyanceId) %>
                <%  Html.RenderPartial("ConveyanceEdit", new Tormor.Web.Models.ConveyanceViewModel(Model.Conveyance)); %>
                <%: Html.ValidationMessageFor(model => model.ConveyanceId) %>
            </td>
            
        </tr><tr>

            <td class="editor-label">
                <%: Html.LabelFor(model => model.InOutDate) %>
            </td>
            <td class="editor-field" colspan="3">
                <%: Html.EditorFor(model => model.InOutDate)%>
                <%: Html.EditorFor(model => model.InOutTime)%>
                <%: Html.ValidationMessageFor(model => model.InOutDate) %>
                <%: Html.ValidationMessageFor(model => model.InOutTime) %>
            </td>
        </tr><tr class="portin">

            <td class="editor-label">
                <%: Html.LabelFor(model => model.PortInFrom) %>
            </td>
            <td class="editor-field" colspan="3">
                <%: Html.EditorFor(model => model.PortInFrom)%>
                <%: Html.ValidationMessageFor(model => model.PortInFrom)%>
            </td>
            </tr><tr class="portin">

            <td class="editor-label">
                <%: Html.LabelFor(model => model.PortInTo) %>
            </td>
            <td class="editor-field" colspan="3">
                <%: Html.EditorFor(model => model.PortInTo,"PortDetailThai")%>
                <%: Html.ValidationMessageFor(model => model.PortInTo)%>
            </td>
            </tr><tr class="portout">

            <td class="editor-label">
                <%: Html.LabelFor(model => model.PortOutFrom) %>
            </td>
            <td class="editor-field" colspan="3">
                <%: Html.EditorFor(model => model.PortOutFrom, "PortDetailThai")%>
                <%: Html.ValidationMessageFor(model => model.PortOutFrom)%>
            </td>
            </tr><tr class="portout">

            <td class="editor-label">
                <%: Html.LabelFor(model => model.PortOutTo) %>
            </td>
            <td class="editor-field" colspan="3">
                <%: Html.EditorFor(model => model.PortOutTo)%>
                <%: Html.ValidationMessageFor(model => model.PortOutTo)%>
            </td>
            </tr><tr>

            <td class="editor-label">
                <%: Html.LabelFor(model => model.AgencyName) %>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.AgencyName, new { style="width:150px;"})%>
                <%: Html.ValidationMessageFor(model => model.AgencyName) %>
            </td>

            <td class="editor-label">
                <%: Html.LabelFor(model => model.InspectOfficer) %>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.InspectOfficer, new { style = "width:150px;" })%>
                <%: Html.ValidationMessageFor(model => model.InspectOfficer) %>
            </td>
            
            </tr><tr>

            <td class="editor-label">
                <%: Html.LabelFor(model => model.NumCrew) %>
            </td>
            <td class="editor-field">
                <%: Html.EditorFor(model => model.NumCrew)%>
                <%: Html.ValidationMessageFor(model => model.NumCrew) %>
            </td>
            
            <td class="editor-label">
                <%: Html.LabelFor(model => model.NumPassenger) %>
            </td>
            <td class="editor-field">
                <%: Html.EditorFor(model => model.NumPassenger)%>
                <%: Html.ValidationMessageFor(model => model.NumPassenger) %>
            </td>

            </tr><tr>
            
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
