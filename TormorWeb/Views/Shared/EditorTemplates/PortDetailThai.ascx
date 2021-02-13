<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.DomainModel.PortDetail>" %>
<div class="portdetail-form">
    <table class="generaleditsubform"><tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.PortName) %>
            </td>
            <td class="editor-field">
                <%: Html.ComboBoxRef_Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty)+".PortName",
                        Model.PortName, 15, null, new { style = "width:180px" })%>
                <%: Html.ValidationMessageFor(model => model.PortName)%>
            </td>
            
            <td class="editor-label">
                <%: Html.LabelFor(model => model.Country) %>
            </td>
            <td class="editor-field"> 
                <%: Html.TextBox("Country", 
                        "ประเทศไทย", new { style = "width:120px", @readonly = "readonly", @class = "readonly" })%>
                <%: Html.ValidationMessageFor(model => model.Country)%>
            </td>
            
    </tr></table>
</div>
