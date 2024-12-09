using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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
    /// Interaction logic for ComputerInfo.xaml
    /// </summary>
    public partial class ComputerInfo : Window
    {
        public ComputerInfo()
        {
            InitializeComponent();
            LoadData();
        }
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





        private void LoadData()
        {
            string connectionString = "Server=DESKTOP-B019U82\\SQLEXPRESS;Database=CLS_DB;Integrated Security=SSPI;TrustServerCertificate=True;";
            int integer_computer_id=0;
            int integer_lab_id= LabsPage.lab;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    
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

                    connection.Open();

                    // Retrieve data for info1 DataGrid with filtering
                    string queryInfo1 = "SELECT computerID, labID, caseSerialNumber, screenSerialNumber, computerName FROM Computers WHERE computerID = @id AND labID = @lid";
                    DataTable info1Data = new DataTable();
                    using (SqlCommand command = new SqlCommand(queryInfo1, connection))
                    {
                        // Replace @id and @lid with actual parameter values
                        command.Parameters.AddWithValue("@id", integer_computer_id);
                        command.Parameters.AddWithValue("@lid", integer_lab_id);

                        SqlDataReader reader = command.ExecuteReader();
                        info1Data.Load(reader);
                    }
                    info1.ItemsSource = info1Data.DefaultView;

                    // Retrieve data for info2 DataGrid with filtering
                    string queryInfo2 = "SELECT processor, RAM, storage, graphicsCard, operatingSystem, MACAddress, status FROM Computers WHERE computerID = @id AND labID = @lid";
                    DataTable info2Data = new DataTable();
                    using (SqlCommand command = new SqlCommand(queryInfo2, connection))
                    {
                        // Replace @id and @lid with actual parameter values
                        command.Parameters.AddWithValue("@id", integer_computer_id);
                        command.Parameters.AddWithValue("@lid", integer_lab_id);

                        SqlDataReader reader = command.ExecuteReader();
                        info2Data.Load(reader);
                    }
                    info2.ItemsSource = info2Data.DefaultView;

                    // Retrieve data for info3 DataGrid with filtering
                    string queryInfo3 = "SELECT connectionToNetworkStatus FROM computers WHERE computerID = @id AND labID = @lid";
                    DataTable info3Data = new DataTable();
                    using (SqlCommand command = new SqlCommand(queryInfo3, connection))
                    {
                        // Replace @id and @lid with actual parameter values
                        command.Parameters.AddWithValue("@id", integer_computer_id);
                        command.Parameters.AddWithValue("@lid", integer_lab_id);

                        SqlDataReader reader = command.ExecuteReader();
                        info3Data.Load(reader);
                    }
                    info3.ItemsSource = info3Data.DefaultView;
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


    }
}
