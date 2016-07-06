using NotificationsExtensions.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NotificationsExtensions.Toasts;
using System.Xml;
using Windows.UI.Notifications;
using System.Threading;

namespace WebToaster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Sync();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (App.listener.IsListening)
            {
                App.listener.Stop();
            }
            else
            {
                App.listener.Start(new string[] { textBox.Text });
            }
            Sync();
        }

        void Sync()
        {
            textBox.IsEnabled = !App.listener.IsListening;
            button.Content = App.listener.IsListening
                ? "Stop"
                : "Start";
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            textBoxSlackEndPoint.IsEnabled = ((CheckBox)sender).IsChecked.HasValue && ((CheckBox)sender).IsChecked.Value;
            if (textBoxSlackEndPoint.IsEnabled)
            {
                SlackProxy p = new SlackProxy();
                p.Uri = textBoxSlackEndPoint.Text;
                p.Register();
            }
        }
    }
}
