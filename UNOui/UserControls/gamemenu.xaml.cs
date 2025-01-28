using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UNOui
{
    public partial class GameMenu : UserControl
    {
        public GameMenu()
        {
            InitializeComponent();
        }

        public void ToEnglish()
        {
            mainmenubutton.Content = "Main menu";
            restartbutton.Content = "Restart";
            resumebutton.Content = "Resume";
        }

        public void ToUkrainian()
        {
            mainmenubutton.Content = "Головне меню";
            restartbutton.Content = "Рестарт";
            resumebutton.Content = "Продовжити";
        }

        public void Resume(object sender, RoutedEventArgs e)
        {
            Settings.GameMenuOpened = false;
            Grid parent = (Grid)Parent;
            parent.Children.Remove(this);
        }

        private void CloseGame(object sender, RoutedEventArgs e)
        {
            ExitConfirmation areyousure = new ExitConfirmation(3);
            Items.GameItem.gamegrid.Children.Add(areyousure);
        }

        private void ButtonMouseEnter(object sender, MouseEventArgs e)
        {
            Items.MainWindowItem.ButtonMouseEnter(sender, e);
        }

        private void ButtonMouseLeave(object sender, MouseEventArgs e)
        {
            Items.MainWindowItem.ButtonMouseLeave(sender, e);
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            Items.GameMenuItem = this;
            if (Settings.Language == 2)
            {
                ToUkrainian();
            }
            else
            {
                ToEnglish();
            }
        }

        private void Restart(object sender, RoutedEventArgs e)
        {
            ExitConfirmation areyousure = new ExitConfirmation(2);
            Items.GameItem.gamegrid.Children.Add(areyousure);
        }
    }
}
