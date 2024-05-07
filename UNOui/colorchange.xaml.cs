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
    /// Interaction logic for colorchange.xaml
    /// </summary>
    public partial class colorchange : UserControl
    {
        public colorchange()
        {
            InitializeComponent();
        }
        private void remove()
        {
            Grid parent = (Grid)Parent;
            parent.Children.Remove(this);
        }
        private void greenbutton(object sender, RoutedEventArgs e)
        {
            remove();
            CardsList.topcard.color = "Green";
            if (CardsList.topcard.number == -4)
            {
                CardsList.topcard.image = Items.gameitem.getcardimage("greendrawfour");
            }
            else if (CardsList.topcard.number == -5)
            {
                CardsList.topcard.image = Items.gameitem.getcardimage("greenwildcard");
            }
            Items.gameitem.one();
        }
        private void redbutton(object sender, RoutedEventArgs e)
        {
            remove();
            CardsList.topcard.color = "Red";
            if (CardsList.topcard.number == -4)
            {
                CardsList.topcard.image = Items.gameitem.getcardimage("reddrawfour");
            }
            else if (CardsList.topcard.number == -5)
            {
                CardsList.topcard.image = Items.gameitem.getcardimage("redwildcard");
            }
            Items.gameitem.one();
        }
        private void bluebutton(object sender, RoutedEventArgs e)
        {
            remove();
            CardsList.topcard.color = "Blue";
            if (CardsList.topcard.number == -4)
            {
                CardsList.topcard.image = Items.gameitem.getcardimage("bluedrawfour");
            }
            else if (CardsList.topcard.number == -5)
            {
                CardsList.topcard.image = Items.gameitem.getcardimage("bluewildcard");
            }
            Items.gameitem.one();
        }
        private void yellowbutton(object sender, RoutedEventArgs e)
        {
            remove();
            CardsList.topcard.color = "Yellow";
            if(CardsList.topcard.number == -4)
            {
                CardsList.topcard.image = Items.gameitem.getcardimage("yellowdrawfour");
            }
            else if (CardsList.topcard.number == -5)
            {
                CardsList.topcard.image = Items.gameitem.getcardimage("yellowwildcard");
            }
            Items.gameitem.one();
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

        }
    }
}
