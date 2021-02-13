<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Menu2 Page (ค้นหาและเพิ่มข้อมูลโดยตรง) - SKIMO-IS
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<center>
<h1>
<%= Html.ActionLink("goto index2","index2") %>
</h1>

<p></p>
<p>ยินดีต้อนรับสู่ระบบงานสารสนเทศของตรวจคนเข้าเมืองจังหวัดสมุทรสาคร</p>
<p>Welcome to Samutsakhon Immigration Office Information System<br />(SKIMO-IS)</p>

<% using(Html.BeginForm()) { %>
   <input type="submit" value="xxx" />
<% } %>
</center>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
