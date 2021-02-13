using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.UI;
using System.Linq.Expressions;
using System.Web.Routing;

namespace NNS.MVCHelpers
{
    public static class HtmlComboBoxHelpers
    {
        private class LoadJSName
        {
            private string _onLoad = "DSDropDownList_onLoad";
            private string _onChange = "DSDropDownList_onChange";
            private string _onDataBound = "DSDropDownList_onDataBound";

            public string OnLoad { get { return _onLoad; } set { _onLoad = value; } }
            public string OnChange { get { return _onChange; } set { _onChange = value; } }
            public string OnDataBound { get { return _onDataBound; } set { _onDataBound = value; } }

            private string _getValueFromObject(object a, System.Reflection.PropertyInfo[] props,
                string name, string defaultValue)
            {
                var temp = props.Where(x => x.Name.ToLower() == name).Select(x => x.GetValue(a, null)).FirstOrDefault();
                if (temp != null)
                    return temp.ToString();
                return defaultValue;
            }
            public LoadJSName(object a)
            {
                if (a != null)
                {
                    //this.OnLoad = jsEventName.OnLoad;
                    var type = a.GetType();
                    var props = type.GetProperties();

                    _onLoad = _getValueFromObject(a, props, "onload", _onLoad);
                    _onChange = _getValueFromObject(a, props, "onchange", _onChange);
                    _onDataBound = _getValueFromObject(a, props, "ondatabound", _onDataBound);
                }
            }
        }

