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
        RequestListener l = new RequestListener();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            l.Start();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            l.Stop();
        }

    }
}
