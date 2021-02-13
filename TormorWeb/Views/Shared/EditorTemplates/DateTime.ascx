<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DateTime?>" %>
<% 
    string onChange = ViewData["onchange"] != null ? (string)ViewData["onchange"] : "";
%>
<%= Html.Telerik().DatePicker()
        .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
        .Format(Globals.DateFormat).Min(DateExtension.SQLDateMinValue)
        .Value(Model == null ? null : (Model > DateTime.MinValue? Model : DateTime.Today))
                    .ClientEvents(c =>
                    {
                        if (!string.IsNullOrEmpty(onChange))
                            c.OnChange(onChange);
                    })
%>
