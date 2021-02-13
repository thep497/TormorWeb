<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DateTime?>" %>

<% 
    var result = "";
    if (Model != null)
    {
        var value = Model ?? DateTime.MinValue;
        if (value.TimeOfDay == TimeSpan.Parse("0:00:00"))
            result = value.ToString(Globals.DateFormat);
        else
            result = value.ToString(Globals.DateTimeFormat);
    }
%>
<%: result %>
