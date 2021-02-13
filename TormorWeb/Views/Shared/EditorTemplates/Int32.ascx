<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<int?>" %>

<%= Html.Telerik().IntegerTextBox()
        .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
        .InputHtmlAttributes(new { style = "width:60px" })
        .MaxValue(int.MinValue)
        .MaxValue(int.MaxValue)
        .Spinners(false)
        .Value(Model) 
%>
