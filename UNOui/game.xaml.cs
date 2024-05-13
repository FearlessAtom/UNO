using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Dynamic;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace UNOui
{
    /// <summary>
    /// Interaction logic for game.xaml
    /// </summary>
    public partial class game : UserControl
    {
        public game()
        {
            InitializeComponent();
        }
        public void closegame()
        {
            Settings.setgameopened(false);
            Items.mainwindowitem.play.Visibility = Visibility.Visible;
            Items.mainwindowitem.settings.Visibility = Visibility.Visible;
            Items.mainwindowitem.exit.Visibility = Visibility.Visible;
            gamecanvas.Children.Clear();
            player.cards.Clear();
            Grid parent = (Grid)Parent;
            parent.Children.Remove(this);
        }
        public CardsList player = new CardsList();
        public CardsList botone = new CardsList();
        private void loaded(object sender, RoutedEventArgs e)
        {
            Items.gameitem = this;
            Table.turn = 1;

            CardsList.allcards.Clear();
            CardsList.allcards.Add(player);
            CardsList.allcards.Add(botone);
            Table.topcardsrotateangle.Clear();
            Table.topcards.Clear();
            gamecanvas.Children.Clear();
            Table.topcard = Cards.randomcard();
            Table.topcardsrotateangle.Add(0);
            if (Table.topcard.number == -4 || Table.topcard.number == -5)
            {
                UserControl colorchange = new colorchange();
                gamegrid.Children.Add(colorchange);
            }
            //checkforwildcards(Table.topcard);
            player.setcards();
            botone.setcards();
        }
        public void settings()
        {
            if(player.cards.Count == 0) { return; }
            Cards card = player.cards[0];
            card.image.Source = Cards.getcardimage("drawfour").Source;
            card.color = "none";
            card.number = -4;
            Table.refreshvisuals();
        }
        public int actualgap = 10;
        public void deckdown(object sender, MouseButtonEventArgs e)
        {
            if(Table.turn != 1) { return; }
            player.addcard();
            Cards card = player.cards[player.cards.Count - 1];
            if(card.comparecard())
            {
                UserControl draworplay = new draworplay();
                gamegrid.Children.Add(draworplay);
            }
            else
            {
                Table.nextturn();
                Table.checkforturn();
            }
        }
        public void menu(object sender, RoutedEventArgs e)
        {
            Settings.setgamemenuopened(true);
            UserControl menu = new gamemenu();
            gamegrid.Children.Add(menu);
        }
        private void mouseenter(object sender, MouseEventArgs e)
        {
            Items.mainwindowitem.buttonmouseenter(sender, e);
        }
        private void mouseleave(object sender, MouseEventArgs e)
        {
            Items.mainwindowitem.buttonmouseleave(sender, e);
        }
        UIElement drag = null;
        bool dragging = false;
        Point offset;
        int cardzindex;
        public void addtotopcardsmemory(int randomnumber)
        {
            System.Windows.Controls.Image image = Cards.randomcard().image;
            image.Width = image.Height = 150;
            image.Source = Table.topcard.image.Source;
            Table.topcards.Add(image);
            Table.topcardsrotateangle.Add(randomnumber);
        }
        public void mousedown(object sender, MouseButtonEventArgs e)
        {
            if(Table.turn != 1) { return; }
            drag = (System.Windows.Controls.Image)sender;
            cardzindex = Canvas.GetZIndex(drag);
            Canvas.SetZIndex(drag, 1);
            Table.draggedimage = (System.Windows.Controls.Image)drag;
            offset = e.GetPosition(gamecanvas);
            offset.Y = offset.Y - Canvas.GetTop(drag);
            offset.X = offset.X - Canvas.GetLeft(drag);
            gamecanvas.CaptureMouse();
            dragging = true;
        }
        public void mousemove(object sender, MouseEventArgs e)
        {
            if(drag == null){return;}
            var position = e.GetPosition((IInputElement)sender);
            Canvas.SetTop(drag, position.Y - offset.Y);
            Canvas.SetLeft(drag, position.X - offset.X);
        }
        public void mouseup(object sender, MouseButtonEventArgs e)
        {
            if (Table.draggedimage != null)
            {
                Canvas.SetZIndex(Table.draggedimage, cardzindex);
                double bottom = Canvas.GetTop(Table.draggedimage);
                if (bottom < gamecanvas.ActualHeight * (Table.draggedimage.Height / (gamecanvas.ActualHeight / 2)) && Cards.imagetocard(Table.draggedimage).comparecard())
                {
                    Cards draggedcard = Cards.imagetocard(Table.draggedimage);
                    Random random = new Random();
                    int randomnumber = random.Next(-45, 45);
                    if(player.cards.Count > 1)
                    {
                        addtotopcardsmemory(randomnumber);
                    }
                    draggedcard.playcard();
                    Table.topcard.image.RenderTransform = Cards.rotate(randomnumber);
                    if ((draggedcard.number == -4 || draggedcard.number == -5) && draggedcard.color == "none")
                    {
                        UserControl changecolor = new colorchange();
                        gamegrid.Children.Add(changecolor);
                    }
                    else
                    {
                        checkforwildcards(Table.topcard);
                        Table.nextturn();
                        Table.checkforturn();
                    }
                    Canvas.SetZIndex(Table.topcard.image, 1);
                    player.cards.Remove(draggedcard);
                    gamecanvas.Children.Remove(draggedcard.image);
                    Canvas.SetZIndex(Table.draggedimage, cardzindex);
                }
                drag = null;
                gamecanvas.ReleaseMouseCapture();
                dragging = false;
                Table.refreshvisuals();
            }
        }
        public void checkforwildcards(Cards card)
        {
            if (card.number == -5 && Table.turn != 1)
            {
                card.image = Cards.getcardimage("bluewildcard");
                card.color = "Blue";
            }
            else if (card.number == -4 && Table.turn != 1)
            {
                card.image = Cards.getcardimage("bluedrawfour");
                card.color = "Blue";
                Table.nextturn();
                for (int index = 0; index < 4; index++)
                {
                    CardsList.allcards[Table.turn - 1].addcard();
                }
            }
            else if(card.number == -1)
            {
                if(Settings.getplayercount() == 2)
                {
                    Table.nextturn();
                }
            }
            else if (card.number == -2)
            {
                Table.nextturn();
                CardsList.allcards[Table.turn - 1].addcard();
                CardsList.allcards[Table.turn - 1].addcard();
            }
            else if(card.number == -3)
            {
                Table.nextturn();
            }
        }
        public void botplaycard(List<Cards> cards)
        {
            if (cards.Count == 0) {return; }
            Random random = new Random();
            int randomnumber = random.Next(-45, 45);
            bool foundplayablecard = false;
            Task.Delay(TimeSpan.FromSeconds(1.5)).ContinueWith(task =>
            {
                for (int index = 0; index < cards.Count; index++)
                {
                    if (cards[index].comparecard())
                    {
                        addtotopcardsmemory(randomnumber);
                        checkforwildcards(cards[index]);
                        cards[index].playcard();
                        cards.RemoveAt(index);
                        foundplayablecard = true;
                        Canvas.SetZIndex(Table.topcard.image, 1);
                        Table.topcard.image.RenderTransform = Cards.rotate(randomnumber);
                        break;
                    }
                }
                if (!foundplayablecard)
                {
                    Cards newcard = Cards.randomcard();
                    if (newcard.comparecard())
                    {
                        addtotopcardsmemory(randomnumber);
                        checkforwildcards(newcard);
                        newcard.playcard();
                        Canvas.SetZIndex(Table.topcard.image, 1);
                        Table.topcard.image.RenderTransform = Cards.rotate(randomnumber);
                    }
                    else
                    {
                        cards.Add(newcard);
                    }
                }
                Table.nextturn();
                Table.checkforturn();
                Table.refreshvisuals();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        private void resize(object sender, SizeChangedEventArgs e)
        {
            Table.refreshvisuals();
        }
    }
}