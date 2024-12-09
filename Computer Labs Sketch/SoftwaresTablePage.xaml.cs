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
    /// Interaction logic for SoftwaresTablePage.xaml
    /// </summary>
    public partial class SoftwaresTablePage : Window
    {
        private string connectionString = "Server=DESKTOP-B019U82\\SQLEXPRESS;Database=CLS_DB;Integrated Security=SSPI;TrustServerCertificate=True;";

        public SoftwaresTablePage()
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
            if (softwares.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)softwares.SelectedItem;
                projname.Text = selectedRow["softwareName"].ToString();
                projversion.Text = selectedRow["version"].ToString();
                projcategory.Text = selectedRow["softwareCategory"].ToString();


            }
            else
            {

                projname.Clear();
                projversion.Clear();
                projcategory.Clear();
            }
        }
        public void LoadData()
        {

            var stublishingConnection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SqlConnection connection = new SqlConnection(stublishingConnection.GetSection("ConnectionString").Value);
            connection.Open();
            string query = "SELECT * FROM Software";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            softwares.ItemsSource = dataTable.DefaultView;
        }


        private void UpdateStudentInDatabase(int softwareid, string name, string ver, string cat)
        {
            var stublishingConnection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SqlConnection connection = new SqlConnection(stublishingConnection.GetSection("ConnectionString").Value);
            connection.Open();
            string query = "UPDATE Software SET [softwareName]=@softwareName, [version]=@version, [softwareCategory]=@softwareCategory WHERE softwareid=@softwareid";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@softwareName", name);
            command.Parameters.AddWithValue("@version", ver);
            command.Parameters.AddWithValue("@softwareCategory", cat);
            command.Parameters.AddWithValue("@softwareID", softwareid);

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
        public void Insert(string name, string ver, string cat)
        {
            if (projname.Text == "" || projversion.Text == "" || projcategory.Text == "")
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

                string sqlQuery = "INSERT INTO Software ([softwareName], [version], [softwareCategory]) Values (@softwareName, @version, @softwareCategory);";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, connection);
                sqlCommand.Parameters.AddWithValue("@softwareName", name);
                sqlCommand.Parameters.AddWithValue("@version", ver);
                sqlCommand.Parameters.AddWithValue("@softwareCategory", cat);


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
            string name = projname.Text;
            string ver = projversion.Text;
            string cat = projcategory.Text;



            Insert(name, ver, cat);
        }
        private void Updatebtn(object sender, RoutedEventArgs e)
        {
            if (softwares.SelectedItem != null)
            {
                int softwareid;
                DataRowView selectedRow = (DataRowView)softwares.SelectedItem;
                softwareid = (int)(selectedRow["softwareID"]);
                string name = projname.Text;
                string ver = projversion.Text;
                string cat = projcategory.Text;

                UpdateStudentInDatabase(softwareid, name, ver, cat);
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
            if (softwares.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)softwares.SelectedItem;
                int softwareid = Convert.ToInt32(selectedRow["softwareID"]);

                // Assuming that Students.ItemsSource is a DataView
                DataView labsDataView = (DataView)softwares.ItemsSource;

                // Find the row to delete based on the StudentID
                DataRow[] rowsToDelete = labsDataView.Table.Select($"softwareID = {softwareid}");

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

                    string deleteQuery = "DELETE FROM Software WHERE softwareid = @softwareID";
                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@softwareID", softwareid);
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
            projname.Clear();
            projversion.Clear();
            projcategory.Clear();

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
