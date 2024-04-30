using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections;
using System.Configuration;
namespace UNOui
{
    /// <summary>
    /// Interaction logic for settingsusercontrol.xaml
    /// </summary>
    public partial class settingsusercontrol : UserControl
    {
        const string path = "C:\\Users\\357\\Desktop\\UNO\\UNOui\\data\\settings.txt";
        public settingsusercontrol()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        public void closesettings(object sender, RoutedEventArgs e)
        {
            if(Settings.getsaved() == false && !Settings.getconfirmation())
            {
                UserControl confirmation = new confirmation();
                maingridsettings.Children.Add(confirmation);
                Settings.setconfirmation(true);
            }
            else
            {
                Settings.setconfirmation(false);
                Items.playbutton.Visibility = Visibility.Visible;
                Items.settingsbutton.Visibility = Visibility.Visible;
                Items.exitbutton.Visibility = Visibility.Visible;
                Settings.setsettingsopened(false);
                setloadedsettings(sender, e);
                Grid grid = (Grid)Parent;
                grid.Children.Remove(this);
            }
        }
        public Color brushestocolor(Brush brush)
        {
            SolidColorBrush solidbrush = (SolidColorBrush)brush;
            return solidbrush.Color;
        }
        private void buttonmouseenter(object sender, MouseEventArgs e)
        {
            Items.mainwindowitem.buttonmouseenter(sender, e);
        }
        private void buttonmouseleave(object sender, MouseEventArgs e)
        {
            Items.mainwindowitem.buttonmouseleave(sender, e);
        }
        private void playercountchange(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Button[] array = { playercounttwo, playercountthree, playercountfour };
            for (int index = 0; index < array.Length; index++)
            {
                if (button == array[index])
                {
                    playercountload(index + 2);
                }
            }
        }
        public void saved()
        {
            if (Settings.getcardcount() == UnsavedSettings.cardcount &&
                Settings.getopponent() == UnsavedSettings.opponent &&
                Settings.getplayercount() == UnsavedSettings.playercount &&
                Settings.getdrawuntilplayable() == UnsavedSettings.drawuntilplayable &&
                Settings.getforceplay() == UnsavedSettings.forceplay && 
                Settings.getstacking() == UnsavedSettings.stacking &&
                Settings.getlanguage() == UnsavedSettings.language &&
                Settings.getjumpin() == UnsavedSettings.jumpin)
            {
                unsavedchanges.Visibility = Visibility.Hidden;
                Settings.setsaved(true);
            }
            else
            {
                unsavedchanges.Visibility = Visibility.Visible;
                Settings.setsaved(false);
                MainWindow atom = new MainWindow();
            }
        }
        private void playercountload(int number)
        {
            Button[] array = { playercounttwo, playercountthree, playercountfour };
            double margin = playercounttwo.ActualHeight * 0.05;
            bool found = false;
            for (int index = 0; index < array.Length; index++)
            {
                if (number == index + 2)
                {
                    array[index].Background = Brushes.LightGreen;
                    array[index].Margin = new Thickness(0, 0, 0, 0);
                    array[index].FontSize = 30;
                    found = true;
                }
                else if (!found)
                {
                    array[index].Background = Brushes.LightGreen;
                    array[index].Margin = new Thickness(0, margin, 0, margin);
                    array[index].FontSize = 20;
                }
                else if (found)
                {
                    array[index].Background = Brushes.Gray;
                    array[index].Margin = new Thickness(0, margin, 0, margin);
                    array[index].FontSize = 20;
                }
            }
            UnsavedSettings.playercount = number;
            saved();
        }
        public void setloadedsettings(object sender, RoutedEventArgs e)
        {
            Items.settingsitem = this;
            playercountload(Settings.getplayercount());
            opponent(Settings.getopponent());
            startcardschange(Settings.getcardcount());
            forceplay(Settings.getforceplay());
            drawuntilplayable(Settings.getdrawuntilplayable());
            stacking(Settings.getstacking());
            language(Settings.getlanguage());
            jumpin(Settings.getjumpin());
            saved();
        }
        public void savesettings(object sender, RoutedEventArgs e)
        {
            Settings.setplayercount(UnsavedSettings.playercount);
            Settings.setopponent(UnsavedSettings.opponent);
            Settings.setcardcount(UnsavedSettings.cardcount);
            Settings.setdrawuntilplayable(UnsavedSettings.drawuntilplayable);
            Settings.setforceplay(UnsavedSettings.forceplay);
            Settings.setstacking(UnsavedSettings.stacking);
            Settings.setlanguage(UnsavedSettings.language);
            Settings.setjumpin(UnsavedSettings.jumpin);
            StreamWriter writer = new StreamWriter(path);
            writer.WriteLine(Settings.getplayercount().ToString());
            writer.WriteLine(Settings.getopponent().ToString());
            writer.WriteLine(Settings.getcardcount().ToString());
            writer.WriteLine(Settings.getdrawuntilplayable().ToString());
            writer.WriteLine(Settings.getforceplay().ToString());
            writer.WriteLine(Settings.getstacking().ToString());
            writer.WriteLine(Settings.getlanguage().ToString());
            writer.WriteLine(Settings.getjumpin().ToString());
            writer.Close();
            saved();
        }
        private void setopponentbutton(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (sender == computer)
            {
                opponent(2);
            }
            else
            {
                opponent(1);
            }
        }
        private void opponent(int opponent)
        {
            if (opponent == 2)
            {
                UnsavedSettings.opponent = 2;
                computer.Background = Brushes.LightGreen;
                computer.FontSize = 25;
                computer.Margin = new Thickness(0);

                players.Background = Brushes.Gray;
                players.FontSize = 20;
                players.Margin = new Thickness(0, 3, 0, 3);
            }
            else
            {
                UnsavedSettings.opponent = 1;

                players.Background = Brushes.LightGreen;
                players.FontSize = 25;
                players.Margin = new Thickness(0);

                computer.Background = Brushes.Gray;
                computer.FontSize = 20;
                computer.Margin = new Thickness(0, 3, 0, 3);
            }
            saved();
        }
        private void cardcountbutton(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button == minus)
                {
                    if (UnsavedSettings.cardcount != 1)
                    {
                        UnsavedSettings.cardcount--;
                    }
                }
            else
                {
                    if (UnsavedSettings.cardcount != 10)
                    {
                        UnsavedSettings.cardcount++;
                    }
                }
            startcardschange(UnsavedSettings.cardcount);
            saved();
            }
        private void startcardschange(int number)
        {
            if (number == 1)
            {
                minus.FontSize = 20;
                minus.Background = Brushes.Gray;
            }
            else if (number == 10)
            {
            plus.FontSize = 20;
            plus.Background = Brushes.Gray;
            }
            else
            {
                plus.FontSize = 25;
                plus.Background = Brushes.LightGreen;
                minus.FontSize = 25;
                minus.Background = Brushes.LightGreen;
            }
            UnsavedSettings.cardcount = number;
            startingcardscount.Text = number.ToString();
        }
        private void drawuntilplayable(int number)
        {
            if(number == 1)
            {
                drawuntilplayableon.Margin = new Thickness(0);
                drawuntilplayableon.FontSize = 25;
                drawuntilplayableon.Background = Brushes.LightGreen;

                drawuntilplayableoff.Margin = new Thickness(0, 3, 0, 3);
                drawuntilplayableoff.FontSize = 20;
                drawuntilplayableoff.Background = Brushes.Gray;
            }
            else
            {
                drawuntilplayableon.Margin = new Thickness(0, 3, 0, 3);
                drawuntilplayableon.FontSize = 20;
                drawuntilplayableon.Background = Brushes.Gray;

                drawuntilplayableoff.Margin = new Thickness(0);
                drawuntilplayableoff.FontSize = 25;
                drawuntilplayableoff.Background = Brushes.OrangeRed;
            }
            UnsavedSettings.drawuntilplayable = number;
            saved();
        }
        private void drawuntilplayablebutton(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if(button == drawuntilplayableon)
            {
                UnsavedSettings.drawuntilplayable = 1;
                drawuntilplayable(1);
            }
            else
            {
                UnsavedSettings.drawuntilplayable = 2;
                drawuntilplayable(2);
                drawuntilplayableoff.Background = Brushes.OrangeRed;
            }
        }
        private void forceplaybutton(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if(button == forceplayon)
            {
                UnsavedSettings.forceplay = 1;
                forceplay(1);
            }
            else
            {
                UnsavedSettings.forceplay = 2;
                forceplay(2);
            }
        }
        private void forceplay(int number)
        {
            if (number == 1)
            {
                forceplayon.Margin = new Thickness(0);
                forceplayon.FontSize = 25;
                forceplayon.Background = Brushes.LightGreen;

                forceplayoff.Margin = new Thickness(0, 3, 0, 3);
                forceplayoff.FontSize = 20;
                forceplayoff.Background = Brushes.Gray;
            }
            else
            {
                forceplayon.Margin = new Thickness(0, 3, 0, 3);
                forceplayon.FontSize = 20;
                forceplayon.Background = Brushes.Gray;

                forceplayoff.Margin = new Thickness(0);
                forceplayoff.FontSize = 25;
                forceplayoff.Background = Brushes.OrangeRed;
            }
            UnsavedSettings.forceplay = number;
            saved();
        }
        private void stackingbutton(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if(button == stackingon)
            {
                UnsavedSettings.stacking = 1;
                stacking(1);
            }
            else
            {
                UnsavedSettings.stacking = 2;
                stacking(2);
            }
        }
        private void stacking(int number)
        {
            if (number == 1)
            {
                stackingon.Margin = new Thickness(0);
                stackingon.FontSize = 25;
                stackingon.Background = Brushes.LightGreen;

                stackingoff.Margin = new Thickness(0, 3, 0, 3);
                stackingoff.FontSize = 20;
                stackingoff.Background = Brushes.Gray;
            }
            else
            {
                stackingon.Margin = new Thickness(0, 3, 0, 3);
                stackingon.FontSize = 20;
                stackingon.Background = Brushes.Gray;

                stackingoff.Margin = new Thickness(0);
                stackingoff.FontSize = 25;
                stackingoff.Background = Brushes.OrangeRed;
            }
            UnsavedSettings.stacking = number;
            saved();
        }
        private void jumpinbutton(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if(button == jumpinon)
            {
                jumpin(1);
            }
            else
            {
                jumpin(2);
            }
        }
        private void jumpin(int number)
        {
            if (number == 1)
            {
                jumpinon.Margin = new Thickness(0);
                jumpinon.FontSize = 25;
                jumpinon.Background = Brushes.LightGreen;

                jumpinoff.Margin = new Thickness(0, 3, 0, 3);
                jumpinoff.FontSize = 20;
                jumpinoff.Background = Brushes.Gray;
            }
            else
            {
                jumpinon.Margin = new Thickness(0, 3, 0, 3);
                jumpinon.FontSize = 20;
                jumpinon.Background = Brushes.Gray;

                jumpinoff.Margin = new Thickness(0);
                jumpinoff.FontSize = 25;
                jumpinoff.Background = Brushes.OrangeRed;
            }
            UnsavedSettings.jumpin = number;
            saved();
        }
        private void languagebutton(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if(button == english)
            {
                language(1);
            }
            else
            {
                language(2);
            }
        }
        private void language(int number)
        {
            if (number == 2)
            {
                toukrainian();
                UnsavedSettings.language = 2;
                ukrainian.Background = Brushes.LightGreen;
                ukrainian.FontSize = 25;
                ukrainian.Margin = new Thickness(0);

                english.Background = Brushes.Gray;
                english.FontSize = 20;
                english.Margin = new Thickness(0, 3, 0, 3);
            }
            else
            {
                toenglish();
                UnsavedSettings.language = 1;

                english.Background = Brushes.LightGreen;
                english.FontSize = 25;
                english.Margin = new Thickness(0);

                ukrainian.Background = Brushes.Gray;
                ukrainian.FontSize = 20;
                ukrainian.Margin = new Thickness(0, 3, 0, 3);
            }
            UnsavedSettings.language = number;
            saved();
        }
        public void toenglish()
        {
            languagetextblock.Text = "Language";
            english.Content = "English";
            ukrainian.Content = "Ukrainian";
            opponenttextblock.Text = "Opponent";
            players.Content = "Players";
            computer.Content = "Computer";
            playercounttextblock.Text = "Player count";
            startingcardscountrun.Text = "Starting cards count";
            startingcardscounttextblock.Text = "Defines how many cards each player is starting with";
            forceplayrun.Text = "Force play";
            forceplaytextblock.Text = "If you draw a playable card, it will be played automatically";
            stackingrun.Text = "Stacking";
            stackingtextblock.Text = "Block draw cards by stacking another one on top";
            drawcardsuntilplayablerun.Text = "Draw cards until playable";
            drawcardsuntilplayabletextblock.Text = "You have to draw cards until you get a playable one";
            unsavedchanges.Text = "Unsaved changes";
            jumpinrun.Text = "Jump in";
            jumpintextblock.Text = "If a player has the exact same card that is currently being played, they can play the card out of turn";
            Items.mainwindowitem.play.Content = "Play";
            Items.mainwindowitem.exit.Content = "Exit";
            Items.mainwindowitem.settings.Content = "Settings";
        }
        public void toukrainian()
        {
            languagetextblock.Text = "Мова";
            ukrainian.Content = "Українська";
            english.Content = "Англійська";
            opponenttextblock.Text = "Противник";
            players.Content = "Гравці";
            computer.Content = "Комп'ютер";
            playercounttextblock.Text = "Кількість гравців";
            startingcardscountrun.Text = "Початкова кількість карток";
            startingcardscounttextblock.Text = "Визначає, скільки карток отримує кожен гравець на початку гри";
            forceplayrun.Text = "Примусова зустріч";
            forceplaytextblock.Text = "Якщо ви витягнете грабельну карту, вона буде зіграна автоматично";
            stackingrun.Text = "Накладання";
            stackingtextblock.Text = "Блокуйте +2 або + 4, накладаючи іншу карту";
            drawcardsuntilplayablerun.Text = "Тягніть до грабельної";
            drawcardsuntilplayabletextblock.Text = "Тягніть карти, поки не отримаєте ту, якою можете походити";
            unsavedchanges.Text = "Незбережені зміни";
            jumpinrun.Text = "Вступайте";
            jumpintextblock.Text = "Якщо у гравця є та сама карта, що зараз грається, він може зіграти карту без черги";
            Items.mainwindowitem.play.Content = "Грати";
            Items.mainwindowitem.exit.Content = "Вихід";
            Items.mainwindowitem.settings.Content = "Налаштування";
        }

    }
}