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

namespace UNOui
{
    /// <summary>
    /// Interaction logic for confirmation.xaml
    /// </summary>
    public partial class confirmation : UserControl
    {
        public confirmation()
        {
            InitializeComponent();
        }
        public void cancel(object sender, RoutedEventArgs e)
        {
            Settings.setconfirmation(false);
            Grid panel = (Grid)Parent;
            panel.Children.Remove(this);
        }
        private void buttonmouseenter(object sender, MouseEventArgs e)
        {
            Items.mainwindowitem.buttonmouseenter(sender, e);
        }
        private void buttonmouseleave(object sender, MouseEventArgs e)
        {
            Items.mainwindowitem.buttonmouseleave(sender, e);
        }
        private void dontsave(object sender, RoutedEventArgs e)
        {
            Grid panel = (Grid)Parent;
            panel.Children.Remove(this);
            Items.settingsitem.closesettings(sender, e);
        }
        private void save(object sender, RoutedEventArgs e)
        {
            Grid panel = (Grid)Parent;
            panel.Children.Remove(this);
            Items.settingsitem.savesettings(sender, e);
            Items.settingsitem.closesettings(sender, e);

        }
        private void loaded(object sender, RoutedEventArgs e)
        {
            if(Settings.getlanguage() == 1)
            {
                toenglish();
            }
            else
            {
                toukrainian();
            }
        }
        private void toenglish()
        {
            message.Text = "Unsaved changes";
        }
        private void toukrainian()
        {
            message.Text = "Незбережені зміни";
        }
    }
}
