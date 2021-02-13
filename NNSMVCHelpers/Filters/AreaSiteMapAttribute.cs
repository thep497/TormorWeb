using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Telerik.Web.Mvc;

namespace NNS.MVCHelpers
{
    #region Comment
    /// <summary>
    /// Represents an attribute that is used to populate a <see cref="Telerik.Web.Mvc.SiteMapBase"/>
    /// in view data.
    /// </summary>
    #endregion
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class AreaSiteMapAttribute : FilterAttribute, IActionFilter
    {
        private string _siteMapName = "web.sitemap";
        private string _viewDataKey = "siteMap";
        #region Comment
        /// <summary>
        /// Gets/sets the name of the .sitemap file to use
        /// </summary>
        /// <remarks>If this property is not set if will default to "web.sitemap" is used. This file
        /// must exist within the project at "~/Areas/area".</remarks>
        #endregion
        public string SiteMapName
        {
            get { return _siteMapName; }
            set { _siteMapName = value; }
        }
        #region Comment
        /// <summary>
        /// Gets/sets the <see cref="ViewData" key that will be used to store the
        /// <see cref="Telerik.Web.Mvc.SiteMap"/>./>
        /// </summary>
        /// <remarks>If this property is not set if will default to "siteMap".</remarks>
        #endregion
        public string ViewDataKey
        {
            get { return _viewDataKey; }
            set { _viewDataKey = value; }
        }
        #region IActionFilter Members
        #region Comment
        /// <inheritdoc/>
        #endregion
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
        #region Comment
        /// <inheritdoc/>
        #endregion
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string area = null;
            // Try to find the area
            if (filterContext.RouteData.DataTokens.ContainsKey("area"))
            {
                area = (string)filterContext.RouteData.DataTokens["area"];
            }
            if (area != null)
            {
                // Load the area's sitemap if it not already loaded
                if (!SiteMapManager.SiteMaps.ContainsKey(area))
                {
                    SiteMapManager.SiteMaps.Register<XmlSiteMap>(area, sitemap => sitemap.LoadFrom(string.Format("~/Areas/{0}/{1}", area, _siteMapName)));
                }
                // Fetch the site map and store in in ViewData
                var siteMap = (XmlSiteMap)SiteMapManager.SiteMaps[area];
                filterContext.Controller.ViewData[_viewDataKey] = siteMap;
            }
        }
        #endregion
    }
}
