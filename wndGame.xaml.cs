using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
        MainWindow mainWindow;
        public bool yourturn = true;
        public bool free;
        public bool singelplayer;
        Random random = new Random();
        static Uri imageUriCross = new Uri("pack://application:,,,/TikTacFinni;component/Resources/Cross.png");
        static Uri imageUriCircle = new Uri("pack://application:,,,/TikTacFinni;component/Resources/Circle.png");
        BitmapImage Cross = new BitmapImage(imageUriCross);
        BitmapImage Circle = new BitmapImage(imageUriCircle);
        Gamefield gfur = new Gamefield() { Width = 200, Height = 200, Margin = new Thickness(400, 484, 0, 0), Background = new SolidColorBrush(Color.FromRgb(46, 43, 46)) };
        Gamefield gfum = new Gamefield() { Width = 200, Height = 200, Margin = new Thickness(200, 484, 199, 0), Background = new SolidColorBrush(Color.FromRgb(64, 62, 65)) };
        Gamefield gful = new Gamefield() { Width = 200, Height = 200, Margin = new Thickness(0, 484, 399, 0), Background = new SolidColorBrush(Color.FromRgb(46, 43, 46)) };
        Gamefield gfmr = new Gamefield() { Width = 200, Height = 200, Margin = new Thickness(400, 284, 0, 200), Background = new SolidColorBrush(Color.FromRgb(64, 62, 65)) };
        Gamefield gfmm = new Gamefield() { Width = 200, Height = 200, Margin = new Thickness(200, 284, 199, 200), Background = new SolidColorBrush(Color.FromRgb(46, 43, 46)) };
        Gamefield gfml = new Gamefield() { Width = 200, Height = 200, Margin = new Thickness(0, 284, 399, 200), Background = new SolidColorBrush(Color.FromRgb(64, 62, 65)) };
        Gamefield gfor = new Gamefield() { Width = 200, Height = 200, Margin = new Thickness(400, 84, 0, 400), Background = new SolidColorBrush(Color.FromRgb(46, 43, 46)) };
        Gamefield gfom = new Gamefield() { Width = 200, Height = 200, Margin = new Thickness(200, 84, 199, 400), Background = new SolidColorBrush(Color.FromRgb(64, 62, 65)) };
        Gamefield gfol = new Gamefield() { Width = 200, Height = 200, Margin = new Thickness(0, 84, 399, 400), Background = new SolidColorBrush(Color.FromRgb(46, 43, 46)) };

        public wndGame(MainWindow mainWindow)
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
            this.mainWindow = mainWindow;
            
        }

        public void gfur_click(object sender, RoutedEventArgs e)
        {
            click(sender);
            winnotification();
        }

        public void click(object sender)
        {
            // Überprüfen, ob es der Zug des Spielers ist
            if (yourturn)
            {
                // Überprüfen, ob das Element, das geklickt wurde, ein Canvas ist
                if (sender is Gamefield gfur)
                {
                    if(gfur.free) 
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
                        gfur.isred = true;
                    }
                    
                }
            }
            else
            {
                // Überprüfen, ob das Element, das geklickt wurde, ein Canvas ist
                if (sender is Gamefield gfur)
                {
                    if (gfur.free)
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
                        gfur.isblue = true;
                    }
                        
                }
            }
        }
        public void winnotification()
        {
            if (winred())
            {
                MessageBox.Show("Rot (Kreuz) hat gewonnen");
                gamereset();
            }
            else if (winblue()) 
            {
                MessageBox.Show("Blau (Kreis) hat gewonnen");
                gamereset();
            }
            else if (stalemate())
            {
                MessageBox.Show("Niemand hat gewonnen");
                gamereset();
            }
        }
        public bool winred()
        {
            //check horizontal
            if (gfur.isred && gfum.isred && gful.isred)
            {
                return true;
            }
            else if(gfmr.isred && gfmm.isred && gfml.isred)
            {
                return true;
            }
            else if(gfor.isred && gfom.isred && gfol.isred)
            {
                return true;
            }
            //check vertical
            else if (gfur.isred && gfmr.isred && gfor.isred)
            {
                return true;
            }
            else if (gfum.isred && gfmm.isred && gfom.isred)
            {
                return true;
            }
            else if (gful.isred && gfml.isred && gfol.isred)
            {
                return true;
            }
            //check diagonal
            else if (gfur.isred && gfmm.isred && gfol.isred)
            {
                return true;
            }
            else if (gful.isred && gfom.isred && gfor.isred)
            {
                return true;
            }
            return false;
        }
        public bool winblue()
        {
            //check horizontal
            if (gfur.isblue && gfum.isblue && gful.isblue)
            {
                return true;
            }
            else if (gfmr.isblue && gfmm.isblue && gfml.isblue)
            {
                return true;
            }
            else if (gfor.isblue && gfom.isblue && gfol.isblue)
            {
                return true;
            }
            //check vertical
            else if (gfur.isblue && gfmr.isblue && gfor.isblue)
            {
                return true;
            }
            else if (gfum.isblue && gfmm.isblue && gfom.isblue)
            {
                return true;
            }
            else if (gful.isblue && gfml.isblue && gfol.isblue)
            {
                return true;
            }
            //check diagonal
            else if (gfur.isblue && gfmm.isblue && gfol.isblue)
            {
                return true;
            }
            else if (gful.isblue && gfom.isblue && gfor.isblue)
            {
                return true;
            }
            return false;
        }
        public bool stalemate()
        {
            if (!gfur.free && !gfum.free && !gful.free && !gfmr.free && !gfmm.free && !gfml.free && !gfor.free && !gfom.free && !gfol.free)
            {
                return true;
            }
            return false;
        }
        public void gamereset()
        {
            gfur.Children.Clear();
            gfum.Children.Clear();
            gful.Children.Clear();
            gfmr.Children.Clear();
            gfmm.Children.Clear();
            gfml.Children.Clear();
            gfor.Children.Clear();
            gfom.Children.Clear();
            gfol.Children.Clear();
            gfur.isblue = false;
            gfum.isblue = false;
            gful.isblue = false;
            gfmr.isblue = false;
            gfmm.isblue = false;
            gfml.isblue = false;
            gfor.isblue = false;
            gfom.isblue = false;
            gfol.isblue = false;
            gfur.isred = false;
            gfum.isred = false;
            gful.isred = false;
            gfmr.isred = false;
            gfmm.isred = false;
            gfml.isred = false;
            gfor.isred = false;
            gfom.isred = false;
            gfol.isred = false; 
            gfur.free = true;
            gfum.free = true;
            gful.free = true;
            gfmr.free = true;
            gfmm.free = true;
            gfml.free = true;
            gfor.free = true;
            gfom.free = true;
            gfol.free = true;
            yourturn = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Close();
        }

        private void btnswapgamemode_Click(object sender, RoutedEventArgs e)
        {
            if (singelplayer)
            {
                singelplayer = false;
                btnswapgamemode.Content = "Change to Singleplayer";
            }
            else
            {
                singelplayer = true;
                btnswapgamemode.Content = "Change to Multiplayer";
            }
        }
    }
}
