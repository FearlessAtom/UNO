using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UNOui
{
    public partial class ColorChange : UserControl
    {
        public ColorChange()
        {
            InitializeComponent();
        }

        public void AddFour()
        {
            Table.RefreshVisuals();
            Task.Delay(TimeSpan.FromSeconds(0.5)).ContinueWith(task =>
            {
                CardHolder.AllCards[Table.turn - 1].AddCard();
            }, TaskScheduler.FromCurrentSynchronizationContext());
            Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith(task =>
            {
                CardHolder.AllCards[Table.turn - 1].AddCard();
            }, TaskScheduler.FromCurrentSynchronizationContext());
            Task.Delay(TimeSpan.FromSeconds(1.5)).ContinueWith(task =>
            {
                CardHolder.AllCards[Table.turn - 1].AddCard();
            }, TaskScheduler.FromCurrentSynchronizationContext());
            Task.Delay(TimeSpan.FromSeconds(2)).ContinueWith(task =>
            {
                CardHolder.AllCards[Table.turn - 1].AddCard();
                Table.SetNextTurn();
                Table.CheckForTurn();
                Table.RefreshVisuals();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void Close()
        {
            Grid parent = (Grid)Parent;
            parent.Children.Remove(this);
        }

        public void ChangeColor(string color, string drawfour, string wildcard)
        {
            Close();
            Table.topcard.color = color;
            if (Table.topcard.number == -4)
            {
                Table.topcard.image.Source = Card.CardNameToImage(drawfour).Source;
                AddFour();
            }
            else if (Table.topcard.number == -5)
            {
                Table.topcard.image.Source = Card.CardNameToImage(wildcard).Source;
                Table.RefreshVisuals();
                Table.CheckForTurn();
            }
        }

        private void ChooseGreenColor(object sender, RoutedEventArgs e)
        {
            ChangeColor("Green","greendrawfour", "greenwildcard");
        }

        private void ChooseRedColor(object sender, RoutedEventArgs e)
        {
            ChangeColor("Red", "reddrawfour", "redwildcard");
        }

        private void ChooseBlueColor(object sender, RoutedEventArgs e)
        {
            ChangeColor("Blue", "bluedrawfour", "bluewildcard");
        }

        private void ChooseYellowColor(object sender, RoutedEventArgs e)
        {
            ChangeColor("Yellow", "yellowdrawfour", "yellowwildcard");
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
