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
using System.Collections;
using System.Threading;
using System.Windows.Threading;
using Project.Assets.ControlClasses;
namespace UNOui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        Random random = new Random();
        public int randominteger(int min, int max)
        {
            return random.Next(min, max);
        }
        public double randomdouble(double min, double max)
        {
            return random.NextDouble() * (max - min) + min;

        }
        public void exitmenubutton(object sender, RoutedEventArgs e)
        {
            if (Settings.getsettingsopened())
            {
                return;
            }
            Settings.setexitconfirmationopened(true);
            UserControl exit = new exitconfirmation(1);
            MainGrid.Children.Add(exit); 
        }
        private void settingsbutton(object sender, RoutedEventArgs e)
        {
            if (Settings.getsettingsopened())
            {
                return;
            }
            UserControl settings = new settingsusercontrol();
            MainGrid.Children.Add(settings);
            Settings.setsettingsopened(true);
            Items.playbutton.Visibility = Visibility.Hidden;
            Items.settingsbutton.Visibility = Visibility.Hidden;
            Items.exitbutton.Visibility = Visibility.Hidden;
        }
        private void playbutton(object sender, RoutedEventArgs e)
        {
            if (Settings.getsettingsopened())
            {
                return;
            }
            play.Visibility = Visibility.Hidden;
            settings.Visibility = Visibility.Hidden;
            exit.Visibility = Visibility.Hidden;
            Settings.setgameopened(true);
            UserControl game = new game();
            MainGrid.Children.Add(game);
        }
        public void buttonmouseenter(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            Brush brush = button.Background;
            SolidColorBrush solidbrush = (SolidColorBrush)brush;
            Color color = solidbrush.Color;
            Items.buttoncolor = color;
            RadialGradientBrush gradient = darkergradient(color);
            button.Background = gradient;
        }
        public void buttonmouseleave(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = new SolidColorBrush(Items.buttoncolor);
        }
        public RadialGradientBrush darkergradient(Color color)
        {
            RadialGradientBrush gradient = new RadialGradientBrush();
            gradient.RadiusX = 0.99;
            gradient.RadiusY = 0.99;
            gradient.GradientStops.Add(new GradientStop(color, 0.0));
            gradient.GradientStops.Add(new GradientStop(Colors.Black, 3.0));
            return gradient;
        }
        public void setlanguage()
        {
            if(Settings.getlanguage() == 1)
            {
                toenglish();
            }
            else
            {
                toukrainian();
            }
        }
        public void toenglish()
        {
            Items.mainwindowitem.play.Content = "Play";
            Items.mainwindowitem.exit.Content = "Exit";
            Items.mainwindowitem.settings.Content = "Settings";
        }
        public void toukrainian()
        {
            Items.mainwindowitem.play.Content = "Грати";
            Items.mainwindowitem.exit.Content = "Вихід";
            Items.mainwindowitem.settings.Content = "Налаштування";
        }
        private void setsettings(object sender, RoutedEventArgs e)
        {
            Items.mainwindowitem = this;
            Items.playbutton = play;
            Items.settingsbutton = settings;
            Items.exitbutton = exit;
            const string path = @"..\..\..\data\settings.txt";
            StreamReader reader = new StreamReader(path);
            string playercount = reader.ReadLine();
            string fullscreen = reader.ReadLine();
            string cardcount = reader.ReadLine();
            string drawuntilplayable = reader.ReadLine();
            string forceplay = reader.ReadLine();
            string stacking = reader.ReadLine();
            string language = reader.ReadLine();
            string jumpin = reader.ReadLine();
            Settings.setplayercount(Convert.ToInt16(playercount));
            Settings.setfullscreen(Convert.ToInt16(fullscreen));
            Settings.setcardcount(Convert.ToInt16(cardcount));
            Settings.setdrawuntilplayable(Convert.ToInt16(drawuntilplayable));
            Settings.setforceplay(Convert.ToInt16(forceplay));
            Settings.setsounds(Convert.ToInt16(stacking));
            Settings.setlanguage(Convert.ToInt16(language));
            Settings.setrandomdirection(Convert.ToInt16(jumpin));
            UnsavedSettings.drawuntilplayable = Settings.getdrawuntilplayable();
            setlanguage();
            reader.Close();
        }
        private void keydown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (Items.gameitem.gamegrid.Children.Contains(Items.draworplayitem))
                {
                    Items.draworplayitem.draw(sender, e);
                }
                else if (Settings.getgameopened() == true)
                {
                    if (Settings.getgamemenuopened() == false)
                    {
                        Items.gameitem.menu(sender, e);
                    }
                    else
                    {
                        Items.gamemenuitem.resume(sender, e);
                    }
                }
                else if (Settings.getsettingsopened() == true)
                {
                    Items.settingsitem.closesettings(sender, e);
                }
                else if(Settings.getexitconfirmationopened() == true)
                {
                    Items.exitconfirmationitem.cancel(sender, e);
                }
                else
                {
                    exitmenubutton(sender, e);
                }
            }
        }
    }
}
