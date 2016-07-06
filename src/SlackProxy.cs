using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebToaster
{
    /// <summary>
    /// Class for forwarding message to Slack WebHook URL
    /// </summary>
    class SlackProxy : INotificationProxy
    {
        public string Uri { get; set; }

        public async void Send(string msg)
        {
            var upload = string.Format("{{\"text\" : \"{0}\"}}", msg);
            var b = Encoding.Default.GetBytes(upload);
            using (HttpClient _web = new HttpClient())
            {
                var result = await _web.PostAsync(Uri, new StringContent(upload, Encoding.UTF8, "application/json"));
            }
        }

        public string PayLoad
        {
            get;
            set;
        }
    }
}
