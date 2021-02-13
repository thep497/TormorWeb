<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.DomainModel.ConveyanceInOut>" %>

<% Html.Telerik().TabStrip()
        .Name("ConveyanceTab")
        .Items(tabstrip =>
        {
            tabstrip.Add()
                .Text("ข้อมูลการเข้า/ออก")
                .ContentHtmlAttributes(new { @class = "conveyancesinout-edit" })
                .Content(() =>
                {%>
                    <% Html.RenderPartial("ConveyanceInOutEdit_P1"); %>
                <%});

            if ((Model != null) && (Model.Id > 0))
            {
                if (Model.NumCrew > 0)
                {
                    tabstrip.Add()
                        .Text("รายชื่อคนประจำพาหนะ (เรือ)")
                        .ContentHtmlAttributes(new { @class = "conveyancesinout-edit" })
                        .Content(() =>
                        {%>
                        <% 
                            //th20110409 เพิ่มการแสดงรายการเพิ่มลด ลงใน Tab ของคนประจำเรือ (เฉพาะกรณีออก PD18-540102 Req 8)
                            IList<Tormor.DomainModel.AddRemoveCrew> addRemoveCrews = null;
                            if (Model.InOutType == ModelConst.CONVINOUT_OUT) //เอาเฉพาะกรณีออกเท่านั้น
                                addRemoveCrews = Model.DiffCrew;
                            Html.RenderPartial("ConveyanceInOutEditCrew",
                               new Tormor.Web.Models.CrewViewModel(Model.Id, true, Model.Conveyance.Name,
                                   Model.InOutDate, Model.Passengers, addRemoveCrews)); 
                        %>
                    <%});
                }

                //th20110408 PD18-540102 Req 8.7 ย้าย Tab
                //แสดงรายการบุคคลเข้าออกเพิ่มเติม
                if (Model.NumAddRemoveCrew > 0 && Model.InOutType != ModelConst.CONVINOUT_OUT) //th20110409 กรณีออกไม่ต้องแสดงเลย สามารถทำให้ไปรวมใน Tab แรกได้แล้ว
                {
                    tabstrip.Add()
                        .Text("รายชื่อคนประจำพาหนะ (ที่เพิ่ม/ลด)")
                        .ContentHtmlAttributes(new { @class = "conveyancesinout-edit" })
                        .Content(() =>
                        {%>
                        <% Html.RenderPartial("ConveyanceInOutEditCrew",
                               new Tormor.Web.Models.CrewViewModel(Model.Id, Model.Conveyance.Name,
                                   Model.InOutDate, Model.DiffCrew)); 
                        %>
                    <%});
                }
                
                if (Model.NumPassenger > 0)
                {
                    tabstrip.Add()
                        .Text("รายชื่อผู้โดยสาร")
                        .ContentHtmlAttributes(new { @class = "conveyancesinout-edit" })
                        .Content(() =>
                        {%>
                        <% Html.RenderPartial("ConveyanceInOutEditCrew",
                               new Tormor.Web.Models.CrewViewModel(Model.Id, false, Model.Conveyance.Name,
                                   Model.InOutDate, Model.Passengers)); 
                        %>
                    <%});
                }
                
            }
        })
        .SelectedIndex(0)
        .Render();
%>
<%-- กรณีมี Detail ต้องเรียก RenderPartial ModalDetailWindow และต้องอยู่นอกฟอร์มด้วย --%>
<% Html.RenderPartial("ModalDetailWindow"); %>
<%= Html.DefineEditForm() %> 

<script type="text/javascript">
    $(document).ready(function () {
        //SetShowOnly();
        setTimeout(function () {
            var inout = $("#InOutType_Old").val();
            refreshInOut(inout);
        }, 1000);
    });

    function refreshInOut(inout) {
        if ((inout != null) && (inout != "")) {
            //ซ่อน/แสดงตาม in/out
            if (inout == '2') {
                $('.portin').hide();
                $('.portout').show();
            }
            else {
                $('.portin').show();
                $('.portout').hide();
            }
        }
    }
    function ConveyanceInOutChange() {
        DSDropDownList_Change($(this));
        var inout = $(this).data("tComboBox").value();
        refreshInOut(inout);
    }

</script>

