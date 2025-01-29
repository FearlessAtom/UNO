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
            if (!Settings.EnabledSounds)
            {
                return;
            }
            soundplayer.Open(new Uri(path, UriKind.Relative));
            soundplayer.Play();
        }

        public static void PlayCardSound()
        {
            PlaySound(@"..\..\..\Music\cardsound.mp3");
        }

        public static void PlayCardTakeSound()
        {
            PlaySound(@"..\..\..\Music\cardtake.mp3");
        }

        public static void PlayUNOSound()
        {
            soundplayer.Volume = 0.1;
            PlaySound(@"..\..\..\Music\unosound.mp3");
            soundplayer.Volume = 0.5;
        }
    }
}