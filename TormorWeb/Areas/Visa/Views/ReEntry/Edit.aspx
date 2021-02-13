<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Tormor.DomainModel.ReEntry>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<% ViewData["PageTitle"] = "แก้ไขข้อมูลการขอ Re-Entry "+Model.Alien.Name.FullName+" (ลำดับที่ "+Model.Code+")"; %>
<%: ViewData["PageTitle"] %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% Html.RenderPartial("ReEntryEdit",Model); %>

<% using (Html.BeginForm("Delete","ReEntry")) {%>
            <input name="id" type="hidden" value="<%: Model.Id %>" />
            <%= Html.AntiForgeryToken() %>
            <input id="btnDelete" type="submit" value="Delete" style="visibility:hidden;" />
<% } %>
<% Html.RenderPartial("UpdateInfo", Model.UpdateInfo); %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

