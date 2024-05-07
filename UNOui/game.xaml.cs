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
        public void settings()
        {
            for(int index = 0; index < CardsList.topcards.Count; index++)
            {
                MessageBox.Show(CardsList.topcards[index].Source.ToString() + CardsList.topcardsrotateangle[index]);
            }
            //MessageBox.Show(CardsList.playercards.Count.ToString());
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
            CardsList.topcardsrotateangle.Clear();
            CardsList.playercards.Clear();
            CardsList.topcards.Clear();
            gamecanvas.Children.Clear();
            CardsList.topcard = Cards.randomcard();
            CardsList.topcardsrotateangle.Add(0);
            if (CardsList.topcard.number == -4 || CardsList.topcard.number == -5)
            {
                UserControl colorchange = new colorchange();
                gamegrid.Children.Add(colorchange);
            }
            for (int index = 0; index < Settings.getcardcount(); index++)
            {
                addcard();
            }
            one();
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
        public RotateTransform rotate(int number)
        {
            RotateTransform rotate = new RotateTransform(number);
            rotate.CenterX = CardsList.topcard.image.Width / 2;
            rotate.CenterY = CardsList.topcard.image.Height / 2 + number;
            return rotate;
        }
        public void addtotopcardsmemory(int randomnumber)
        {
            System.Windows.Controls.Image ihateblackpeople = Cards.randomcard().image;
            ihateblackpeople.Width = ihateblackpeople.Height = 150;
            ihateblackpeople.Source = CardsList.topcard.image.Source;
            CardsList.topcards.Add(ihateblackpeople);
            CardsList.topcardsrotateangle.Add(randomnumber);
        }
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
            if (CardsList.draggedimage != null)
            {
                Canvas.SetZIndex(CardsList.draggedimage, cardzindex);
                double bottom = Canvas.GetTop(CardsList.draggedimage);
                if (bottom < gamecanvas.ActualHeight * (CardsList.draggedimage.Height / gamecanvas.ActualHeight) && comparecard(CardsList.topcard, imagetocard(CardsList.draggedimage)))
                {
                    Cards draggedcard = imagetocard(CardsList.draggedimage);
                    Random random = new Random();
                    int randomnumber = random.Next(-45, 45);
                    if(CardsList.playercards.Count > 1)
                    {
                        addtotopcardsmemory(randomnumber);

                    }
                    CardsList.topcard.image.RenderTransform = rotate(randomnumber);
                    CardsList.topcard.image.Source = draggedcard.image.Source;
                    CardsList.topcard.number = draggedcard.number;
                    CardsList.topcard.color = draggedcard.color;
                    CardsList.topcard.listcount = draggedcard.listcount;
                    Canvas.SetZIndex(CardsList.topcard.image, 1);
                    if ((draggedcard.number == -4 || draggedcard.number == -5) && draggedcard.color == "none")
                    {
                        UserControl changecolor = new colorchange();
                        gamegrid.Children.Add(changecolor);
                    }
                    CardsList.playercards.Remove(draggedcard);
                    gamecanvas.Children.Remove(draggedcard.image);
                    Canvas.SetZIndex(CardsList.draggedimage, cardzindex);
                }
                drag = null;
                gamecanvas.ReleaseMouseCapture();
                dragging = false;
                one();
            }
        }
        private void topcards()
        {
            if(CardsList.topcards.Count > 10)
            {
                //CardsList.topcards.RemoveAt(0);
            }
            for(int index = 0; index < CardsList.topcards.Count; index++)
            {
                CardsList.topcards[index].RenderTransform = rotate(CardsList.topcardsrotateangle[index]);
                Canvas.SetTop(CardsList.topcards[index], 0);
                Canvas.SetLeft(CardsList.topcards[index], gamecanvas.ActualWidth / 2 - 75);
                gamecanvas.Children.Remove(CardsList.topcards[index]);
                gamecanvas.Children.Add(CardsList.topcards[index]);
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
                topcards();
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
