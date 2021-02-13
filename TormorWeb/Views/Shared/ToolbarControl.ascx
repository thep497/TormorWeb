<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<% if (ViewData["__tbController"] != null)
   { %>

<% Html.Telerik().Menu()
                .Name("NNSToolbar")
                .HtmlAttributes(new { id = "NNSToolbar", style = "border-bottom:2px solid #202020;" })
                .Items(menu =>
                {
                    menu.AddToolbarSeparator();
                    menu.AddToolbarItem("Save", "Save and continue editing", ViewData["__tb_Save"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"], false, "toolbarsavebutton")
                        .ImageUrl("~/Content/Images/Toolbars/apply.png");
                    menu.AddToolbarItem("S->C", "Save and close", ViewData["__tb_Save"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"], false, "toolbarsaveclosebutton")
                        .ImageUrl("~/Content/Images/Toolbars/saveclose.png");
                    menu.AddToolbarItem("Close", "Close this page", ViewData["__tb_Close"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"])
                        .ImageUrl("~/Content/Images/Toolbars/Exit.png");
                    menu.AddToolbarSeparator();
                    menu.AddToolbarItem("New", "Insert new data", ViewData["__tb_New"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"])
                        .ImageUrl("~/Content/Images/Toolbars/edit_add.png")
                        .Items(menup =>
                        {
                            menup.AddToolbarItem(ViewData["__tbCaption_New1"], "", ViewData["__tb_New1"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"], true);
                            menup.AddToolbarItem(ViewData["__tbCaption_New2"], "", ViewData["__tb_New2"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"], true);
                            menup.AddToolbarItem(ViewData["__tbCaption_New3"], "", ViewData["__tb_New3"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"], true);
                            menup.AddToolbarItem(ViewData["__tbCaption_New4"], "", ViewData["__tb_New4"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"], true);
                            menup.AddToolbarItem(ViewData["__tbCaption_New5"], "", ViewData["__tb_New5"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"], true);
                        });
                    menu.AddToolbarItem("Delete", "Delete this data", ViewData["__tb_GiveUp"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"], false, "toolbargiveupbutton")
                        .ImageUrl("~/Content/Images/Toolbars/edit_remove.png");
                    //menu.AddToolbarItem("Purge", "Delete this data from database", ViewData["__tb_Delete"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"], false, "toolbardeletebutton")
                    //    .ImageUrl("~/Content/Images/Toolbars/editdelete.png");
                    menu.AddToolbarSeparator();
                    menu.AddToolbarItem("Print", "", ViewData["__tb_Print"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"])
                        .ImageUrl("~/Content/Images/Toolbars/printer_48.png")
                        .Items(menup =>
                        {
                            menup.AddToolbarItem(ViewData["__tbCaption_Print1"], "", ViewData["__tb_Print1"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"], true);
                            menup.AddToolbarItem(ViewData["__tbCaption_Print2"], "", ViewData["__tb_Print2"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"], true);
                            menup.AddToolbarItem(ViewData["__tbCaption_Print3"], "", ViewData["__tb_Print3"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"], true);
                            menup.AddToolbarItem(ViewData["__tbCaption_Print4"], "", ViewData["__tb_Print4"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"], true);
                            menup.AddToolbarItem(ViewData["__tbCaption_Print5"], "", ViewData["__tb_Print5"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"], true);
                        });
                    menu.AddToolbarItem("Other", "", ViewData["__tb_Other"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"])
                        .ImageUrl("~/Content/Images/Toolbars/spanner_48.png")
                        .Items(menup =>
                        {
                            menup.AddToolbarItem(ViewData["__tbCaption_Other1"], "", ViewData["__tb_Other1"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"], true);
                            menup.AddToolbarItem(ViewData["__tbCaption_Other2"], "", ViewData["__tb_Other2"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"], true);
                            menup.AddToolbarItem(ViewData["__tbCaption_Other3"], "", ViewData["__tb_Other3"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"], true);
                            menup.AddToolbarItem(ViewData["__tbCaption_Other4"], "", ViewData["__tb_Other4"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"], true);
                            menup.AddToolbarItem(ViewData["__tbCaption_Other5"], "", ViewData["__tb_Other5"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"], true);
                        });
                    menu.AddToolbarSeparator();
                    menu.AddToolbarItem("Date", "Show the date filter panel", ViewData["__tb_Date"], ViewData["__tbController"], ViewData["__tbObjectRoute"], ViewData["__tbUseRouteDic"], false, "toolbardatebutton")
                        .ImageUrl("~/Content/Images/Toolbars/Calendar.png");
                    menu.AddToolbarSeparator();
                })
                .Render();
%>

<% Html.Telerik().Window()
                        .Name("DatePanel")
                        .Title("กรุณาเลือกช่วงวันที่")
                        .Draggable(true)
                        .Resizable(resizing => resizing.Enabled(false))
                        .Visible(false)
                        .Modal(false)
                        .Width(400)
       //.Height(120)
                        .Buttons(b => b.Close())
                        .Content(() =>
                        {%>

<% using (Html.BeginForm((ViewData["__tb_Date"] ?? "#").ToString(),
                          ViewData["__tbController"].ToString(), 
                          FormMethod.Get, new { id = "DateSearch" })) //th20110105 ต้องเป็น Get เพราะใน grid จะใช้ส่งต่อกรณีที่เรียก refresh หรือ sort ตามหัว, ฯลฯ //เพื่อส่งผ่าน __tbObjectRoute ได้
   { %>
    <div class="dtppanel">
        <div class="dtppanel-cb">
        <%= Html.Telerik().DropDownList()
                    .Name("dtpSelectRange")
        //.SelectedIndex((int)(ViewData["dtpselectrange"] ?? 0))
                    .ClientEvents(events => events.OnChange("onSelectRangeChange"))
                    .Items(ddl =>
                    {
                        ddl.Add().Value("0").Text("เลือก...");
                        ddl.Add().Value("1").Text("ข้อมูลวันปัจจุบัน");
                        ddl.Add().Value("2").Text("ย้อนไปต้นเดือนนี้");
                        ddl.Add().Value("3").Text("ย้อนไปต้นปีนี้");
                        ddl.Add().Value("4").Text("ข้อมูลเดือนที่แล้ว");
                        ddl.Add().Value("5").Text("ข้อมูลปีที่แล้ว");
                        ddl.Add().Value("6").Text("ไม่กำหนดระยะเวลา");
                    })
        %>
        </div>

        <%-- //th20110105 vvv ส่วนนี้เอาค่าจาก ViewData["__tbObjectRoute"] มาทำเป็น hidden field เพื่อให้ส่งผ่านไปพร้อม ๆ กับตัวแปลอื่น ๆ ได้ NC5401002 --%>
        <%= Html.HiddenFieldFromRoutevalue(ViewData["__tbObjectRoute"])%>
        <%-- //th20110105 ^^^ --%>

        <div class="dtppanel-cmd"><input id="dtppanel-cmdbtn" type="submit" value="Search" /></div>
        <div style="clear:both; padding-bottom:7px;"></div>

        <div class="dtppanel-input" id="dtppanel-input">
        แสดงข้อมูลตั้งแต่
        <%= Html.Telerik().DatePicker()
                    .Name("dtpFromDate")
                    .Format(Globals.DateFormat)
                    .Value((DateTime?)ViewData["dtpstartdate"])
        %>
        ถึง
        <%= Html.Telerik().DatePicker()
                    .Name("dtpToDate")
                    .Format(Globals.DateFormat)
                    .Value((DateTime?)ViewData["dtpenddate"])
        %>
        </div>
    </div>
<% } %>
                        <%})
                        .Render();
%>

<%-- ถ้าต้องการแสดงปุ่มกรองวันที่ --%>
<% if (ViewData["__tb_Date"] != null)
   { %>
<% Html.Telerik().ScriptRegistrar()
           .OnDocumentReady(() =>
           {%> searchDatePanelSetting(<%= ViewData["dtpselectrange"] ?? 0%>) <%}); %>
<% } //จบ if กรองวันที่ %>


<%= Html.JavascriptTag("nns/NNSToolbarControl", true)%>
<% } //if ข้างบนสุด 
   else //if (ViewData["__tbMessage"] != null)
   { //ถ้าไม่มี toolbar จะให้แสดงอะไร ???
%>
    <h2><%: ViewData["__tbMessage"] %></h2>
<%       
   }
%>
