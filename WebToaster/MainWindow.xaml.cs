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
            if (App.l.IsListening)
            {
                App.l.Stop();
            }
            else
            {
                App.l.Start(new string[] { textBox.Text });
            }
            Sync();
        }

        void Sync()
        {
            textBox.IsEnabled = !App.l.IsListening;
            button.Content = App.l.IsListening
                ? "Stop"
                : "Start";
        }
    }
}
