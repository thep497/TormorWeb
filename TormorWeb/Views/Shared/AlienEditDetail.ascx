<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.Web.Models.AlienViewModel>" %>

<%
    var inCrewPage = (bool)(ViewData["InCrewPage"] ?? false);
%>
<div id="alien-form">
  <em>เพิ่มเติม/แก้ไขข้อมูลบุคคลต่างด้าว</em>
  <table>
    <% if (!inCrewPage) { %> <%--//th20110408 PD18-540102 Req 2 เอาไว้ตรวจสอบว่าจะแสดง IDCard/Seaman หรือไม่--%>
    <tr>
    <td class="editor-field" colspan="7">
        <%--<%: Html.LabelFor(model => model.Alien.PassportCard) %>--%>
        <%: Html.EditorFor(model => model.Alien.PassportCard, "IDDocument")%>
        <%: Html.ValidationMessageFor(model => model.Alien.PassportCard)%>
    </td>
    </tr>
    <% } else { %>
    <tr>
    <td class="editor-field" colspan="7">
        <% Html.RenderPartial("AlienEditDetail_CrewCard"); //th20110407 PD18-540102 Req 2 %>
    </td>
    </tr>
    <% } %>

    <tr>
    <td class="editor-field" colspan="7">
        <%: Html.HiddenFor(model => model.Alien.Id) %> <%--ต้องมีเพื่อให้ update กลับได้ถูกต้อง--%>
        <%: Html.HiddenFor(model => model.Alien.Name.FullName) %> <%-- เอาไว้แสดงค่ากลับในช่อง search --%>
        <%: Html.EditorFor(model => model.Alien.Name) %>
        <%: Html.ValidationMessageFor(model => model.Alien.Name) %>
    </td>
    </tr>
    
    <tr>
    <td class="editor-label">
        <%: Html.LabelFor(model => model.Alien.Sex) %>
    </td>
    <td class="editor-field">
        <%: Html.EditorFor(model => model.Alien.Sex) %>
        <%: Html.ValidationMessageFor(model => model.Alien.Sex) %>
    </td>
            
    <td class="editor-label">
        <%: Html.LabelFor(model => model.Alien.DateOfBirth) %>
    </td>
    <td class="editor-field">
        <%: Html.EditorFor(model => model.Alien.DateOfBirth, new { onchange = "onDateOfBirthChange" })%>
        <span id="alien-age">
        <% if (Model != null) { %>
        อายุ <%: Html.Telerik().IntegerTextBoxFor(model => model.Alien.Age)
                             //.Name("AlienAge")
                             .MinValue(0)
                             .MaxValue(150)
                             .InputHtmlAttributes(new { style = "width:30px" })
                             .Spinners(false)
                             .ClientEvents(events => events.OnChange("onAgeChange"))
                             .Value((Model.Alien.DateOfBirth ?? DateTime.Today).CalcAgeYear())%> ปี
        <% } %>
        </span>
        <%: Html.ValidationMessageFor(model => model.Alien.DateOfBirth) %>
    </td>

    <td class="editor-label">
        <%: Html.LabelFor(model => model.Alien.Nationality) %>
    </td>
    <td class="editor-field">
        <%: Html.EditorFor(model => model.Alien.Nationality) %>
        <%: Html.ValidationMessageFor(model => model.Alien.Nationality) %>
    </td>

    <td class="editor-field showonly-crew">
        <%: Html.EditorFor(model => model.Alien.IsThai) %>
        <%: Html.LabelFor(model => model.Alien.IsThai) %>
        <%: Html.ValidationMessageFor(model => model.Alien.IsThai) %>
    </td>

    </tr>
    
    <tr>
    <td class="editor-field showonly-endorse" colspan="7">
        <%: Html.EditorFor(model => model.Alien.HabitatCard,"IDDocument")%>
        <%: Html.ValidationMessageFor(model => model.Alien.HabitatCard)%>
    </td>
    </tr>

    <tr id="alien_currentaddress">
    <td class="editor-field showonly-visa showonly-endorse showonly-stay" colspan="7">
        <%: Html.LabelFor(model => model.Alien.CurrentAddress)%>
        <%: Html.EditorFor(model => model.Alien.CurrentAddress)%>
        <%: Html.ValidationMessageFor(model => model.Alien.CurrentAddress)%>
    </td>
  </tr></table>
</div>

<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        InitialAlienEdit();
    });
</script>