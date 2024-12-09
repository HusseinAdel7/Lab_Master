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
    /// Interaction logic for MoreInfoAboutComputersPage.xaml
    /// </summary>
    public partial class MoreInfoAboutComputersPage : Window
    {
        public MoreInfoAboutComputersPage()
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

        private void Backbtn(object sender, RoutedEventArgs e)
        {
            if (LabsPage.lab == 4 )
            {
                SketchLab4Page ee = new SketchLab4Page();
                this.Hide();
                ee.Show();
            }
            if (LabsPage.lab == 3 )
            {
                SketchLab3Page ee = new SketchLab3Page();
                this.Hide();
                ee.Show();
            }
            if (LabsPage.lab == 2 )
            {
                SketchLab2Page ee = new SketchLab2Page();
                this.Hide();
                ee.Show();
            }
            if (LabsPage.lab == 1 )
            {
                SketchLab1Page ee = new SketchLab1Page();
                this.Hide();
                ee.Show();
            }
            if (LabsPage.lab == 5 )
            {
                SketchClass2Page ee = new SketchClass2Page();
                this.Hide();
                ee.Show();
            }
            if (LabsPage.lab == 6 )
            {
                SketchClass1Page ee = new SketchClass1Page();
                this.Hide();
                ee.Show();
            }
            if (LabsPage.lab == 7 )
            {
                SketchCiscoPage ee = new SketchCiscoPage();
                this.Hide();
                ee.Show();
            }
            
            
        }

        private void ComputerInfobtn(object sender, RoutedEventArgs e)
        {
            ComputerInfo ee = new ComputerInfo();
            this.Hide();
            ee.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SoftwareInfo ee = new SoftwareInfo();
            this.Hide();
            ee.Show();

        }

        private void StudentsInfobtn(object sender, RoutedEventArgs e)
        {
            StudentsInfo ee = new StudentsInfo();
            this.Hide();
            ee.Show();
        }
    }
}
