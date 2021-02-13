<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<% ViewData["PageTitle"] = Html.ShowTitle(String.Format("SKIMO-IS (Build {0}) - ตรวจคนเข้าเมืองจังหวัดสมุทรสาคร", VersionInfo.GetBuildString()), null, null, ""); %>
<%: ViewData["PageTitle"] %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<center>
<h1>
<p></p>
<p>ยินดีต้อนรับสู่ระบบงานสารสนเทศของตรวจคนเข้าเมืองจังหวัดสมุทรสาคร</p>
<p>Welcome to Samutsakhon Immigration Office Information System<br />(SKIMO-IS)</p>
</h1>
</center>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
