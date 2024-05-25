using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNOui
{
    class Settings
    {
        protected static int playercount;
        protected static int fullscreen;
        protected static int cardcount;
        protected static int drawuntilplayable;
        protected static int forceplay;
        protected static int sounds;
        protected static int language;
        protected static int randomdirection;
        protected static bool gamemenuopened;
        protected static bool exitconfirmationopened;
        protected static bool settingsopened;
        protected static bool gameopened;
        protected static bool saved;
        protected static bool confirmation = false;
        public Settings(int playerCount, int Opponent, int Cardcount)
        {
            playercount = playerCount;
            fullscreen = Opponent;
            cardcount = Cardcount;
        }
        public static void setplayercount(int newplayercount)
        {
            playercount = newplayercount;
        }
        public static int getplayercount()
        {
            return playercount;
        }
        public static int getfullscreen()
        {
            if (fullscreen == 1)
            {
                Items.mainwindowitem.WindowState = System.Windows.WindowState.Maximized;
            }
            return fullscreen;
        }
        public static void setfullscreen(int Fullscreen)
        {
            if(Fullscreen == 1)
            {
                Items.mainwindowitem.WindowState = System.Windows.WindowState.Maximized;
            }
            fullscreen = Fullscreen;
        }
        public static int getcardcount()
        {
            return cardcount;
        }
        public static void setcardcount(int Cardcount)
        {
            cardcount = Cardcount;
        }
        public static bool getsaved()
        {
            return saved;
        }
        public static void setsaved(bool Saved)
        {
            saved = Saved;
        }
        public static int getdrawuntilplayable()
        {
            return drawuntilplayable;
        }
        public static void setdrawuntilplayable(int Drawuntilplayble)
        {
            drawuntilplayable = Drawuntilplayble;
        }
        public static bool getsettingsopened()
        {
            return settingsopened;
        }
        public static void setsettingsopened(bool SettingsOpened)
        {
            settingsopened = SettingsOpened;
        }
        public static int getforceplay()
        {
            return forceplay;
        }
        public static void setforceplay(int Forceplay)
        {
            forceplay = Forceplay;
        }
        public static int getsounds()
        {
            return sounds;
        }
        public static void setsounds(int Sounds)
        {
            sounds = Sounds;
        }
        public static int getlanguage()
        {
            return language;
        }
        public static void setlanguage(int Language)
        {
            language = Language;
        }
        public static bool getconfirmation()
        {
            return confirmation;
        }
        public static void setconfirmation(bool Confirmation)
        {
            confirmation = Confirmation;
        }
        public static int getrandomdirection()
        {
            return randomdirection;
        }
        public static void setrandomdirection(int Randomdirection)
        {
            randomdirection = Randomdirection;
        }
        public static bool getgameopened()
        {
            return gameopened;
        }
        public static void setgameopened(bool Gameopened)
        {
            gameopened = Gameopened;
        }
        public static bool getexitconfirmationopened()
        {
            return exitconfirmationopened;
        }
        public static void setexitconfirmationopened(bool Exitconfirmationopened)
        {
            exitconfirmationopened = Exitconfirmationopened;
        }
        public static bool getgamemenuopened()
        {
            return gamemenuopened;
        }
        public static void setgamemenuopened(bool Gamemenuopened)
        {
            gamemenuopened = Gamemenuopened;
        }

    }
}
