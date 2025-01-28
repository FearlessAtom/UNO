using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UNOui
{
    public partial class Game : UserControl
    {
        public Game()
        {
            InitializeComponent();
        }

        private void MenuButtonMouseEnter(object sender, MouseEventArgs e)
        {
            Items.MainWindowItem.ButtonMouseEnter(sender, e);
        }

        private void MenuButtonMouseLeave(object sender, MouseEventArgs e)
        {
            Items.MainWindowItem.ButtonMouseLeave(sender, e);
        }

        public void ToEnglish()
        {
            MenuButton.Content = "Menu";
        }

        public void ToUkrainian()
        {
            MenuButton.Content = "Меню";
        }

        public void OpenMenuButton(object sender, RoutedEventArgs e)
        {
            Settings.GameMenuOpened = true;
            UserControl menu = new GameMenu();
            gamegrid.Children.Add(menu);
        }

        private void ResizeWindow(object sender, SizeChangedEventArgs e)
        {
            Table.RefreshVisuals();
        }

        public void CloseGameButton()
        {
            Settings.GameOpened = false;
            Items.MainWindowItem.play.Visibility = Visibility.Visible;
            Items.MainWindowItem.settings.Visibility = Visibility.Visible;
            Items.MainWindowItem.exit.Visibility = Visibility.Visible;
            Grid parent = (Grid)Parent;
            parent.Children.Remove(this);
        }

        public Player player = new Player("You");
        public Bot botone;
        public Bot bottwo;
        public Bot botthree;

        public void LoadGame(object sender, RoutedEventArgs e)
        {
            if(Settings.Language == 2)
            {
                ToUkrainian();
            }
            else
            {
                ToEnglish();
            }
            Items.GameItem = this;
            gamecanvas.Children.Clear();
            Table.turn = 1;
            CardHolder.AllCards.Clear();
            CardHolder.AllCards.Add(player);
            Random random = new Random();
            if(Settings.RandomDirection == 1)
            {
                Table.direction = random .Next(0, 2) == 0 ? true : false;
            }
            else
            {
                Table.direction = true;
            }
            if(Settings.PlayerCount == 2)
            {
                botone = new Bot(2, "1");
                Bot.AllCards.Add(botone);
            }
            else if (Settings.PlayerCount == 3)
            {
                botone = new Bot(1, "1");
                Bot.AllCards.Add(botone);
                bottwo = new Bot(3, "2");
                Bot.AllCards.Add(bottwo);
            }
            else if (Settings.PlayerCount == 4)
            {
                botone = new Bot(1, "1");
                CardHolder.AllCards.Add(botone);
                bottwo = new Bot(2, "2");
                Bot.AllCards.Add(bottwo);
                botthree = new Bot(3, "3");
                Bot.AllCards.Add(botthree);
            }
            Table.TopCardsClear();
            Table.SetRandomTopCard();
            if (Table.topcard.IsWildCard())
            {
                UserControl colorchange = new ColorChange();
                gamegrid.Children.Add(colorchange);
            }
            player.SetCards();
            botone.SetCards();
            if (Settings.PlayerCount >= 3)
            {
                bottwo.SetCards(); 
            }
            if (Settings.PlayerCount == 4)
            {
                botthree.SetCards();
            }
            player.CheckForUno();
            player.RefreshUNO();
        }
        
        public int ActualGap = 10;
        UIElement Drag = null;
        Point OffSet;
        public int CardZIndex;

        public void CardMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(Table.turn != 1) { return; }
            Drag = (System.Windows.Controls.Image)sender;
            CardZIndex = Canvas.GetZIndex(Drag);
            Canvas.SetZIndex(Drag, 1);
            Table.draggedimage = (System.Windows.Controls.Image)Drag;
            OffSet = e.GetPosition(gamecanvas);
            OffSet.Y = OffSet.Y - Canvas.GetTop(Drag);
            OffSet.X = OffSet.X - Canvas.GetLeft(Drag);
            gamecanvas.CaptureMouse();
        }

        public void CardMouseMove(object sender, MouseEventArgs e)
        {
            if(Drag == null){return;}
            var position = e.GetPosition((IInputElement)sender);
            Canvas.SetTop(Drag, position.Y - OffSet.Y);
            Canvas.SetLeft(Drag, position.X - OffSet.X);
        }

        public void CardMouseUp(object sender, MouseButtonEventArgs e)
        {
            if(Drag == null)
            {
                return;
            }
            Drag = null;
            player.PlayCard();
            gamecanvas.ReleaseMouseCapture();
            Table.RefreshVisuals();
        }

        public void DeckDown(object sender, MouseButtonEventArgs e)
        {
            player.DeckDown(sender, e);
        }
    }
}