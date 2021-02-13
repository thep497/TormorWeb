<%@ Page Title="" Language="C#" MasterPageFile="../Shared/UserAdmin.Master" Inherits="System.Web.Mvc.ViewPage<NNS.Web.Areas.UserAdministration.Models.UserAdministration.DetailsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%  ViewData["PageTitle"] = "User Profile: " + Model.DisplayName + " [" + Model.Status + "]"; %>
<%: ViewData["PageTitle"]%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h3 class="mvcMembership">User Profile</h3>
	<div class="mvcMembership-account">
    <% if (Model.Profile.LastUpdatedDate == null) { %>
	    <dl class="mvcMembership">
		    <dt>Last Changed:</dt>
		    <dd><%:  Model.Profile.LastUpdatedDate.ToString(Globals.DateTimeFormat) %></dd>
	    </dl>
    <% } %>
    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        
        <fieldset>
            <%--<legend>Fields</legend>--%>
            <table>
            <tr>
            <td>
            <div class="editor-label">
                ชื่อผู้ใช้งาน
            </div>
            </td>
            <td>
            <div class="editor-field">
                <%: Html.TextBox("Profile_FirstName",Model.Profile.FirstName) %>
                <%: Html.ValidationMessageFor(model => model.Profile.FirstName) %>
            </div>
            </td>
            </tr>

            <tr>
            <td>
            <div class="editor-label">
                นามสกุล
            </div>
            </td>
            <td>
            <div class="editor-field">
                <%: Html.TextBox("Profile_LastName",Model.Profile.LastName)%>
                <%: Html.ValidationMessageFor(model => model.Profile.LastName) %>
            </div>
            </td>
            </tr>

            <tr>
            <td>
            <div class="editor-label">
                ตำแหน่ง
            </div>
            </td>
            <td>
            <div class="editor-field">
                <%: Html.TextBox("Profile_Position",Model.Profile.Position)%>
                <%: Html.ValidationMessageFor(model => model.Profile.Position) %>
            </div>
            </td>
            </tr>

            <tr>
            <td>
            <div class="editor-label">
                เบอร์โทรศัพท์
            </div>
            </td>
            <td>
            <div class="editor-field">
                <%: Html.TextBox("Profile_Phone",Model.Profile.Phone)%>
                <%: Html.ValidationMessageFor(model => model.Profile.Phone) %>
            </div>
            </td>
            </tr>

            <tr>
            <td>
            <div class="editor-label">
                Fax
            </div>
            </td>
            <td>
            <div class="editor-field">
                <%: Html.TextBox("Profile_Fax",Model.Profile.Fax)%>
                <%: Html.ValidationMessageFor(model => model.Profile.Fax) %>
            </div>
            </td>
            </tr>

            <tr>
            <td>
            <div class="editor-label">
                ภาษา
            </div>
            </td>
            <td>
            <div class="editor-field">
                <% var selectIndex = Array.IndexOf(Model.profileCultureList,Model.Profile.Culture);
                   selectIndex = selectIndex < 0 ? 0 : selectIndex; 
                %>
                <%= Html.Telerik().DropDownList()
                      .Name("Profile_Culture")
                      .SelectedIndex(selectIndex)
                      .BindTo(new SelectList(Model.profileCultureList))
                      .HtmlAttributes(new { style = string.Format("width:{0}px",80) })
                %>
                <%: Html.ValidationMessageFor(model => model.Profile.Culture) %>
            </div>
            </td>
            </tr>

            <tr>
            <td>
            <div class="editor-label">
                บรรทัดต่อหน้า
            </div>
            </td>
            <td>
            <div class="editor-field">
                <%= Html.Telerik().IntegerTextBox()
                                  .Name("Profile_PageSize")
                                  .Value(Model.Profile.PageSize)                
                                  .Spinners(false)
                                  .MinValue(0)
                                  .MaxValue(1000000)
                                  .InputHtmlAttributes(new { style = "width:40px" })
                %>
                <%: Html.ValidationMessageFor(model => model.Profile.PageSize) %>
            </div>
            </td>
            </tr>

            <tr>
            <td>
            <div class="editor-label">
                ความสูงหน้าจอ (Grid)
            </div>
            </td>
            <td>
            <div class="editor-field">
                <%= Html.Telerik().IntegerTextBox()
                                  .Name("Profile_MainScreenHeight")
                                  .Value(Model.Profile.MainScreenHeight)
                                  .Spinners(false)
                                  .MinValue(0)
                                  .MaxValue(1000)
                                  .InputHtmlAttributes(new { style = "width:60px" })
                %>
                <%: Html.ValidationMessageFor(model => model.Profile.MainScreenHeight) %>
            </div>
            </td>
            </tr>
            </table>
            
            <p>
                <input type="submit" value="Save Profile" />
            </p>
        </fieldset>

    <% } %>
    </div>


<%--    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>
--%>
</asp:Content>

