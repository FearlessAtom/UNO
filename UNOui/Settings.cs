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
        protected static int opponent;
        protected static int cardcount;
        protected static int drawuntilplayable;
        protected static int forceplay;
        protected static int stacking;
        protected static int language;
        protected static int jumpin;
        protected static bool gamemenuopened;
        protected static bool exitconfirmationopened;
        protected static bool settingsopened;
        protected static bool gameopened;
        protected static bool saved;
        protected static bool confirmation = false;
        public Settings(int playerCount, int Opponent, int Cardcount)
        {
            playercount = playerCount;
            opponent = Opponent;
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
        public static int getopponent()
        {
            return opponent;
        }
        public static void setopponent(int Opponent)
        {
            opponent = Opponent;
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
        public static int getstacking()
        {
            return stacking;
        }
        public static void setstacking(int Stacking)
        {
            stacking = Stacking;
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
        public static int getjumpin()
        {
            return jumpin;
        }
        public static void setjumpin(int Jumpin)
        {
            jumpin = Jumpin;
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
