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
    /// Interaction logic for ChoosingPage.xaml
    /// </summary>
    public partial class ChoosingPage : Window
    {
        public ChoosingPage()
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

        private void labsbtn(object sender, RoutedEventArgs e)
        {
            LabsPage ee = new LabsPage();
            this.Hide();
            ee.Show();

        }
        
        private void reportsbtn(object sender, RoutedEventArgs e)
        {
            TechnicalValidityPage ee = new TechnicalValidityPage();
            this.Hide();
            ee.Show();


        } 
        private void dashboardbtn(object sender, RoutedEventArgs e)
        {
            ChoosingTablesToInsert ee = new ChoosingTablesToInsert();
            this.Hide();
            ee.Show();


        }

    
    }
}
