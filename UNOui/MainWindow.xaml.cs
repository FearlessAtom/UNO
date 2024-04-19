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
            this.Close();
        }

        private void settingsbutton(object sender, RoutedEventArgs e)
        {
            
            UserControl settings = new settingsusercontrol();
            MainGrid.Children.Add(settings);
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

        private void setsettings(object sender, RoutedEventArgs e)
        {
            const string path = "C:\\Users\\357\\Desktop\\UNO\\UNOui\\data\\settings.txt";
            StreamReader reader = new StreamReader(path);
            string playercount = reader.ReadLine();
            string opponent = reader.ReadLine();
            string cardcount = reader.ReadLine();
            Settings.setplayercount(Convert.ToInt16(playercount));
            Settings.setopponent(Convert.ToInt16(opponent));
            Settings.setcardcount(Convert.ToInt16(cardcount));
            UnsavedSettings.cardcount = Settings.getcardcount();
            reader.Close();
        }
    }
}
