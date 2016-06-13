using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebToaster
{
    class RequestListener
    {
        public static HttpListener listener = new HttpListener();

        public void Start(string[] prefixes = null)
        {
            try
            {
                Stop();

                //netsh http add urlacl url=http://+:80/MyUri user=DOMAIN\user
                if (prefixes == null)
                {
                    listener.Prefixes.Add("http://*:8081/Incoming/");
                }
                else
                {
                    prefixes.ToList().ForEach(c => listener.Prefixes.Add(c));
                }
                listener.Start();

                System.Threading.ThreadPool.QueueUserWorkItem(Waiting);
            }
            catch (Exception error)
            {
                Console.Write(error.Message);
            }
        }

        public void Stop()
        {
            if (listener.IsListening)
            {
                listener.Stop();
                listener.Close();
            }
        }

        public bool IsListening
        {
            get
            {
                return listener.IsListening;
            }
        }

        void Waiting(object state)
        {
            while (listener.IsListening)
            {
                try
                {
                    var context = listener.BeginGetContext(new AsyncCallback(RequestHandler.ListenerCallback), listener);
                    context.AsyncWaitHandle.WaitOne();
                }
                catch (System.Net.HttpListenerException)
                {
                    return;
                }
                catch (System.ObjectDisposedException)
                {
                    return;
                }
            }
        }
    }
}
