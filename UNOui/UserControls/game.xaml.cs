using Project.Assets.ControlClasses;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace UNOui
{
    /// <summary>
    /// Interaction logic for game.xaml
    /// </summary>
    public partial class game : UserControl
    {
        public game()
        {
            InitializeComponent();
        }
        public void toenglish()
        {
            menubutton.Content = "Menu";
        }
        public void toukrainian()
        {
            menubutton.Content = "Меню";
        }
        public void menu(object sender, RoutedEventArgs e)
        {
            Settings.setgamemenuopened(true);
            UserControl menu = new gamemenu();
            gamegrid.Children.Add(menu);
        }
        private void resize(object sender, SizeChangedEventArgs e)
        {
            Table.refreshvisuals();
        }
        public void closegame()
        {
            Settings.setgameopened(false);
            Items.mainwindowitem.play.Visibility = Visibility.Visible;
            Items.mainwindowitem.settings.Visibility = Visibility.Visible;
            Items.mainwindowitem.exit.Visibility = Visibility.Visible;
            Grid parent = (Grid)Parent;
            parent.Children.Remove(this);
        }
        public Player player = new Player("You");
        public Bot botone;
        public Bot bottwo;
        public Bot botthree;
        public void loaded(object sender, RoutedEventArgs e)
        {
            if(Settings.getlanguage() == 2)
            {
                toukrainian();
            }
            else
            {
                toenglish();
            }
            Items.gameitem = this;
            gamecanvas.Children.Clear();
            Table.turn = 1;
            CardHolder.allcards.Clear();
            CardHolder.allcards.Add(player);
            Random random = new Random();
            if(Settings.getrandomdirection() == 1)
            {
                Table.direction = random .Next(0, 2) == 0 ? true : false;
            }
            else
            {
                Table.direction = true;
            }
            if(Settings.getplayercount() == 2)
            {
                botone = new Bot(2, "1");
                Bot.allcards.Add(botone);
            }
            else if (Settings.getplayercount() == 3)
            {
                botone = new Bot(1, "1");
                Bot.allcards.Add(botone);
                bottwo = new Bot(3, "2");
                Bot.allcards.Add(bottwo);
            }
            else if (Settings.getplayercount() == 4)
            {
                botone = new Bot(1, "1");
                CardHolder.allcards.Add(botone);
                bottwo = new Bot(2, "2");
                Bot.allcards.Add(bottwo);
                botthree = new Bot(3, "3");
                Bot.allcards.Add(botthree);
            }
            Table.clear();
            Table.settopcard();
            if (Table.topcard.wildcard())
            {
                UserControl colorchange = new colorchange();
                gamegrid.Children.Add(colorchange);
            }
            player.setcards();
            botone.setcards();
            if (Settings.getplayercount() >= 3)
            {
                bottwo.setcards(); 
            }
            if (Settings.getplayercount() == 4)
            {
                botthree.setcards();
            }
            player.checkforuno();
            player.refreshuno();
        }
        public int actualgap = 10;
        private void mouseenter(object sender, MouseEventArgs e)
        {
            Items.mainwindowitem.buttonmouseenter(sender, e);
        }
        private void mouseleave(object sender, MouseEventArgs e)
        {
            Items.mainwindowitem.buttonmouseleave(sender, e);
        }
        UIElement drag = null;
        Point offset;
        public int cardzindex;
        public void mousedown(object sender, MouseButtonEventArgs e)
        {
            if(Table.turn != 1) { return; }
            drag = (System.Windows.Controls.Image)sender;
            cardzindex = Canvas.GetZIndex(drag);
            Canvas.SetZIndex(drag, 1);
            Table.draggedimage = (System.Windows.Controls.Image)drag;
            offset = e.GetPosition(gamecanvas);
            offset.Y = offset.Y - Canvas.GetTop(drag);
            offset.X = offset.X - Canvas.GetLeft(drag);
            gamecanvas.CaptureMouse();
        }
        public void mousemove(object sender, MouseEventArgs e)
        {
            if(drag == null){return;}
            var position = e.GetPosition((IInputElement)sender);
            Canvas.SetTop(drag, position.Y - offset.Y);
            Canvas.SetLeft(drag, position.X - offset.X);
        }
        public void mouseup(object sender, MouseButtonEventArgs e)
        {
            if(drag == null)
            {
                return;
            }
            drag = null;
            player.playcard();
            gamecanvas.ReleaseMouseCapture();
            Table.refreshvisuals();
        }
        public void deckdown(object sender, MouseButtonEventArgs e)
        {
            player.deckdown(sender, e);
        }
    }
}