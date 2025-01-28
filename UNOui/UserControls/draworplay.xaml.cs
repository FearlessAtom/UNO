using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UNOui
{
    public partial class DrawOrPlay : UserControl
    {
        public DrawOrPlay()
        {
            InitializeComponent();
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            if (Settings.ForcePlay == 1)
            {
                playbutton.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetColumnSpan(playbutton, 2);
                drawbutton.Visibility = Visibility.Hidden;
            }
            Image image = new Image();
            Items.DrawOrPlayItem = this;
            cardimage.Source = Items.GameItem.player.Cards[Items.GameItem.player.Cards.Count - 1].image.Source;
        }

        public void Close()
        {
            Grid parent = (Grid)Parent;
            parent.Children.Remove(this);
        }

        public void DrawCard(object sender, RoutedEventArgs e)
        {
            Table.SetNextTurn();
            Table.CheckForTurn();
            Close();
        }

        private void PlayCard(object sender, RoutedEventArgs e)
        {
            Card card = Items.GameItem.player.Cards[Items.GameItem.player.Cards.Count - 1];
            Random random = new Random();
            int randomnumber = random.Next(-45, 45);

            if (Items.GameItem.player.Cards.Count > 1)
            {
                Table.AddToTopCards(randomnumber);
            }

            card.PlayCard();
            Table.topcard.image.RenderTransform = Card.rotate(randomnumber);
            Table.topcard.SetZIndexToOne();
            Items.GameItem.player.Cards.Remove(card);
            Items.GameItem.gamecanvas.Children.Remove(card.image);

            if (Table.topcard.number != -1)
            {
                Table.SetNextTurn();
            }

            Close();
            Table.RefreshVisuals();
            Task.Delay(TimeSpan.FromSeconds(0.5)).ContinueWith(task =>
            {
                if ((card.number == -4 || card.number == -5) && card.color == "none")
                {
                    UserControl changecolor = new ColorChange();
                    Items.GameItem.gamegrid.Children.Add(changecolor);
                }
                else
                {
                    Items.GameItem.player.CheckForWildCards();
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void ButtonMouseEnter(object sender, MouseEventArgs e)
        {
            Items.MainWindowItem.ButtonMouseEnter(sender, e);
        }

        private void ButtonMouseLeave(object sender, MouseEventArgs e)
        {
            Items.MainWindowItem.ButtonMouseLeave(sender, e);
        }
    }
}
