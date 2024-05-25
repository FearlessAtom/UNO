using System;
using System.Collections.Generic;
using System.Linq;
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

namespace UNOui.UserControls
{
    /// <summary>
    /// Interaction logic for ScoreBoard.xaml
    /// </summary>
    public partial class ScoreBoard : UserControl
    {
        public ScoreBoard()
        {
            InitializeComponent();
        }
        public void toenglish()
        {
            mainmenubutton.Content = "Main menu";
            restartbutton.Content = "Restart";
            exitthegamebutton.Content = "Exit the game";
        }
        public void toukrainian()
        {
            mainmenubutton.Content = "Головне меню";
            restartbutton.Content = "Рестарт";
            exitthegamebutton.Content = "Вийти з гри";
        }
        private void loaded(object sender, RoutedEventArgs e)
        {
            scoreboard();
            if(Settings.getlanguage() == 2)
            {
                toukrainian();
            }
            else
            {
                toenglish();
            }
        }
        public int getpoints(CardHolder thing)
        {
            int result = 0;
            for(int index = 0; index < thing.cards.Count; index++)
            {
                if (thing.cards[index].number == -5 || thing.cards[index].number == -4 || thing.cards[index].number == -3 || thing.cards[index].number == -2 || thing.cards[index].number == -1)
                {
                    result = result + 20;
                }
                else
                {
                    result = result + thing.cards[index].number;
                }
            }
            return result;
        }
        public void swap(CardHolder a, CardHolder b)
        {
            string tempname = a.name;
            a.name = b.name;
            b.name = tempname;
            List<Card> tempcards = a.cards;
            a.cards = b.cards;
            b.cards = tempcards;
        }
        public static int allpoints = 0;
        public void scoreboard()
        {
            List<CardHolder> copy = CardHolder.allcards;
            for(int index = 0; index < copy.Count - 1; index++)
            {
                for(int j = 0; j < copy.Count - 1 - index; j++)
                {
                    if (getpoints(copy[j]) > getpoints(copy[j + 1]))
                    {
                        swap(copy[j], copy[j + 1]);
                    }
                }
            }
            for(int index = 0; index < copy.Count; ++index)
            {
                int points = getpoints(copy[index]);
                allpoints = allpoints + points;
                Bot.allcards[index].points = points;
                ScoreBoardItem item = new ScoreBoardItem(copy[index].name, copy[index].cards, copy[index].points);
                stackpanel.Children.Add(item);
            }
            stackpanel.SetHorizontalOffset(0);
            stackpanel.SetVerticalOffset(0);
        }
        public void remove()
        {
            Grid parent = (Grid)Parent;
            parent.Children.Remove(this);
        }
        private void exit(object sender, RoutedEventArgs e)
        {
            Items.mainwindowitem.Close();
        }
        private void mainmenu(object sender, RoutedEventArgs e)
        {
            remove();
            Items.gameitem.closegame();
        }
        private void restart(object sender, RoutedEventArgs e)
        {
            remove();
            Items.gameitem.loaded(new object(), new RoutedEventArgs());
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
