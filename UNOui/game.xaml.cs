using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        private void loaded(object sender, RoutedEventArgs e)
        {
            Items.gameitem = this;
            for(int index = 0; index < Settings.getcardcount(); index++)
            {
                addcard();
            }
        }
        
        public void closegame()
        {
            Settings.setgameopened(false);
            Items.mainwindowitem.play.Visibility = Visibility.Visible;
            Items.mainwindowitem.settings.Visibility = Visibility.Visible;
            Items.mainwindowitem.exit.Visibility = Visibility.Visible;
            gamecanvas.Children.Clear();
            CardsList.playercards.Clear();
            Grid parent = (Grid)Parent;
            parent.Children.Remove(this);
        }
        public void menu(object sender, RoutedEventArgs e)
        {
            Settings.setgamemenuopened(true);
            UserControl menu = new gamemenu();
            gamegrid.Children.Add(menu);
        }
        private void mouseenter(object sender, MouseEventArgs e)
        {
            Items.mainwindowitem.buttonmouseenter(sender, e);
        }
        private void mouseleave(object sender, MouseEventArgs e)
        {
            Items.mainwindowitem.buttonmouseleave(sender, e);
        }
        public System.Windows.Controls.Image getcardimage(string card)
        {
            System.Windows.Controls.Image image = new System.Windows.Controls.Image();
            string source = "C:\\Users\\357\\Desktop\\UNO\\UNOui\\assets\\UNOcards\\" + card + ".png";
            image.Source = new BitmapImage(new Uri(source));
            return image;
        }
        UIElement drag = null;
        bool dragging = false;
        Point offset;
        private void mousedown(object sender, MouseButtonEventArgs e)
        {
            drag = (System.Windows.Controls.Image)sender;
            CardsList.draggedimage = (System.Windows.Controls.Image)drag;
            offset = e.GetPosition(gamecanvas);
            offset.Y = offset.Y - Canvas.GetTop(drag);
            offset.X = offset.X - Canvas.GetLeft(drag);
            gamecanvas.CaptureMouse();
            dragging = true;
        }
        private void mousemove(object sender, MouseEventArgs e)
        {
            if(drag == null){return;}
            var position = e.GetPosition((IInputElement)sender);
            Canvas.SetTop(drag, position.Y - offset.Y);
            Canvas.SetLeft(drag, position.X - offset.X);
        }
        private void mouseup(object sender, MouseButtonEventArgs e)
        {
            gamecanvas.Children.Remove(CardsList.draggedimage);
            CardsList.playercards.Remove(CardsList.imagetocard(CardsList.draggedimage));
            drag = null;
            gamecanvas.ReleaseMouseCapture();
            dragging = false;
            one();

        }
        private void cardenter(object sender, MouseEventArgs e)
        {
            if (!dragging)
            {
                System.Windows.Controls.Image image = (System.Windows.Controls.Image)sender;
                Canvas.SetTop(image, (int)gamecanvas.ActualHeight - image.Width - 30);
            }
        }
        private void cardleave(object sender, MouseEventArgs e)
        {
            if (!dragging)
            {
                System.Windows.Controls.Image image = (System.Windows.Controls.Image)sender;
                Canvas.SetTop(image, (int)gamecanvas.ActualHeight - image.Width);
            }
        }
        private void resize(object sender, SizeChangedEventArgs e)
        {
            one();
        }
        public void removecard(Cards card)
        {
            gamecanvas.Children.Remove(card.image);
            CardsList.playercards.Remove(card);
            one();
        }
        public void one()
        {
            if (CardsList.playercards.Count != 0)
            {
                int gap = ((CardsList.playercards.Count * 150 - CardsList.playercards.Count * 100) / 2) - 30;
                gamecanvas.Children.Clear();
                if (CardsList.playercards.Count != 0)
                {
                    for (int index = 0; index < CardsList.playercards.Count; index++)
                    {
                        int currentindex = index;
                        System.Windows.Controls.Image image = CardsList.playercards[index].image;
                        image.Width = 150;
                        image.Height = 150;
                        Canvas.SetTop(image, (int)gamecanvas.ActualHeight - image.Width);
                        Canvas.SetLeft(image, (int)gamecanvas.ActualWidth / 2 - (image.Width / 2) + index * 50 - gap);
                        gamecanvas.Children.Add(image);
                    }
                }
            }
        }
        public void addcard()
        {
            Cards card = new Cards();
            card.listcount = CardsList.playercards.Count + 1;
            card.image.MouseEnter += cardenter;
            card.image.MouseLeave += cardleave;
            card.image.PreviewMouseDown += mousedown;
            gamecanvas.PreviewMouseMove += mousemove;
            gamecanvas.PreviewMouseUp += mouseup;
            card.image.MouseDown += mousedown;
            CardsList.playercards.Add(card);
            one();
        }
    }
}
