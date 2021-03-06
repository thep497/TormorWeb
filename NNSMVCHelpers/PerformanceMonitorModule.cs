using System;
using System.Diagnostics;
using System.Web;

namespace PerformanceMonitors
{
    public class PerformanceMonitorModule : IHttpModule
    {
        public void Dispose() { /* Nothing to do */ }

        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += delegate(object sender, EventArgs e)
            {
                HttpContext requestContext = ((HttpApplication)sender).Context;
                Stopwatch timer = new Stopwatch();
                requestContext.Items["Timer"] = timer;
                timer.Start();
            };
            context.PostRequestHandlerExecute += delegate(object sender, EventArgs e)
            {
                HttpContext requestContext = ((HttpApplication)sender).Context;
                Stopwatch timer = (Stopwatch)requestContext.Items["Timer"];
                timer.Stop();

                // Don't interfere with non-HTML responses
                var IsAjax = requestContext.Request.Headers.Get("x-requested-with");
                if ((requestContext.Response.ContentType == "text/html") && (IsAjax == null))
                {
                    double seconds = (double)timer.ElapsedTicks / Stopwatch.Frequency;
                    string result =
                     string.Format("{0:F4} seconds ({1:F0} req/sec).", seconds, 1 / seconds);
                    requestContext.Response.Write("<div class='PerformanceMonitor'>");
                    requestContext.Response.Write("Page loaded in " + result);
                    requestContext.Response.Write("</div>");
                }
            };
        }
    }
}