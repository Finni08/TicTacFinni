using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TikTacFinni
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        wndGame wndGame;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Singelplayerbtn_Click(object sender, RoutedEventArgs e)
        {
            wndGame = new wndGame(this) { singelplayer = true };
            wndGame.Show();
            Hide();
            wndGame.btnswapgamemode.Content = "Change to Multiplayer";
        }

        private void Multiplayerbtn_Click(object sender, RoutedEventArgs e)
        {
            wndGame = new wndGame(this) { singelplayer = false };
            wndGame.Show();
            Hide();
            wndGame.btnswapgamemode.Content = "Change to Singleplayer";
        }
    }
}