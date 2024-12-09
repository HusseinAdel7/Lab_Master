using System;
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
    /// Interaction logic for AlreadyinDBPage.xaml
    /// </summary>
    public partial class AlreadyinDBPage : Window
    {
        public bool OkButtonClicked { get; private set; }

        public AlreadyinDBPage()
        {
            InitializeComponent();
        }
        private void Okbtn(object sender, RoutedEventArgs e)
        {
            OkButtonClicked = true;

            // Close Form2
            this.Close();
        }
    }
}
