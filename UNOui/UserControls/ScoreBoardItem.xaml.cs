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
    /// Interaction logic for ScoreBoardItem.xaml
    /// </summary>
    public partial class ScoreBoardItem : UserControl
    {
        public string name;
        public List<Card> cards;
        public int points;
        public ScoreBoardItem(string name, List<Card> cards, int points)
        {
            InitializeComponent();
            this.points = points;
            this.cards = cards;
            this.name = name;
            nickname.Text = (char.IsDigit(name[0]) ? (Settings.getlanguage() == 2 ? "Бот " : "Bot ") : "") + (name == "You" ? (Settings.getlanguage() == 2 ? "Ви" : "You") : name ) + "(" + points + ")";
            addcards();
        }
        public void addcards()
        {
            if(points == 0)
            {
                TextBlock score = new TextBlock()
                {
                    FontSize = 40,
                    FontFamily = new FontFamily("Verdana"),
                    Text = (Settings.getlanguage() == 2 ? "Переможець" : "Winner"),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                };
                Height = 90;
                items.Children.Add(score);
            }
            else
            {
                for (int index = 0; index < cards.Count; index++)
                {
                    Image image = new Image();
                    image.Width = 60;
                    image.Height = 95;
                    image.Source = cards[index].image.Source;
                    items.Children.Add(image);
                }
            }
        }
    }
}
