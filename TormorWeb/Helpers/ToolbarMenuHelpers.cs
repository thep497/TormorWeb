// *******************************************************************
//1. ชื่อโปรแกรม 	: NeoSKIMO
//2. ชื่อระบบ 		: WebUI
//3. ชื่อ Unit 	: ToolbarMenuHelpers
//4. ผู้เขียนโปรแกรม	: อมรเทพ
//5. วันที่สร้าง	    : 201012xx
//6. จุดประสงค์ของ Unit นี้ : Controller สำหรับจัดการ Toolbar
// *******************************************************************
// แก้ไขครั้งที่ : 0
// ประวัติการแก้ไข
// ครั้งที่ 0 : เริ่มต้นสร้าง unit ใหม่
// *******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Web.Mvc.UI;
using System.Web.Mvc;
using System.ComponentModel;
using NNS.MVCHelpers;
using NNS.GeneralHelpers;
using System.Web.Routing;
using Tormor.DomainModel;
using Tormor.Web.Models;

namespace NNS.MVCHelpers
{
    public static class ToolbarMenuHelpers
    {
        public static MenuItemBuilder AddToolbarSeparator(this MenuItemFactory menu)
        {
            return menu.Add().Text(".").Enabled(false)
                .HtmlAttributes(new { @class = "toolbarsep" })
                .LinkHtmlAttributes(new { style = "visibility:hidden" });
        }

        public static MenuItemBuilder AddToolbarItem(this MenuItemFactory menu,
            object ocaption, object ohint,
            object actionname, object controllername, object objectRoute, 
            object useRouteDic, bool IsSubMenu = false, 
            string html_id="")
        {
            RouteValueDictionary rdict;
            if (!(bool)(useRouteDic ?? false))
            {
                rdict = new RouteValueDictionary(objectRoute);
            }
            else if (objectRoute != null)
                rdict = (RouteValueDictionary)objectRoute;
            else
                rdict = new RouteValueDictionary();

            string str_action, str_controller, str_area;
            str_action = UpMisc.SplitActionString(actionname, out str_controller, out str_area);
            try
            {
                if (str_controller == "") str_controller = (string)controllername.ToString();
                if (str_area != "")
                {
                    if (rdict.ContainsKey("area")) //containkey ไม่สนใจตัวพิมพ์เล็ก/ใหญ่ ใส่อย่างไรก็ได้
                        rdict.Remove("area");
                    rdict.Add("area", str_area);
                }
            }
            catch
            {
            }

            string classname, attacheddata = "";

            if (!IsSubMenu)
                classname = "toolbaritem";
            else
                classname = "toolbarsubmenu";

            string caption = (ocaption ?? "-").ToString();
            MenuItemBuilder result = menu.Add().Text(" "+caption);

            if ((str_action != null) && (str_action.ToString() != "") &&
                (str_controller != null) && (str_controller != ""))
            {
                if (html_id == "toolbardatebutton") //ถ้าเป็นปุ่มวันที่ ไม่ต้องใส่ action ให้
                    classname += ""; //ไม่ถือว่า disable นะ
                else if ((html_id == "toolbarsavebutton") ||
                         (html_id == "toolbarsaveclosebutton") ||
                         (html_id == "toolbargiveupbutton") ||
                         (html_id == "toolbardeletebutton") ||
                         (html_id == "tbdetailsavebutton") ||
                         (html_id == "tbdetailgiveupbutton") ||
                         (html_id == "tbdetailclosebutton")) //ถ้าเป็นปุ่ม Save/GiveUp/Delete action ที่ส่งมาคือชื่อปุ่ม
                    attacheddata = str_action.ToString();
                else if ((str_action.ToString() != "#"))
                    result = result.Action(str_action.ToString(), str_controller.ToString(), rdict);
            }
            else
            {
                if (IsSubMenu)
                    result = result.Visible(false);
                result = result.Enabled(false);
                classname += " toolbarinactive";
            }

            //ใส่ hint ให้ปุ่มซะหน่อย
            string hint = (ohint ?? "").ToString();
            if (hint == "") hint = caption;
            result = result.ImageHtmlAttributes(new { @class = classname, style = "margin-left:auto;margin-right:auto;" });
            if (html_id == "")
                result = result.HtmlAttributes(new { @class = classname, title = hint });
            else
                result = result.HtmlAttributes(new { @class = classname, title = hint, id = html_id, data = attacheddata });
            return result;
        }

