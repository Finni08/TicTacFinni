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
using System.Windows.Shapes;

namespace TikTacFinni
{
    /// <summary>
    /// Interaktionslogik für wndGame.xaml
    /// </summary>
    public partial class wndGame : Window
    {
        public bool yourturn = true;
        public bool free;
        Random random = new Random();
        static Uri imageUriCross = new Uri("pack://application:,,,/TikTacFinni;component/Resources/Cross.png");
        BitmapImage Cross = new BitmapImage(imageUriCross);


        public wndGame()
        {
            InitializeComponent();
            //yourturn =random.Next(2) == 0;
        }

        public void cvsur_click(object sender, RoutedEventArgs e)
        {
            // Überprüfen, ob es der Zug des Spielers ist
            if (yourturn)
            {
                // Überprüfen, ob das Element, das geklickt wurde, ein Canvas ist
                if (sender is Canvas cvsur)
                {
                    // Ein neues Image-Element erstellen
                    Image newImage = new Image
                    {
                        Source = Cross, // Setzen der Bildquelle auf das Kreuz-Bild
                        Width = 200, // Setzen der Breite (optional)
                        Height = 200 // Setzen der Höhe (optional)
                    };

                    // Platzieren des Image im Canvas (z.B. zentriert)
                    Canvas.SetLeft(newImage, (cvsur.ActualWidth - newImage.Width) / 2);
                    Canvas.SetTop(newImage, (cvsur.ActualHeight - newImage.Height) / 2);

                    // Hinzufügen des Image zum Canvas
                    cvsur.Children.Add(newImage);

                    // Zug des Gegners
                    yourturn = false;
                }
            }
        }
    }
}
