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
    /// Interaction logic for GroupsTablePage.xaml
    /// </summary>
    public partial class GroupsTablePage : Window
    {
        private string connectionString = "Server=DESKTOP-B019U82\\SQLEXPRESS;Database=CLS_DB;Integrated Security=SSPI;TrustServerCertificate=True;";

        public GroupsTablePage()
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
            if (groups.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)groups.SelectedItem;
                groname.Text = selectedRow["groupName"].ToString();
                lev.Text = selectedRow["groupLevel"].ToString();

            }
            else
            {
                groname.Clear();
                lev.Clear();
            }
        }
        public void LoadData()
        {

            var stublishingConnection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SqlConnection connection = new SqlConnection(stublishingConnection.GetSection("ConnectionString").Value);
            connection.Open();
            string query = "SELECT * FROM Groups";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            groups.ItemsSource = dataTable.DefaultView;
        }


        private void UpdateStudentInDatabase(int groupid, string name, string levv)
        {
            var stublishingConnection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SqlConnection connection = new SqlConnection(stublishingConnection.GetSection("ConnectionString").Value);
            connection.Open();
            string query = "UPDATE Groups SET [groupName]=@groupName, [groupLevel]=@groupLevel WHERE groupid=@groupid";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@groupName", name);
            command.Parameters.AddWithValue("@groupLevel", levv);
            command.Parameters.AddWithValue("groupID", groupid);

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

        }
        public void Insert(string name, string levv)
        {
            if (groname.Text == "" || lev.Text == "")
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

                string sqlQuery = "INSERT INTO Groups ([groupName], [groupLevel]) Values (@groupName, @groupLevel);";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, connection);
                sqlCommand.Parameters.AddWithValue("@groupName", name);
                sqlCommand.Parameters.AddWithValue("@groupLevel", levv);
                sqlCommand.CommandType = CommandType.Text;
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
                LoadData();
            }
        }


        #endregion



        #region Buttons


        private void Addbtn(object sender, RoutedEventArgs e)
        {
            string name = groname.Text;
            string levv = lev.Text;

            Insert(name, levv);
        }
        private void Updatebtn(object sender, RoutedEventArgs e)
        {
            if (groups.SelectedItem != null)
            {
                int groupid;
                DataRowView selectedRow = (DataRowView)groups.SelectedItem;
                groupid = (int)(selectedRow["groupID"]);
                string name = groname.Text;
                string levv = lev.Text;


                UpdateStudentInDatabase(groupid, name, levv);
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
        private void Deletebtn(object sender, RoutedEventArgs e)
        {
            if (groups.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)groups.SelectedItem;
                int groupid = Convert.ToInt32(selectedRow["groupID"]);

                var stublishingConnection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                SqlConnection connection = new SqlConnection(stublishingConnection.GetSection("ConnectionString").Value);
                connection.Open();

                // Step 1: Delete related records from LabGroups
                string deleteLabGroupsQuery = "DELETE FROM LabGroups WHERE groupID = @groupID";
                using (SqlCommand command = new SqlCommand(deleteLabGroupsQuery, connection))
                {
                    command.Parameters.AddWithValue("@groupID", groupid);
                    command.ExecuteNonQuery();
                }

                // Step 2: Delete students who belong to this group (if needed)
                string deleteStudentsQuery = "DELETE FROM Students WHERE groupId = @groupID";
                using (SqlCommand command = new SqlCommand(deleteStudentsQuery, connection))
                {
                    command.Parameters.AddWithValue("@groupID", groupid);
                    command.ExecuteNonQuery();
                }

                // Step 3: Now, delete the group itself
                string deleteGroupQuery = "DELETE FROM Groups WHERE groupID = @groupID";
                using (SqlCommand command = new SqlCommand(deleteGroupQuery, connection))
                {
                    command.Parameters.AddWithValue("@groupID", groupid);
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
            groname.Clear();
            lev.Clear();

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
