<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.DomainModel.Address>" %>
<div class="address-form">
    <table class="generaleditsubform"><tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.AddrNo) %>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.AddrNo, new { style = "width:120px" })%>
                <%: Html.ValidationMessageFor(model => model.AddrNo) %>
            </td>
            
            <td class="editor-label">
                <%: Html.LabelFor(model => model.Road) %>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.Road, new { style = "width:100px" })%>
                <%: Html.ValidationMessageFor(model => model.Road) %>
            </td>
            
            <td class="editor-label">
                <%: Html.LabelFor(model => model.Tumbol) %>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.Tumbol, new { style = "width:80px" })%>
                <%: Html.ValidationMessageFor(model => model.Tumbol) %>
            </td>
            </tr>

            <tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.Amphur) %>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.Amphur, new { style = "width:100px" })%>
                <%: Html.ValidationMessageFor(model => model.Amphur) %>
            </td>
            
            <td class="editor-label">
                <%: Html.LabelFor(model => model.Province) %>
            </td>
            <td class="editor-field">
                <%: Html.EditorFor(model => model.Province)%>
                <%: Html.ValidationMessageFor(model => model.Province) %>
            </td>
            
            <td class="editor-label">
                <%: Html.LabelFor(model => model.Postcode) %>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.Postcode, new { style = "width:50px" })%>
                <%: Html.ValidationMessageFor(model => model.Postcode) %>
            </td>
            </tr>

            <tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.Phone) %>
            </td>
            <td class="editor-field" colspan="5">
                <%: Html.TextBoxFor(model => model.Phone, new { style = "width:480px" })%>
                <%: Html.ValidationMessageFor(model => model.Phone) %>
            </td>
    </tr></table>
</div>
