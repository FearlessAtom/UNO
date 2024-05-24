using Project.Assets.ControlClasses;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Numerics;
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
//using static System.Net.Mime.MediaTypeNames;

namespace UNOui
{
    public class Player : CardsList
    {

    }
    public class Bot : CardsList
    {
        public int points;
        public int position;
        public bool itsturn;
        public string name;
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
        public void botplaycard()
        {
            if (cards.Count == 0 || Table.getwin()) { return; }
            itsturn = true;
            Table.refreshvisuals();
            int randomnumber = Items.mainwindowitem.randominteger(-45, 45);
            bool foundplayablecard = false;
            double delay = Items.mainwindowitem.randomdouble(1, 2);
            Task.Delay(TimeSpan.FromSeconds(delay)).ContinueWith(task =>
            {
                for (int index = 0; index < cards.Count; index++)
                {
                    if (cards[index].comparecard())
                    {
                        Table.addtotopcards(randomnumber);
                        cards[index].playcard();
                        if(Table.topcard.number != -1)
                        {
                            Table.nextturn();
                        }
                        cards[index].checkforwildcard();
                        cards.RemoveAt(index);
                        foundplayablecard = true;
                        Table.topcard.zindex();
                        Table.topcard.image.RenderTransform = Cards.rotate(randomnumber);
                        break;
                    }
                }
                if (!foundplayablecard)
                {
                    Cards newcard = Cards.randomcard();
                    if (newcard.comparecard())
                    {
                        Table.addtotopcards(randomnumber);
                        newcard.playcard();
                        if (Table.topcard.number != -1)
                        {
                            Table.nextturn();
                        }
                        newcard.checkforwildcard();
                        Table.topcard.zindex();
                        Table.topcard.image.RenderTransform = Cards.rotate(randomnumber);
                    }
                    else
                    {
                        cards.Add(newcard);
                        Table.nextturn();
                        Table.checkforturn();
                    }
                }
                itsturn = false;
                Table.refreshvisuals();
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
                    image.Source = Cards.getcardimage("cardbackground").Source;
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
                }
            }
        }
    }
    public class CardsList
    {
        public List<Cards> cards = new List<Cards>();
        public static List<Bot> allcards = new List<Bot>();
        public CardsList()
        {

        }
        public void removecard(Cards card)
        {
            Items.gameitem.gamecanvas.Children.Remove(card.image);
            cards.Remove(card);
            Table.refreshvisuals();
        }
        public void addcard()
        {
            Cards card = Cards.randomcard();
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
    public class Cards
    {
        public Image image;
        public int number;
        public string color;
        public static int nigger;
        public Cards()
        {

        }
        public Cards(Image image, int number, string color)
        {
            this.image = image;
            this.number = number;
            this.color = color;
        }
        public Cards(Cards card)
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
        public void turndelay()
        {
            Task.Delay(TimeSpan.FromSeconds(2)).ContinueWith(task =>
            {
                Table.checkforturn();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        public void checkforwildcard()
        {
            if(Table.topcard.number == -5)
            {
                Table.topcard.color = "Blue";
                Table.topcard.image = getcardimage("bluewildcard");
                Table.refreshvisuals();
                turndelay();
            }
            else if(Table.topcard.number == -4)
            {
                Table.topcard.color = "Blue";
                Table.topcard.image = getcardimage("bluedrawfour");
                Table.refreshvisuals();
                //Table.nextturn();
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
            else if (Table.topcard.number == -3)
            {
                Table.nextturn();
                turndelay();
            }
            else if(Table.topcard.number == -2)
            {
                CardsList.allcards[Table.turn - 1].addcard();
                
                Task.Delay(TimeSpan.FromSeconds(0.5)).ContinueWith(task =>
                {
                    CardsList.allcards[Table.turn - 1].addcard();
                    Table.nextturn();
                    turndelay();
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            else if(Table.topcard.number == -1)
            {
                if(Settings.getplayercount() == 2)
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
        public static Cards randomcard()
        { 
            List<Cards> cards = new List<Cards>
            {
                //Draw cards
                new Cards(getcardimage("drawfour"), -4, "none"),
                new Cards(getcardimage("wildcard"), -5, "none"),

                //Yellow
                new Cards(getcardimage("yellowreverse"), -1, "Yellow"),
                new Cards(getcardimage("yellowdrawtwo"), -2, "Yellow"),
                new Cards(getcardimage("yellowblock"), -3, "Yellow"),
                new Cards(getcardimage("yellowone"), 1, "Yellow"),
                new Cards(getcardimage("yellowtwo"), 2, "Yellow"),
                new Cards(getcardimage("yellowthree"), 3, "Yellow"),
                new Cards(getcardimage("yellowfour"), 4, "Yellow"),
                new Cards(getcardimage("yellowfive"), 5, "Yellow"),
                new Cards(getcardimage("yellowsix"), 6, "Yellow"),
                new Cards(getcardimage("yellowseven"), 7, "Yellow"),
                new Cards(getcardimage("yelloweight"), 8, "Yellow"),
                new Cards(getcardimage("yellownine"), 9, "Yellow"),
                
                //Blue
                new Cards(getcardimage("bluereverse"), -1, "Blue"),
                new Cards(getcardimage("bluedrawtwo"), -2, "Blue"),
                new Cards(getcardimage("blueblock"), -3, "Blue"),
                new Cards(getcardimage("blueone"), 1, "Blue"),
                new Cards(getcardimage("bluetwo"), 2, "Blue"),
                new Cards(getcardimage("bluethree"), 3, "Blue"),
                new Cards(getcardimage("bluefour"), 4, "Blue"),
                new Cards(getcardimage("bluefive"), 5, "Blue"),
                new Cards(getcardimage("bluesix"), 6, "Blue"),
                new Cards(getcardimage("blueseven"), 7, "Blue"),
                new Cards(getcardimage("blueeight"), 8, "Blue"),
                new Cards(getcardimage("bluenine"), 9, "Blue"),
                
                //Red
                new Cards(getcardimage("redreverse"), -1, "Red"),
                new Cards(getcardimage("reddrawtwo"), -2, "Red"),
                new Cards(getcardimage("redblock"), -3, "Red"),
                new Cards(getcardimage("redone"), 1, "Red"),
                new Cards(getcardimage("redtwo"), 2, "Red"),
                new Cards(getcardimage("redthree"), 3, "Red"),
                new Cards(getcardimage("redfour"), 4, "Red"),
                new Cards(getcardimage("redfive"), 5, "Red"),
                new Cards(getcardimage("redsix"), 6, "Red"),
                new Cards(getcardimage("redseven"), 7, "Red"),
                new Cards(getcardimage("redeight"), 8, "Red"),
                new Cards(getcardimage("rednine"), 9, "Red"),

                //Green
                new Cards(getcardimage("greenreverse"), -1, "Green"),
                new Cards(getcardimage("greendrawtwo"), -2, "Green"),
                new Cards(getcardimage("greenblock"), -3, "Green"),
                new Cards(getcardimage("greenone"), 1, "Green"),
                new Cards(getcardimage("greentwo"), 2, "Green"),
                new Cards(getcardimage("greenthree"), 3, "Green"),
                new Cards(getcardimage("greenfour"), 4, "Green"),
                new Cards(getcardimage("greenfive"), 5, "Green"),
                new Cards(getcardimage("greensix"), 6, "Green"),
                new Cards(getcardimage("greenseven"), 7, "Green"),
                new Cards(getcardimage("greeneight"), 8, "Green"),
                new Cards(getcardimage("greennine"), 9, "Green"),
            };
            Random random = new Random();
            return cards[random.Next(0, cards.Count)];
        }
        public static Cards imagetocard(Image image)
        {
            for (int index = 0; index < Items.gameitem.player.cards.Count; index++)
            {
                if (Items.gameitem.player.cards[index].image == image)
                {
                    return Items.gameitem.player.cards[index];
                }
            }
            return new Cards();
        }
        public static RotateTransform rotate(int number)
        {
            if(Table.topcard.number == -5 || Table.topcard.number == -4)
            {
                nigger = 2;
            }
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
        public static Cards topcard;
        public static Image draggedimage;
        public static Image deckimage;
        public static bool getwin()
        {
            for(int index = 0; index < CardsList.allcards.Count; index++)
            {
                if (CardsList.allcards[index].cards.Count == 0)
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
                if (turn > CardsList.allcards.Count)
                {
                    turn = 1;
                }
            }
            else
            {
                turn--;
                if (turn < 1)
                {
                    turn = CardsList.allcards.Count;
                }
            }
            Items.gameitem.turntextblock.Text = turn.ToString();
        }
        public static void checkforturn()
        {
            refreshvisuals();
            if (getwin())
            {
                Task.Delay(TimeSpan.FromSeconds(1.5)).ContinueWith(task =>
                {
                    ScoreBoard board = new ScoreBoard();
                    Items.gameitem.gamegrid.Children.Add(board);
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            else
            {
                if (turn != 1)
                {
                    Bot.allcards[turn - 1].botplaycard();
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
            topcard = Cards.randomcard();
            topcardsrotateangle.Add(0);
        }
        public static void addtotopcards(int randomnumber)
        {
            Image image = Cards.randomcard().image;
            image.Width = image.Height = 150;
            image.Source = topcard.image.Source;
            topcards.Add(image);
            topcardsrotateangle.Add(randomnumber);
        }
        //one
        public static void refreshvisuals()
        {
            if(Items.gameitem == null){return;}
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
            refreshdeck(0);
            playable();
            refreshtopcards();
            refreshbotsdeck(0);
            refreshdirection();
        }
        private static void refreshdeck(int gap)
        {
            Image deck = new Image();
            deck = Cards.getcardimage("cardbackground");
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
            Cards topcard = Table.topcard;
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
            if (CardsList.allcards.Count >= 3)
            {
                Items.gameitem.bottwo.refreshbotsdeck(gap);
            }
            if (CardsList.allcards.Count == 4)
            {
                Items.gameitem.botthree.refreshbotsdeck(gap);
            }
        }   
        public static void refreshdirection()
        {
            Image image = new Image();
            image.Source = Cards.getcardimage(direction ? "arrowsclockwise" : "arrowscounterclockwise").Source;
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
                topcards[index].RenderTransform = Cards.rotate(topcardsrotateangle[index]);
                Canvas.SetTop(topcards[index], Items.gameitem.gamecanvas.ActualHeight / 3);
                Canvas.SetLeft(topcards[index], Items.gameitem.gamecanvas.ActualWidth / 2 - 75);
                Items.gameitem.gamecanvas.Children.Remove(topcards[index]);
                Items.gameitem.gamecanvas.Children.Add(topcards[index]);
            }
        }
    }
}
