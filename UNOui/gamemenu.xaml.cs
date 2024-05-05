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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Items.gameitem.addcard();
        }

        private void settings(object sender, RoutedEventArgs e)
        {
            //Items.gameitem.removecard(0);
        }
    }
}
