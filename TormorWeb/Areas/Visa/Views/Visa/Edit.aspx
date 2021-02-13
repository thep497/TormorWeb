<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Tormor.DomainModel.VisaDetail>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<% ViewData["PageTitle"] = "แก้ไขข้อมูลการขออยู่ต่อของ "+Model.Alien.Name.FullName+" (ลำดับที่ "+Model.Code+")"; %>
<%: ViewData["PageTitle"] %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% Html.RenderPartial("VisaEdit",Model); %>

<% using (Html.BeginForm("Delete","Visa")) {%>
            <input name="id" type="hidden" value="<%: Model.Id %>" />
            <%= Html.AntiForgeryToken() %>
            <input id="btnDelete" type="submit" value="Delete" style="visibility:hidden;" />
<% } %>
<% Html.RenderPartial("UpdateInfo", Model.UpdateInfo); %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

