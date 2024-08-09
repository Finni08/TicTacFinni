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
        public bool singelplayer;
        Random random = new Random();
        static Uri imageUriCross = new Uri("pack://application:,,,/TikTacFinni;component/Resources/Cross.png");
        static Uri imageUriCircle = new Uri("pack://application:,,,/TikTacFinni;component/Resources/Circle.png");
        BitmapImage Cross = new BitmapImage(imageUriCross);
        BitmapImage Circle = new BitmapImage(imageUriCircle);


        public wndGame()
        {
            InitializeComponent();
            //yourturn =random.Next(2) == 0;
            gfur.MouseDown += gfur_click;
            gfum.MouseDown += gfur_click;
            gful.MouseDown += gfur_click;
            gfmr.MouseDown += gfur_click;
            gfmm.MouseDown += gfur_click;
            gfml.MouseDown += gfur_click;
            gfor.MouseDown += gfur_click;
            gfom.MouseDown += gfur_click;
            gfol.MouseDown += gfur_click;
            GridMain.Children.Add(gfur);
            GridMain.Children.Add(gfum);
            GridMain.Children.Add(gful);
            GridMain.Children.Add(gfmr);
            GridMain.Children.Add(gfmm);
            GridMain.Children.Add(gfml);
            GridMain.Children.Add(gfor);
            GridMain.Children.Add(gfom);
            GridMain.Children.Add(gfol);
        }

        public void gfur_click(object sender, RoutedEventArgs e)
        {
            click(sender);
        }
       Gamefield gfur = new Gamefield() { Width = 200, Height = 200, Margin =new Thickness( 400,484,0,0), Background = new SolidColorBrush(Color.FromRgb (46, 43, 46))};
        Gamefield gfum = new Gamefield() { Width = 200, Height = 200, Margin = new Thickness(200, 484, 199, 0), Background = new SolidColorBrush(Color.FromRgb(64, 62, 65)) };
        Gamefield gful = new Gamefield() { Width = 200, Height = 200, Margin = new Thickness(0, 484, 399, 0), Background = new SolidColorBrush(Color.FromRgb(46, 43, 46)) };
        Gamefield gfmr = new Gamefield() { Width = 200, Height = 200, Margin = new Thickness(400, 284, 0, 200), Background = new SolidColorBrush(Color.FromRgb(64, 62, 65)) };
        Gamefield gfmm = new Gamefield() { Width = 200, Height = 200, Margin = new Thickness(200, 284, 199, 200), Background = new SolidColorBrush(Color.FromRgb(46, 43, 46)) };
        Gamefield gfml = new Gamefield() { Width = 200, Height = 200, Margin = new Thickness(0, 284, 399, 200), Background = new SolidColorBrush(Color.FromRgb(64, 62, 65)) };
        Gamefield gfor = new Gamefield() { Width = 200, Height = 200, Margin = new Thickness(400, 84, 0, 400), Background = new SolidColorBrush(Color.FromRgb(46, 43, 46)) };
        Gamefield gfom = new Gamefield() { Width = 200, Height = 200, Margin = new Thickness(200, 84, 199, 400), Background = new SolidColorBrush(Color.FromRgb(64, 62, 65)) };
        Gamefield gfol = new Gamefield() { Width = 200, Height = 200, Margin = new Thickness(0, 84, 399, 400), Background = new SolidColorBrush(Color.FromRgb(46, 43, 46)) };
        public void click(object sender)
        {
            // Überprüfen, ob es der Zug des Spielers ist
            if (yourturn)
            {
                // Überprüfen, ob das Element, das geklickt wurde, ein Canvas ist
                if (sender is Gamefield gfur)
                {
                    if(gfur.free ==true) 
                    {
                        // Ein neues Image-Element erstellen
                        Image newImage = new Image
                        {
                            Source = Cross, // Setzen der Bildquelle auf das Kreuz-Bild
                            Width = 200, // Setzen der Breite (optional)
                            Height = 200 // Setzen der Höhe (optional)
                        };

                        // Platzieren des Image im Canvas (z.B. zentriert)
                        Canvas.SetLeft(newImage, (gfur.ActualWidth - newImage.Width) / 2);
                        Canvas.SetTop(newImage, (gfur.ActualHeight - newImage.Height) / 2);

                        // Hinzufügen des Image zum Canvas
                        gfur.Children.Add(newImage);

                        // Zug des Gegners
                        yourturn = false;
                        gfur.free = false;
                    }
                    
                }
            }
            else
            {
                // Überprüfen, ob das Element, das geklickt wurde, ein Canvas ist
                if (sender is Gamefield gfur)
                {
                    if (gfur.free == true)
                    {
                        // Ein neues Image-Element erstellen
                        Image newImage = new Image
                        {
                            Source = Circle, // Setzen der Bildquelle auf das Kreuz-Bild
                            Width = 200, // Setzen der Breite (optional)
                            Height = 200 // Setzen der Höhe (optional)
                        };

                        // Platzieren des Image im Canvas (z.B. zentriert)
                        Canvas.SetLeft(newImage, (gfur.ActualWidth - newImage.Width) / 2);
                        Canvas.SetTop(newImage, (gfur.ActualHeight - newImage.Height) / 2);

                        // Hinzufügen des Image zum Canvas
                        gfur.Children.Add(newImage);

                        // turn of player 1
                        yourturn = true;
                        gfur.free = false;
                    }
                        
                }
            }
        }
    }
}
