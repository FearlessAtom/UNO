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

namespace Project.Assets.ControlClasses
{
    public class Audio
    {
        static MediaPlayer musicplayer = new MediaPlayer();
        static MediaPlayer soundplayer = new MediaPlayer();
        public static void playmusic()
        {
            return;
            musicplayer.Open(new Uri("C:\\Users\\357\\\\Desktop\\UNO\\UNOui\\Music\\music.mp3"));
            musicplayer.Volume = 0.1;
            musicplayer.MediaEnded += playerended;
            musicplayer.Play();
        }
        private static void playerended(object sender, EventArgs e)
        {
            musicplayer.Position = TimeSpan.Zero;
            musicplayer.Play();
        }
        public static void playcardsound()
        {
            return;
            soundplayer.Open(new Uri("C:\\Users\\357\\\\Desktop\\UNO\\UNOui\\Music\\cardsound.mp3"));
            soundplayer.Play();
        }
    }
}