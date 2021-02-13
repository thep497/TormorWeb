<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%= Html.Telerik().TreeView()
       .Name("MainMenu")
       .BindTo("siteMap")
       .ExpandAll(true)
       .HtmlAttributes(new { id = "MainMenu" })
%>
