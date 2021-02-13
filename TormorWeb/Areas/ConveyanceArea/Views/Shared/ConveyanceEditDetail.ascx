<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.Web.Models.ConveyanceViewModel>" %>

<div id="conveyance-form">
  <em>เพิ่มเติม/แก้ไขข้อมูลพาหนะ</em>
  <table >
    <tr>
        <td class="editor-label">
            <%: Html.LabelFor(model => model.Conveyance.Name) %>
        </td>
        <td class="editor-field">
            <%: Html.HiddenFor(model => model.Conveyance.Id) %>
            <%: Html.TextBoxFor(model => model.Conveyance.Name, new { style="width:300px;" })%>
            <%: Html.ValidationMessageFor(model => model.Conveyance.Name) %>
        </td>

    </tr><tr>
            
        <td class="editor-label">
            <%: Html.LabelFor(model => model.Conveyance.RegistrationNo) %>
        </td>
        <td class="editor-field">
            <%: Html.TextBoxFor(model => model.Conveyance.RegistrationNo, new { style = "width:300px;" })%>
            <%: Html.ValidationMessageFor(model => model.Conveyance.RegistrationNo) %>
        </td>
            
    </tr><tr>

        <td class="editor-label">
            <%: Html.LabelFor(model => model.Conveyance.OwnerName) %>
        </td>
        <td class="editor-field">
            <%: Html.TextBoxFor(model => model.Conveyance.OwnerName, new { style = "width:300px;" })%>
            <%: Html.ValidationMessageFor(model => model.Conveyance.OwnerName) %>
        </td>
            
  </tr></table>
</div>
