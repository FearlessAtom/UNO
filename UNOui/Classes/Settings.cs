namespace UNOui
{
    class Settings
    {
        public static int PlayerCount { get; set; }
        public static int Fullscreen { get; set; }
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
        
        public Settings(int PlayerCount, int Fullscreen, int CardCount)
        {
            Settings.PlayerCount = PlayerCount;
            Settings.Fullscreen = Fullscreen;
            Settings.CardCount = CardCount;
        }

        public static bool getconfirmation()
        {
            return Confirmation;
        }

        public static void setconfirmation(bool Confirmation)
        {
            Settings.Confirmation = Confirmation;
        }
        public static int getrandomdirection()
        {
            return RandomDirection;
        }
        public static void setrandomdirection(int Randomdirection)
        {
            RandomDirection = Randomdirection;
        }
        public static bool getgameopened()
        {
            return GameOpened;
        }
        public static void setgameopened(bool Gameopened)
        {
            GameOpened = Gameopened;
        }
        public static bool getexitconfirmationopened()
        {
            return ExitConfirmationOpened;
        }
        public static void setexitconfirmationopened(bool Exitconfirmationopened)
        {
            ExitConfirmationOpened = Exitconfirmationopened;
        }
        public static bool getgamemenuopened()
        {
            return GameMenuOpened;
        }
        public static void setgamemenuopened(bool Gamemenuopened)
        {
            GameMenuOpened = Gamemenuopened;
        }

        public static int getfullscreen()
        {
            if (Fullscreen == 1)
            {
                Items.MainWindowItem.WindowState = System.Windows.WindowState.Maximized;
            }

            return Fullscreen;
        }
        public static void setfullscreen(int Fullscreen)
        {
            if(Fullscreen == 1)
            {
                Items.MainWindowItem.WindowState = System.Windows.WindowState.Maximized;
            }

            Settings.Fullscreen = Fullscreen;
        }
    }
}
