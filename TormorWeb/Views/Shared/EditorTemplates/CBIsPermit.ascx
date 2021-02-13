<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<bool?>" %>

<%= Html.Telerik().DropDownListFor(m => m)
            .Items(m => {
                m.Add().Text("คอยอนุมัติ").Value(" ");
                m.Add().Text("อนุมัติ").Value("true");
                m.Add().Text("ไม่อนุมัติ").Value("false");
            })
            .HtmlAttributes(new { style = "width:100px"})
            .SelectedIndex(Model == null ? 0 : Model == true ? 1 : 2)
            .ClientEvents(events => events.OnChange("DSDropDownList_onChange"))
            
%>
