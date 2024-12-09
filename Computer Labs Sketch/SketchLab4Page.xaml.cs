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

namespace Computer_Labs_Sketch
{
    /// <summary>
    /// Interaction logic for SketchLab4Page.xaml
    /// </summary>
    public partial class SketchLab4Page : Window
    {
        string buttonNumber = "";
        public static int integer_button_number;

        public SketchLab4Page()
        {
            InitializeComponent();
        }

        #region Header Properties Minimize and Close

            private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    DragMove();
                }
            }
            private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
            {
                Window currentWindow = Window.GetWindow(this);

                if (currentWindow != null)
                {
                    currentWindow.WindowState = WindowState.Minimized;
                }
            }
            private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion


        #region On mouse Enter and On Mouse Leaver
            private void OnMouseEnter(object sender, MouseEventArgs e)
            {
                // When mouse enters, hide the TextBox and show the image
                infoTextBoxd1.Visibility = Visibility.Visible;
                Picuted1btn.Visibility = Visibility.Hidden;
            }

            private void OnMouseLeave(object sender, MouseEventArgs e)
            {
                // When mouse leaves, hide the image and show the TextBox
                infoTextBoxd1.Visibility = Visibility.Hidden;
                Picuted1btn.Visibility = Visibility.Visible;

            }
        #endregion

        #region More Details Button

        private void MoreDetails(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string buttonName = b.Name;
            buttonNumber = buttonName.Substring(3);  // this would be "01" , "02", "19"
            if (int.TryParse(buttonNumber, out integer_button_number))
            {
                MoreInfoAboutComputersPage ee = new MoreInfoAboutComputersPage();
                this.Hide();
                ee.Show();
            }
        }

        #endregion

        #region Back Button
        private void Backbtn(object sender, RoutedEventArgs e)
            {
                LabsPage ee = new LabsPage();
                this.Hide();
                ee.Show();
            }
        #endregion

    }
}
