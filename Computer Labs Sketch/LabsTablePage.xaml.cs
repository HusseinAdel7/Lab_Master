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
    /// Interaction logic for LabsTablePage.xaml
    /// </summary>
    public partial class LabsTablePage : Window
    {
        private string connectionString = "Server=DESKTOP-B019U82\\SQLEXPRESS;Database=CLS_DB;Integrated Security=SSPI;TrustServerCertificate=True;";

        public LabsTablePage()
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
            if (labs.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)labs.SelectedItem;
                labname.Text = selectedRow["labName"].ToString();
                labnumber.Text = selectedRow["labCode"].ToString();
                lablocation.Text = selectedRow["location"].ToString();
                labequipment.Text = selectedRow["equipments"].ToString();


            }
            else
            {

                labname.Clear();
                labnumber.Clear();
                lablocation.Clear();
                labequipment.Clear();
            }
        }
        public void LoadData()
        {

            var stublishingConnection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SqlConnection connection = new SqlConnection(stublishingConnection.GetSection("ConnectionString").Value);
            connection.Open();
            string query = "SELECT * FROM Labs";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            labs.ItemsSource = dataTable.DefaultView;
        }
       
        
        private void UpdateStudentInDatabase(int labid, string name, string number, string loaction, string equipment)
        {
            var stublishingConnection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SqlConnection connection = new SqlConnection(stublishingConnection.GetSection("ConnectionString").Value);
            connection.Open();
            string query = "UPDATE Labs SET [labName]=@labName, [labCode]=@labCode, [location]=@location, [equipments]=@equipments WHERE labid=@labid";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@labName", name);
            command.Parameters.AddWithValue("@labCode", number);
            command.Parameters.AddWithValue("@location", loaction);
            command.Parameters.AddWithValue("@equipments", equipment);
            command.Parameters.AddWithValue("@LabID", labid);

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
        public void Insert(string labnamee, string labnumbere, string lablocatione, string labequipmente)
        {
            if (labname.Text == "" || labnumber.Text == "" || lablocation.Text == "" || labequipment.Text == "")
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

                string sqlQuery = "INSERT INTO Labs ([labName], [labCode], [location], [equipments]) Values (@labName, @labCode, @location, @equipments);";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, connection);
                sqlCommand.Parameters.AddWithValue("@labName", labnamee);
                sqlCommand.Parameters.AddWithValue("@labCode", labnumbere);
                sqlCommand.Parameters.AddWithValue("@location", lablocatione);
                sqlCommand.Parameters.AddWithValue("@equipments", labequipmente);

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
            string name = labname.Text;
            string number = labnumber.Text;
            string location = lablocation.Text;
            string equipment = labequipment.Text;


            Insert(name, number, location, equipment);
        }
        private void Updatebtn(object sender, RoutedEventArgs e)
        {
            if (labs.SelectedItem != null)
            {
                int labid;
                DataRowView selectedRow = (DataRowView)labs.SelectedItem;
                labid = (int)(selectedRow["labID"]);
                string name = labname.Text;
                string number = labnumber.Text;
                string location = lablocation.Text;
                string equipment = labequipment.Text;

                UpdateStudentInDatabase(labid, name, number, location, equipment);
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
            if (labs.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)labs.SelectedItem;
                int labID = Convert.ToInt32(selectedRow["LabID"]);

                // Assuming that Students.ItemsSource is a DataView
                DataView labsDataView = (DataView)labs.ItemsSource;

                // Find the row to delete based on the StudentID
                DataRow[] rowsToDelete = labsDataView.Table.Select($"LabID = {labID}");

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

                    string deleteQuery = "DELETE FROM Labs WHERE labID = @LabID";
                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@LabID", labID);
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
            labname.Text = "";
            labnumber.Text = "";
            lablocation.Text = "";
            labequipment.Text = "";

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
