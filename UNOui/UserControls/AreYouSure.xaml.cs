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
        int action;
        public exitconfirmation(int action)
        {
            InitializeComponent();
            this.action = action;
        }
        private void toenglish()
        {
            maintextblock.Text = "Are you sure?";
            cancelbutton.Content = "Cancel";
            exitbutton.Content = (action == 2 ? "Restart" : "Exit"); 
        }
        private void toukrainian()
        {
            maintextblock.Text = "Вы впевнені?";
            cancelbutton.Content = "Закрити";
            exitbutton.Content = (action == 2 ? "Рестарт" : "Вийти");
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
            if (action == 1)
            {
                Settings.setexitconfirmationopened(false);
            }
            Grid parent = (Grid)Parent;
            parent.Children.Remove(this);
        }
        private void exit(object sender, RoutedEventArgs e)
        {
            switch (action)
            {
                case 1:
                    Items.mainwindowitem.Close();
                    break;
                case 2:
                    cancel(new object(), new RoutedEventArgs());
                    Items.gamemenuitem.resume(sender, e);
                    Items.gameitem.loaded(sender, e);
                    break;
                case 3:
                    Items.gamemenuitem.resume(sender, e);
                    Items.gameitem.closegame();
                    break;
            }
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
