using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UNOui.UserControls
{
    public partial class ScoreBoard : UserControl
    {
        public ScoreBoard()
        {
            InitializeComponent();
        }
        public void ToEnglish()
        {
            mainmenubutton.Content = "Main menu";
            restartbutton.Content = "Restart";
            exitthegamebutton.Content = "Exit the game";
        }

        public void ToUkrainina()
        {
            mainmenubutton.Content = "Головне меню";
            restartbutton.Content = "Рестарт";
            exitthegamebutton.Content = "Вийти з гри";
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            LoadScoreBoard();
            if(Settings.Language == 2)
            {
                ToUkrainina();
            }
            else
            {
                ToEnglish();
            }
        }
        public int GetPoints(CardHolder thing)
        {
            int result = 0;
            for(int index = 0; index < thing.Cards.Count; index++)
            {
                if (thing.Cards[index].number == -5 || thing.Cards[index].number == -4 || thing.Cards[index].number == -3 || thing.Cards[index].number == -2 || thing.Cards[index].number == -1)
                {
                    result = result + 20;
                }
                else
                {
                    result = result + thing.Cards[index].number;
                }
            }
            return result;
        }

        public void Swap(CardHolder a, CardHolder b)
        {
            string tempname = a.Name;
            a.Name = b.Name;
            b.Name = tempname;
            List<Card> tempcards = a.Cards;
            a.Cards = b.Cards;
            b.Cards = tempcards;
        }

        public static int AllPoints = 0;

        public void LoadScoreBoard()
        {
            List<CardHolder> copy = CardHolder.AllCards;
            for(int index = 0; index < copy.Count - 1; index++)
            {
                for(int j = 0; j < copy.Count - 1 - index; j++)
                {
                    if (GetPoints(copy[j]) > GetPoints(copy[j + 1]))
                    {
                        Swap(copy[j], copy[j + 1]);
                    }
                }
            }
            for(int index = 0; index < copy.Count; ++index)
            {
                int points = GetPoints(copy[index]);
                AllPoints = AllPoints + points;
                Bot.AllCards[index].Points = points;
                ScoreBoardItem item = new ScoreBoardItem(copy[index].Name, copy[index].Cards, copy[index].Points);
                stackpanel.Children.Add(item);
            }
            stackpanel.SetHorizontalOffset(0);
            stackpanel.SetVerticalOffset(0);
        }

        public void Close()
        {
            Grid parent = (Grid)Parent;
            parent.Children.Remove(this);
        }

        private void ExitButton(object sender, RoutedEventArgs e)
        {
            Items.MainWindowItem.Close();
        }

        private void MainMenuButton(object sender, RoutedEventArgs e)
        {
            Close();
            Items.GameItem.CloseGameButton();
        }

        private void RestartButton(object sender, RoutedEventArgs e)
        {
            Close();
            Items.GameItem.LoadGame(new object(), new RoutedEventArgs());
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
