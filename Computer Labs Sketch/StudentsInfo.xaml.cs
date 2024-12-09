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
    /// Interaction logic for StudentsInfo.xaml
    /// </summary>
    public partial class StudentsInfo : Window
    {
        public StudentsInfo()
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
            LoadStudentData(integer_computer_id);

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


        private void LoadStudentData(int computerID)
        {
            string connectionString = "Server=DESKTOP-B019U82\\SQLEXPRESS;Database=CLS_DB;Integrated Security=SSPI;TrustServerCertificate=True;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL query to retrieve student data along with group information based on computerID
                    string queryStudents = @"
                SELECT 
                    S.studentID, 
                    S.StudentName, 
                    S.studentNumber, 
                    G.groupName
                FROM Students S
                JOIN StudentComputers SC ON S.studentID = SC.studentID  -- Join to the StudentComputers table
                JOIN Computers C ON SC.computerID = C.computerID  -- Join to Computers table to filter by computerID
                LEFT JOIN Groups G ON S.groupID = G.groupID  -- Join to Groups table to get group name
                WHERE C.computerID = @computerID";  // Filter by the specified computerID

                    DataTable studentData = new DataTable();
                    using (SqlCommand command = new SqlCommand(queryStudents, connection))
                    {
                        command.Parameters.AddWithValue("@computerID", computerID);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        int rowsAffected = adapter.Fill(studentData);

                        // Debugging: Check how many rows were returned
                        Console.WriteLine($"{rowsAffected} rows returned.");

                        // Check if any data was retrieved
                        if (rowsAffected == 0)
                        {
                            MessageBox.Show("No students found for this computer.", "No Data", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }

                    // Check if the DataTable has data
                    if (studentData.Rows.Count > 0)
                    {
                        // Bind the DataTable to the DataGridView or other UI control
                        students.ItemsSource = studentData.DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("No students assigned to this computer.", "No Data", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
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
