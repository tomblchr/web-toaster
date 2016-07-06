using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace WebToaster
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static RequestListener listener = new RequestListener();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            (new Notifier()).Register();

            listener.Start();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            listener.Stop();

            base.OnExit(e);
        }

    }
}
