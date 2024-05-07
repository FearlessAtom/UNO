using System;
using System.Collections.Generic;
using System.Linq;
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
            if(Settings.getforceplay() == 1)
            {
                playbutton.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetColumnSpan(playbutton, 2);
                drawbutton.Visibility = Visibility.Hidden;
            }
            Image image = new Image();
            Items.draworplayitem = this;
            cardimage.Source = CardsList.playercards[CardsList.playercards.Count - 1].image.Source;
        }
        public static void playcard(Cards card)
        {
            Random random = new Random();
            int randomnumber = random.Next(-45, 45);
            Items.gameitem.addtotopcardsmemory(randomnumber);
            CardsList.topcard.image.Source = card.image.Source;
            CardsList.topcard.number = card.number;
            CardsList.topcard.color = card.color;
            CardsList.topcard.listcount = card.listcount;
            CardsList.topcard.image.RenderTransform = Items.gameitem.rotate(randomnumber);
            CardsList.playercards.Remove(card);
            Items.gameitem.gamecanvas.Children.Remove(card.image);
            Items.gameitem.one();
            if(card.number == -4 || card.number == -5)
            {
                UserControl changecolor = new colorchange();
                Items.gameitem.gamegrid.Children.Add(changecolor);
            }
            Canvas.SetZIndex(CardsList.topcard.image, 1);
        }
        public void remove()
        {
            Grid parent = (Grid)Parent;
            parent.Children.Remove(this);
        }
        private void draw(object sender, RoutedEventArgs e)
        {
            remove();
        }
        private void play(object sender, RoutedEventArgs e)
        {
            playcard(CardsList.playercards[CardsList.playercards.Count - 1]);
            remove();
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
