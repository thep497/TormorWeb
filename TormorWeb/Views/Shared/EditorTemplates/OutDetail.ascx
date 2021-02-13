<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.DomainModel.OutDetail>" %>
<div class="outdetail-form">
<table class="generaleditsubform">
<tr>
        <td class="editor-label" style="width:100px">
            <%: Html.LabelFor(model => model.Destination) %>
        </td>
        <td class="editor-field">
            <%: Html.TextBoxFor(model => model.Destination) %>
            <%: Html.ValidationMessageFor(model => model.Destination) %>
        </td>
            
        <td class="editor-label">
            <%: Html.LabelFor(model => model.ByVehicle) %>
        </td>
        <td class="editor-field">
            <%: Html.EditorFor(model => model.ByVehicle,"CBArrivalBy") %>
            <%: Html.ValidationMessageFor(model => model.ByVehicle) %>
        </td>
            
        <td class="editor-label">
            <%: Html.LabelFor(model => model.OutDate) %>
        </td>
        <td class="editor-field">
            <%: Html.EditorFor(model => model.OutDate) %>
            <%: Html.ValidationMessageFor(model => model.OutDate) %>
        </td>
</tr>            
</table>
</div>