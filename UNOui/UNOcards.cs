using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
namespace UNOui
{
    public class CardsList
    {
        public static List<Cards> playercards = new List<Cards>();
        public static Cards dragggedcard;
        public static Image draggedimage;

        public static Cards imagetocard(Image image)
        {
            for(int index = 0; index < playercards.Count; index++)
            {
                if (playercards[index].image == image)
                { 
                    return playercards[index];
                }
            }
            return null;
        }
    }
    public class Cards
    {
        public Image image;
        public int number;
        public string color;
        public int listcount;
        public Cards(Image image, int number, string color, int listcount)
        {
            this.image = image;
            this.number = number;
            this.color = color;
            this.listcount = listcount;
        }
        public Cards randomcard()
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
        public Cards()
        {
            Cards card = randomcard();
            image = card.image;
            number = card.number;
            color = card.color;
            listcount = card.listcount;
        }
    }
}
