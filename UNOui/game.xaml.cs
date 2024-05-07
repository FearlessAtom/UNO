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
        int actualgap = 10;
        public void deckdown(object sender, MouseButtonEventArgs e)
        {
            addcard();
            Cards card = CardsList.playercards[CardsList.playercards.Count - 1];
            if(comparecard(CardsList.topcard, card))
            {
                
                UserControl draworplay = new draworplay();
                gamegrid.Children.Add(draworplay);
            }
        }
        public void getdeck(int gap)
        {
            System.Windows.Controls.Image deck = new System.Windows.Controls.Image();
            deck = getcardimage("cardbackground");
            deck.Width = deck.Height = 150;
            deck.MouseDown += deckdown;
            CardsList.deckimage = deck;
            Canvas.SetTop(deck, 0);
            Canvas.SetLeft(deck, gamecanvas.ActualWidth / 4 - gap);
            gamecanvas.Children.Add(deck);
            if (gap <= actualgap * 3) { getdeck(gap + actualgap); }
        }
        private void loaded(object sender, RoutedEventArgs e)
        {
            Items.gameitem = this;
            CardsList.topcard = Cards.randomcard();
            System.Windows.Controls.Image deck2 = new System.Windows.Controls.Image();
            System.Windows.Controls.Image deck3 = new System.Windows.Controls.Image();
            if (CardsList.topcard.number == -4 || CardsList.topcard.number == -5)
            {
                UserControl colorchange = new colorchange();
                gamegrid.Children.Add(colorchange);
            }
            for (int index = 0; index < Settings.getcardcount(); index++)
            {
                addcard();
            }
        }
        public void closegame()
        {
            Settings.setgameopened(false);
            Items.mainwindowitem.play.Visibility = Visibility.Visible;
            Items.mainwindowitem.settings.Visibility = Visibility.Visible;
            Items.mainwindowitem.exit.Visibility = Visibility.Visible;
            gamecanvas.Children.Clear();
            CardsList.playercards.Clear();
            Grid parent = (Grid)Parent;
            parent.Children.Remove(this);
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
        public System.Windows.Controls.Image getcardimage(string card)
        {
            System.Windows.Controls.Image image = new System.Windows.Controls.Image();
            string source = "C:\\Users\\357\\Desktop\\UNO\\UNOui\\assets\\UNOcards\\" + card + ".png";
            image.Source = new BitmapImage(new Uri(source));
            return image;
        }
        UIElement drag = null;
        bool dragging = false;
        Point offset;
        int cardzindex;
        private void mousedown(object sender, MouseButtonEventArgs e)
        {
            drag = (System.Windows.Controls.Image)sender;
            cardzindex = Canvas.GetZIndex(drag);
            Canvas.SetZIndex(drag, 1);
            CardsList.draggedimage = (System.Windows.Controls.Image)drag;
            offset = e.GetPosition(gamecanvas);
            offset.Y = offset.Y - Canvas.GetTop(drag);
            offset.X = offset.X - Canvas.GetLeft(drag);
            gamecanvas.CaptureMouse();
            dragging = true;
        }
        private void mousemove(object sender, MouseEventArgs e)
        {
            if(drag == null){return;}
            var position = e.GetPosition((IInputElement)sender);
            Canvas.SetTop(drag, position.Y - offset.Y);
            Canvas.SetLeft(drag, position.X - offset.X);
        }
        private void mouseup(object sender, MouseButtonEventArgs e)
        {
            if(CardsList.draggedimage != null)
            {
                Canvas.SetZIndex(CardsList.draggedimage, cardzindex);
                double bottom = Canvas.GetTop(CardsList.draggedimage);
                if (bottom < gamecanvas.ActualHeight * (CardsList.draggedimage.Height / gamecanvas.ActualHeight) && comparecard(CardsList.topcard, imagetocard(CardsList.draggedimage))){
                    CardsList.topcard.image.Source = CardsList.draggedimage.Source;
                    
                    for(int index = 0; index < CardsList.playercards.Count; index++)
                    {
                        if (CardsList.playercards[index].image == CardsList.draggedimage)
                        {
                            CardsList.topcard.number = CardsList.playercards[index].number;
                            CardsList.topcard.color = CardsList.playercards[index].color;
                            CardsList.topcard.listcount = CardsList.playercards[index].listcount;
                            if ((CardsList.playercards[index].number == -4 || CardsList.playercards[index].number == -5) && CardsList.playercards[index].color == "none")
                            {
                                UserControl changecolor = new colorchange();
                                gamegrid.Children.Add(changecolor);
                            }
                            break;
                        }
                    }
                    gamecanvas.Children.Remove(CardsList.topcard.image);
                    gamecanvas.Children.Remove(CardsList.draggedimage);
                    CardsList.playercards.Remove(CardsList.imagetocard(CardsList.draggedimage));
                }
                Canvas.SetZIndex(CardsList.draggedimage, cardzindex);
                drag = null;
                gamecanvas.ReleaseMouseCapture();
                dragging = false;
                one();
            }
        }
        private void cardenter(object sender, MouseEventArgs e)
        {
            if (!dragging)
            {
                System.Windows.Controls.Image image = (System.Windows.Controls.Image)sender;
                Canvas.SetTop(image, (int)gamecanvas.ActualHeight - image.Width - 30);
            }
        }
        private void cardleave(object sender, MouseEventArgs e)
        {
            if (!dragging)
            {
                System.Windows.Controls.Image image = (System.Windows.Controls.Image)sender;
                Canvas.SetTop(image, (int)gamecanvas.ActualHeight - image.Width);
                one();
            }
        }
        private void resize(object sender, SizeChangedEventArgs e)
        {
            one();
        }
        public void removecard(Cards card)
        {
            gamecanvas.Children.Remove(card.image);
            CardsList.playercards.Remove(card);
            one();
        }
        public void one()
        {
            if (CardsList.playercards.Count != 0)
            {
                gamecanvas.Children.Clear();
                CardsList.topcard.image.Width = CardsList.topcard.image.Height = 150;
                Canvas.SetTop(CardsList.topcard.image, 0);
                Canvas.SetLeft(CardsList.topcard.image, gamecanvas.ActualWidth / 2 - CardsList.topcard.image.Width / 2);
                gamecanvas.Children.Add(CardsList.topcard.image);
                int gap = ((CardsList.playercards.Count * 150 - CardsList.playercards.Count * 100) / 2) - 30;
                for (int index = 0; index < CardsList.playercards.Count; index++)
                {
                    System.Windows.Controls.Image image = CardsList.playercards[index].image;
                    image.Width = 150;
                    image.Height = 150;
                    Canvas.SetTop(image, (int)gamecanvas.ActualHeight - image.Width);
                    Canvas.SetLeft(image, (int)gamecanvas.ActualWidth / 2 - (image.Width / 2) + index * 50 - gap);
                    gamecanvas.Children.Add(image);
                }
                getdeck(0);
                playable();
            }
        }
        public void playable()
        {
            Cards topcard = CardsList.topcard;
            bool playable = false; ;
            if(topcard == null) { return; }
            for(int index = 0; index < CardsList.playercards.Count; ++index)
            {
                if(CardsList.playercards[index].color == "none" || topcard.color == CardsList.playercards[index].color || topcard.number == CardsList.playercards[index].number)
                {
                    Canvas.SetTop(CardsList.playercards[index].image, (int)gamecanvas.ActualHeight - CardsList.playercards[index].image.Width - 7);
                    playable = true;
                }
            }
            System.Windows.Controls.Image image = new System.Windows.Controls.Image();
            DropShadowEffect dropShadowEffect = new DropShadowEffect
            {
                Color = Colors.LightYellow,
                Direction = 135,
                ShadowDepth = 5,
                Opacity = 0.7
            };
            if (!playable) { CardsList.deckimage.Effect = dropShadowEffect; }
        }
        public Cards getcard()
        {
            List<Cards> cards = new List<Cards>
            {
                //Draw cards
                new Cards(Items.gameitem.getcardimage("drawfour"), -1, "none", 0),
                new Cards(Items.gameitem.getcardimage("wildcard"), -1, "none", 0),

                //Yellow
                new Cards(Items.gameitem.getcardimage("yellowreverse"), -1, "Yellow", 0),
                new Cards(Items.gameitem.getcardimage("yellowdrawtwo"), -1, "Yellow", 0),
                new Cards(Items.gameitem.getcardimage("yellowblock"), -1, "Yellow", 0),
                new Cards(Items.gameitem.getcardimage("yellowone"), 1, "Yellow", 0),
                new Cards(Items.gameitem.getcardimage("yellowtwo"), 2, "Yellow", 0),
                new Cards(Items.gameitem.getcardimage("yellowthree"), 3, "Yellow", 0),
                new Cards(Items.gameitem.getcardimage("yellowfour"), 4, "Yellow", 0),
                new Cards(Items.gameitem.getcardimage("yellowfive"), 5, "Yellow", 0),
                new Cards(Items.gameitem.getcardimage("yellowsix"), 6, "Yellow", 0),
                new Cards(Items.gameitem.getcardimage("yellowseven"), 7, "Yellow", 0),
                new Cards(Items.gameitem.getcardimage("yelloweight"), 8, "Yellow", 0),
                new Cards(Items.gameitem.getcardimage("yellownine"), 9, "Yellow", 0),
                
                //Blue
                new Cards(Items.gameitem.getcardimage("bluereverse"), -1, "Blue", 0),
                new Cards(Items.gameitem.getcardimage("bluedrawtwo"), -1, "Blue", 0),
                new Cards(Items.gameitem.getcardimage("blueblock"), -1, "Blue", 0),
                new Cards(Items.gameitem.getcardimage("blueone"), 1, "Blue", 0),
                new Cards(Items.gameitem.getcardimage("bluetwo"), 2, "Blue", 0),
                new Cards(Items.gameitem.getcardimage("bluethree"), 3, "Blue", 0),
                new Cards(Items.gameitem.getcardimage("bluefour"), 4, "Blue", 0),
                new Cards(Items.gameitem.getcardimage("bluefive"), 5, "Blue", 0),
                new Cards(Items.gameitem.getcardimage("bluesix"), 6, "Blue", 0),
                new Cards(Items.gameitem.getcardimage("blueseven"), 7, "Blue", 0),
                new Cards(Items.gameitem.getcardimage("blueeight"), 8, "Blue", 0),
                new Cards(Items.gameitem.getcardimage("bluenine"), 9, "Blue", 0),
                
                //Red
                new Cards(Items.gameitem.getcardimage("redreverse"), -1, "Red", 0),
                new Cards(Items.gameitem.getcardimage("reddrawtwo"), -1, "Red", 0),
                new Cards(Items.gameitem.getcardimage("redblock"), -1, "Red", 0),
                new Cards(Items.gameitem.getcardimage("redone"), 1, "Red", 0),
                new Cards(Items.gameitem.getcardimage("redtwo"), 2, "Red", 0),
                new Cards(Items.gameitem.getcardimage("redthree"), 3, "Red", 0),
                new Cards(Items.gameitem.getcardimage("redfour"), 4, "Red", 0),
                new Cards(Items.gameitem.getcardimage("redfive"), 5, "Red", 0),
                new Cards(Items.gameitem.getcardimage("redsix"), 6, "Red", 0),
                new Cards(Items.gameitem.getcardimage("redseven"), 7, "Red", 0),
                new Cards(Items.gameitem.getcardimage("redeight"), 8, "Red", 0),
                new Cards(Items.gameitem.getcardimage("rednine"), 9, "Red", 0),

                //Green
                new Cards(Items.gameitem.getcardimage("greenreverse"), -1, "Green", 0),
                new Cards(Items.gameitem.getcardimage("greendrawtwo"), -1, "Green", 0),
                new Cards(Items.gameitem.getcardimage("greenblock"), -1, "Green", 0),
                new Cards(Items.gameitem.getcardimage("greenone"), 1, "Green", 0),
                new Cards(Items.gameitem.getcardimage("greentwo"), 2, "Green", 0),
                new Cards(Items.gameitem.getcardimage("greenthree"), 3, "Green", 0),
                new Cards(Items.gameitem.getcardimage("greenfour"), 4, "Green", 0),
                new Cards(Items.gameitem.getcardimage("greenfive"), 5, "Green", 0),
                new Cards(Items.gameitem.getcardimage("greensix"), 6, "Green", 0),
                new Cards(Items.gameitem.getcardimage("greenseven"), 7, "Green", 0),
                new Cards(Items.gameitem.getcardimage("greeneight"), 8, "Green", 0),
                new Cards(Items.gameitem.getcardimage("greennine"), 9, "Green", 0),
            };
            Random random = new Random();
            return cards[random.Next(0, cards.Count)];
        }
        public void addcard()
        {
            Cards card = Cards.randomcard();
            card.listcount = CardsList.playercards.Count + 1;
            card.image.MouseEnter += cardenter;
            card.image.MouseLeave += cardleave;
            card.image.PreviewMouseDown += mousedown;
            gamecanvas.PreviewMouseMove += mousemove;
            gamecanvas.PreviewMouseUp += mouseup;
            card.image.MouseDown += mousedown;
            CardsList.playercards.Add(card);
            one();
        }
        public Cards imagetocard(System.Windows.Controls.Image image)
        {
            for(int index = 0; index < CardsList.playercards.Count; index++)
            {
                if (CardsList.playercards[index].image == image)
                {
                    return CardsList.playercards[index];
                }
            }
            return new Cards();
        }
        public bool comparecard(Cards topcard, Cards draggedcard)
        {
            return draggedcard.color == topcard.color || draggedcard.number == topcard.number || ((draggedcard.number == -4 || draggedcard.number == -5) && draggedcard.color == "none");
        }
    }
}
