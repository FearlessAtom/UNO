using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UNOui
{
    public partial class Confirmation : UserControl
    {
        public Confirmation()
        {
            InitializeComponent();
        }

        public void Cancel(object sender, RoutedEventArgs e)
        {
            Settings.Confirmation = false;
            Grid panel = (Grid)Parent;
            panel.Children.Remove(this);
        }

        private void ButtonMouseEnter(object sender, MouseEventArgs e)
        {
            Items.MainWindowItem.ButtonMouseEnter(sender, e);
        }

        private void ButtonMouseLeave(object sender, MouseEventArgs e)
        {
            Items.MainWindowItem.ButtonMouseLeave(sender, e);
        }

        private void DontSave(object sender, RoutedEventArgs e)
        {
            Grid panel = (Grid)Parent;
            panel.Children.Remove(this);
            Items.SettingsItem.CloseSettings(sender, e);
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            Grid panel = (Grid)Parent;
            panel.Children.Remove(this);
            Items.SettingsItem.SaveSettings(sender, e);
            Items.SettingsItem.CloseSettings(sender, e);

        }

        private void Load(object sender, RoutedEventArgs e)
        {
            if(UnsavedSettings.Language == 1)
            {
                ToEnglish();
            }
            else
            {
                ToUkrainian();
            }
        }

        private void ToEnglish()
        {
            message.Text = "Unsaved changes";
            savebutton.Content = "Save";
            dontsavebutton.Content = "Don't save";
            cancelbutton.Content = "Cancel";
        }

        private void ToUkrainian()
        {
            message.Text = "Незбережені зміни";
            savebutton.Content = "Зберегти";
            dontsavebutton.Content = "Не зберігати";
            cancelbutton.Content = "Закрити";
        }
    }
}
