using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;

namespace UNOui
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Random random = new Random();

        public int RandomInteger(int min, int max)
        {
            return random.Next(min, max);
        }

        public double RandomDouble(double min, double max)
        {
            return random.NextDouble() * (max - min) + min;

        }

        public void ExitMenuButton(object sender, RoutedEventArgs e)
        {
            if (Settings.IsSettingsOpened)
            {
                return;
            }

            Settings.IsExitConfirmationOpened = true;
            UserControl exit = new ExitConfirmation(1);
            MainGrid.Children.Add(exit); 
        }

        private void SettingsButton(object sender, RoutedEventArgs e)
        {
            if (Settings.IsSettingsOpened)
            {
                return;
            }

            UserControl settings = new SettingsUserControl();
            MainGrid.Children.Add(settings);
            Settings.IsSettingsOpened = true;
        }

        private void PlayButton(object sender, RoutedEventArgs e)
        {
            if (Settings.IsSettingsOpened)
            {
                return;
            }

            Settings.IsGameOpened = true;
            UserControl game = new Game();
            MainGrid.Children.Add(game);
        }

        public void ButtonMouseEnter(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            Brush brush = button.Background;
            SolidColorBrush solidbrush = (SolidColorBrush)brush;
            Color color = solidbrush.Color;
            Items.ButtonColor = color;
            RadialGradientBrush gradient = GetDarkerGradient(color);
            button.Background = gradient;
        }

        public void ButtonMouseLeave(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = new SolidColorBrush(Items.ButtonColor);
        }

        public RadialGradientBrush GetDarkerGradient(Color color)
        {
            RadialGradientBrush gradient = new RadialGradientBrush();
            gradient.RadiusX = 0.99;
            gradient.RadiusY = 0.99;
            gradient.GradientStops.Add(new GradientStop(color, 0.0));
            gradient.GradientStops.Add(new GradientStop(Colors.Black, 3.0));
            return gradient;
        }

        public void SetLanguage()
        {
            if(Settings.Language == 1)
            {
                ToEnglish();
            }
            else
            {
                ToUkrainian();
            }
        }
        
        public void ToEnglish()
        {
            Items.MainWindowItem.play.Content = "Play";
            Items.MainWindowItem.exit.Content = "Exit";
            Items.MainWindowItem.settings.Content = "Settings";
        }

        public void ToUkrainian()
        {
            Items.MainWindowItem.play.Content = "Грати";
            Items.MainWindowItem.exit.Content = "Вихід";
            Items.MainWindowItem.settings.Content = "Налаштування";
        }

        private void SetSettings(object sender, RoutedEventArgs e)
        {
            Items.MainWindowItem = this;
            Items.PlayButton = play;
            Items.SettingsButton = settings;
            Items.ExitButton = exit;
            const string path = Items.SettingsFilePath;

            StreamReader reader;
            try
            {
                reader = new StreamReader(path);
            }

            catch (Exception exeption)
            {
                MessageBox.Show("Error loading the settings!");
                Close();
                return;
            }

            string playercount = reader.ReadLine();
            string fullscreen = reader.ReadLine();
            string cardcount = reader.ReadLine();
            string drawuntilplayable = reader.ReadLine();
            string forceplay = reader.ReadLine();
            string stacking = reader.ReadLine();
            string language = reader.ReadLine();
            string jumpin = reader.ReadLine();
            Settings.PlayerCount = Convert.ToInt16(playercount);
            Settings.Fullscreen = Convert.ToBoolean(fullscreen);
            Settings.CardCount = Convert.ToInt16(cardcount);
            Settings.DrawUntilPlayable = Convert.ToBoolean(drawuntilplayable);
            Settings.ForcePlay = Convert.ToBoolean(forceplay);
            Settings.EnabledSounds = Convert.ToBoolean(stacking);
            Settings.Language = Convert.ToInt16(language);
            Settings.RandomDirection = Convert.ToBoolean(jumpin);
            Settings.UnsavedDrawUntilPlayable = Settings.DrawUntilPlayable;
            SetLanguage();
            reader.Close();
        }

        private void ApplicationKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (Items.GameItem.gamegrid.Children.Contains(Items.DrawOrPlayItem))
                {
                    Items.DrawOrPlayItem.DrawCard(sender, e);
                }
                else if (Settings.IsGameOpened == true)
                {
                    if (Settings.IsGameMenuOpened == false)
                    {
                        Items.GameItem.OpenMenuButton(sender, e);
                    }
                    else
                    {
                        Items.GameMenuItem.Resume(sender, e);
                    }
                }
                else if (Settings.IsSettingsOpened == true)
                {
                    Items.SettingsItem.CloseSettings(sender, e);
                }
                else if(Settings.IsExitConfirmationOpened == true)
                {
                    Items.ExitConfirmationItem.Close(sender, e);
                }
                else
                {
                    ExitMenuButton(sender, e);
                }
            }
        }
    }
}
