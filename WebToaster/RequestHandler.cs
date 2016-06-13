using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebToaster
{
    static class RequestHandler
    {
        public static void ListenerCallback(IAsyncResult result)
        {
            HttpListener l = (HttpListener)result.AsyncState;

            if (l.IsListening)
            {
                var context = l.EndGetContext(result);
                Notifier n = new Notifier();
                n.PopUp(context.Request.RawUrl);
                context.Response.Close();
            }
        }
    }
}