        private static string getExpressionName<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            var propName = helper.ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty);
            //return propName;
            var expText = ExpressionHelper.GetExpressionText(expression);
            if (string.IsNullOrEmpty(propName) || (propName == expText))
                return expText;
            return propName + "." + expText;
        }

        #region Combobox
        public static MvcHtmlString ComboBoxRef_NameFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
            int refTypeId, object jsEventName=null, object htmlAttributes=null)
        {
            return ComboBoxRef_Name(helper,
                        getExpressionName(helper,expression),
                        ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model,
                        refTypeId, jsEventName, htmlAttributes);
        }
        public static MvcHtmlString ComboBoxRef_Name(this HtmlHelper helper, string name, object value,
            int refTypeId, object jsEventName = null, object htmlAttributes = null)
        {
            return ComboBoxRef(helper,
                        name, value,
                        refTypeId, "_getReferenceComboDropDownAjax", false, jsEventName, htmlAttributes);
        }

        public static MvcHtmlString ComboBoxRef_CodeNameFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
            int refTypeId, object jsEventName = null, object htmlAttributes = null)
        {
            return ComboBoxRef_CodeName(helper,
                        getExpressionName(helper, expression),
                        ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model,
                        refTypeId, jsEventName, htmlAttributes);
        }
        public static MvcHtmlString ComboBoxRef_CodeName(this HtmlHelper helper, string name, object value,
            int refTypeId, object jsEventName = null, object htmlAttributes = null)
        {
            return ComboBoxRef(helper,
                        name, value,
                        refTypeId, "_getReferenceComboDropDown_CodeName_Ajax", false, jsEventName, htmlAttributes);
        }

        /// <summary>
        /// แสดง Combobox ที่อ้างอิงกับ zz_reference
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name">ID ของ combobox</param>
        /// <param name="value">ค่าจาก Model</param>
        /// <param name="refTypeId"></param>
        /// <param name="actionName">ชื่อ action ใน ReferenceController ที่เรียกใช้งาน</param>
        /// <param name="wantPleaseSelect">ต้องการคำว่า Please Select ด้วยหรือไม่</param>
        /// <param name="jsEventName">ชื่อ JavaScript ที่ให้ทำเมื่อเกิด Event หลัก</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString ComboBoxRef(this HtmlHelper helper,string name, object value,
            int refTypeId,
            string actionName, bool wantPleaseSelect=false, 
            object jsEventName=null, object htmlAttributes=null)
        {
            return ComboBoxRef(helper, name, value, actionName, "Reference", "reference", 
                //new { area = "reference", reftypeid = refTypeId, wantPleaseSelect = wantPleaseSelect, value = value },
                new { reftypeid = refTypeId, wantPleaseSelect = wantPleaseSelect }, //area กับ value ไม่ต้องส่งไปก็ได้ ส่งเฉพาะ parameter ของ callback
                jsEventName, htmlAttributes);
        }

        /// <summary>
        /// สร้าง combobox จาก action/controller/area/routevalue ที่กำหนด (Action เป็น Ajax)
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name">ชื่อของ combobox</param>
        /// <param name="value">ค่า default</param>
        /// <param name="actionName">Action (function) ที่เรียกข้อมูล</param>
        /// <param name="controllerName">Controller ที่รับผิดชอบ</param>
        /// <param name="areaName">Area ของ controller</param>
        /// <param name="routeValue">ค่า parameter ของ function</param>
        /// <param name="jsEventName">event javascript ของ combobox</param>
        /// <param name="htmlAttributes">html ที่ต้องการแสดง เช่นความกว้างเป็นต้น</param>
        /// <returns></returns>
        public static MvcHtmlString ComboBoxRef(this HtmlHelper helper, string name, object value,
            string actionName, string controllerName, string areaName, object routeValue,
            object jsEventName = null, object htmlAttributes = null)
        {
            var jsEvent = new LoadJSName(jsEventName);

            if (htmlAttributes == null)
                htmlAttributes = new { style = "width:120px;" };

            var rdic = (routeValue == null) ? new RouteValueDictionary()
                                            : new RouteValueDictionary(routeValue);
            if (!rdic.ContainsKey("area")) //containkey ไม่สนใจตัวพิมพ์เล็ก/ใหญ่ ใส่อย่างไรก็ได้
                rdic.Add("area", areaName);
            if (!rdic.ContainsKey("value"))
                rdic.Add("value", value);

            var result = helper.Telerik().ComboBox()
                    .Name(name)
                    .DataBinding(bind => bind.Ajax().Select(actionName, controllerName, rdic))
                    .AutoFill(true)
                    .Filterable(filter => filter.FilterMode(AutoCompleteFilterMode.Contains))
                    .HighlightFirstMatch(false)
                    .ClientEvents(events => events.OnLoad(jsEvent.OnLoad)
                                                  .OnChange(jsEvent.OnChange)
                                                  .OnDataBound(jsEvent.OnDataBound))
                    .HtmlAttributes(htmlAttributes)
                    .ToString();
            return MvcHtmlString.Create(result);
        }
        #endregion

        #region DropDownList
        public static MvcHtmlString DropDownListRef_NameFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
            int refTypeId, object jsEventName = null, object htmlAttributes = null)
        {
            return DropDownListRef_Name(helper,
                        getExpressionName(helper, expression),
                        ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model,
                        refTypeId, jsEventName, htmlAttributes);
        }
        public static MvcHtmlString DropDownListRef_Name(this HtmlHelper helper, string name, object value,
            int refTypeId, object jsEventName = null, object htmlAttributes = null)
        {
            return DropDownListRef(helper, name, value,
                        refTypeId, "_getReferenceComboDropDownAjax", true, jsEventName, htmlAttributes);
        }


        public static MvcHtmlString DropDownListRef_CodeNameFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
            int refTypeId, object jsEventName = null, object htmlAttributes = null)
        {
            return DropDownListRef_CodeName(helper,
                        getExpressionName(helper, expression),
                        ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model,
                        refTypeId, jsEventName, htmlAttributes);
        }
        public static MvcHtmlString DropDownListRef_CodeName(this HtmlHelper helper, string name, object value,
            int refTypeId, object jsEventName = null, object htmlAttributes = null)
        {
            return DropDownListRef(helper, name, value,
                        refTypeId, "_getReferenceComboDropDown_CodeName_Ajax", true, jsEventName, htmlAttributes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="refTypeId"></param>
        /// <param name="wantPleaseSelect"></param>
        /// <param name="actionName"></param>
        /// <param name="jsEventName"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString DropDownListRef(this HtmlHelper helper, string name, object value,
            int refTypeId,
            string actionName, bool wantPleaseSelect = true,
            object jsEventName = null, object htmlAttributes = null)
        {
            return DropDownListRef(helper, name, value, actionName, "Reference", "reference",
                        new { reftypeid = refTypeId, wantPleaseSelect = wantPleaseSelect },
                        jsEventName, htmlAttributes);
        }

        /// <summary>
        /// สร้าง dropdownlist จาก action/controller/area/routevalue ที่กำหนด (Action เป็น Ajax)
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name">ชื่อของ dropdownlist</param>
        /// <param name="value">ค่า default</param>
        /// <param name="actionName">Action (function) ที่เรียกข้อมูล</param>
        /// <param name="controllerName">Controller ที่รับผิดชอบ</param>
        /// <param name="areaName">Area ของ controller</param>
        /// <param name="routeValue">ค่า parameter ของ function</param>
        /// <param name="jsEventName">event javascript ของ dropdownlist</param>
        /// <param name="htmlAttributes">html ที่ต้องการแสดง เช่นความกว้างเป็นต้น</param>
        /// <returns></returns>
        public static MvcHtmlString DropDownListRef(this HtmlHelper helper, string name, object value,
            string actionName, string controllerName, string areaName, object routeValue,
            object jsEventName = null, object htmlAttributes = null)
        {
            var jsEvent = new LoadJSName(jsEventName);

            if (htmlAttributes == null)
                htmlAttributes = new { style = "width:120px;" };

            var rdic = (routeValue == null) ? new RouteValueDictionary()
                                            : new RouteValueDictionary(routeValue);
            if (!rdic.ContainsKey("area")) //containkey ไม่สนใจตัวพิมพ์เล็ก/ใหญ่ ใส่อย่างไรก็ได้
                rdic.Add("area", areaName);
            if (!rdic.ContainsKey("value"))
                rdic.Add("value", value);


            var result = helper.Telerik().DropDownList()
                    .Name(name)
                    .DataBinding(bind => bind.Ajax().Select(actionName, controllerName, rdic))
                    .ClientEvents(events => events.OnLoad(jsEvent.OnLoad)
                                                  .OnChange(jsEvent.OnChange)
                                                  .OnDataBound(jsEvent.OnDataBound))
                    .HtmlAttributes(htmlAttributes)
                    .ToString();
            return MvcHtmlString.Create(result);
        }
        #endregion

        #region AutoComplete
        public static MvcHtmlString AutoCompleteRef_NameFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
            int refTypeId, string refRefName = "", int pxWidth = 300, int pxHeight = 15)
        {
            return AutoCompleteRef_Name(helper,
                        getExpressionName(helper, expression),
                        ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model,
                        refTypeId, refRefName, pxWidth, pxHeight);
        }
        public static MvcHtmlString AutoCompleteRef_Name(this HtmlHelper helper, string name, object value,
            int refTypeId, string refRefName="", int pxWidth=300, int pxHeight=15)
        {
            return AutoCompleteRef(helper, name, value, refTypeId, refRefName, pxWidth, pxHeight);
        }

        /// <summary>
        /// แสดง Autocomplete ที่อ้างอิงกับ zz_reference
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name">ID ของ combobox</param>
        /// <param name="value">ค่าจาก Model</param>
        /// <param name="refTypeId"></param>
        /// <param name="actionName">ชื่อ action ใน ReferenceController ที่เรียกใช้งาน</param>
        /// <param name="wantPleaseSelect">ต้องการคำว่า Please Select ด้วยหรือไม่</param>
        /// <param name="jsEventName">ชื่อ JavaScript ที่ให้ทำเมื่อเกิด Event หลัก</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString AutoCompleteRef(this HtmlHelper helper, string name, object value,
            int refTypeId, string refRefName = "",
            int pxWidth = 300, int pxHeight = 15, string actionName = "")
        {
            if (string.IsNullOrWhiteSpace(actionName))
                actionName = "_getReferenceAutoCompleteAjax";
            return AutoCompleteRef(helper,name,value,
                actionName, "Reference","reference",
                new { reftypeid = refTypeId, refRefName = refRefName },
                pxWidth,pxHeight);
        }
        public static MvcHtmlString AutoCompleteRef(this HtmlHelper helper, string name, object value,
            string actionName, string controllerName, string areaName, object routeValue,
            int pxWidth = 300, int pxHeight = 15)
        {
            //ถ้าเป็น autocomplete ต้องใส่ value ไว้ใน htmlAttributes
            object htmlAttributes = new { value = value, style = string.Format("background-color:white; width:{0}px; height:{1}px", pxWidth, pxHeight) };

            var rdic = (routeValue == null) ? new RouteValueDictionary()
                                            : new RouteValueDictionary(routeValue);
            if (!rdic.ContainsKey("area")) //containkey ไม่สนใจตัวพิมพ์เล็ก/ใหญ่ ใส่อย่างไรก็ได้
                rdic.Add("area", areaName);

            var result = helper.Telerik().AutoComplete()
                    .Name(name)
                    .DataBinding(bind => bind.Ajax().Select(actionName, controllerName, rdic))
                    .AutoFill(true)
                    .Filterable(filter => filter.FilterMode(AutoCompleteFilterMode.Contains))
                    .HighlightFirstMatch(false)
                    .HtmlAttributes(htmlAttributes)
                    .ToString();
            //autocomplete ไม่ต้องใช้ client event
                    //.ClientEvents(events => events.OnLoad(jsEvent.OnLoad)
                    //                  .OnChange(jsEvent.OnChange)
                    //                  .OnDataBound(jsEvent.OnDataBound))

            return MvcHtmlString.Create(result);
        }
        #endregion

    }
}
