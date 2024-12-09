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
    /// Interaction logic for SketchLab2Page.xaml
    /// </summary>
    public partial class SketchLab2Page : Window
    {
        string buttonNumber = "";
        public static int integer_button_number;

        public SketchLab2Page()
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


        private void Backbtn(object sender, RoutedEventArgs e)
        {
            LabsPage ee = new LabsPage();
            this.Hide();
            ee.Show();
        }
    }
}
