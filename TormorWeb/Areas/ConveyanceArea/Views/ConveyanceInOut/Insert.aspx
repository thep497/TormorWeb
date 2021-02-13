<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Tormor.DomainModel.ConveyanceInOut>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<% ViewData["PageTitle"] = "เพิ่มข้อมูลพาหนะ (เรือ) เข้า/ออก"; %>
<%: ViewData["PageTitle"] %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% Html.RenderPartial("ConveyanceInOutEdit",Model); %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
