<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DateTime?>" %>

<%= Html.Telerik().DatePicker()
        .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
        .Format(Globals.DateFormat).Min(DateExtension.SQLDateMinValue)
        .Value(Model == null ? null : (Model > DateTime.MinValue ? Model : DateTime.Today))
%>