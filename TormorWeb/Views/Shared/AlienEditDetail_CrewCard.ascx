<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.Web.Models.AlienViewModel>" %>

<div class="crewcard-form">
<table class="generaleditsubform">
    <tr class="showonly-crew showonly-addremovecrew">
    <td class="editor-label">
        <%: Html.LabelFor(model => model.Alien.PassportCard) %>
    </td>
    <td class="editor-field">
        <%: Html.TextBoxFor(model => model.Alien.PassportCard.DocNo, new { style="width:80px;" })%>
        <%: Html.ValidationMessageFor(model => model.Alien.PassportCard.DocNo)%>
    </td>
    <td class="editor-label">
        <%: Html.LabelFor(model => model.Alien.IDCardNo) %>
    </td>
    <td class="editor-field">
        <%: Html.TextBoxFor(model => model.Alien.IDCardNo, new { style = "width:80px;" })%>
        <%: Html.ValidationMessageFor(model => model.Alien.IDCardNo)%>
    </td>
    <td class="editor-label">
        <%: Html.LabelFor(model => model.Alien.SeamanCardNo) %>
    </td>
    <td class="editor-field">
        <%: Html.TextBoxFor(model => model.Alien.SeamanCardNo, new { style = "width:80px;" })%>
        <%: Html.ValidationMessageFor(model => model.Alien.SeamanCardNo)%>
    </td>
    </tr>
</table>
</div>    
