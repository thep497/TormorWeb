<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.DomainModel.IDDocument>" %>
<div class="iddocument-form">
<table class="generaleditsubform"><tr>
    <td class="editor-label">
        <%: Html.LabelFor(model => model) %>
    </td>
    <td class="editor-field">
        <%: Html.TextBoxFor(model => model.DocNo) %>
        <%: Html.ValidationMessageFor(model => model.DocNo) %>
    </td>
            
    <td class="editor-label showonly-endorse">
        <%: Html.LabelFor(model => model.DateIssued) %>
    </td>
    <td class="editor-field showonly-endorse">
        <%: Html.EditorFor(model => model.DateIssued) %>
        <%: Html.ValidationMessageFor(model => model.DateIssued) %>
    </td>
            
    <td class="editor-label showonly-endorse">
        <%: Html.LabelFor(model => model.DateExpired) %>
    </td>
    <td class="editor-field showonly-endorse">
        <%: Html.EditorFor(model => model.DateExpired)%>
        <%: Html.ValidationMessageFor(model => model.DateExpired)%>
    </td>
            
    <td class="editor-label showonly-endorse">
        <%: Html.LabelFor(model => model.IssuedFrom) %>
    </td>
    <td class="editor-field showonly-endorse">
        <%: Html.TextBoxFor(model => model.IssuedFrom, new { style="width:120px;"})%>
        <%: Html.ValidationMessageFor(model => model.IssuedFrom) %>
    </td>
</tr></table>
</div>