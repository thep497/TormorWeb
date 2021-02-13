<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.DomainModel.ConveyanceSearchInfo>" %>

<div id="SearchAllPanel">
<% using (Html.BeginForm("DoSearch", "ConveyanceInOut", new { area = "ConveyanceArea" }, FormMethod.Post, new { id = "MainSearch" }))
   { %>
    <table class="generaleditform">
        <tr>
            <td class="editor-field" colspan="4" style="border-width:1px;;text-align:center;">
                <%: Html.CheckBoxFor(model => model.WantIn)%>
                <%: Html.LabelFor(model => model.WantIn)%>
                &nbsp;
                <%: Html.CheckBoxFor(model => model.WantOut)%>
                <%: Html.LabelFor(model => model.WantOut)%>
            </td>

            <td class="editor-label" colspan="2">
                <button id="ClearSearch">Clear</button>
                <button id="SearchAll" type="submit">ค้นหาข้อมูล</button>
            </td>
        </tr><tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.InOutDateFrom)%>
            </td>
            <td class="editor-field">
                <%: Html.EditorFor(model => model.InOutDateFrom)%>
                &nbsp;<br />
                <%: Html.EditorFor(model => model.InOutDateTo)%>
            </td>
                        
            <td class="editor-label">
                <%: Html.LabelFor(model => model.AgencyName) %>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.AgencyName) %>
                <%: Html.ValidationMessageFor(model => model.AgencyName) %>
            </td>
            
            <td class="editor-label">
                <%: Html.LabelFor(model => model.InspectOfficer) %>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.InspectOfficer) %>
                <%: Html.ValidationMessageFor(model => model.InspectOfficer) %>
            </td>
        </tr><tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.Name) %>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.Name) %>
                <%: Html.ValidationMessageFor(model => model.Name) %>
            </td>
            
            <td class="editor-label">
                <%: Html.LabelFor(model => model.RegistrationNo) %>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.RegistrationNo) %>
                <%: Html.ValidationMessageFor(model => model.RegistrationNo) %>
            </td>
            
            <td class="editor-label">
                <%: Html.LabelFor(model => model.OwnerName) %>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.OwnerName) %>
                <%: Html.ValidationMessageFor(model => model.OwnerName) %>
            </td>
            
        </tr><tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.PortInTo) %>
            </td>
            <td class="editor-field">
                <%: Html.EditorFor(model => model.PortInTo) %>
                <%: Html.ValidationMessageFor(model => model.PortInTo) %>
            </td>
            

            <td class="editor-label">
                <%: Html.LabelFor(model => model.PortInFrom) %>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.PortInFrom) %>
                <%: Html.ValidationMessageFor(model => model.PortInFrom) %>
            </td>
            
            <td class="editor-label">
                <%: Html.LabelFor(model => model.PortInFrom_Country) %>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.PortInFrom_Country) %>
                <%: Html.ValidationMessageFor(model => model.PortInFrom_Country) %>
            </td>
            
        </tr><tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.PortOutFrom) %>
            </td>
            <td class="editor-field">
                <%: Html.EditorFor(model => model.PortOutFrom) %>
                <%: Html.ValidationMessageFor(model => model.PortOutFrom) %>
            </td>
            
            <td class="editor-label">
                <%: Html.LabelFor(model => model.PortOutTo) %>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.PortOutTo) %>
                <%: Html.ValidationMessageFor(model => model.PortOutTo) %>
            </td>
            
            <td class="editor-label">
                <%: Html.LabelFor(model => model.PortOutTo_Country) %>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.PortOutTo_Country) %>
                <%: Html.ValidationMessageFor(model => model.PortOutTo_Country) %>
            </td>
            
        </tr><tr>
            <td class="editor-label">
                <%: Html.LabelFor(model => model.AlienName) %>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.AlienName)%>
                <%: Html.ValidationMessageFor(model => model.AlienName) %>
            </td>
            
            <td class="editor-label">
                <%: Html.LabelFor(model => model.AlienPassport) %>
            </td>
            <td class="editor-field">
                <%: Html.TextBoxFor(model => model.AlienPassport)%>
                <%: Html.ValidationMessageFor(model => model.AlienPassport)%>
            </td>
            
            <td class="editor-label">
                <%: Html.LabelFor(model => model.AlienNationality) %>
            </td>
            <td class="editor-field">
                <%: Html.EditorFor(model => model.AlienNationality)%>
                <%: Html.ValidationMessageFor(model => model.AlienNationality)%>
            </td>
            
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
