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
        public Bot player;
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
            drawing = false;
            Table.turn = 1;

            Bot.allcards.Clear();
            
            player = new Bot(1, "You");
            Bot.allcards.Add(player);
            Table.direction = true;
            //Table.direction = Items.mainwindowitem.randominteger(1, 2) == 1 ? true : false;

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
                Bot.allcards.Add(botone);
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
            botone.cards[0].number = -4;
            botone.cards[0].color = "none";
            botone.cards[0].image = Cards.getcardimage("drawfour");
        }
        public void settings()
        {
            if (botone.cards.Count == 0)
            {
                return;
            }
            botone.removecard(botone.cards[0]);
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
        Canvas.SetZIndex(Table.draggedimage, cardzindex);
        double bottom = Canvas.GetTop(Table.draggedimage);
        if (bottom < gamecanvas.ActualHeight * (Table.draggedimage.Height / (gamecanvas.ActualHeight / 2)) && Cards.imagetocard(Table.draggedimage).comparecard())
        {
            Cards draggedcard = Cards.imagetocard(Table.draggedimage);
            Random random = new Random();
            int randomnumber = random.Next(-45, 45);
            if (player.cards.Count > 1)
            {
                Table.addtotopcards(randomnumber);
            }
            draggedcard.playcard();
            Table.topcard.image.RenderTransform = Cards.rotate(randomnumber);
            Table.topcard.zindex();
            player.cards.Remove(draggedcard);
            gamecanvas.Children.Remove(draggedcard.image);
            Canvas.SetZIndex(Table.draggedimage, cardzindex);
            if(Table.topcard.number != -1)
            {
                Table.nextturn();
            }
            Task.Delay(TimeSpan.FromSeconds(0.5)).ContinueWith(task =>
            {
                if ((draggedcard.number == -4 || draggedcard.number == -5) && draggedcard.color == "none")
                {
                    UserControl changecolor = new colorchange();
                    gamegrid.Children.Add(changecolor);
                }
                else
                {
                    Table.topcard.checkforwildcard();
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        drag = null;
        gamecanvas.ReleaseMouseCapture();
        Table.refreshvisuals();
        }
        public bool drawing = false;
        public void deckdown(object sender, MouseButtonEventArgs e)
        {
            if((e != null && drawing) || Table.turn != 1) { return; }
            drawing = true;
            player.addcard();
            Cards card = player.cards[player.cards.Count - 1];
            if(card.comparecard())
            {
                drawing = false;
                UserControl draworplay = new draworplay();
                gamegrid.Children.Add(draworplay);
            }
            else
            {
                if (Settings.getdrawuntilplayable() == 1 && !card.comparecard())
                {
                Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith(task =>
                    {
                        deckdown(new object(), null);
                    }, TaskScheduler.FromCurrentSynchronizationContext());
                }
                else
                {
                    drawing = false;
                    Table.nextturn();
                    Table.checkforturn();
                }
            }
        }
    }
}