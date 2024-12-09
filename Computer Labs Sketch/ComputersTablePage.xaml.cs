using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.Devices;
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
    /// Interaction logic for ComputersTablePage.xaml
    /// </summary>
    public partial class ComputersTablePage : Window
    {
        private string connectionString = "Server=DESKTOP-B019U82\\SQLEXPRESS;Database=CLS_DB;Integrated Security=SSPI;TrustServerCertificate=True;";

        public ComputersTablePage()
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
            if (computers.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)computers.SelectedItem;
                labid.Text = selectedRow["labID"].ToString();
                casee.Text = selectedRow["caseSerialNumber"].ToString();
                screan.Text = selectedRow["screenSerialNumber"].ToString();
                computername.Text = selectedRow["computerName"].ToString();
                processor.Text = selectedRow["processor"].ToString();
                ram.Text = selectedRow["RAM"].ToString();
                hard.Text = selectedRow["storage"].ToString();
                graphicname.Text = selectedRow["graphicsCard"].ToString();
                win.Text = selectedRow["operatingSystem"].ToString();
                mac.Text = selectedRow["MACAddress"].ToString();
                status.Text = selectedRow["status"].ToString();
                internetconnection.Text = selectedRow["connectionToNetworkStatus"].ToString();


            }
            else
            {

                labid.Clear();
                casee.Clear();
                screan.Clear();
                computername.Clear();
                processor.Clear();
                ram.Clear();
                hard.Clear();
                graphicname.Clear();
                win.Clear();
                mac.Clear();
                status.Clear();
                internetconnection.Clear();

            }
        }
        public void LoadData()
        {

            var stublishingConnection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SqlConnection connection = new SqlConnection(stublishingConnection.GetSection("ConnectionString").Value);
            connection.Open();
            string query = "SELECT * FROM Computers";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            computers.ItemsSource = dataTable.DefaultView;
        }

        private void UpdateStudentInDatabase(string x1, string x2, string x3, string x4, string x5, string x6, string x7, string x8, string x9, string x10, string x11, string x12,int x13)
        {
            var stublishingConnection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SqlConnection connection = new SqlConnection(stublishingConnection.GetSection("ConnectionString").Value);
            connection.Open();
            string query = "UPDATE Computers SET [labID]=@x1, [caseSerialNumber]=@x2, [screenSerialNumber]=@x3, [computerName]=@x4, [processor]=@x5, [RAM]=@x6, [storage]=@x7, [graphicsCard]=@x8, [operatingSystem]=@x9, [MACAddress]=@x10, [status]=@x11, [connectionToNetworkStatus]=@x12 WHERE computerID=@x13";
            SqlCommand command = new SqlCommand(query, connection);



            command.Parameters.AddWithValue("@x1", x1);
            command.Parameters.AddWithValue("@x2", x2);
            command.Parameters.AddWithValue("@x3", x3);
            command.Parameters.AddWithValue("@x4", x4);
            command.Parameters.AddWithValue("@x5", x5);
            command.Parameters.AddWithValue("@x6", x6);
            command.Parameters.AddWithValue("@x7", x7);
            command.Parameters.AddWithValue("@x8", x8);
            command.Parameters.AddWithValue("@x9", x9);
            command.Parameters.AddWithValue("@x10", x10);
            command.Parameters.AddWithValue("@x11", x11);
            command.Parameters.AddWithValue("@x12", x12);
            command.Parameters.AddWithValue("@x13", x13);

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
        
        public void Insert(string x1, string x2, string x3, string x4, string x5, string x6, string x7, string x8, string x9, string x10, string x11, string x12)
        {
            if (labid.Text == "" || casee.Text == "" || screan.Text == "" || computername.Text == "" || processor.Text == "" || ram.Text == "" || hard.Text == "" || graphicname.Text == "" || win.Text == "" || mac.Text == "" || status.Text == "" || internetconnection.Text == "" )
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

                string sqlQuery = "INSERT INTO Computers ([labID], [caseSerialNumber], [screenSerialNumber], [computerName],[processor], [RAM], [storage], [graphicsCard],[operatingSystem], [MACAddress], [status], [connectionToNetworkStatus]) Values (@x1, @x2, @x3, @x4,@x5, @x6, @x7, @x8,@x9, @x10, @x11, @x12);";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, connection);
                sqlCommand.Parameters.AddWithValue("@x1", x1);
                sqlCommand.Parameters.AddWithValue("@x2", x2);
                sqlCommand.Parameters.AddWithValue("@x3", x3);
                sqlCommand.Parameters.AddWithValue("@x4", x4);
                sqlCommand.Parameters.AddWithValue("@x5", x5);
                sqlCommand.Parameters.AddWithValue("@x6", x6);
                sqlCommand.Parameters.AddWithValue("@x7", x7);
                sqlCommand.Parameters.AddWithValue("@x8", x8);
                sqlCommand.Parameters.AddWithValue("@x9", x9);
                sqlCommand.Parameters.AddWithValue("@x10", x10);
                sqlCommand.Parameters.AddWithValue("@x11", x11);
                sqlCommand.Parameters.AddWithValue("@x12", x12);

                sqlCommand.CommandType = CommandType.Text;

                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
                LoadData();
                this.IsEnabled = false;
                AddDonePage dataFieldsPage = new AddDonePage();
                dataFieldsPage.ShowDialog();
                if (dataFieldsPage.OkButtonClicked)
                {
                    this.IsEnabled = true;
                }

            }
        }


        #endregion



        #region Buttons


        private void Addbtn(object sender, RoutedEventArgs e)
        {
            string x1 = labid.Text;
            string x2 = casee.Text;
            string x3 = screan.Text;
            string x4 = computername.Text;
            string x5 = processor.Text;
            string x6 = ram.Text;
            string x7 = hard.Text;
            string x8 = graphicname.Text;
            string x9 = win.Text;
            string x10 = mac.Text;
            string x11 = status.Text;
            string x12 = internetconnection.Text;


            Insert(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12);
        }
        private void Updatebtn(object sender, RoutedEventArgs e)
        {
            if (computers.SelectedItem != null)
            {
                int computerid;
                DataRowView selectedRow = (DataRowView)computers.SelectedItem;
                computerid = (int)(selectedRow["computerID"]);
                string x1 = labid.Text;
                string x2 = casee.Text;
                string x3 = screan.Text;
                string x4 = computername.Text;
                string x5 = processor.Text;
                string x6 = ram.Text;
                string x7 = hard.Text;
                string x8 = graphicname.Text;
                string x9 = win.Text;
                string x10 = mac.Text;
                string x11 = status.Text;
                string x12 = internetconnection.Text;

                UpdateStudentInDatabase(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, computerid);
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
            if (computers.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)computers.SelectedItem;
                int computerid = Convert.ToInt32(selectedRow["computerID"]);

                // Assuming that Students.ItemsSource is a DataView
                DataView labsDataView = (DataView)computers.ItemsSource;

                // Find the row to delete based on the StudentID
                DataRow[] rowsToDelete = labsDataView.Table.Select($"computerID = {computerid}");

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

                    string deleteQuery = "DELETE FROM Computers WHERE computerid = @computerID";
                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@computerID", computerid);
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

            labid.Clear();
            casee.Clear();
            screan.Clear();
            computername.Clear();
            processor.Clear();
            ram.Clear();
            hard.Clear();
            graphicname.Clear();
            win.Clear();
            mac.Clear();
            status.Clear();
            internetconnection.Clear();

        }


        #endregion



        #region Back Buttons

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

        #endregion

    }
}
