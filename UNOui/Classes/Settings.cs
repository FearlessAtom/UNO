using System;
using System.IO;
using System.Windows;

namespace UNOui
{
    class Settings
    {
        public static int PlayerCount { get; set; }
        public static int CardCount { get; set; }
        public static int DrawUntilPlayable { get; set; }
        public static int ForcePlay { get; set; }
        public static int EnabledSounds { get; set; }
        public static int Language { get; set; }
        public static int RandomDirection { get; set; }
        public static bool GameMenuOpened { get; set; }
        public static bool ExitConfirmationOpened { get; set; }
        public static bool SettingsOpened { get; set; }
        public static bool GameOpened { get; set; }
        public static bool Saved { get; set; }
        public static bool Confirmation  { get; set; } = false;

        protected static int _Fullscreen;
        public static int Fullscreen
        {
            get
            {
                if (_Fullscreen == 1)
                {
                    Items.MainWindowItem.WindowState = System.Windows.WindowState.Maximized;
                }

                return _Fullscreen;
            }

            set
            {
                if(_Fullscreen == 1)
                {
                    Items.MainWindowItem.WindowState = System.Windows.WindowState.Maximized;
                }

                _Fullscreen = value;
            }
        }
        
        public Settings(int PlayerCount, int Fullscreen, int CardCount)
        {
            Settings.PlayerCount = PlayerCount;
            Settings.Fullscreen = Fullscreen;
            Settings.CardCount = CardCount;
        }

        public static int UnsavedPlayerCount { get; set; }
        public static int UnsavedFullscreen { get; set; }
        public static int UnsavedCardCount { get; set; }
        public static int UnsavedDrawUntilPlayable { get; set; }
        public static int UnsavedForcePlay { get; set; }
        public static int UnsavedEnabledSounds { get; set; }
        public static int UnsavedLanguage { get; set; }
        public static int UnsavedRandomDirection { get; set; }


        static public void SaveSettings()
        {
            PlayerCount = UnsavedPlayerCount;
            Fullscreen = UnsavedFullscreen;
            CardCount = UnsavedCardCount;
            DrawUntilPlayable = UnsavedDrawUntilPlayable;
            ForcePlay = UnsavedForcePlay;
            EnabledSounds = UnsavedEnabledSounds;
            Language = UnsavedLanguage;
            RandomDirection = UnsavedRandomDirection;
            StreamWriter writer = null;

            try
            {
                writer = new StreamWriter(Items.SettingsFilePath);
            }

            catch(Exception exception)
            {
                MessageBox.Show("Error saving the settings!");
                return;
            }

            writer.WriteLine(PlayerCount.ToString());
            writer.WriteLine(Fullscreen.ToString());
            writer.WriteLine(CardCount.ToString());
            writer.WriteLine(DrawUntilPlayable.ToString());
            writer.WriteLine(ForcePlay.ToString());
            writer.WriteLine(EnabledSounds.ToString());
            writer.WriteLine(Language.ToString());
            writer.WriteLine(RandomDirection.ToString());
            writer.Close();
        }

        static public bool AreSaved()
        {
            return CardCount == UnsavedCardCount &&
                Fullscreen == UnsavedFullscreen &&
                PlayerCount == UnsavedPlayerCount &&
                DrawUntilPlayable == UnsavedDrawUntilPlayable &&
                ForcePlay == UnsavedForcePlay &&
                EnabledSounds == UnsavedEnabledSounds &&
                Language == UnsavedLanguage &&
                RandomDirection == UnsavedRandomDirection;
        } 
    }
}
