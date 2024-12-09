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
    /// Interaction logic for LabsPage.xaml
    /// </summary>
    public partial class LabsPage : Window
    {
        public static int lab;
        public LabsPage()
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

        private void lab4btn(object sender, RoutedEventArgs e)
        {
            lab = 4;
            SketchLab4Page ee = new SketchLab4Page();
            this.Hide();
            ee.Show();
        }

        private void lab3btn(object sender, RoutedEventArgs e)
        {
            lab =3;
            SketchLab3Page ee = new SketchLab3Page();
            this.Hide();
            ee.Show();

        }

        private void lab2btn(object sender, RoutedEventArgs e)
        {
            lab = 2;

            SketchLab2Page ee = new SketchLab2Page();
            this.Hide();
            ee.Show();

        }

        private void class2btn(object sender, RoutedEventArgs e)
        {
            lab = 6;
            SketchClass2Page ee = new SketchClass2Page();
            this.Hide();
            ee.Show();

        }

        private void class1btn(object sender, RoutedEventArgs e)
        {
            lab = 5;
            SketchClass1Page ee = new SketchClass1Page();
            this.Hide();
            ee.Show();

        }

        private void ciscobtn(object sender, RoutedEventArgs e)
        {
            lab = 7;
            SketchCiscoPage ee = new SketchCiscoPage();
            this.Hide();
            ee.Show();

        }

        private void lab1btn(object sender, RoutedEventArgs e)
        {
            lab = 1;
            SketchLab1Page ee = new SketchLab1Page();
            this.Hide();
            ee.Show();

        }

        private void Backbtn(object sender, RoutedEventArgs e)
        {
            ChoosingPage ee = new ChoosingPage();
            this.Hide();
            ee.Show();
        }
        private void lmslab5btn(object sender, RoutedEventArgs e)
        {
            LMSLab1 ee = new LMSLab1();
            this.Hide();
            ee.Show();
        }
         private void lmslab6btn(object sender, RoutedEventArgs e)
        {
            LMSLab2 ee = new LMSLab2();
            this.Hide();
            ee.Show();
        }
         private void lmslab7btn(object sender, RoutedEventArgs e)
        {
            LMSLab3 ee = new LMSLab3();
            this.Hide();
            ee.Show();
        }
         private void lmslab8btn(object sender, RoutedEventArgs e)
        {
            LMSLab2 ee = new LMSLab2();
            this.Hide();
            ee.Show();
        }
         private void lmslab9btn(object sender, RoutedEventArgs e)
        {
            LMSLab5 ee = new LMSLab5();
            this.Hide();
            ee.Show();
        }
         private void lmslab10btn(object sender, RoutedEventArgs e)
        {
            LMSLab1 ee = new LMSLab1();
            this.Hide();
            ee.Show();
        }
         private void lmslab11btn(object sender, RoutedEventArgs e)
        {
            LMSLab1 ee = new LMSLab1();
            this.Hide();
            ee.Show();
        }

        private void cyberseclabbtn(object sender, RoutedEventArgs e)
        {
            LMSLab5 ee = new LMSLab5();
            this.Hide();
            ee.Show();
        }
    }
}
