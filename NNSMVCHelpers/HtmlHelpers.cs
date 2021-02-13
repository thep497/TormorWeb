using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Web.Mvc;
using NNS.GeneralHelpers;
using Tormor.DomainModel;

namespace NNS.MVCHelpers
{
    public static class HtmlHelpers
    {
        public static string HiddenFieldFromRoutevalue(this HtmlHelper helper, object routeValue)
        {
            var result = "";
            if (routeValue != null)
            {
                try
                {
                    var rDict = new RouteValueDictionary(routeValue);
                    foreach (var dict in rDict)
                    {
                        result += string.Format(@"<input name='{0}' type='hidden' value='{1}' />", dict.Key, dict.Value);
                    }
                }
                catch
                {
                }
            }
            return result;
        }

        public static string ShowTitle(this HtmlHelper helper, string title, object dtpStart, object dtpEnd, string format)
        {
            var result = title;
            if (!((DateTime?)dtpStart).IsNull())
            {
                var dtpstart = Convert.ToDateTime(dtpStart).ToString(format);
                var dtpend = Convert.ToDateTime(dtpEnd).ToString(format);
                result += " ระหว่างวันที่ "+ dtpstart + " ถึง " + dtpend;
            }
            return result;
        }

        public static string DefineEditForm(this HtmlHelper html)
        {
            string result = @"<script language='javascript' type='text/javascript'>$(document).ready(function () { if (!IsDataForm) SetDataForm(); });</script>";
            return result;
        }

        ///// <summary>
        ///// ย่อให้ script สั้น ๆ ในหน้า page
        ///// </summary>
        ///// <param name="helper"></param>
        ///// <param name="script"></param>
        ///// <returns></returns>
        //public static string MinScript(this HtmlHelper helper, string script)
        //{
        //    string result = script;
        //    string oldval;
        //    int counter = 0;
        //    do {
        //        oldval = result;
        //        result = result.Replace("  ", " ");
        //    } while ((oldval != result) || (++counter > 100));
        //    return result;
        //}

        public static string Truncate(this HtmlHelper helper, string input, int length)
        {
            if (input.Length <= length)
            {
                return input;
            }
            else
            {
                return input.Substring(0, length) + "...";
            }
        }

        public static string JavascriptTag(this HtmlHelper html, string javascriptName, bool noMinVer=false)
        {
            // Instantiate a UrlHelper
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            var format = "\r\n<script src='" + urlHelper.Content("~/Scripts/{0}.js") + "' type='text/javascript'></script>\r\n";

            if (!noMinVer)
            {
#if (!DEBUG)
                javascriptName += ".min";
#endif
            }
            return string.Format(format, javascriptName);
        }

        public static string ImageLink(this HtmlHelper htmlHelper, string imgSrc, string alt, string actionName, string controllerName, object routeValues, object htmlAttributes, object imgHtmlAttributes)
        {
            UrlHelper urlHelper = ((Controller)htmlHelper.ViewContext.Controller).Url;
            string imgtag = htmlHelper.Image(imgSrc, alt, imgHtmlAttributes).ToString();
            string url = urlHelper.Action(actionName, controllerName, routeValues);

            TagBuilder imglink = new TagBuilder("a");
            imglink.MergeAttribute("href", url);
            imglink.InnerHtml = imgtag;
            imglink.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);

            return imglink.ToString();
        }

        public static string ImageRouteLink(this HtmlHelper htmlHelper, string imgSrc, string alt, string url, object htmlAttributes, object imgHtmlAttributes)
        {
            UrlHelper urlHelper = ((Controller)htmlHelper.ViewContext.Controller).Url;
            string imgtag = htmlHelper.Image(imgSrc, alt, imgHtmlAttributes).ToString();

            TagBuilder imglink = new TagBuilder("a");
            imglink.MergeAttribute("href", url);
            imglink.InnerHtml = imgtag;
            imglink.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);

            return imglink.ToString();
        }
        public static string ImageRouteLink(this HtmlHelper htmlHelper, string imgSrc, string alt, string url)
        {
            return ImageRouteLink(htmlHelper, imgSrc, alt, url, new object { }, new object { });
        }

        public static IDictionary<string, string> GetModelStateErrors(this ViewDataDictionary viewDataDictionary)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (var modelStateKey in viewDataDictionary.ModelState.Keys)
            {
                var modelStateValue = viewDataDictionary.ModelState[modelStateKey];
                foreach (var error in modelStateValue.Errors)
                {
                    var errorMessage = error.ErrorMessage;
                    var exception = error.Exception;
                    if (!String.IsNullOrEmpty(errorMessage))
                    {
                        dict.Add(modelStateKey, "Egads! A Model Error Message! " + errorMessage);
                    }
                    if (exception != null)
                    {
                        dict.Add(modelStateKey, "Egads! A Model Error Exception! " + exception.ToString());
                    }
                }
            }
            return dict;
        }
    }
}