using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;

namespace NNS.CustomEvents
{
    /// <summary>
    /// Summary description for Class1
    /// </summary>
    public abstract class WebCustomEvent : WebBaseEvent
    {
        public WebCustomEvent(string message, object eventSource, int eventCode) : base (message, eventSource, eventCode)  
    	{
    	}
    }

    public class RecordDeletedEvent : WebCustomEvent
    {
        private const int eventCode = WebEventCodes.WebExtendedBase + 10;
        private const string message = "The {0} with ID = {1} was deleted by users {2}.";
        private const string message2 = "Deleted__Type={0}__ID={1}__Code={3}__MasterID={2}__MasterCode={4}__User={5}";

        public RecordDeletedEvent(string entity, int id, object eventSource)
            : base(string.Format(message, entity, id,
                HttpContext.Current.User.Identity.Name), eventSource, eventCode)
        {
        }
        public RecordDeletedEvent(string entity, int id, int mastID, string code, string mastCode, object eventSource)
            : base(string.Format(message2, entity, id, mastID, code, mastCode,
                HttpContext.Current.User.Identity.Name), eventSource, eventCode)
        {
        }
    }
    public class RecordCalceledEvent : WebCustomEvent
    {
        private const int eventCode = WebEventCodes.WebExtendedBase + 11;
        private const string message = "Canceled__Type={0}__ID={1}__Code={3}__MasterID={2}__MasterCode={4}__User={5}";

        public RecordCalceledEvent(string entity, int id, int mastID, string code, string mastCode, object eventSource)
            : base(string.Format(message, entity, id, mastID, code, mastCode,
                HttpContext.Current.User.Identity.Name), eventSource, eventCode)
        {
        }
    }
}