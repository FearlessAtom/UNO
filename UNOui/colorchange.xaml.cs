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
        public void addfour()
        {
            for (int index = 0; index < 4; index++)
            {
                Items.gameitem.addcard(CardsList.allcards[CardsList.turn]);
            }
            Items.gameitem.nextturn();
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
                CardsList.topcard.image.Source = Items.gameitem.getcardimage("greendrawfour").Source;
                addfour();
            }
            else if (CardsList.topcard.number == -5)
            {
                CardsList.topcard.image.Source = Items.gameitem.getcardimage("greenwildcard").Source;
                Items.gameitem.botplaycard(CardsList.allcards[CardsList.turn]);
            }
            Canvas.SetZIndex(CardsList.topcard.image, 1);
            Items.gameitem.one();
            Items.gameitem.nextturn();
        }
        private void redbutton(object sender, RoutedEventArgs e)
        {
            remove();
            CardsList.topcard.color = "Red";
            if (CardsList.topcard.number == -4)
            {
                CardsList.topcard.image.Source = Items.gameitem.getcardimage("reddrawfour").Source;
                addfour();
            }
            else if (CardsList.topcard.number == -5)
            {
                CardsList.topcard.image.Source = Items.gameitem.getcardimage("redwildcard").Source;
                Items.gameitem.botplaycard(CardsList.allcards[CardsList.turn]);
            }
            Canvas.SetZIndex(CardsList.topcard.image, 1);
            Items.gameitem.one();
            Items.gameitem.nextturn();
        }
        private void bluebutton(object sender, RoutedEventArgs e)
        {
            remove();
            CardsList.topcard.color = "Blue";
            if (CardsList.topcard.number == -4)
            {
                CardsList.topcard.image.Source = Items.gameitem.getcardimage("bluedrawfour").Source;
                addfour();
            }
            else if (CardsList.topcard.number == -5)
            {
                CardsList.topcard.image.Source = Items.gameitem.getcardimage("bluewildcard").Source;
                Items.gameitem.botplaycard(CardsList.allcards[CardsList.turn]);
            }
            Canvas.SetZIndex(CardsList.topcard.image, 1);
            Items.gameitem.one();
        }
        private void yellowbutton(object sender, RoutedEventArgs e)
        {
            remove();
            CardsList.topcard.color = "Yellow";
            if(CardsList.topcard.number == -4)
            {
                CardsList.topcard.image.Source = Items.gameitem.getcardimage("yellowdrawfour").Source;
                addfour();
            }
            else if (CardsList.topcard.number == -5)
            {
                CardsList.topcard.image.Source = Items.gameitem.getcardimage("yellowwildcard").Source;
                Items.gameitem.botplaycard(CardsList.allcards[CardsList.turn]);
            }
            Canvas.SetZIndex(CardsList.topcard.image, 1);
            Items.gameitem.one();
            Items.gameitem.nextturn();
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
