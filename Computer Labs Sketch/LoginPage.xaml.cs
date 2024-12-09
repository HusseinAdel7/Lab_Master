using System;
using Microsoft.Data.SqlClient;

using Microsoft.Extensions.Configuration;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Computer_Labs_Sketch
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        bool resFromAdmin = false;

        #region Initializeing Function 

        public LoginPage()
        {
            InitializeComponent();
        }

        #endregion


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


        #region Functions 

            public bool SelectFromAdemin(string UserName, string Passwordd)
            {
                var stublishingConnection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                SqlConnection connection = new SqlConnection(stublishingConnection.GetSection("ConnectionString").Value);


                var sqlquery = "SELECT * FROM Admins WHERE username=@username AND password=@password";
                SqlCommand sqlCommand = new SqlCommand(sqlquery, connection);
                sqlCommand.Parameters.AddWithValue("@username", UserName);
                sqlCommand.Parameters.AddWithValue("@password", Passwordd);

                sqlCommand.CommandType = CommandType.Text;


                connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows == true)
                {
                    resFromAdmin = true;
                }
                else
                {
                    resFromAdmin = false;
                    Username.Text = "";
                    Password.Clear();
                }
                connection.Close();
                return resFromAdmin;
            }

            private void Username_GotFocus(object sender, RoutedEventArgs e)
            {
                Error.Content = ""; 
            }

            private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            Error.Content = ""; 
        }

        #endregion


        #region Buttons "Login"

             private void Loginbtn(object sender, RoutedEventArgs e)
        {
            string usernamee = Username.Text;
            string passwordd = Password.Password;
            if (usernamee =="" &&  passwordd == "")
            {
                Error.Content = "من فضلك ادخل اسم المستخدم و كلمة السر";
            }
            else if (usernamee == "" )
            {
                Error.Content = "من فضلك ادخل اسم المستخدم";
            }
            else if ( passwordd == "")
            {
                Error.Content = "من فضلك ادخل كلمة السر";
            }
            else
            {
                bool res = SelectFromAdemin(usernamee, passwordd);
                if (res == true)
                {
                    ChoosingPage ee = new ChoosingPage();
                    this.Hide();
                    ee.Show();
                }
                else
                {
                    Error.Content = "اسم المستخدم او كلمة السر خطأ";
                }

            }
           
        }

        #endregion

    }
}
