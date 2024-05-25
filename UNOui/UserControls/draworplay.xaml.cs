using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
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
    /// Interaction logic for draworplay.xaml
    /// </summary>
    public partial class draworplay : UserControl
    {
        public draworplay()
        {
            InitializeComponent();
        }
        private void loaded(object sender, RoutedEventArgs e)
        {
            if (Settings.getforceplay() == 1)
            {
                playbutton.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetColumnSpan(playbutton, 2);
                drawbutton.Visibility = Visibility.Hidden;
            }
            Image image = new Image();
            Items.draworplayitem = this;
            cardimage.Source = Items.gameitem.player.cards[Items.gameitem.player.cards.Count - 1].image.Source;
        }
        public void remove()
        {
            Grid parent = (Grid)Parent;
            parent.Children.Remove(this);
        }
        public void draw(object sender, RoutedEventArgs e)
        {
            Table.nextturn();
            Table.checkforturn();
            remove();
        }
        private void play(object sender, RoutedEventArgs e)
        {
            Card card = Items.gameitem.player.cards[Items.gameitem.player.cards.Count - 1];
            Random random = new Random();
            int randomnumber = random.Next(-45, 45);
            if (Items.gameitem.player.cards.Count > 1)
            {
                Table.addtotopcards(randomnumber);
            }
            card.playcard();
            Table.topcard.image.RenderTransform = Card.rotate(randomnumber);
            Table.topcard.zindex();
            Items.gameitem.player.cards.Remove(card);
            Items.gameitem.gamecanvas.Children.Remove(card.image);
            if (Table.topcard.number != -1)
            {
                Table.nextturn();
            }
            remove();
            Table.refreshvisuals();
            Task.Delay(TimeSpan.FromSeconds(0.5)).ContinueWith(task =>
            {
                if ((card.number == -4 || card.number == -5) && card.color == "none")
                {
                    UserControl changecolor = new colorchange();
                    Items.gameitem.gamegrid.Children.Add(changecolor);
                }
                else
                {
                    Items.gameitem.player.checkforwildcard();
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
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
