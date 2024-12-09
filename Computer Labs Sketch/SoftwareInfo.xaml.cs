using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for SoftwareInfo.xaml
    /// </summary>
    public partial class SoftwareInfo : Window
    {
        public SoftwareInfo()
        {

            InitializeComponent();
            int integer_computer_id = 0;
            int integer_lab_id = LabsPage.lab;

            if (integer_lab_id == 1)
            {
                integer_computer_id = SketchLab1Page.integer_button_number;
            }
            else if (integer_lab_id == 2)
            {
                integer_computer_id = SketchLab2Page.integer_button_number;

            }
            else if (integer_lab_id == 3)
            {
                integer_computer_id = SketchLab3Page.integer_button_number;

            }
            else if (integer_lab_id == 4)
            {
                integer_computer_id = SketchLab4Page.integer_button_number;

            }
            else if (integer_lab_id == 5)
            {
                integer_computer_id = SketchClass1Page.integer_button_number;

            }
            else if (integer_lab_id == 6)
            {
                integer_computer_id = SketchClass2Page.integer_button_number;

            }
            else if (integer_lab_id == 7)
            {
                integer_computer_id = SketchCiscoPage.integer_button_number;

            }
            LoadSoftwareData(integer_computer_id);
        }



        private void LoadSoftwareData(int computerID)
        {
            string connectionString = "Server=DESKTOP-B019U82\\SQLEXPRESS;Database=CLS_DB;Integrated Security=SSPI;TrustServerCertificate=True;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Retrieve software data for a specific computer ID
                    string querySoftware = @"
                SELECT s.softwareName, s.version, s.softwareCategory
                FROM Software s
                INNER JOIN ComputersSoftware cs ON s.softwareID = cs.softwareID
                WHERE cs.computerID = @computerID";

                    DataTable softwareData = new DataTable();
                    using (SqlCommand command = new SqlCommand(querySoftware, connection))
                    {
                        command.Parameters.AddWithValue("@computerID", computerID);

                        SqlDataReader reader = command.ExecuteReader();
                        softwareData.Load(reader);
                    }

                    Softwares.ItemsSource = softwareData.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Backbtn(object sender, RoutedEventArgs e)
        {
            MoreInfoAboutComputersPage ee = new MoreInfoAboutComputersPage();
            this.Hide();
            ee.Show();
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
    }
}
