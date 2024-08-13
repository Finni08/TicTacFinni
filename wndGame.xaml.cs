using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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
            yourturn =random.Next(2) == 0;
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
        public void placeCross(Gamefield gfur)
        {
            if (gfur.free)
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
        public void placeCircle(Gamefield gfur)
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

        public void click(object sender)
        {
            // Überprüfen, ob es der Zug des Spielers ist
            if (yourturn)
            {
                // Überprüfen, ob das Element, das geklickt wurde, ein Canvas ist
                if (sender is Gamefield gfur)
                {
                    placeCross(gfur);
                }
            }
            else
            {
                if (!singelplayer)
                {
                    // Überprüfen, ob das Element, das geklickt wurde, ein Canvas ist
                    if (sender is Gamefield gfur)
                    {
                        placeCircle(gfur);
                    }
                }
                else
                {
                    aimove();
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
            else if (gfmr.isred && gfmm.isred && gfml.isred)
            {
                return true;
            }
            else if (gfor.isred && gfom.isred && gfol.isred)
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
                gamereset();
            }
            else
            {
                singelplayer = true;
                btnswapgamemode.Content = "Change to Multiplayer";
                gamereset();
            }
        }
        //kys your fucking nigger
        public void aimove()
        {
            if (singelplayer && !yourturn)
            {
                if (gfmm.free)
                {
                    placeCircle(gfmm);
                }
                else if (gfor.free && !aidefendingreqired())
                {
                    placeCircle(gfor);
                }
                else if (aiatackingreqired())
                {
                    aiattacking();
                }
                else if (aidefendingreqired())
                {
                    aidefending();
                }
                else
                {
                    placeanywhere();
                }
            }
        }
        public void aidefending()
        {
            //check horizontal
            if ((gfum.isred && (gfur.isred || gful.isred ) && (gfur.free || gful.free)) || gfur.isred && gful.isred && gfum.free)
            {
                if (gfur.free)
                {
                    placeCircle(gfur);
                }
                else if (gful.free)
                {
                    placeCircle(gful);
                }
                else if (gfum.free)
                {
                    placeCircle(gfum);
                }
            }
            else if ((gfmm.isred && (gfmr.isred || gfml.isred) && (gfmr.free || gfml.free)) || gfmr.isred && gfml.isred && gfmm.free)
            {
                if (gfmr.free)
                {
                    placeCircle(gfmr);
                }
                else if (gfml.free)
                {
                    placeCircle(gfml);
                }
                else if (gfmm.free)
                {
                    placeCircle(gfmm);
                }
            }
            else if ((gfom.isred && (gfor.isred || gfol.isred) && (gfor.free || gfol.free)) || gfor.isred && gfol.isred && gfom.free)
            {
                if (gfor.free)
                {
                    placeCircle(gfor);
                }
                else if (gfol.free)
                {
                    placeCircle(gfol);
                }
                else if (gfom.free)
                {
                    placeCircle(gfom);
                }
            }
            //check vertical
            else if ((gfmr.isred && (gfur.isred || gfor.isred) &&(gfor.free || gfur.free)) || gfor.isred && gfur.isred && gfmr.free)
            {
                if (gfor.free)
                {
                    placeCircle(gfor);
                }
                else if (gfur.free)
                {
                    placeCircle(gfur);
                }
                else if (gfmr.free)
                {
                    placeCircle(gfmr);
                }
            }
            else if ((gfmm.isred && (gfum.isred || gfom.isred) &&(gfum.free || gfom.free)) || gfum.isred && gfom.isred && gfmm.free)
            {
                if (gfum.free)
                {
                    placeCircle(gfum);
                }
                else if (gfom.free)
                {
                    placeCircle(gfom);
                }
                else if (gfmm.free)
                {
                    placeCircle(gfmm);
                }
            }
            else if ((gfml.isred && (gful.isred || gfol.isred) &&(gful.free || gfol.free)) || gful.isred && gfol.isred && gfml.free)
            {
                if (gful.free)
                {
                    placeCircle(gful);
                }
                else if (gfol.free)
                {
                    placeCircle(gfol);
                }
                else if (gfml.free)
                {
                placeCircle(gfml); 
                }
            }
            //check diagonal 
            else if ((gfmm.isred && (gfur.isred || gfol.isred) && (gfur.free || gfol.free)) || gfur.isred && gfol.isred && gfmm.free)
            {
                if (gfur.free)
                {
                    placeCircle(gfur);
                }
                else if (gfol.free)
                {
                    placeCircle(gfol);
                }
                else if (gfmm.free)
                {
                    placeCircle(gfmm);
                }
            }
            else if ((gfmm.isred && (gful.isred || gfor.isred) && (gful.free || gfor.free))|| gful.isred && gfor.isred && gfmm.free)
            {
                if (gful.free)
                {
                    placeCircle(gful);
                }
                else if (gfor.free)
                {
                    placeCircle(gfor);
                }
                else if (gfmm.isred)
                {
                    placeCircle(gfmm);
                }
            }
        }
        public void aiattacking()
        {
            //check horizontal
            if ((gfum.isblue && (gfur.isblue || gful.isblue) && (gfur.free || gful.free)) || gfur.isblue && gful.isblue && gfum.free)
            {
                if (gfur.free)
                {
                    placeCircle(gfur);
                }
                else if (gful.free)
                {
                    placeCircle(gful);
                }
                else if (gfum.free)
                {
                    placeCircle(gfum);
                }
            }
            else if ((gfmm.isblue && (gfmr.isblue || gfml.isblue) && (gfmr.free || gfml.free)) || gfmr.isblue && gfml.isblue && gfmm.free)
            {
                if (gfmr.free)
                {
                    placeCircle(gfmr);
                }
                else if (gfml.free)
                {
                    placeCircle(gfml);
                }
                else if (gfmm.free)
                {
                    placeCircle(gfmm);
                }
            }
            else if ((gfom.isblue && (gfor.isblue || gfol.isblue) && (gfor.free || gfol.free)) || gfor.isblue && gfol.isblue && gfom.free)
            {
                if (gfor.free)
                {
                    placeCircle(gfor);
                }
                else if (gfol.free)
                {
                    placeCircle(gfol);
                }
                else if (gfom.free)
                {
                    placeCircle(gfom);
                }
            }
            //check vertical
            else if ((gfmr.isblue && (gfur.isblue || gfor.isblue) && (gfor.free || gfur.free)) || gfor.isblue && gfur.isblue && gfmr.free)
            {
                if (gfor.free)
                {
                    placeCircle(gfor);
                }
                else if (gfur.free)
                {
                    placeCircle(gfur);
                }
                else if (gfmr.free)
                {
                    placeCircle(gfmr);
                }
            }
            else if ((gfmm.isblue && (gfum.isblue || gfom.isblue) && (gfum.free || gfom.free)) || gfum.isblue && gfom.isblue && gfmm.free)
            {
                if (gfum.free)
                {
                    placeCircle(gfum);
                }
                else if (gfom.free)
                {
                    placeCircle(gfom);
                }
                else if (gfmm.free)
                {
                    placeCircle(gfmm);
                }
            }
            else if ((gfml.isblue && (gful.isblue || gfol.isblue) && (gful.free || gfol.free)) || gful.isblue && gfol.isblue && gfml.free)
            {
                if (gful.free)
                {
                    placeCircle(gful);
                }
                else if (gfol.free)
                {
                    placeCircle(gfol);
                }
                else if (gfml.free)
                {
                    placeCircle(gfml);
                }
            }
            //check diagonal
            else if ((gfmm.isblue && (gfur.isblue || gfol.isblue) && (gfur.free || gfol.free)) || gfur.isblue && gfol.isblue && gfmm.free)
            {
                if (gfur.free)
                {
                    placeCircle(gfur);
                }
                else if (gfol.free)
                {
                    placeCircle(gfol);
                }
                else if (gfmm.free)
                {
                    placeCircle(gfmm);
                }
            }
            else if ((gfmm.isblue && (gful.isblue || gfor.isblue) && (gful.free || gfor.free)) || gful.isblue && gfor.isblue && gfmm.free)
            {
                if (gful.free)
                {
                    placeCircle(gful);
                }
                else if (gfor.free)
                {
                    placeCircle(gfor);
                }
                else if (gfmm.free)
                {
                    placeCircle(gfmm);
                }
            }
        }
        public bool aidefendingreqired()
        {
            //check horizontal
            if ((gfum.isred && (gfur.isred || gful.isred) && (gfur.free || gful.free)) || gfur.isred && gful.isred && gfum.free)
            {
                return true;
            }
            else if ((gfmm.isred && (gfmr.isred || gfml.isred) && (gfmr.free || gfml.free)) || gfmr.isred && gfml.isred && gfmm.free)
            {
                return true;
            }
            else if ((gfom.isred && (gfor.isred || gfol.isred) && (gfor.free || gfol.free)) || gfor.isred && gfol.isred && gfom.free)
            {
                return true;
            }
            //check vertical
            else if ((gfmr.isred && (gfur.isred || gfor.isred) && (gfor.free || gfur.free)) || gfor.isred && gfur.isred && gfmr.free)
            {
                return true;
            }
            else if ((gfmm.isred && (gfum.isred || gfom.isred) && (gfum.free || gfom.free)) || gfum.isred && gfom.isred && gfmm.free)
            {
                return true;
            }
            else if ((gfml.isred && (gful.isred || gfol.isred) && (gful.free || gfol.free)) || gful.isred && gfol.isred && gfml.free)
            {
                return true;
            }
            //check diagonal 
            else if ((gfmm.isred && (gfur.isred || gfol.isred) && (gfur.free || gfol.free)) || gfur.isred && gfol.isred && gfmm.free)
            {
                return true;
            }
            else if ((gfmm.isred && (gful.isred || gfor.isred) && (gful.free || gfor.free)) || gful.isred && gfor.isred && gfmm.free)
            {
                return true;
            }
            return false;
        }
        public bool aiatackingreqired()
        {
            //check horizontal
            if ((gfum.isblue && (gfur.isblue || gful.isblue) && (gfur.free || gful.free)) || gfur.isblue && gful.isblue && gfum.free)
            {
                return true;
            }
            else if ((gfmm.isblue && (gfmr.isblue || gfml.isblue) && (gfmr.free || gfml.free)) || gfmr.isblue && gfml.isblue && gfmm.free)
            {
                return true;
            }
            else if ((gfom.isblue && (gfor.isblue || gfol.isblue) && (gfor.free || gfol.free)) || gfor.isblue && gfol.isblue && gfom.free)
            {
                return true;
            }
            //check vertical
            else if ((gfmr.isblue && (gfur.isblue || gfor.isblue) && (gfor.free || gfur.free)) || gfor.isblue && gfur.isblue && gfmr.free)
            {
                return true;
            }
            else if ((gfmm.isblue && (gfum.isblue || gfom.isblue) && (gfum.free || gfom.free)) || gfum.isblue && gfom.isblue && gfmm.free)
            {
                return true;
            }
            else if ((gfml.isblue && (gful.isblue || gfol.isblue) && (gful.free || gfol.free)) || gful.isblue && gfol.isblue && gfml.free)
            {
                return true;
            }
            //check diagonal
            else if ((gfmm.isblue && (gfur.isblue || gfol.isblue) && (gfur.free || gfol.free)) || gfur.isblue && gfol.isblue && gfmm.free)
            {
                return true;
            }
            else if ((gfmm.isblue && (gful.isblue || gfor.isblue) && (gful.free || gfor.free)) || gful.isblue && gfor.isblue && gfmm.free)
            {
                return true;
            }
            return false;
        }
        public void placeanywhere()
        {
            if (gfol.free)
            {
                placeCircle(gfol);
            }
            else if (gfom.free)
            {
                placeCircle(gfom);
            }
            else if (gfml.free)
            {
                placeCircle(gfml);
            }
            else if (gfmr.free)
            {
                placeCircle(gfmr);
            }
            else if (gful.free)
            {
                placeCircle(gful);
            }
            else if (gfum.free)
            {
                placeCircle(gfum);
            }
            else if (gfur.free)
            {
                placeCircle(gfur);
            }
        }
    }
}
