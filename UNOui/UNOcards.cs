using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
namespace UNOui
{
    public class CardsList
    {
        public List<Cards> cards = new List<Cards>();
        public static List<CardsList> allcards = new List<CardsList>();
        public CardsList()
        {

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
        public void playcard()
        {
            Table.topcard.image.Source = image.Source;
            Table.topcard.number = number;
            Table.topcard.color = color;
        }
        public void cardenter(object sender, MouseEventArgs e)
        {
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
        public void removecard()
        {
            Items.gameitem.gamecanvas.Children.Remove(image);
            Items.gameitem.player.cards.Remove(this);
            Table.refreshvisuals();
        }
        public static Image getcardimage(string card)
        {
            Image image = new Image();
            string source = "C:\\Users\\357\\Desktop\\UNO\\UNOui\\assets\\UNOcards\\" + card + ".png";
            image.Source = new BitmapImage(new Uri(source));
            return image;
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
            RotateTransform rotate = new RotateTransform(number);
            rotate.CenterX = Table.topcard.image.Width / 2;
            rotate.CenterY = Table.topcard.image.Height / 2 + number;
            return rotate;
        }
    }
    public class Table
    {
        public static bool direction;
        public static int turn;
        public static List<Image> topcards = new List<Image>();
        public static List<int> topcardsrotateangle = new List<int>();
        public static Cards topcard;
        public static Image draggedimage;
        public static Image deckimage;
        public static void nextturn()
        {
            turn++;
            if (turn > CardsList.allcards.Count)
            {
                turn = 1;
            }
        }
        public static void checkforturn()
        {
            if (turn != 1)
            {
                Items.gameitem.botplaycard(CardsList.allcards[turn - 1].cards);
            }
        }
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
        }
        private static void refreshdeck(int gap)
        {
            System.Windows.Controls.Image deck = new System.Windows.Controls.Image();
            deck = Cards.getcardimage("cardbackground");
            deck.Width = deck.Height = 150;
            deck.MouseDown += Items.gameitem.deckdown;
            Table.deckimage = deck;
            Canvas.SetTop(deck, Items.gameitem.gamecanvas.ActualHeight / 3);
            Canvas.SetLeft(deck, Items.gameitem.gamecanvas.ActualWidth / 4 - gap);
            Items.gameitem.gamecanvas.Children.Add(deck);
            if (gap <= Items.gameitem.actualgap * 3) { refreshdeck(gap + Items.gameitem.actualgap); }
        }
        private static void playable()
        {
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
            if (!playable) { deckimage.Effect = dropShadowEffect; }
        }
        private static void refreshbotsdeck(int gap)
        {
            if (Settings.getplayercount() >= 2)
            {
                int initialgap = 7;
                for (int index = 0; index < Items.gameitem.botone.cards.Count; index++)
                {
                    Image image = new Image();
                    RotateTransform rotate = new RotateTransform(gap - (Items.gameitem.botone.cards.Count * initialgap) / 4);
                    image.Width = image.Height = 150;
                    rotate.CenterX = image.Width * 0.73;
                    rotate.CenterY = image.Height * 0.89;
                    image.Source = Cards.getcardimage("cardbackground").Source;
                    image.RenderTransform = rotate;
                    Canvas.SetTop(image, 20);
                    Canvas.SetLeft(image, Items.gameitem.gamecanvas.ActualWidth / 2 - (image.Width / 2));
                    Items.gameitem.gamecanvas.Children.Add(image);
                    gap = gap + initialgap;
                }
            }
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
