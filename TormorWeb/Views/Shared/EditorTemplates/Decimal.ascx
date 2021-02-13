<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Decimal?>" %>

<%= Html.Telerik().NumericTextBox<Decimal>()
        .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
        .DecimalDigits(2)
        .InputHtmlAttributes(new { style = "width:100%" })
        .Spinners(false)
        .Value(Model ?? 0m)
%>
