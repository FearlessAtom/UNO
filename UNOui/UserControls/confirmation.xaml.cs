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

            dontsavebutton.MouseEnter += (sender, e) => Items.MainWindowItem.ButtonMouseLeave(sender, e);
            dontsavebutton.MouseLeave += (sender, e) => Items.MainWindowItem.ButtonMouseLeave(sender, e);

            cancelbutton.MouseEnter += (sender, e) => Items.MainWindowItem.ButtonMouseLeave(sender, e);
            cancelbutton.MouseLeave += (sender, e) => Items.MainWindowItem.ButtonMouseLeave(sender, e);

            savebutton.MouseEnter += (sender, e) => Items.MainWindowItem.ButtonMouseLeave(sender, e);
            savebutton.MouseLeave += (sender, e) => Items.MainWindowItem.ButtonMouseLeave(sender, e);
        }

        public void Cancel(object sender, RoutedEventArgs e)
        {
            Settings.Confirmation = false;
            Grid panel = (Grid)Parent;
            panel.Children.Remove(this);
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
            Items.SettingsItem.CloseSettings(sender, e);
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            if(Settings.UnsavedLanguage == UNOui.Language.English)
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
