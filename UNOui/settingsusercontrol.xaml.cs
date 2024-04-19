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
        private void closesettings(object sender, RoutedEventArgs e)
        {
            Grid panel = (Grid)this.Parent;
            panel.Children.Remove(this);
        }
        private void buttonmouseenter(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            RadialGradientBrush gradient = new RadialGradientBrush();
            gradient.RadiusX = 0.99;
            gradient.RadiusY = 0.99;
            gradient.GradientStops.Add(new GradientStop(Colors.AliceBlue, 0.0));
            gradient.GradientStops.Add(new GradientStop(Colors.Black, 3.0));
            button.Background = gradient;
        }
        private void buttonmouseleave(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            Brush color = new SolidColorBrush(Colors.Blue);
            button.Background = Brushes.LightGray;
        }
        private void playercountchange(object sender, RoutedEventArgs e)
        {
            unsavedchanges.Visibility = Visibility.Visible;
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
        }
        public void setloadedsettings(object sender, RoutedEventArgs e)
        {
            playercountload(Settings.getplayercount());
            opponent(Settings.getopponent());
            startcardschange(Settings.getcardcount());
            unsavedchanges.Visibility = Visibility.Hidden;
        }
        public void savesettings(object sender, RoutedEventArgs e)
        {
            Settings.setplayercount(UnsavedSettings.playercount);
            Settings.setopponent(UnsavedSettings.opponent);
            Settings.setcardcount(UnsavedSettings.cardcount);
            StreamWriter writer = new StreamWriter(path);
            writer.WriteLine(Settings.getplayercount().ToString());
            writer.WriteLine(Settings.getopponent().ToString());
            writer.WriteLine(Settings.getcardcount().ToString());
            writer.Close();
            unsavedchanges.Visibility = Visibility.Hidden;
        }
        private void setopponentbutton(object sender, RoutedEventArgs e)
        {
            unsavedchanges.Visibility = Visibility.Visible;
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
        }
        private void cardcountbutton(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            bool changed = false;
            if (button == minus)
                {
                    if (UnsavedSettings.cardcount != 1)
                    {
                        UnsavedSettings.cardcount--;
                        changed = true;
                    }
                }
            else
                {
                    if (UnsavedSettings.cardcount != 10)
                    {
                        UnsavedSettings.cardcount++;
                        changed = true;
                    }
                }
                if(changed)unsavedchanges.Visibility = Visibility.Visible;
                startcardschange(UnsavedSettings.cardcount);
            }
            void startcardschange(int number)
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
                startingcardscount.Text = number.ToString();
            }
        }
    }