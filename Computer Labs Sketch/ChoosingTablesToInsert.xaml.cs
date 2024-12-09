using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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
    /// Interaction logic for ChoosingTablesToInsert.xaml
    /// </summary>
    public partial class ChoosingTablesToInsert : Window
    {
        public ChoosingTablesToInsert()
        {
            InitializeComponent();
        }

        private void Grid_MouseDownn(object sender, MouseButtonEventArgs e)
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



        private void labPicturebtn(object sender, RoutedEventArgs e)
        {
            LabsTablePage ee = new LabsTablePage();
            this.Hide();
            ee.Show();
        }
        private void computerPicturebtn(object sender, RoutedEventArgs e)
        {
            ComputersTablePage ee = new ComputersTablePage();
            this.Hide();
            ee.Show();
        }
        private void softwarePicturebtn(object sender, RoutedEventArgs e)
        {
            SoftwaresTablePage ee = new SoftwaresTablePage();
            this.Hide();
            ee.Show();
        }
        private void groupPicturebtn(object sender, RoutedEventArgs e)
        {
            GroupsTablePage ee = new GroupsTablePage();
            this.Hide();
            ee.Show();
        }
        private void studentPicturebtn(object sender, RoutedEventArgs e)
        {
            StudentsTablePage ee = new StudentsTablePage();
            this.Hide();
            ee.Show();
        }
        private void labgroupPicturebtn(object sender, RoutedEventArgs e)
        {
            GroupsLabTablePage ee = new GroupsLabTablePage();
            this.Hide();
            ee.Show();
        }



        private void labTextbtn(object sender, RoutedEventArgs e)
        {
            LabsTablePage ee = new LabsTablePage();
            this.Hide();
            ee.Show();
        }
        private void computerTextbtn(object sender, RoutedEventArgs e)
        {
            ComputersTablePage ee = new ComputersTablePage();
            this.Hide();
            ee.Show();
        }
        private void softwareTextbtn(object sender, RoutedEventArgs e)
        {
            SoftwaresTablePage ee = new SoftwaresTablePage();
            this.Hide();
            ee.Show();
        }
        private void groupTextbtn(object sender, RoutedEventArgs e)
        {
            GroupsTablePage ee = new GroupsTablePage();
            this.Hide();
            ee.Show();
        }
        private void studentTextbtn(object sender, RoutedEventArgs e)
        {
            StudentsTablePage ee = new StudentsTablePage();
            this.Hide();
            ee.Show();
        }
        private void labgroupTextbtn(object sender, RoutedEventArgs e)
        {
            GroupsLabTablePage ee = new GroupsLabTablePage();
            this.Hide();
            ee.Show();
        }
        private void Backbtn(object sender, RoutedEventArgs e)
        {
            ChoosingPage ee = new ChoosingPage();
            this.Hide();
            ee.Show();
        }
    }
}
