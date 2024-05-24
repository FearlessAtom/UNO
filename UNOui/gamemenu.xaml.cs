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
    /// Interaction logic for gamemenu.xaml
    /// </summary>
    public partial class gamemenu : UserControl
    {
        public gamemenu()
        {
            InitializeComponent();
        }
        public void toenglish()
        {
            mainmenubutton.Content = "Main menu";
            settingsbutton.Content = "Settings";
            restartbutton.Content = "Restart";
            resumebutton.Content = "Resume";
        }
        public void toukrainian()
        {
            mainmenubutton.Content = "Головне меню";
            settingsbutton.Content = "Налаштування";
            restartbutton.Content = "Рестарт";
            resumebutton.Content = "Продовжити";
        }
        public void resume(object sender, RoutedEventArgs e)
        {
            Settings.setgamemenuopened(false);
            Grid parent = (Grid)Parent;
            parent.Children.Remove(this);
        }
        private void closegame(object sender, RoutedEventArgs e)
        {
            resume(sender, e);
            Items.gameitem.closegame();
        }
        private void mouseenter(object sender, MouseEventArgs e)
        {
            Items.mainwindowitem.buttonmouseenter(sender, e);
        }
        private void mouseleave(object sender, MouseEventArgs e)
        {
            Items.mainwindowitem.buttonmouseleave(sender, e);
        }
        private void loaded(object sender, RoutedEventArgs e)
        {
            Items.gamemenuitem = this;
            if (Settings.getlanguage() == 2)
            {
                toukrainian();
            }
            else
            {
                toenglish();
            }
        }
        private void restart(object sender, RoutedEventArgs e)
        {
            resume(sender, e);
            Items.gameitem.loaded(sender, e);
        }
        private void settings(object sender, RoutedEventArgs e)
        {
            Items.gameitem.settings();
        }
    }
}
