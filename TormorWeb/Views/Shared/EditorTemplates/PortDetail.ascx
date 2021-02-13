<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.DomainModel.PortDetail>" %>
<div class="portdetail-form">
    <table class="generaleditsubform"><tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.PortName) %>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.PortName, new { style = "width:180px" })%>
                <%: Html.ValidationMessageFor(model => model.PortName)%>
            </td>
            
            <td class="editor-label">
                <%: Html.LabelFor(model => model.Country) %>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.Country, new { style = "width:120px" })%>
                <%: Html.ValidationMessageFor(model => model.Country)%>
            </td>
            
    </tr></table>
</div>
