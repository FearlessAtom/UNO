using System;
using System.IO;
using System.Windows;

namespace UNOui
{
    class Settings
    {
        public static int PlayerCount { get; set; }
        public static int CardCount { get; set; }
        public static bool DrawUntilPlayable { get; set; }
        public static bool ForcePlay { get; set; }
        public static bool EnabledSounds { get; set; }
        public static int Language { get; set; }
        public static bool RandomDirection { get; set; }
        public static bool IsGameMenuOpened { get; set; }
        public static bool IsExitConfirmationOpened { get; set; }
        public static bool IsSettingsOpened { get; set; }
        public static bool IsGameOpened { get; set; }
        public static bool IsSaved { get; set; }
        public static bool Confirmation  { get; set; } = false;

        protected static bool _Fullscreen;
        public static bool Fullscreen
        {
            get
            {
                if (_Fullscreen == true)
                {
                    Items.MainWindowItem.WindowState = System.Windows.WindowState.Maximized;
                }

                return _Fullscreen;
            }

            set
            {
                if(_Fullscreen == true)
                {
                    Items.MainWindowItem.WindowState = System.Windows.WindowState.Maximized;
                }

                _Fullscreen = value;
            }
        }
        
        public Settings(int PlayerCount, bool Fullscreen, int CardCount)
        {
            Settings.PlayerCount = PlayerCount;
            Settings.Fullscreen = Fullscreen;
            Settings.CardCount = CardCount;
        }

        public static int UnsavedPlayerCount { get; set; }
        public static bool UnsavedFullscreen { get; set; }
        public static int UnsavedCardCount { get; set; }
        public static bool UnsavedDrawUntilPlayable { get; set; }
        public static bool UnsavedForcePlay { get; set; }
        public static bool UnsavedEnabledSounds { get; set; }
        public static int UnsavedLanguage { get; set; }
        public static bool UnsavedRandomDirection { get; set; }


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
