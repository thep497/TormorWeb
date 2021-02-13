<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.DomainModel.CrewInOut>" %>

<table><tr>
    <td class="editor-label">
        <%: Html.LabelFor(model => model.InDate) %>
    </td>
    <td class="editor-field">
        <%: Html.EditorFor(model => model.InDate) %>
        <%: Html.ValidationMessageFor(model => model.InDate) %>
    </td>
            
    <td class="editor-label">
        <%: Html.LabelFor(model => model.InMethod) %>
    </td>
    <td class="editor-field">
        <%: Html.TextBox("InMethod", 
                         "เรือ", new { style = "width:120px", @readonly = "readonly", @class = "readonly" })%>
        <%: Html.ValidationMessageFor(model => model.InMethod) %>
    </td>
            
    <td class="editor-label" style="width:70px">
        ชื่อเรือ
    </td>
    <td class="editor-field">
        <%: Html.TextBoxFor(model => model.InWay) %>
        <%: Html.ValidationMessageFor(model => model.InWay) %>
    </td>
</tr></table>