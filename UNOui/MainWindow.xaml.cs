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
        
        private void exitmenubutton(object sender, RoutedEventArgs e)
        {
            if (!Settings.getsettingsopened())
            {
                Close();
            }
        }

        private void settingsbutton(object sender, RoutedEventArgs e)
        {
            
            if(!Settings.getsettingsopened())
            {
                UserControl settings = new settingsusercontrol();
                MainGrid.Children.Add(settings);
                Settings.setsettingsopened(true);
                Items.playbutton.Visibility = Visibility.Hidden;
                Items.settingsbutton.Visibility = Visibility.Hidden;
                Items.exitbutton.Visibility = Visibility.Hidden;

            }
        }
        private void playbutton(object sender, RoutedEventArgs e)
        {
            if (!Settings.getsettingsopened())
            {
                MessageBox.Show("Uno");
            }
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
            button.Background = Brushes.AliceBlue;
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
            const string path = "C:\\Users\\357\\Desktop\\UNO\\UNOui\\data\\settings.txt";
            StreamReader reader = new StreamReader(path);
            string playercount = reader.ReadLine();
            string opponent = reader.ReadLine();
            string cardcount = reader.ReadLine();
            string drawuntilplayable = reader.ReadLine();
            string forceplay = reader.ReadLine();
            string stacking = reader.ReadLine();
            string language = reader.ReadLine();
            Settings.setplayercount(Convert.ToInt16(playercount));
            Settings.setopponent(Convert.ToInt16(opponent));
            Settings.setcardcount(Convert.ToInt16(cardcount));
            Settings.setdrawuntilplayable(Convert.ToInt16(drawuntilplayable));
            Settings.setforceplay(Convert.ToInt16(forceplay));
            Settings.setstacking(Convert.ToInt16(stacking));
            Settings.setlanguage(Convert.ToInt16(language));
            UnsavedSettings.drawuntilplayable = Settings.getdrawuntilplayable();
            setlanguage();
            reader.Close();
        }

    }
}
