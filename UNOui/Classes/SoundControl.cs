using System.Windows.Media;
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

        public static void PlaySound(string path)
        {
            if (Settings.Sounds == 2)
            {
                return;
            }
            soundplayer.Open(new Uri(path));
            soundplayer.Play();
        }

        public static void PlayCardSound()
        {
            PlaySound("C:\\Users\\357\\\\Desktop\\UNO\\UNOui\\Music\\cardsound.mp3");
        }

        public static void PlayCardTakeSound()
        {
            PlaySound("C:\\Users\\357\\Desktop\\UNO\\UNOui\\Music\\cardtake.mp3");
        }

        public static void PlayUNOSound()
        {
            soundplayer.Volume = 0.1;
            PlaySound("C:\\Users\\357\\Desktop\\UNO\\UNOui\\Music\\unosound.mp3");
            soundplayer.Volume = 0.5;
        }
    }
}