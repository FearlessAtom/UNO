using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace UNOui
{
    public partial class SettingsUserControl : UserControl
    {
        public SettingsUserControl()
        {
            InitializeComponent();
            
            CloseButton.MouseEnter += (sender, e) => Items.MainWindowItem.ButtonMouseEnter(sender, e);
            CloseButton.MouseLeave += (sender, e) => Items.MainWindowItem.ButtonMouseLeave(sender, e);
            
            SaveButton.MouseEnter += (sender, e) => Items.MainWindowItem.ButtonMouseEnter(sender, e);
            SaveButton.MouseLeave += (sender, e) => Items.MainWindowItem.ButtonMouseLeave(sender, e);
            
            ResetButton.MouseEnter += (sender, e) => Items.MainWindowItem.ButtonMouseEnter(sender, e);
            ResetButton.MouseLeave += (sender, e) => Items.MainWindowItem.ButtonMouseLeave(sender, e);

            SaveButton.Click += (sender, e) =>
            {
                Settings.SaveSettings();
                IsSaved();
            };
        }

        public void SetLoadedSettings(object sender, RoutedEventArgs e)
        {
            Items.SettingsItem = this;
            PlayerCountLoad(Settings.PlayerCount);
            Fullscreen(Settings.Fullscreen);
            StartCardChange(Settings.CardCount);
            ForcePlay(Settings.ForcePlay);
            DrawUntilPlayable(Settings.DrawUntilPlayable);
            Stacking(Settings.EnabledSounds);
            LanguageSetting(Settings.LanguageSetting);
            JumpIn(Settings.RandomDirection);
            IsSaved();
        }

        public void IsSaved()
        {
            if (Settings.AreSaved())
            {
                unsavedchanges.Visibility = Visibility.Hidden;
                Settings.IsSaved = true;
            }

            else
            {
                unsavedchanges.Visibility = Visibility.Visible;
                Settings.IsSaved = false;
            }
        }

        public void CloseSettings(object sender, RoutedEventArgs e)
        {
            if (Settings.IsSaved == false && !Settings.Confirmation)
            {
                UserControl confirmation = new Confirmation();
                maingridsettings.Children.Add(confirmation);
                Settings.Confirmation = true;
            }

            else
            {
                Settings.Confirmation = false;
                Settings.IsSettingsOpened = false;
                SetLoadedSettings(sender, e);
                Grid grid = (Grid)Parent;
                grid.Children.Remove(this);
            }
        }

        public Color BrushesToColor(Brush brush)
        {
            SolidColorBrush solidbrush = (SolidColorBrush)brush;
            return solidbrush.Color;
        }

        private void PlayerCountChange(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Button[] array = { playercounttwo, playercountthree, playercountfour };

            for (int index = 0; index < array.Length; index++)
            {
                if (button == array[index])
                {
                    PlayerCountLoad(index + 2);
                }
            }
        }

        private void PlayerCountLoad(int number)
        {
            Button[] array = { playercounttwo, playercountthree, playercountfour };
            double margin = playercounttwo.ActualHeight * 0.05;
            bool found = false;
            for (int index = 0; index < array.Length; index++)
            {
                if (number == index + 2)
                {
                    array[index].Background = Brushes.LightGreen;
                    array[index].Margin = new Thickness(0, 0, 0, 0);
                    array[index].FontSize = 30;
                    found = true;
                }
                else if (!found)
                {
                    array[index].Background = Brushes.LightGreen;
                    array[index].Margin = new Thickness(0, margin, 0, margin);
                    array[index].FontSize = 20;
                }
                else if (found)
                {
                    array[index].Background = Brushes.Gray;
                    array[index].Margin = new Thickness(0, margin, 0, margin);
                    array[index].FontSize = 20;
                }
            }
            Settings.UnsavedPlayerCount = number;
            IsSaved();
        }


        private void SetFullscreenButton(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (sender == fullscreenoff)
            {
                Fullscreen(false);
                Items.MainWindowItem.WindowState = WindowState.Normal;
            }

            else
            {
                Fullscreen(true);
                Items.MainWindowItem.WindowState = WindowState.Maximized;
            }
        }

        private void Fullscreen(bool state)
        {
            if (!state)
            {
                Settings.UnsavedFullscreen = false;
                fullscreenoff.Background = Brushes.LightGreen;
                fullscreenoff.FontSize = 25;
                fullscreenoff.Margin = new Thickness(0);

                fullscreenon.Background = Brushes.Gray;
                fullscreenon.FontSize = 20;
                fullscreenon.Margin = new Thickness(0, 3, 0, 3);
            }
            else
            {
                Settings.UnsavedFullscreen = true;

                fullscreenon.Background = Brushes.LightGreen;
                fullscreenon.FontSize = 25;
                fullscreenon.Margin = new Thickness(0);

                fullscreenoff.Background = Brushes.Gray;
                fullscreenoff.FontSize = 20;
                fullscreenoff.Margin = new Thickness(0, 3, 0, 3);
            }
            IsSaved();
        }

        private void CardCountButton(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button == minus)
                {
                    if (Settings.UnsavedCardCount != 1)
                    {
                        Settings.UnsavedCardCount--;
                    }
                }
            else
                {
                    if (Settings.UnsavedCardCount != 10)
                    {
                        Settings.UnsavedCardCount++;
                    }
                }
            StartCardChange(Settings.UnsavedCardCount);
            IsSaved();
        }

        private void StartCardChange(int number)
        {
            if (number == 1)
            {
                minus.FontSize = 20;
                minus.Background = Brushes.Gray;
            }
            else if (number == 10)
            {
            plus.FontSize = 20;
            plus.Background = Brushes.Gray;
            }
            else
            {
                plus.FontSize = 25;
                plus.Background = Brushes.LightGreen;
                minus.FontSize = 25;
                minus.Background = Brushes.LightGreen;
            }
            Settings.UnsavedCardCount = number;
            startingcardscount.Text = number.ToString();
        }

        private void DrawUntilPlayable(bool state)
        {
            if(state)
            {
                drawuntilplayableon.Margin = new Thickness(0);
                drawuntilplayableon.FontSize = 25;
                drawuntilplayableon.Background = Brushes.LightGreen;

                drawuntilplayableoff.Margin = new Thickness(0, 3, 0, 3);
                drawuntilplayableoff.FontSize = 20;
                drawuntilplayableoff.Background = Brushes.Gray;
            }
            else
            {
                drawuntilplayableon.Margin = new Thickness(0, 3, 0, 3);
                drawuntilplayableon.FontSize = 20;
                drawuntilplayableon.Background = Brushes.Gray;

                drawuntilplayableoff.Margin = new Thickness(0);
                drawuntilplayableoff.FontSize = 25;
                drawuntilplayableoff.Background = Brushes.OrangeRed;
            }
            Settings.UnsavedDrawUntilPlayable = state;
            IsSaved();
        }
        private void DrawUntilPlayableButton(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if(button == drawuntilplayableon)
            {
                Settings.UnsavedDrawUntilPlayable = true;
                DrawUntilPlayable(true);
            }
            else
            {
                Settings.UnsavedDrawUntilPlayable = false;
                DrawUntilPlayable(false);
                drawuntilplayableoff.Background = Brushes.OrangeRed;
            }
        }
        private void ForcePlayButton(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if(button == forceplayon)
            {
                Settings.UnsavedForcePlay = true;
                ForcePlay(true);
            }
            else
            {
                Settings.UnsavedForcePlay = false;
                ForcePlay(false);
            }
        }
        private void ForcePlay(bool state)
        {
            if (state)
            {
                forceplayon.Margin = new Thickness(0);
                forceplayon.FontSize = 25;
                forceplayon.Background = Brushes.LightGreen;

                forceplayoff.Margin = new Thickness(0, 3, 0, 3);
                forceplayoff.FontSize = 20;
                forceplayoff.Background = Brushes.Gray;
            }

            else
            {
                forceplayon.Margin = new Thickness(0, 3, 0, 3);
                forceplayon.FontSize = 20;
                forceplayon.Background = Brushes.Gray;

                forceplayoff.Margin = new Thickness(0);
                forceplayoff.FontSize = 25;
                forceplayoff.Background = Brushes.OrangeRed;
            }
            Settings.UnsavedForcePlay = state;
            IsSaved();
        }

        private void StackingButton(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if(button == stackingon)
            {
                Settings.UnsavedEnabledSounds = true;
                Stacking(true);
            }

            else
            {
                Settings.UnsavedEnabledSounds = false;
                Stacking(false);
            }
        }
        private void Stacking(bool state)
        {
            if (state)
            {
                stackingon.Margin = new Thickness(0);
                stackingon.FontSize = 25;
                stackingon.Background = Brushes.LightGreen;

                stackingoff.Margin = new Thickness(0, 3, 0, 3);
                stackingoff.FontSize = 20;
                stackingoff.Background = Brushes.Gray;
            }
            else
            {
                stackingon.Margin = new Thickness(0, 3, 0, 3);
                stackingon.FontSize = 20;
                stackingon.Background = Brushes.Gray;

                stackingoff.Margin = new Thickness(0);
                stackingoff.FontSize = 25;
                stackingoff.Background = Brushes.OrangeRed;
            }
            Settings.UnsavedEnabledSounds = state;
            IsSaved();
        }

        private void JumpInButton(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if(button == jumpinon)
            {
                JumpIn(true);
            }
            else
            {
                JumpIn(false);
            }
        }

        private void JumpIn(bool state)
        {
            if (state)
            {
                jumpinon.Margin = new Thickness(0);
                jumpinon.FontSize = 25;
                jumpinon.Background = Brushes.LightGreen;

                jumpinoff.Margin = new Thickness(0, 3, 0, 3);
                jumpinoff.FontSize = 20;
                jumpinoff.Background = Brushes.Gray;
            }
            else
            {
                jumpinon.Margin = new Thickness(0, 3, 0, 3);
                jumpinon.FontSize = 20;
                jumpinon.Background = Brushes.Gray;

                jumpinoff.Margin = new Thickness(0);
                jumpinoff.FontSize = 25;
                jumpinoff.Background = Brushes.OrangeRed;
            }
            Settings.UnsavedRandomDirection = state;
            IsSaved();
        }

        private void LanguageButton(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if(button == english)
            {
                LanguageSetting(UNOui.Language.English);
            }
            else
            {
                LanguageSetting(UNOui.Language.Ukrainian);
            }
        }

        private void LanguageSetting(Language lang)
        {
            if (lang == UNOui.Language.Ukrainian)
            {
                ToUkrainian();
                Settings.UnsavedLanguage = UNOui.Language.Ukrainian;
                ukrainian.Background = Brushes.LightGreen;
                ukrainian.FontSize = 25;
                ukrainian.Margin = new Thickness(0);

                english.Background = Brushes.Gray;
                english.FontSize = 20;
                english.Margin = new Thickness(0, 3, 0, 3);
            }
            else
            {
                ToEnglish();
                Settings.UnsavedLanguage = UNOui.Language.English;

                english.Background = Brushes.LightGreen;
                english.FontSize = 25;
                english.Margin = new Thickness(0);

                ukrainian.Background = Brushes.Gray;
                ukrainian.FontSize = 20;
                ukrainian.Margin = new Thickness(0, 3, 0, 3);
            }
            Settings.UnsavedLanguage = lang;
            IsSaved();
        }

        public void ToEnglish()
        {
            languagetextblock.Text = "LanguageSetting";
            english.Content = "English";
            ukrainian.Content = "Ukrainian";
            fullscreentextblock.Text = "Fullscreen";
            playercounttextblock.Text = "Player count";
            startingcardscountrun.Text = "Starting cards count";
            startingcardscounttextblock.Text = "Defines how many cards each player is starting with";
            forceplayrun.Text = "Force play";
            forceplaytextblock.Text = "If you draw a playable card, it will be played automatically";
            soundstextblock.Text = "Sounds";
            drawcardsuntilplayablerun.Text = "Draw cards until playable";
            drawcardsuntilplayabletextblock.Text = "You have to draw cards until you get a playable one";
            unsavedchanges.Text = "Unsaved changes";
            randomdirectionrun.Text = "Random direction";
            randomdirectiontextblock.Text = "If set to \"on\", the direction of play is randomly determined for each round; if set to \"off\", the direction is always clockwise";
            Items.MainWindowItem.play.Content = "Play";
            Items.MainWindowItem.exit.Content = "Exit";
            Items.MainWindowItem.settings.Content = "Settings";
            drawuntilplayableon.Content = forceplayon.Content = stackingon.Content = jumpinon.Content = fullscreenon.Content = "On";
            drawuntilplayableoff.Content = forceplayoff.Content = stackingoff.Content = jumpinoff.Content = fullscreenoff.Content = "Off";
        }

        public void ToUkrainian()
        {
            languagetextblock.Text = "Мова";
            ukrainian.Content = "Українська";
            english.Content = "Англійська";
            fullscreentextblock.Text = "На весь екран";
            playercounttextblock.Text = "Кількість гравців";
            startingcardscountrun.Text = "Початкова кількість карток";
            startingcardscounttextblock.Text = "Визначає, скільки карток отримує кожен гравець на початку гри";
            forceplayrun.Text = "Примусова зустріч";
            forceplaytextblock.Text = "Якщо ви витягнете грабельну карту, вона буде зіграна автоматично";
            soundstextblock.Text = "Звуки";
            drawcardsuntilplayablerun.Text = "Тягніть до грабельної";
            drawcardsuntilplayabletextblock.Text = "Тягніть карти, поки не отримаєте ту, якою можете походити";
            unsavedchanges.Text = "Незбережені зміни";
            randomdirectiontextblock.Text = "Якщо \"Вкл.\", напрям гри визначається випадково для кожного раунду; якщо встановлено \"Викл.\", напрям завжди за годинниковою стрілкою";
            randomdirectionrun.Text = "Випадковий напрям";
            Items.MainWindowItem.play.Content = "Грати";
            Items.MainWindowItem.exit.Content = "Вихід";
            Items.MainWindowItem.settings.Content = "Налаштування";
            drawuntilplayableon.Content = forceplayon.Content = stackingon.Content = jumpinon.Content = fullscreenon.Content = "Вкл.";
            drawuntilplayableoff.Content = forceplayoff.Content = stackingoff.Content = jumpinoff.Content = fullscreenoff.Content = "Викл.";
        }
    }
}
