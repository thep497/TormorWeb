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
        <%: Html.ComboBoxRef_NameFor(model => model.InMethod,8) %>
        <%: Html.ValidationMessageFor(model => model.InMethod) %>
    </td>
            
    <td class="editor-label" style="width:70px">
        <%: Html.LabelFor(model => model.InWay) %>
    </td>
    <td class="editor-field">
        <%: Html.TextBoxFor(model => model.InWay) %>
        <%: Html.ValidationMessageFor(model => model.InWay) %>
    </td>
</tr></table>