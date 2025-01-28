namespace UNOui
{
    class Settings
    {
        public static int PlayerCount { get; set; }
        public static int CardCount { get; set; }
        public static int DrawUntilPlayable { get; set; }
        public static int ForcePlay { get; set; }
        public static int Sounds { get; set; }
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
    }
}
