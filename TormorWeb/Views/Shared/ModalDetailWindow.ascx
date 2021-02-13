<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div class="detailwindow-wrap">
<% Html.Telerik().Window()
           .Name("detailwindow")
           .Title("กรุณากรอกข้อมูล")
           .Draggable(true)
           .Resizable(resizing => resizing.Enabled(false))
           .Modal(true)
           .Effects(eff => eff.Zoom().OpenDuration(200).CloseDuration(100))
           .Content(() =>
           {%>
<% Html.Telerik().Menu()
                .Name("NNSDetailToolbar")
                .HtmlAttributes(new { id = "NNSDetailToolbar" })
                .Items(menu =>
                {
                    menu.AddToolbarSeparator();
                    menu.AddToolbarItem("Save", "Save and close", "btnDetlSave", "dummy", null, null, false, "tbdetailsavebutton")
                        .ImageUrl("~/Content/Images/Toolbars/apply.png");
                    menu.AddToolbarItem("Close", "Close this page without saving", "btnDetlClose", "dummy", null, null, false, "tbdetailclosebutton")
                        .ImageUrl("~/Content/Images/Toolbars/Exit.png");
                    menu.AddToolbarSeparator();
                    menu.AddToolbarItem("Delete", "Delete this data", "btnDetlDelete", "dummy", null, null, false, "tbdetailgiveupbutton")
                        .ImageUrl("~/Content/Images/Toolbars/edit_remove.png");
                    menu.AddToolbarSeparator();
                })
                .Render();
%>
                <div id="detailwindow-content">
                </div>
           <%})
           .Visible(false)
           .Render();
%>
</div>

<%= Html.JavascriptTag("nns/NNSModalDetailWindow",true)  %>
