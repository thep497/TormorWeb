<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.DomainModel.PersonName>" %>
<div class="personname-form">
    <table class="generaleditsubform"><tr>   <%--personname-form--%>
    <%--<td class="editor-label">
        <%: Html.LabelFor(model => model.Title) %>
    </td>--%>
    <td class="editor-field">
        <%: Html.EditorFor(model => model.Title)%>
        <%: Html.ValidationMessageFor(model => model.Title) %>
    </td>
            
    <td class="editor-label">
        <%: Html.LabelFor(model => model.FirstName) %>
    </td>
    <td class="editor-field">
        <%: Html.TextBoxFor(model => model.FirstName, new { style = "width:130px" })%>
        <%: Html.ValidationMessageFor(model => model.FirstName) %>
    </td>
            
    <td class="editor-label">
        <%: Html.LabelFor(model => model.MiddleName) %>
    </td>
    <td class="editor-field">
        <%: Html.TextBoxFor(model => model.MiddleName, new { style = "width:80px" })%>
        <%: Html.ValidationMessageFor(model => model.MiddleName) %>
    </td>
            
    <td class="editor-label">
        <%: Html.LabelFor(model => model.LastName) %>
    </td>
    <td class="editor-field">
        <%: Html.TextBoxFor(model => model.LastName, new { style = "width:200px" })%>
        <%: Html.ValidationMessageFor(model => model.LastName) %>
    </td>
    </tr></table>
</div>