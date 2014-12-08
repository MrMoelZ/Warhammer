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
using Warhammer.Helpers;
using Warhammer.Models;
using Warhammer.TheGame;

namespace Warhammer
{
    public partial class MainWindow : Window
    {
        Game game;
        public MainWindow()
        {
            InitializeComponent();
            game = new Game(botcanvas, maincanvas, StatusBar, InfoBarPart1, InfoBarPart2, InfoBarPart3);
            game.StartUp();
        }

        private void NextPhase_Click(object sender, RoutedEventArgs e)
        {
            game.NextPhase();
        }

        private void ExpandMenu(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            b.Height = b.Height == 300 ? 100 : 300;
            //bool expanded;
            //StackPanel sp = new StackPanel();
            //sp.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            //sp.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            //sp.Orientation = Orientation.Vertical;
            //Sidebar.Children.Add(sp);

            


            

            //Button menu_button_NewGame = new Button();
            //menu_button_NewGame.Content = "New Game";
            //menu_button_NewGame.Height = 50;
            //menu_button_NewGame.Width = 50;
            //    sp.Children.Add(menu_button_NewGame);
            //Button menu_button_Save = new Button();
            //menu_button_Save.Height = 50;
            //menu_button_Save.Width = 50;
            //menu_button_Save.Content = "Save";
            //    sp.Children.Add(menu_button_Save);
            //Button menu_button_Load = new Button();
            //Button menu_button_Options = new Button();
            //Button menu_button_Exit = new Button();
        }
        
    }
}
