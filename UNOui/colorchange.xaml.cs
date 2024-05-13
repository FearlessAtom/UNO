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
                CardsList.allcards[Table.turn].addcard();
            }
            Table.nextturn();
        }
        private void remove()
        {
            Grid parent = (Grid)Parent;
            parent.Children.Remove(this);
        }
        private void greenbutton(object sender, RoutedEventArgs e)
        {
            remove();
            Table.topcard.color = "Green";
            if (Table.topcard.number == -4)
            {
                Table.topcard.image.Source = Cards.getcardimage("greendrawfour").Source;
                addfour();
            }
            else if (Table.topcard.number == -5)
            {
                Table.topcard.image.Source = Cards.getcardimage("greenwildcard").Source;
                Items.gameitem.botplaycard(CardsList.allcards[Table.turn].cards);
            }
            Canvas.SetZIndex(Table.topcard.image, 1);
            Table.refreshvisuals();
            Table.nextturn();
        }
        private void redbutton(object sender, RoutedEventArgs e)
        {
            remove();
            Table.topcard.color = "Red";
            if (Table.topcard.number == -4)
            {
                Table.topcard.image.Source = Cards.getcardimage("reddrawfour").Source;
                addfour();
            }
            else if (Table.topcard.number == -5)
            {
                Table.topcard.image.Source = Cards.getcardimage("redwildcard").Source;
                Items.gameitem.botplaycard(CardsList.allcards[Table.turn].cards);
            }
            Canvas.SetZIndex(Table.topcard.image, 1);
            Table.refreshvisuals();
            Table.nextturn();
        }
        private void bluebutton(object sender, RoutedEventArgs e)
        {
            remove();
            Table.topcard.color = "Blue";
            if (Table.topcard.number == -4)
            {
                Table.topcard.image.Source = Cards.getcardimage("bluedrawfour").Source;
                addfour();
            }
            else if (Table.topcard.number == -5)
            {
                Table.topcard.image.Source = Cards.getcardimage("bluewildcard").Source;
                Items.gameitem.botplaycard(CardsList.allcards[Table.turn].cards);
            }
            Canvas.SetZIndex(Table.topcard.image, 1);
            Table.refreshvisuals();
            Table.nextturn();
        }
        private void yellowbutton(object sender, RoutedEventArgs e)
        {
            remove();
            Table.topcard.color = "Yellow";
            if(Table.topcard.number == -4)
            {
                Table.topcard.image.Source = Cards.getcardimage("yellowdrawfour").Source;
                addfour();
            }
            else if (Table.topcard.number == -5)
            {
                Table.topcard.image.Source = Cards.getcardimage("yellowwildcard").Source;
                Items.gameitem.botplaycard(CardsList.allcards[Table.turn].cards);
            }
            Canvas.SetZIndex(Table.topcard.image, 1);
            Table.refreshvisuals();
            Table.nextturn();
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
