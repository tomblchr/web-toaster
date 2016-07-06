using NotificationsExtensions.Toasts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace WebToaster
{
    class Notifier : INotificationProxy
    {
        public void Send(string message)
        {
            ToastContent content = new ToastContent()
            {
                Launch = "lei",

                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Attribution = new ToastGenericAttributionText() { Text = message }
                    }
                },
                Scenario = ToastScenario.Default,
                Audio = new ToastAudio()
                {
                    Src = new Uri("ms-winsoundevent:Notification.IM")
                }
            };

            var d = new Windows.Data.Xml.Dom.XmlDocument();
            d.LoadXml(content.GetContent());

            var toast = new ToastNotification(d);
            ToastNotificationManager.CreateToastNotifier("Web Toaster").Show(toast);
        }
    }
}
