using System;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;

namespace NeoFLWeb.Shared
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class KeepSessionAlive : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Session["KeepSessionAlive"] = DateTime.Now;
            //context.Response.AddHeader("Content-Length", "0"); 
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
