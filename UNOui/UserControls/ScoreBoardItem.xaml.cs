using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UNOui.UserControls
{
    public partial class ScoreBoardItem : UserControl
    {
        public string PlayName;
        public List<Card> Cards;
        public int Points;

        public ScoreBoardItem(string PlayerName, List<Card> Cards, int Points)
        {
            InitializeComponent();
            this.Points = Points;
            this.Cards = Cards;
            this.PlayName = PlayerName;
            nickname.Text = (char.IsDigit(Name[0]) ? (Settings.Language == 2 ? "Бот " : "Bot ") : "") +
                (Name == "You" ?(Settings.Language == 2 ? "Ви" : "You") : Name) + "(" + Points + ")";
            
            AddCards();
        }

        public void AddCards()
        {
            if(Points == 0)
            {
                TextBlock score = new TextBlock()
                {
                    FontSize = 40,
                    FontFamily = new FontFamily("Verdana"),
                    Text = (Settings.Language == 2 ? "Переможець" : "Winner"),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                };
                Height = 90;
                items.Children.Add(score);
            }
            else
            {
                for (int index = 0; index < Cards.Count; index++)
                {
                    Image image = new Image();
                    image.Width = 60;
                    image.Height = 95;
                    image.Source = Cards[index].image.Source;
                    items.Children.Add(image);
                }
            }
        }
    }
}
