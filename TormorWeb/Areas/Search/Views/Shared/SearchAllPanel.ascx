<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.DomainModel.AlienSearchInfo>" %>

<div id="SearchAllPanel">
<% using (Html.BeginForm("DoSearch", "Search", new { area = "Search" }, FormMethod.Post, new { id="MainSearch"}))
   { %>
    <table class="generaleditform">
        <tr>
            <td class="editor-field" colspan="4" style="border-width:1px;">
                <%: Html.CheckBoxFor(model => model.WantVisa)%>
                <%: Html.LabelFor(model => model.WantVisa)%>
                &nbsp;
                <%: Html.CheckBoxFor(model => model.WantReEntry)%>
                <%: Html.LabelFor(model => model.WantReEntry)%>
                &nbsp;
                <%: Html.CheckBoxFor(model => model.WantEndorse)%>
                <%: Html.LabelFor(model => model.WantEndorse)%>
                &nbsp;
                <%: Html.CheckBoxFor(model => model.WantStay)%>
                <%: Html.LabelFor(model => model.WantStay)%>
                &nbsp;
                <%: Html.CheckBoxFor(model => model.WantShip)%>
                <%: Html.LabelFor(model => model.WantShip)%>
            </td>
            <td class="editor-label" colspan="2">
                <button id="ClearSearch">Clear</button>
                <button id="SearchAll" type="submit">ค้นหาข้อมูล</button>
            </td>

            </tr><tr>

            <td class="editor-label">
                <%: Html.LabelFor(model => model.AlienName)%>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.AlienName)%>
                <%: Html.ValidationMessageFor(model => model.AlienName)%>
            </td>

            <td class="editor-label">
                <%: Html.LabelFor(model => model.CurrentAddress)%>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.CurrentAddress)%>
                <%: Html.ValidationMessageFor(model => model.CurrentAddress)%>
            </td>
            
            <td class="editor-label">
                <%: Html.LabelFor(model => model.Code)%>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.Code)%>
                <%: Html.ValidationMessageFor(model => model.Code)%>
            </td>
            
            </tr><tr>

            <td class="editor-label">
                <%: Html.LabelFor(model => model.Sex)%>
            </td>
            <td class="editor-field">
                <%: Html.ComboBoxRef_NameFor(model => model.Sex, 101)%>
                <%: Html.ValidationMessageFor(model => model.Sex)%>
            </td>
            
            <td class="editor-label">
                <%: Html.LabelFor(model => model.Nationality)%>
            </td>
            <td class="editor-field">
                <%: Html.EditorFor(model => model.Nationality)%>
                <%: Html.ValidationMessageFor(model => model.Nationality)%>
            </td>
            
            <td class="editor-label">
                <%: Html.LabelFor(model => model.AgeFrom)%>
            </td>
            <td class="editor-field">
                <%: Html.EditorFor(model => model.AgeFrom)%>
                &nbsp;-&nbsp;
                <%: Html.EditorFor(model => model.AgeTo)%>
                &nbsp;ปี
            </td>
            
            </tr><tr>

            <td class="editor-label">
                <%: Html.LabelFor(model => model.PassportNo_Old)%>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.PassportNo_Old)%>
                <%: Html.ValidationMessageFor(model => model.PassportNo_Old)%>
            </td>
            
            <td class="editor-label">
                <%: Html.LabelFor(model => model.PassportNo_Current)%>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.PassportNo_Current)%>
                <%: Html.ValidationMessageFor(model => model.PassportNo_Current)%>
            </td>
            
            <td class="editor-label">
                <%: Html.LabelFor(model => model.HabitatCardNo_Current)%>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.HabitatCardNo_Current)%>
                <%: Html.ValidationMessageFor(model => model.HabitatCardNo_Current)%>
            </td>
            
            </tr><tr>

            <td class="editor-label">
                <%: Html.LabelFor(model => model.DateArriveFrom)%>
            </td>
            <td class="editor-field">
                <%: Html.EditorFor(model => model.DateArriveFrom)%>
                &nbsp;<br />
                <%: Html.EditorFor(model => model.DateArriveTo)%>
            </td>

            <td class="editor-label">
                <%: Html.LabelFor(model => model.PermitDateFrom)%>
            </td>
            <td class="editor-field">
                <%: Html.EditorFor(model => model.PermitDateFrom)%>
                &nbsp;<br />
                <%: Html.EditorFor(model => model.PermitDateTo)%>
            </td>

            <td class="editor-label">
                <%: Html.LabelFor(model => model.InvoiceNo)%>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.InvoiceNo)%>
                <%: Html.ValidationMessageFor(model => model.InvoiceNo)%>
            </td>
            
        </tr>

        <tr>
                                 
            <% if (Model.WantVisa) { %> <%--//th20110407 PD18-540102 Req 3.2--%>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.StayReason)%>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.StayReason)%>
                <%: Html.ValidationMessageFor(model => model.StayReason)%>
            </td>
            <% } %>
            
            <% if (Model.WantShip) { %> <%--//th20110407 PD18-540102 Req 7.4--%>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.ConveyanceName)%>
            </td>
            <td class="editor-field">
                <%: Html.EditorFor(model => model.ConveyanceName, "CBConveyanceNameAll")%>
                <%: Html.ValidationMessageFor(model => model.ConveyanceName)%>
            </td>
            <% } %>
        </tr>
    </table>            
<% } %>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        //เปลี่ยนทางการ submit ให้รวม form กัน
        $('#SearchAll, #dtppanel-cmdbtn').click(function () {
            $(".dtppanel").hide();
            $(".dtppanel").appendTo("#MainSearch");
            $("#MainSearch").submit();
            return false;
        });

        //การทำงานของปุ่ม Clear ในหน้านี้ เราจะ clear ค่าในตัวค้นหาทั้งหมดยกเว้น checkbox ที่จะไม่ clear เลย (:not(input[type=hidden]):not(:checkbox))
        $('#ClearSearch').click(function () {
            $("#MainSearch :input:not(:button):not(input[type=hidden]):not(:checkbox)").val("");
            return false;
        });
    });

</script>
