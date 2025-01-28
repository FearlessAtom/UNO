using System.CodeDom;
using System.Windows.Controls;
using System.Windows.Media;
namespace UNOui
{
    public class Items
    {
        public const string SettingsFilePath = @"..\..\..\data\settings.txt";
        public const string UNOCardsFilePath = @"..\..\..\assets\UNOcards\";

        public static MainWindow MainWindowItem;
        public static SettingsUserControl SettingsItem;
        public static Game GameItem;
        public static ExitConfirmation ExitConfirmationItem;
        public static GameMenu GameMenuItem;
        public static DrawOrPlay DrawOrPlayItem;
        public static Button PlayButton;
        public static Button SettingsButton;
        public static Button ExitButton;
        public static Color ButtonColor;
    }
}
