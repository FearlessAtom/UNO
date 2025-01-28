using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using Microsoft.SqlServer.Server;
using System.Windows.Media;
using System.Threading;
using System.Windows.Threading;
using System.Windows;
using System;
using UNOui;

namespace Project.Assets.ControlClasses
{
    public class Audio
    {
        static MediaPlayer soundplayer = new MediaPlayer()
        {
            Volume = 0.5,
        };
        public static void playsound(string path)
        {
            if (Settings.getsounds() == 2)
            {
                return;
            }
            soundplayer.Open(new Uri(path, UriKind.Relative));
            soundplayer.Play();
        }
        public static void playcardsound()
        {
            playsound(@"..\..\..\Music\cardsound.mp3");
        }
        public static void playcardtakesound()
        {
            playsound(@"..\..\..\Music\cardtake.mp3");
        }
        public static void playunosound()
        {
            soundplayer.Volume = 0.1;
            playsound(@"..\..\..\Music\unosound.mp3");
            soundplayer.Volume = 0.5;
        }
    }
}