        /// <summary>
        /// สำหรับสร้าง Toolbar ด้านบน
        /// </summary>
        /// <param name="viewData">อ้างอิง ViewData ของ Controller ที่ส่งมา</param>
        /// <param name="actions">List ของ action ที่กำหนดให้กับปุ่ม</param>
        /// <param name="controllername">ชื่อ Controller นี้</param>
        /// <param name="routeValues">RouteValues อื่น ๆ เช่น area, id เป็นต้น</param>
        /// <param name="captions">List ของคำที่จะแสดงในเมนูย่อย</param>
        /// <param name="dtpSelectRange">ค่าใน combo เลือกช่วงวันที่</param>
        /// <param name="dtpFromDate">วันที่กรอง เริ่มต้น</param>
        /// <param name="dtpToDate">วันที่กรอง สิ้นสุด</param>
        /// <param name="useRouteDic">ส่งข้อมูล Route เป็น Dictionary (true) หรือ ส่งเป็น object(false) </param>
        public static void SetToolBar(ViewDataDictionary viewData,
            object actions, string controllername, object routeValues, object captions,
            ref int? dtpSelectRange,
            ref DateTime? dtpFromDate,ref DateTime? dtpToDate, 
            bool useRouteDic= false)
        {
            viewData["__tbController"] = controllername;
            viewData["__tbObjectRoute"] = routeValues;
            viewData["__tbUseRouteDic"] = useRouteDic;

            foreach (var a in GetProperties(actions))
            {
                viewData["__tb_" + a.Name] = a.Value;
            }
            foreach (var a in GetProperties(captions))
            {
                viewData["__tbCaption_" + a.Name] = a.Value;
            }

            switch (dtpSelectRange ?? 0)
            {
                case 1:
                    dtpFromDate = dtpToDate = DateTime.Today;
                    break;
                case 2:
                    dtpFromDate = DateTime.Today.StartOfTheMonth();
                    dtpToDate = DateTime.Today;
                    break;
                case 3:
                    dtpFromDate = DateTime.Today.StartOfTheYear();
                    dtpToDate = DateTime.Today;
                    break;
                case 4:
                    dtpFromDate = DateTime.Today.StartOfTheMonth().AddMonths(-1);
                    dtpToDate = ((DateTime)dtpFromDate).EndOfTheMonth();
                    break;
                case 5:
                    dtpFromDate = DateTime.Today.StartOfTheYear().AddYears(-1);
                    dtpToDate = ((DateTime)dtpFromDate).EndOfTheYear();
                    break;
                case 6:
                    dtpFromDate = DateTime.MinValue;
                    dtpToDate = DateTime.MinValue;
                    break;
            }

            //th20110112 กำหนดค่า default ให้กับช่องวันที่ + อ่านค่ามาจาก session
            var searchInfo = new SearchInfoBase();
            if (dtpSelectRange != null) //ถ้ามีการส่งค่ามาให้ function นี้ ให้ set ค่าเก็บไว้
            {
                searchInfo.dtpSelectRange = dtpSelectRange ?? 0;
                searchInfo.dtpFromDate = dtpFromDate;
                searchInfo.dtpToDate = dtpToDate;
                SSSearchInfo.SetSearchInfo(controllername, searchInfo);
            }

            searchInfo = SSSearchInfo.GetSearchInfoBase(controllername);
            if (dtpSelectRange == null)
                dtpSelectRange = searchInfo.dtpSelectRange;
            if (dtpFromDate == null)
                dtpFromDate = searchInfo.dtpFromDate;
            if (dtpToDate == null)
                dtpToDate = searchInfo.dtpToDate;

            viewData["dtpselectrange"] = dtpSelectRange;
            viewData["dtpstartdate"] = dtpFromDate;
            viewData["dtpenddate"] = dtpToDate;
            dtpSelectRange = 0;
        }

        /// <summary>
        /// สำหรับสร้าง Toolbar ด้านบน (แบบไม่เอา Date)
        /// </summary>
        /// <param name="viewData"></param>
        /// <param name="captions"></param>
        /// <param name="actions"></param>
        /// <param name="controllername"></param>
        /// <param name="routeValues"></param>
        public static void SetToolBar(ViewDataDictionary viewData,
            object actions, string controllername, object routeValues=null, object captions=null, 
            bool useRouteDic = false)
        {
            int? dm1 = null;
            DateTime? dm2 = null, dm3 = null;

            SetToolBar(viewData, actions, controllername, routeValues, captions, ref dm1, ref dm2, ref dm3, useRouteDic);
        }

        #region private members
        private static IEnumerable<PropertyValue> GetProperties(object o)
        {
            if (o != null)
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(o);
                foreach (PropertyDescriptor prop in props)
                {
                    object val = prop.GetValue(o);
                    if (val != null)
                    {
                        yield return new PropertyValue { Name = prop.Name, Value = val };
                    }
                }
            }
        }

        private sealed class PropertyValue
        {
            public string Name { get; set; }
            public object Value { get; set; }
        }
        #endregion
    }
}
