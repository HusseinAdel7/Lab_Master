using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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
    /// Interaction logic for StudentsTablePage.xaml
    /// </summary>
    public partial class StudentsTablePage : Window
    {
        private string connectionString = "Server=DESKTOP-B019U82\\SQLEXPRESS;Database=CLS_DB;Integrated Security=SSPI;TrustServerCertificate=True;";

        public StudentsTablePage()
        {
            InitializeComponent();
            LoadData();
        }

        #region header buttons

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


        #region Functions


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (students.SelectedItem != null)
            {
                // Get the selected row from the DataGrid
                DataRowView selectedRow = (DataRowView)students.SelectedItem;

                // Populate the textboxes with the selected row data
                stdname.Text = selectedRow["StudentName"].ToString();
                stdnumber.Text = selectedRow["studentNumber"].ToString();

                // Check if groupId exists and is a valid column
                if (selectedRow.Row.Table.Columns.Contains("groupId"))
                {
                    groname.Text = selectedRow["groupId"].ToString();
                }
                else if (selectedRow.Row.Table.Columns.Contains("groupName"))
                {
                    groname.Text = selectedRow["groupName"].ToString();
                }
                else
                {
                    groname.Text = "No Group Data";
                }
            }
            else
            {
                // Clear the textboxes if no row is selected
                stdname.Clear();
                stdnumber.Clear();
                groname.Clear();
            }
        }

        public void LoadData()
        {
            var stublishingConnection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SqlConnection connection = new SqlConnection(stublishingConnection.GetSection("ConnectionString").Value);

            try
            {
                connection.Open();
                // Query to join Students and Groups to fetch groupName along with other student data
                string query = "SELECT s.studentID, s.StudentName, s.studentNumber, g.groupName " +
                               "FROM Students s " +
                               "JOIN Groups g ON s.groupId = g.groupID";  // Join to get the groupName
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);  // Fill the DataTable with data

                // Set the DataGrid's item source to the DataTable's default view
                students.ItemsSource = dataTable.DefaultView;

                // Optionally, you can also set the column names programmatically if needed
                // for explicit data binding, but using the DefaultView is sufficient for most cases
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading data: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }



       
        private void UpdateStudentInDatabase(int stdid, string name, string number, int groupId)
        {
            var stublishingConnection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SqlConnection connection = new SqlConnection(stublishingConnection.GetSection("ConnectionString").Value);
            connection.Open();

            // Update query: groupId should be an integer in the database
            string query = "UPDATE Students SET [StudentName] = @StudentName, [studentNumber] = @studentNumber, [groupId] = @groupId WHERE studentID = @stdid";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@StudentName", name);
            command.Parameters.AddWithValue("@studentNumber", number);
            command.Parameters.AddWithValue("@groupId", groupId);  // Passing groupId as an integer
            command.Parameters.AddWithValue("@stdid", stdid);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                UpdateDonePage updateDonePage = new UpdateDonePage();
                updateDonePage.ShowDialog();
            }
            else
            {
                MessageBox.Show("Failed to update student.");
            }

            connection.Close();
        }
        public void Insert(string name, string number, string groupName)
        {
            // Check if any fields are empty
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(number) || string.IsNullOrEmpty(groupName))
            {
                this.IsEnabled = false;
                DataFieldsPage dataFieldsPage = new DataFieldsPage();
                dataFieldsPage.ShowDialog();
                if (dataFieldsPage.OkButtonClicked)
                {
                    this.IsEnabled = true;
                }
            }
            else
            {
                var stublishingConnection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                SqlConnection connection = new SqlConnection(stublishingConnection.GetSection("ConnectionString").Value);

                try
                {
                    // Query to get the groupId based on the groupName
                    string groupQuery = "SELECT groupID FROM Groups WHERE groupName = @groupName";
                    SqlCommand groupCommand = new SqlCommand(groupQuery, connection);
                    groupCommand.Parameters.AddWithValue("@groupName", groupName);

                    connection.Open();
                    var groupId = groupCommand.ExecuteScalar();  // Get the groupId

                    if (groupId == null)
                    {
                        MessageBox.Show($"No group found with the name: {groupName}");
                        return; // Exit if no group is found with the given name
                    }

                    // Now insert the student data with the obtained groupId
                    string sqlQuery = "INSERT INTO Students ([StudentName], [studentNumber], [groupId]) Values (@StudentName, @studentNumber, @groupId)";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, connection);
                    sqlCommand.Parameters.AddWithValue("@StudentName", name);
                    sqlCommand.Parameters.AddWithValue("@studentNumber", number);
                    sqlCommand.Parameters.AddWithValue("@groupId", groupId); // Insert the groupId from the lookup

                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.ExecuteNonQuery();  // Execute insert query
                    connection.Close();

                    LoadData();  // Reload the data grid
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }




        #endregion


        #region Buttons


        private void Addbtn(object sender, RoutedEventArgs e)
        {
            string name = stdname.Text;
            string number = stdnumber.Text;
            string groupName = groname.Text;  // Get the groupName (not groupId)

            // Call the Insert method with groupName instead of groupId
            Insert(name, number, groupName);
        }

        private void Updatebtn(object sender, RoutedEventArgs e)
        {
            if (students.SelectedItem != null)
            {
                int stdid;
                DataRowView selectedRow = (DataRowView)students.SelectedItem;
                stdid = (int)(selectedRow["studentID"]);
                string name = stdname.Text;
                string number = stdnumber.Text;
                string groupName = groname.Text;  // Group name entered by the user

                // Get the groupId using the groupName
                int groupId = GetGroupIdByName(groupName);

                if (groupId == -1)
                {
                    MessageBox.Show($"No group found with the name: {groupName}");
                    return;  // Exit if no group is found with the given name
                }

                // Call the method to update the student record
                UpdateStudentInDatabase(stdid, name, number, groupId);  // groupId is now an integer
                LoadData(); // Reload data to refresh the DataGrid
            }
            else
            {
                this.IsEnabled = false;
                SelectLabPage selectlabPage = new SelectLabPage();
                selectlabPage.ShowDialog();
                if (selectlabPage.OkButtonClicked)
                {
                    this.IsEnabled = true;
                }
            }
        }

        private int GetGroupIdByName(string groupName)
        {
            // This function will query the database to get the groupId based on the groupName.
            var stublishingConnection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SqlConnection connection = new SqlConnection(stublishingConnection.GetSection("ConnectionString").Value);

            string query = "SELECT groupID FROM Groups WHERE groupName = @groupName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@groupName", groupName);

            try
            {
                connection.Open();
                var groupId = command.ExecuteScalar();  // Execute query and get the groupId
                return groupId != null ? Convert.ToInt32(groupId) : -1;  // Return -1 if no group is found
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching groupId: {ex.Message}");
                return -1;
            }
            finally
            {
                connection.Close();
            }
        }

        private void Deletebtn(object sender, RoutedEventArgs e)
        {
            if (students.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)students.SelectedItem;
                int stdid = Convert.ToInt32(selectedRow["studentID"]);

                // Assuming that Students.ItemsSource is a DataView
                DataView labsDataView = (DataView)students.ItemsSource;

                // Find the row to delete based on the StudentID
                DataRow[] rowsToDelete = labsDataView.Table.Select($"studentID = {stdid}");

                if (rowsToDelete.Length > 0)
                {
                    foreach (DataRow row in rowsToDelete)
                    {
                        // Step 1: Remove the row from the DataView
                        labsDataView.Table.Rows.Remove(row);
                    }

                    var stublishingConnection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                    SqlConnection connection = new SqlConnection(stublishingConnection.GetSection("ConnectionString").Value);
                    connection.Open();

                    string deleteQuery = "DELETE FROM Students WHERE studentID = @studentID";
                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@studentID", stdid);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            this.IsEnabled = false;
                            DeleteDonePage deleteDonePage = new DeleteDonePage();
                            deleteDonePage.ShowDialog();
                            if (deleteDonePage.OkButtonClicked)
                            {
                                this.IsEnabled = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete row from the database.");
                        }
                    }
                }
            }
            else
            {
                this.IsEnabled = false;
                SelectLabPage selectlabPage = new SelectLabPage();
                selectlabPage.ShowDialog();
                if (selectlabPage.OkButtonClicked)
                {
                    this.IsEnabled = true;
                }
            }
        }
        private void Resetbtn(object sender, RoutedEventArgs e)
        {
            stdname.Clear();
            stdnumber.Clear();
            groname.Clear();

        }


        #endregion



        private void Backbtn(object sender, RoutedEventArgs e)
        {
            ChoosingTablesToInsert homePage = new ChoosingTablesToInsert();
            homePage.Show();
            this.Hide();
        }
        private void Back2btn(object sender, RoutedEventArgs e)
        {
            ChoosingTablesToInsert homePage = new ChoosingTablesToInsert();
            homePage.Show();
            this.Hide();
        }
    }
}
