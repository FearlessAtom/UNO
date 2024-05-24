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
    /// Interaction logic for exitconfirmation.xaml
    /// </summary>
    public partial class exitconfirmation : UserControl
    {
        public exitconfirmation()
        {
            InitializeComponent();
        }
        private void toenglish()
        {
            maintextblock.Text = "Are you sure?";
            cancelbutton.Content = "Cancel";
            exitbutton.Content = "Exit";
        }
        private void toukrainian()
        {
            maintextblock.Text = "Вы впевнені?";
            cancelbutton.Content = "Закрити";
            exitbutton.Content = "Вийти";
        }
        private void loaded(object sender, RoutedEventArgs e)
        {
            Items.exitconfirmationitem = this;
            if(Settings.getlanguage() == 2)
            {
                toukrainian();
            }
            else
            {
                toenglish();
            }
        }
        public void cancel(object sender, RoutedEventArgs e)
        {
            Settings.setexitconfirmationopened(false);
            Grid parent = (Grid)Parent;
            parent.Children.Remove(this);
        }
        private void exit(object sender, RoutedEventArgs e)
        {
            Items.mainwindowitem.Close();
        }
        private void mouseenter(object sender, MouseEventArgs e)
        {
            Items.mainwindowitem.buttonmouseenter(sender, e);
        }
        private void mouseleave(object sender, MouseEventArgs e)
        {
            Items.mainwindowitem.buttonmouseleave(sender, e);
        }
    }
}
