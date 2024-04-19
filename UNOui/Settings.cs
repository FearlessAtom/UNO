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
    }
}
