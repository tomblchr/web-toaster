using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebToaster
{
    static class RequestHandler
    {
        //IServiceCollection
        static object _lock = new object();
        static List<INotificationProxy> _servicecollection = new List<INotificationProxy>();

        class CallInfo
        {
            string _message;

            public CallInfo(NameValueCollection p)
            {
                CallerID = PhoneNumber = string.Empty;

                if (p.AllKeys.Contains("caller"))
                {
                    CallerID = p["caller"];
                }
                if (p.AllKeys.Contains("number"))
                {
                    PhoneNumber = p["number"];
                }
                if (p.AllKeys.Contains("msg"))
                {
                    _message = p["msg"];
                }
            }

            public string PhoneNumber;
            public string CallerID;

            public string Message
            {
                get
                { 
                    if (string.IsNullOrEmpty(CallerID) && string.IsNullOrEmpty(PhoneNumber))
                    {
                        return _message;
                    }
                    return string.Format("Incoming Call: {0} ({1})", CallerID, PhoneNumber); 
                }
            }
        }

        public static void Register(this INotificationProxy proxy)
        {
            lock (_lock)
            {
                var existing = _servicecollection.Where(c => c.GetType() == proxy.GetType());
                if (existing.Any())
                {
                    existing.ToList().ForEach(c => _servicecollection.Remove(c));
                }
                _servicecollection.Add(proxy);
            }
        }

        public static void ListenerCallback(IAsyncResult result)
        {
            HttpListener l = (HttpListener)result.AsyncState;

            if (l.IsListening)
            {
                var context = l.EndGetContext(result);
                CallInfo c = new CallInfo(context.Request.QueryString);
                _servicecollection.ForEach(s => s.Send(c.Message));
                context.Response.Close();
            }
        }
    }
}
