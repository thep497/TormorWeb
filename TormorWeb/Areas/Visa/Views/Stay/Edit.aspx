<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Tormor.DomainModel.Staying90Day>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<% ViewData["PageTitle"] = "แก้ไขข้อมูลการแจ้งอยู่เกินกว่า 90 วันของ " + Model.Alien.Name.FullName + " (ลำดับที่ " + Model.Code + ")"; %>
<%: ViewData["PageTitle"] %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% Html.RenderPartial("StayEdit",Model); %>

<% using (Html.BeginForm("Delete","Stay")) {%>
            <input name="id" type="hidden" value="<%: Model.Id %>" />
            <%= Html.AntiForgeryToken() %>
            <input id="btnDelete" type="submit" value="Delete" style="visibility:hidden;" />
<% } %>
<% Html.RenderPartial("UpdateInfo", Model.UpdateInfo); %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

