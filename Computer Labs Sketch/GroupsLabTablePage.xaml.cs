using DocumentFormat.OpenXml.Spreadsheet;
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
    /// Interaction logic for GroupsLabTablePage.xaml
    /// </summary>
    public partial class GroupsLabTablePage : Window
    {
        private string connectionString = "Server=DESKTOP-B019U82\\SQLEXPRESS;Database=CLS_DB;Integrated Security=SSPI;TrustServerCertificate=True;";

        public GroupsLabTablePage()
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
            if (groupslab.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)groupslab.SelectedItem;

                // Display the groupName and labName in the corresponding textboxes
                gronname.Text = selectedRow["groupName"].ToString();  // Display the groupName
                labname.Text = selectedRow["labName"].ToString();     // Display the labName
            }
            else
            {
                gronname.Clear();
                labname.Clear();
            }
        }
        public void LoadData()
        {
            var stublishingConnection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SqlConnection connection = new SqlConnection(stublishingConnection.GetSection("ConnectionString").Value);
            connection.Open();

            // Modify the query to include labName and groupName by joining Labs and Groups tables
            string query = @"
        SELECT lg.labID, lg.groupID, l.labName, g.groupName
        FROM LabGroups lg
        INNER JOIN Labs l ON lg.labID = l.labID
        INNER JOIN Groups g ON lg.groupID = g.groupID";

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Bind the DataTable to the DataGrid's ItemSource
            groupslab.ItemsSource = dataTable.DefaultView;

            connection.Close();
        }


        private void UpdateGroupsLabInDatabase(int selectedGroupID, int selectedLabID, int groupID, int labID)
        {
            var stublishingConnection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SqlConnection connection = new SqlConnection(stublishingConnection.GetSection("ConnectionString").Value);
            connection.Open();

            try
            {
                // Update the LabGroups table with the new groupID and labID based on the old groupID and labID (selected row)
                string updateQuery = @"
            UPDATE LabGroups
            SET [groupID] = @groupID, [labID] = @labID
            WHERE [groupID] = @selectedGroupID AND [labID] = @selectedLabID";  // Use both selected IDs to find the correct record

                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@groupID", groupID);           // New groupID
                updateCommand.Parameters.AddWithValue("@labID", labID);               // New labID
                updateCommand.Parameters.AddWithValue("@selectedGroupID", selectedGroupID);  // Existing groupID (selected row)
                updateCommand.Parameters.AddWithValue("@selectedLabID", selectedLabID);      // Existing labID (selected row)

                // Execute the update command
                int rowsAffected = updateCommand.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    // Show success message or page
                    UpdateDonePage updateDonePage = new UpdateDonePage();
                    updateDonePage.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Failed to update the LabGroups record.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
        public void Insert(string groupName, string labName)
        {
            if (string.IsNullOrEmpty(groupName) || string.IsNullOrEmpty(labName))
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
                    connection.Open();

                    // Step 1: Get the groupID based on the groupName
                    string getGroupIdQuery = "SELECT groupID FROM [Groups] WHERE groupName = @groupName";
                    SqlCommand getGroupIdCommand = new SqlCommand(getGroupIdQuery, connection);
                    getGroupIdCommand.Parameters.AddWithValue("@groupName", groupName);

                    object groupIdObj = getGroupIdCommand.ExecuteScalar();
                    if (groupIdObj == null)
                    {
                        // Handle case where the groupName doesn't exist
                        MessageBox.Show("Group name not found.");
                        return;
                    }
                    int groupId = (int)groupIdObj;

                    // Step 2: Get the labID based on the labName
                    string getLabIdQuery = "SELECT labID FROM [Labs] WHERE labName = @labName";
                    SqlCommand getLabIdCommand = new SqlCommand(getLabIdQuery, connection);
                    getLabIdCommand.Parameters.AddWithValue("@labName", labName);

                    object labIdObj = getLabIdCommand.ExecuteScalar();
                    if (labIdObj == null)
                    {
                        // Handle case where the labName doesn't exist
                        MessageBox.Show("Lab name not found.");
                        return;
                    }
                    int labId = (int)labIdObj;

                    // Step 3: Check if the combination of groupID and labID already exists in LabGroups
                    string checkExistenceQuery = "SELECT COUNT(*) FROM LabGroups WHERE groupID = @groupID AND labID = @labID";
                    SqlCommand checkExistenceCommand = new SqlCommand(checkExistenceQuery, connection);
                    checkExistenceCommand.Parameters.AddWithValue("@groupID", groupId);
                    checkExistenceCommand.Parameters.AddWithValue("@labID", labId);

                    int count = (int)checkExistenceCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        this.IsEnabled = false;
                        AlreadyinDBPage ee = new AlreadyinDBPage();
                        ee.ShowDialog();
                        if (ee.OkButtonClicked)
                        {
                            this.IsEnabled = true;
                        }
                        return;
                    }

                    // Step 4: Insert the new record into LabGroups table if not already exists
                    string insertQuery = "INSERT INTO LabGroups ([groupID], [labID]) VALUES (@groupID, @labID)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@groupID", groupId);
                    insertCommand.Parameters.AddWithValue("@labID", labId);

                    insertCommand.ExecuteNonQuery();

                    this.IsEnabled = false;
                    AddDonePage dataFieldsPage = new AddDonePage();
                    dataFieldsPage.ShowDialog();
                    if (dataFieldsPage.OkButtonClicked)
                    {
                        this.IsEnabled = true;
                    }
                    // Reload data or any necessary UI updates
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
        }




        // Get the groupID based on the groupName
        private int GetGroupIdByName(string groupName)
        {
            var stublishingConnection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SqlConnection connection = new SqlConnection(stublishingConnection.GetSection("ConnectionString").Value);
            connection.Open();

            string query = "SELECT groupID FROM [Groups] WHERE groupName = @groupName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@groupName", groupName);

            object result = command.ExecuteScalar();
            connection.Close();

            // If no result found, return -1 (to indicate failure)
            return result != null ? (int)result : -1;
        }

        // Get the labID based on the labName
        private int GetLabIdByName(string labName)
        {
            var stublishingConnection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SqlConnection connection = new SqlConnection(stublishingConnection.GetSection("ConnectionString").Value);
            connection.Open();

            string query = "SELECT labID FROM [Labs] WHERE labName = @labName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@labName", labName);

            object result = command.ExecuteScalar();
            connection.Close();

            // If no result found, return -1 (to indicate failure)
            return result != null ? (int)result : -1;
        }

        #endregion



        #region Buttons


        private void Addbtn(object sender, RoutedEventArgs e)
        {
            string groupName = gronname.Text;  // User enters group name
            string labName = labname.Text;      // User enters lab name

            Insert(groupName, labName);  // Pass groupName and labName to the Insert method
        }


        private void Updatebtn(object sender, RoutedEventArgs e)
        {
            if (groupslab.SelectedItem != null)
            {
                // Step 1: Retrieve the selected groupID and labID from the DataGrid
                DataRowView selectedRow = (DataRowView)groupslab.SelectedItem;
                int selectedGroupID = (int)(selectedRow["groupID"]);  // Use the groupID from the selected row
                int selectedLabID = (int)(selectedRow["labID"]);      // Use the labID from the selected row

                // Step 2: Retrieve the groupName and labName from the TextBoxes
                string groupName = gronname.Text;  // User entered groupName
                string labName = labname.Text;     // User entered labName

                // Step 3: Validate the input
                if (string.IsNullOrEmpty(groupName) || string.IsNullOrEmpty(labName))
                {
                    MessageBox.Show("Please enter both the group name and lab name.");
                    return;
                }

                // Step 4: Retrieve groupID and labID based on the entered names
                int groupID = GetGroupIdByName(groupName);
                int labID = GetLabIdByName(labName);

                // Check if the IDs are valid
                if (groupID == -1 || labID == -1)
                {
                    MessageBox.Show("Invalid group name or lab name.");
                    return;
                }

                // Step 5: Call the Update method to update the LabGroups table
                UpdateGroupsLabInDatabase(selectedGroupID, selectedLabID, groupID, labID);  // Pass selected and new IDs

                // Step 6: Reload the DataGrid to refresh the data
                LoadData();
            }
            else
            {
                // If no row is selected, show the SelectLabPage dialog
                this.IsEnabled = false;
                SelectLabPage selectlabPage = new SelectLabPage();
                selectlabPage.ShowDialog();
                if (selectlabPage.OkButtonClicked)
                {
                    this.IsEnabled = true;
                }
            }
        }


        private void Deletebtn(object sender, RoutedEventArgs e)
        {
            if (groupslab.SelectedItem != null)
            {
                // Step 1: Retrieve the selected groupID and labID from the DataGrid
                DataRowView selectedRow = (DataRowView)groupslab.SelectedItem;
                int groupID = Convert.ToInt32(selectedRow["groupID"]);
                int labID = Convert.ToInt32(selectedRow["labID"]);  // Retrieve labID to uniquely identify the row

                // Step 2: Remove the selected row from the DataGrid (local view)
                DataView labsDataView = (DataView)groupslab.ItemsSource;
                DataRow[] rowsToDelete = labsDataView.Table.Select($"groupID = {groupID} AND labID = {labID}"); // Use both groupID and labID

                if (rowsToDelete.Length > 0)
                {
                    foreach (DataRow row in rowsToDelete)
                    {
                        labsDataView.Table.Rows.Remove(row);  // Remove the row from the local DataTable
                    }

                    // Step 3: Delete the row from the database using both groupID and labID
                    var stublishingConnection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                    SqlConnection connection = new SqlConnection(stublishingConnection.GetSection("ConnectionString").Value);
                    connection.Open();

                    // Delete the specific row from LabGroups based on both groupID and labID
                    string deleteQuery = "DELETE FROM LabGroups WHERE groupID = @groupID AND labID = @labID";
                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@groupID", groupID);
                        command.Parameters.AddWithValue("@labID", labID);  // Use both groupID and labID to identify the row
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Step 4: Show success message or page
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
                // If no row is selected, show the SelectLabPage dialog
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
            gronname.Clear();
            labname.Clear();

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
