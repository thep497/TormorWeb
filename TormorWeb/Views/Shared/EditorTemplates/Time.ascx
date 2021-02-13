<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DateTime?>" %>
<% 
    string onChange = ViewData["onchange"] != null ? (string)ViewData["onchange"] : "";
%>

<%= Html.Telerik().TimePicker()
        .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
        .Format(Globals.TimeFormat)
        .Value((Model ?? DateTime.MinValue) > DateTime.MinValue ? Model : DateTime.Today)
        .InputHtmlAttributes(new { style = "width:80px;" })
        .HtmlAttributes(new { style="width:100px;"})
                    .ClientEvents(c =>
                    {
                        if (!string.IsNullOrEmpty(onChange))
                            c.OnChange(onChange);
                    })
%>