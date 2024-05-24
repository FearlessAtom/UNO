using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            Table.refreshvisuals();
            Task.Delay(TimeSpan.FromSeconds(0.5)).ContinueWith(task =>
            {
                CardsList.allcards[Table.turn - 1].addcard();
            }, TaskScheduler.FromCurrentSynchronizationContext());
            Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith(task =>
            {
                CardsList.allcards[Table.turn - 1].addcard();
            }, TaskScheduler.FromCurrentSynchronizationContext());
            Task.Delay(TimeSpan.FromSeconds(1.5)).ContinueWith(task =>
            {
                CardsList.allcards[Table.turn - 1].addcard();
            }, TaskScheduler.FromCurrentSynchronizationContext());
            Task.Delay(TimeSpan.FromSeconds(2)).ContinueWith(task =>
            {
                CardsList.allcards[Table.turn - 1].addcard();
                Table.nextturn();
                Table.checkforturn();
                Table.refreshvisuals();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        private void remove()
        {
            Grid parent = (Grid)Parent;
            parent.Children.Remove(this);
        }
        public void changecolor(string color, string drawfour, string wildcard)
        {
            remove();
            Table.topcard.color = color;
            if (Table.topcard.number == -4)
            {
                Table.topcard.image.Source = Cards.getcardimage(drawfour).Source;
                addfour();
            }
            else if (Table.topcard.number == -5)
            {
                Table.topcard.image.Source = Cards.getcardimage(wildcard).Source;
                Table.refreshvisuals();
                Table.checkforturn();
            }
        }
        private void greenbutton(object sender, RoutedEventArgs e)
        {
            changecolor("Green","greendrawfour", "greenwildcard");
        }
        private void redbutton(object sender, RoutedEventArgs e)
        {
            changecolor("Red", "reddrawfour", "redwildcard");
        }
        private void bluebutton(object sender, RoutedEventArgs e)
        {
            changecolor("Blue", "bluedrawfour", "bluewildcard");
        }
        private void yellowbutton(object sender, RoutedEventArgs e)
        {
            changecolor("Yellow", "yellowdrawfour", "yellowwildcard");
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
