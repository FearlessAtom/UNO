using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UNOui
{
    public partial class ExitConfirmation : UserControl
    {
        int Action;

        public ExitConfirmation(int action)
        {
            InitializeComponent();
            this.Action = action;
        }

        private void ToEnglish()
        {
            maintextblock.Text = "Are you sure?";
            cancelbutton.Content = "Cancel";
            exitbutton.Content = (Action == 2 ? "Restart" : "Exit"); 
        }

        private void ToUkrainian()
        {
            maintextblock.Text = "Вы впевнені?";
            cancelbutton.Content = "Закрити";
            exitbutton.Content = (Action == 2 ? "Рестарт" : "Вийти");
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            Items.ExitConfirmationItem = this;
            if(Settings.Language == 2)
            {
                ToUkrainian();
            }
            else
            {
                ToEnglish();
            }
        }

        public void Close(object sender, RoutedEventArgs e)
        {
            if (Action == 1)
            {
                Settings.setexitconfirmationopened(false);
            }
            Grid parent = (Grid)Parent;
            parent.Children.Remove(this);
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            switch (Action)
            {
                case 1:
                    Items.MainWindowItem.Close();
                    break;
                case 2:
                    Close(new object(), new RoutedEventArgs());
                    Items.GameMenuItem.Resume(sender, e);
                    Items.GameItem.LoadGame(sender, e);
                    break;
                case 3:
                    Items.GameMenuItem.Resume(sender, e);
                    Items.GameItem.CloseGameButton();
                    break;
            }
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
