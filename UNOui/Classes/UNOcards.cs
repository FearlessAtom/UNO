using Project.Assets.ControlClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using UNOui.UserControls;
using static UNOui.Bot;
//using static System.Net.Mime.MediaTypeNames;

namespace UNOui
{
    public class Player : CardHolder
    {
        public bool drawing = false;
        public Player(string name)
        {
            this.name = name;
        }
        public void checkforuno()
        {
            if (cards.Count == 2)
            {
                uno = true;
            }
            if (uno == true && cards.Count == 1)
            {
                unotimer();
            }
        }
        public void playcard()
        {
            Canvas.SetZIndex(Table.draggedimage, Items.gameitem.cardzindex);
            double bottom = Canvas.GetTop(Table.draggedimage);
            if (bottom < Items.gameitem.gamecanvas.ActualHeight * (Table.draggedimage.Height / (Items.gameitem.gamecanvas.ActualHeight / 2)) && Card.imagetocard(Table.draggedimage).comparecard())
            {
                Card draggedcard = Card.imagetocard(Table.draggedimage);
                Random random = new Random();
                int randomnumber = random.Next(-45, 45);
                if (cards.Count > 1)
                {
                    Table.addtotopcards(randomnumber);
                }
                draggedcard.playcard();
                Table.topcard.image.RenderTransform = Card.rotate(randomnumber);
                Table.topcard.zindex();
                cards.Remove(draggedcard);
                checkforuno();
                Items.gameitem.gamecanvas.Children.Remove(draggedcard.image);
                Canvas.SetZIndex(Table.draggedimage, Items.gameitem.cardzindex);
                if (Table.topcard.number != -1)
                {
                    Table.nextturn();
                }
                Task.Delay(TimeSpan.FromSeconds(0.5)).ContinueWith(task =>
                {
                    if ((draggedcard.number == -4 || draggedcard.number == -5) && draggedcard.color == "none")
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
        }
        public void unotimer()
        {
            Task.Delay(TimeSpan.FromSeconds(2)).ContinueWith(task =>
            {
                if (uno == true)
                {
                    cards.Add(Card.randomcard());
                    Task.Delay(TimeSpan.FromSeconds(0.5)).ContinueWith(task =>
                    {
                        cards.Add(Card.randomcard());
                    }, TaskScheduler.FromCurrentSynchronizationContext());
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        public void refreshplayercards()
        {

            Items.gameitem.gamecanvas.Children.Clear();
            Table.topcard.image.Width = Table.topcard.image.Height = 150;
            Canvas.SetTop(Table.topcard.image, Items.gameitem.gamecanvas.ActualHeight / 3);
            Canvas.SetLeft(Table.topcard.image, Items.gameitem.gamecanvas.ActualWidth / 2 - Table.topcard.image.Width / 2);
            Items.gameitem.gamecanvas.Children.Add(Table.topcard.image);
            int gap = ((Items.gameitem.player.cards.Count * 150 - Items.gameitem.player.cards.Count * 100) / 2) - 30;
            for (int index = 0; index < Items.gameitem.player.cards.Count; index++)
            {
                Image image = Items.gameitem.player.cards[index].image;
                image.Width = 150;
                image.Height = 150;
                Canvas.SetTop(image, (int)Items.gameitem.gamecanvas.ActualHeight - image.Width);
                Canvas.SetLeft(image, (int)Items.gameitem.gamecanvas.ActualWidth / 2 - (image.Width / 2) + index * 50 - gap);
                Items.gameitem.gamecanvas.Children.Add(image);
            }
            refreshuno();
        }
        public void unopressed(object sender, MouseButtonEventArgs e)
        {
            Audio.playunosound();
            Items.gameitem.gamecanvas.Children.Remove((Image)sender);
            uno = false;
        }
        public void refreshuno()
        {
            if (cards.Count > 2 || !uno || (Table.turn != 1 && cards.Count != 1))
            {
                return;
            }
            if(cards.Count == 2 && !canplay())
            {
                return;
            }
            Image unobutton= new Image();
            unobutton.Width = unobutton.Height = 150;
            unobutton.Source = Card.getcardimage("unobutton").Source;
            unobutton.MouseDown += unopressed;
            Canvas.SetBottom(unobutton, 10);
            Canvas.SetRight(unobutton, 10);
            Items.gameitem.gamecanvas.Children.Add(unobutton);
        }   
        public void deckdown(object sender, MouseButtonEventArgs e)
        {
            if ((e != null && drawing) || Table.turn != 1) { return; }
            drawing = true;
            addcard();
            Audio.playcardtakesound();
            Card card = cards[cards.Count - 1];
            if (card.comparecard())
            {
                drawing = false;
                UserControl draworplay = new draworplay();
                Items.gameitem.gamegrid.Children.Add(draworplay);
            }
            else
            {
                if (Settings.getdrawuntilplayable() == 1 && !card.comparecard())
                {
                    Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith(task =>
                    {
                        deckdown(new object(), null);
                    }, TaskScheduler.FromCurrentSynchronizationContext());
                }
                else
                {
                    drawing = false;
                    Table.nextturn();
                    Table.checkforturn();
                }
            }
        }
    }
    public class Bot : CardHolder
    {
        public int position;
        public bool itsturn;
        public Bot(int position, string name)
        {
            this.position = position;
            this.name = name;
        }
        DropShadowEffect dropShadowEffect = new DropShadowEffect()
        {
            Color = Colors.Blue,
            Direction = 135,
            ShadowDepth = 5,
            Opacity = 0.7,
        };
        public void unotimer()
        {
            refreshuno();
            Task.Delay(TimeSpan.FromSeconds(2)).ContinueWith(task =>
            {
                if(uno == true)
                {
                    Audio.playunosound();
                }
                uno = false;
                Table.refreshvisuals();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        public void checkforuno()
        {
            if (cards.Count == 1)
            {
                if(Items.mainwindowitem.randominteger(0, 2) == 0)
                {
                    Audio.playunosound();
                }
                else
                {
                    uno = true;
                    unotimer();
                }
            }
        }
        public void cardchangecolor(out string color, out string imagepath, int number)
        {
            int blue = 0;
            int yellow = 0;
            int green = 0;
            int red = 0;
            for(int index = 0; index < cards.Count; index++)
            {
                switch(cards[index].color)
                {
                    case "Blue":
                        blue++;
                        break;
                    case "Yellow":
                        yellow++;
                        break;
                    case "Green":
                        green++;
                        break;
                    case "Red":
                        red++;
                        break;
                }
            }
            int max = Math.Max(blue, Math.Max(yellow, Math.Max(green, red)));
            color = "";
            imagepath = "";
            if(max == blue)
            {
                color = "Blue";
                imagepath = (number == -5 ? "bluewildcard" : "bluedrawfour");
            }
            else if (max == yellow)
            {
                color = "Yellow";
                imagepath = (number == -5 ? "yellowwildcard" : "yellowdrawfour");
            }
            else if (max == green)
            {
                color = "Green";
                imagepath = (number == -5 ? "greenwildcard" : "greendrawfour");
            }
            else if (max == red)
            {
                color = "Red";
                imagepath = (number == -5 ? "redwildcard" : "reddrawfour");
            }
        }
        public Card? playcardlogic()
        {
            bool found = false;
            Card card = new Card
            {
                number = -6,
            };
            for (int index = 0; index < cards.Count; index++)
            {
                if (cards[index].number > card.number && cards[index].comparecard())
                {
                    found = true;
                    card = cards[index];
                }
            }
            if (found)
            {
                return card;
            }
            return null;
        }
        public void unopressed(object sender, MouseButtonEventArgs e)
        {
            if (uno == false)
            {
                return;
            }
            Items.gameitem.gamecanvas.Children.Remove((Image)sender);
            cards.Add(Card.randomcard());
            Task.Delay(TimeSpan.FromSeconds(0.5)).ContinueWith(task =>
            {
                cards.Add(Card.randomcard());
                uno = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        public void refreshuno()
        {
            if (cards.Count != 1)
            {
                return;
            }
            if(uno == false)
            {
                return;
            }
            Image unobutton = new Image();
            unobutton.Width = unobutton.Height = 150;
            unobutton.Source = Card.getcardimage("unobutton").Source;
            unobutton.MouseDown += unopressed;
            Canvas.SetBottom(unobutton, 10);
            Canvas.SetRight(unobutton, 10);
            Items.gameitem.gamecanvas.Children.Add(unobutton);
        }
        public void botplaycard()
        {
            if (cards.Count == 0 || Table.getwin()) { return; }
            itsturn = true;
            Table.refreshvisuals();
            int randomnumber = Items.mainwindowitem.randominteger(-45, 45);
            double delay = Items.mainwindowitem.randomdouble(1, 2);
            Task.Delay(TimeSpan.FromSeconds(delay)).ContinueWith(task =>
            {
                Card? logiccard = playcardlogic();
                if (logiccard != null)
                {
                    Table.addtotopcards(randomnumber);
                    logiccard.playcard();
                    Card card = logiccard;
                    cards.Remove(card);
                    checkforuno();
                    if (Table.topcard.number != -1)
                    {
                        Table.nextturn();
                    }
                    checkforwildcard();
                    Table.topcard.zindex();
                    Table.topcard.image.RenderTransform = Card.rotate(randomnumber);
                    itsturn = false;
                    Table.refreshvisuals();   
                }
                else
                {
                    Card newcard = Card.randomcard();
                    Audio.playcardtakesound();
                    cards.Add(newcard);
                    Table.refreshvisuals();
                    if (newcard.comparecard())
                    {
                        Task.Delay(TimeSpan.FromSeconds(1.5)).ContinueWith(task => {
                            Table.addtotopcards(randomnumber);
                            newcard.playcard();
                            Card card = newcard;
                            cards.Remove(newcard);
                            checkforuno();
                            if (Table.topcard.number != -1)
                            {
                                Table.nextturn();
                            }
                            checkforwildcard();
                            Table.topcard.zindex();
                            Table.topcard.image.RenderTransform = Card.rotate(randomnumber);
                            itsturn = false;
                            Table.refreshvisuals();
                        }, TaskScheduler.FromCurrentSynchronizationContext());
                    }
                    else
                    {
                        if(Settings.getdrawuntilplayable() == 1)
                        {
                            botplaycard();
                        }
                        else
                        {
                            Table.nextturn();
                            Table.checkforturn();
                            itsturn = false;
                            Table.refreshvisuals();
                        }
                    }
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        public void refreshbotsdeck(int gap)
        {
            if (Settings.getplayercount() >= 2)
            {
                int initialgap = 4;
                for (int index = 0; index < cards.Count; index++)
                {
                    Image image = new Image();
                    if (itsturn)
                    { 
                        image.Effect = dropShadowEffect;
                    }
                    RotateTransform rotate = new RotateTransform(gap - (cards.Count * initialgap) / 4);
                    image.Width = image.Height = 150;
                    rotate.CenterX = image.Width * 0.73;
                    rotate.CenterY = image.Height * 0.89;
                    image.Source = Card.getcardimage("cardbackground").Source;
                    image.RenderTransform = rotate;
                    switch (position)
                    {
                        case 1:
                            Canvas.SetLeft(image, 20);
                            Canvas.SetTop(image, Items.gameitem.gamecanvas.ActualHeight / 3);
                            break;
                        case 2:
                            Canvas.SetLeft(image, Items.gameitem.gamecanvas.ActualWidth / 2 - (image.Width / 2));
                            Canvas.SetTop(image, 20);
                        break;
                        case 3:
                            Canvas.SetLeft(image, Items.gameitem.gamecanvas.ActualWidth - 200);
                            Canvas.SetTop(image, Items.gameitem.gamecanvas.ActualHeight / 3);
                            break;
                    }
                    TextBlock nickname = new TextBlock()
                    {
                        Text = (Settings.getlanguage() == 2 ? "Бот " : "Bot ") + name,
                        FontSize = 50,
                        FontWeight = FontWeights.Bold,
                        Foreground = itsturn ? Brushes.Blue : Brushes.Black,
                    };
                    Canvas.SetLeft(nickname, Canvas.GetLeft(image) - image.ActualWidth / 2 + 20);
                    Canvas.SetTop(nickname, Canvas.GetTop(image) + 140);
                    Items.gameitem.gamecanvas.Children.Add(nickname);
                    Items.gameitem.gamecanvas.Children.Add(image);
                    gap = gap + initialgap;
                    refreshuno();
                }
            }
        }
    }
    public class CardHolder
    {
        public bool uno = false;
        public string? name;
        public int points;
        public List<Card> cards = new List<Card>();
        public static List<CardHolder> allcards = new List<CardHolder>();
        public bool canplay()
        {
            for (int index = 0; index < cards.Count; index++)
            {
                if (cards[index].comparecard())
                {
                    return true;
                }
            }
            return false;
        }
        public void checkforwildcard()
        {
            if (Table.topcard.number == -5)
            {
                string color;
                string imagepath;
                ((Bot)this).cardchangecolor(out color, out imagepath, -5);
                Table.topcard.color = color;
                Table.topcard.image = Card.getcardimage(imagepath);
                Table.refreshvisuals();
                Card.turndelay();
            }
            else if (Table.topcard.number == -4)
            {
                string color;
                string imagepath;
                ((Bot)this).cardchangecolor(out color, out imagepath, -4);
                Table.topcard.color = color;
                Table.topcard.image = Card.getcardimage(imagepath);
                Table.refreshvisuals();
                Task.Delay(TimeSpan.FromSeconds(0.5)).ContinueWith(task =>
                {
                    CardHolder.allcards[Table.turn - 1].addcard();
                }, TaskScheduler.FromCurrentSynchronizationContext());
                Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith(task =>
                {
                    CardHolder.allcards[Table.turn - 1].addcard();
                }, TaskScheduler.FromCurrentSynchronizationContext());
                Task.Delay(TimeSpan.FromSeconds(1.5)).ContinueWith(task =>
                {
                    CardHolder.allcards[Table.turn - 1].addcard();
                }, TaskScheduler.FromCurrentSynchronizationContext());
                Task.Delay(TimeSpan.FromSeconds(2)).ContinueWith(task =>
                {
                    CardHolder.allcards[Table.turn - 1].addcard();
                    Table.nextturn();
                    Table.checkforturn();
                    Table.refreshvisuals();
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            else if (Table.topcard.number == -3)
            {
                Table.nextturn();
                Card.turndelay();
            }
            else if (Table.topcard.number == -2)
            {
                CardHolder.allcards[Table.turn - 1].addcard();

                Task.Delay(TimeSpan.FromSeconds(0.5)).ContinueWith(task =>
                {
                    CardHolder.allcards[Table.turn - 1].addcard();
                    Table.nextturn();
                    Card.turndelay();
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            else if (Table.topcard.number == -1)
            {
                if (Settings.getplayercount() == 2)
                {
                    Table.nextturn();
                    Table.nextturn();
                    Table.checkforturn();
                    return;
                }
                Table.direction = !Table.direction;
                Table.nextturn();
                Table.checkforturn();
            }
            else
            {
                Table.checkforturn();
            }
        }
        public void removecard(Card card)
        {
            Items.gameitem.gamecanvas.Children.Remove(card.image);
            cards.Remove(card);
            Table.refreshvisuals();
        }
        public void addcard()
        {
            Card card = Card.randomcard();
            card.image.MouseEnter += card.cardenter;
            card.image.MouseLeave += card.cardleave;
            card.image.PreviewMouseDown += Items.gameitem.mousedown;
            Items.gameitem.PreviewMouseMove += Items.gameitem.mousemove;
            Items.gameitem.gamecanvas.PreviewMouseUp += Items.gameitem.mouseup;
            card.image.MouseDown += Items.gameitem.mousedown;
            cards.Add(card);
            Table.refreshvisuals();
        }
        public void setcards()
        {
            cards.Clear();
            for(int index = 0; index < Settings.getcardcount(); index++)
            {
                addcard();
            }
        }
    }
    public class Card
    {
        public Image image;
        public int number;
        public string color;
        public Card()
        {

        }
        public Card(Image image, int number, string color)
        {
            this.image = image;
            this.number = number;
            this.color = color;
        }
        public Card(Card card)
        {
            image = card.image;
            number = card.number;
            color = card.color;
        }
        
        public void zindex()
        {
            Canvas.SetZIndex(image, 1);
        }
        public bool wildcard()
        {
            return number == -4 || number == -5;
        }
        public void playcard()
        {
            Audio.playcardsound();
            Table.topcard.image.Source = image.Source;
            Table.topcard.number = number;
            Table.topcard.color = color;
        }
        public void cardenter(object sender, MouseEventArgs e)
        {
            if(Table.turn != 1){ return; }
            Canvas.SetTop(image, (int)Items.gameitem.gamecanvas.ActualHeight - image.Width - 30);
        }
        public void cardleave(object sender, MouseEventArgs e)
        {
            Canvas.SetTop(image, (int)Items.gameitem.gamecanvas.ActualHeight - image.Width);
            Table.refreshvisuals();
        }
        public bool comparecard()
        {
            return color == Table.topcard.color || number == Table.topcard.number || ((number == -4 || number == -5) && color == "none");
        }
        public static Image getcardimage(string card)
        {
            Image image = new Image();
            string source = "C:\\Users\\357\\Desktop\\UNO\\UNOui\\assets\\UNOcards\\" + card + ".png";
            image.Source = new BitmapImage(new Uri(source));
            return image;
        }
        public static void turndelay()
        {
            Task.Delay(TimeSpan.FromSeconds(2)).ContinueWith(task =>
            {
                Table.checkforturn();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        public static Card randomcard()
        { 
            List<Card> cards = new List<Card>
            {
                //Draw cards
                new Card(getcardimage("drawfour"), -4, "none"),
                new Card(getcardimage("wildcard"), -5, "none"),

                //Yellow
                new Card(getcardimage("yellowreverse"), -1, "Yellow"),
                new Card(getcardimage("yellowdrawtwo"), -2, "Yellow"),
                new Card(getcardimage("yellowblock"), -3, "Yellow"),
                new Card(getcardimage("yellowone"), 1, "Yellow"),
                new Card(getcardimage("yellowtwo"), 2, "Yellow"),
                new Card(getcardimage("yellowthree"), 3, "Yellow"),
                new Card(getcardimage("yellowfour"), 4, "Yellow"),
                new Card(getcardimage("yellowfive"), 5, "Yellow"),
                new Card(getcardimage("yellowsix"), 6, "Yellow"),
                new Card(getcardimage("yellowseven"), 7, "Yellow"),
                new Card(getcardimage("yelloweight"), 8, "Yellow"),
                new Card(getcardimage("yellownine"), 9, "Yellow"),
                
                //Blue
                new Card(getcardimage("bluereverse"), -1, "Blue"),
                new Card(getcardimage("bluedrawtwo"), -2, "Blue"),
                new Card(getcardimage("blueblock"), -3, "Blue"),
                new Card(getcardimage("blueone"), 1, "Blue"),
                new Card(getcardimage("bluetwo"), 2, "Blue"),
                new Card(getcardimage("bluethree"), 3, "Blue"),
                new Card(getcardimage("bluefour"), 4, "Blue"),
                new Card(getcardimage("bluefive"), 5, "Blue"),
                new Card(getcardimage("bluesix"), 6, "Blue"),
                new Card(getcardimage("blueseven"), 7, "Blue"),
                new Card(getcardimage("blueeight"), 8, "Blue"),
                new Card(getcardimage("bluenine"), 9, "Blue"),
                
                //Red
                new Card(getcardimage("redreverse"), -1, "Red"),
                new Card(getcardimage("reddrawtwo"), -2, "Red"),
                new Card(getcardimage("redblock"), -3, "Red"),
                new Card(getcardimage("redone"), 1, "Red"),
                new Card(getcardimage("redtwo"), 2, "Red"),
                new Card(getcardimage("redthree"), 3, "Red"),
                new Card(getcardimage("redfour"), 4, "Red"),
                new Card(getcardimage("redfive"), 5, "Red"),
                new Card(getcardimage("redsix"), 6, "Red"),
                new Card(getcardimage("redseven"), 7, "Red"),
                new Card(getcardimage("redeight"), 8, "Red"),
                new Card(getcardimage("rednine"), 9, "Red"),

                //Green
                new Card(getcardimage("greenreverse"), -1, "Green"),
                new Card(getcardimage("greendrawtwo"), -2, "Green"),
                new Card(getcardimage("greenone"), 1, "Green"),
                new Card(getcardimage("greentwo"), 2, "Green"),
                new Card(getcardimage("greenthree"), 3, "Green"),
                new Card(getcardimage("greenfour"), 4, "Green"),
                new Card(getcardimage("greenfive"), 5, "Green"),
                new Card(getcardimage("greensix"), 6, "Green"),
                new Card(getcardimage("greenseven"), 7, "Green"),
                new Card(getcardimage("greeneight"), 8, "Green"),
                new Card(getcardimage("greennine"), 9, "Green"),
            };
            Random random = new Random();
            return cards[random.Next(0, cards.Count)];
        }
        public static Card imagetocard(Image image)
        {
            for (int index = 0; index < Items.gameitem.player.cards.Count; index++)
            {
                if (Items.gameitem.player.cards[index].image == image)
                {
                    return Items.gameitem.player.cards[index];
                }
            }
            return new Card();
        }
        public static RotateTransform rotate(int number)
        {
            RotateTransform rotate = new RotateTransform(number);
            rotate.CenterX = Table.topcard.image.Width / 2;
            rotate.CenterY = Table.topcard.image.Height / 2 + number;
            return rotate;
        }
    }
    public class Table
    {
        public static bool direction = true;
        public static int turn;
        public static List<Image> topcards = new List<Image>();
        public static List<int> topcardsrotateangle = new List<int>();
        public static Card topcard;
        public static Image draggedimage;
        public static Image deckimage;
        public static void showscoreboard()
        {
            Task.Delay(TimeSpan.FromSeconds(1.5)).ContinueWith(task =>
            {
                ScoreBoard board = new ScoreBoard();
                Items.gameitem.gamegrid.Children.Add(board);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        public static bool getwin()
        {
            for(int index = 0; index < CardHolder.allcards.Count; index++)
            {
                if (CardHolder.allcards[index].cards.Count == 0)
                {
                    return true;
                }
            }
            return false;
        }
        public static void nextturn()
        {
            if (direction)
            {
                turn++;
                if (turn > CardHolder.allcards.Count)
                {
                    turn = 1;
                }
            }
            else
            {
                turn--;
                if (turn < 1)
                {
                    turn = CardHolder.allcards.Count;
                }
            }
        }
        public static int getnextturn()
        {
            int tempturn = 0;
            if (direction)
            {
                turn++;
                if (turn > CardHolder.allcards.Count)
                {
                    tempturn = 1;
                }
            }
            else
            {
                turn--;
                if (turn < 1)
                {
                    tempturn = CardHolder.allcards.Count;
                }
            }
            return tempturn;
        }
        public static void checkforturn()
        {
            refreshvisuals();
            if (getwin())
            {
                showscoreboard();
            }
            else
            {
                if (turn != 1)
                {
                    ((Bot)CardHolder.allcards[turn - 1]).botplaycard();
                }
            }
        }
        public static void clear()
        {
            topcards.Clear();
            topcardsrotateangle.Clear();
        }
        public static void settopcard()
        {
            topcard = Card.randomcard();
            topcardsrotateangle.Add(0);
        }
        public static void addtotopcards(int randomnumber)
        {
            Image image = Card.randomcard().image;
            image.Width = image.Height = 150;
            image.Source = topcard.image.Source;
            topcards.Add(image);
            topcardsrotateangle.Add(randomnumber);
        }
        //one
        public static void refreshvisuals()
        {
            if (Items.gameitem == null)
            {
                return;
            }
            Items.gameitem.player.refreshplayercards();
            refreshdeck(0);
            playable();
            refreshtopcards();
            refreshbotsdeck(0);
            refreshdirection();
        }
        private static void refreshdeck(int gap)
        {
            Image deck = new Image();
            deck = Card.getcardimage("cardbackground");
            deck.Width = deck.Height = 150;
            deck.MouseDown += Items.gameitem.deckdown;
            deckimage = deck;
            Canvas.SetTop(deck, Items.gameitem.gamecanvas.ActualHeight / 3);
            Canvas.SetLeft(deck, Items.gameitem.gamecanvas.ActualWidth / 3.5 - gap);
            Items.gameitem.gamecanvas.Children.Add(deck);
            if (gap <= Items.gameitem.actualgap * 3) { refreshdeck(gap + Items.gameitem.actualgap); }
        }
        private static void playable()
        {
            if(turn != 1) { return; }
            Card topcard = Table.topcard;
            bool playable = false; ;
            if (topcard == null) { return; }
            for (int index = 0; index < Items.gameitem.player.cards.Count; ++index)
            {
                if (Items.gameitem.player.cards[index].color == "none" || topcard.color == Items.gameitem.player.cards[index].color || topcard.number == Items.gameitem.player.cards[index].number)
                {
                    Canvas.SetTop(Items.gameitem.player.cards[index].image, (int)Items.gameitem.gamecanvas.ActualHeight - Items.gameitem.player.cards[index].image.Width - 7);
                    playable = true;
                }
            }
            Image image = new Image();
            DropShadowEffect dropShadowEffect = new DropShadowEffect
            {
                Color = Colors.LightYellow,
                Direction = 135,
                ShadowDepth = 5,
                Opacity = 0.7
            };
            if (!playable && Table.turn == 1) { deckimage.Effect = dropShadowEffect; }
        }
        private static void refreshbotsdeck(int gap)
        {
            Items.gameitem.botone.refreshbotsdeck(gap);
            if (CardHolder.allcards.Count >= 3)
            {
                Items.gameitem.bottwo.refreshbotsdeck(gap);
            }
            if (CardHolder.allcards.Count == 4)
            {
                Items.gameitem.botthree.refreshbotsdeck(gap);
            }
        }
        public static void refreshdirection()
        {
            if (Settings.getplayercount() == 2)
            {
                return;
            }
            Image image = new Image();
            image.Source = Card.getcardimage(direction ? "arrowsclockwise" : "arrowscounterclockwise").Source;
            image.Width = image.Height = 150;
            Canvas.SetBottom(image, 180);
            Canvas.SetLeft(image, Items.gameitem.gamecanvas.ActualWidth / 2 - image.Width / 2);
            Items.gameitem.gamecanvas.Children.Add(image);
        }
        private static void refreshtopcards()
        {
            if (topcards.Count > 20)
            {
                topcards.RemoveAt(0);
                topcardsrotateangle.RemoveAt(0);
            }
            for (int index = 0; index < topcards.Count; index++)
            {
                topcards[index].RenderTransform = Card.rotate(topcardsrotateangle[index]);
                Canvas.SetTop(topcards[index], Items.gameitem.gamecanvas.ActualHeight / 3);
                Canvas.SetLeft(topcards[index], Items.gameitem.gamecanvas.ActualWidth / 2 - 75);
                Items.gameitem.gamecanvas.Children.Remove(topcards[index]);
                Items.gameitem.gamecanvas.Children.Add(topcards[index]);
            }
        }
    }
}