// System namespaces
using System;
using System.Collections.Generic;
using System.IO;
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

// OfficeOpenXML namespaces
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;



// Microsoft SQL Server namespace
using Microsoft.Data.SqlClient;

namespace Computer_Labs_Sketch
{
    /// <summary>
    /// Interaction logic for TechnicalValidityForLab.xaml
    /// </summary>
    public partial class TechnicalValidityForLab : Window
    {
        string one, two, three, four, five ,labname;

        public TechnicalValidityForLab()
        {
            InitializeComponent();
            UpdateTextBoxBasedOnVariable();
            int labID = TechnicalValidityPage.integer_button_number;
            if (labID == 8) { GetComputerCountsInallLab(labID); }
            else { GetComputerCountsInLab(labID); }

            
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




        public void GetComputerCountsInLab(int labID)
        {
            string connectionString = "Server=DESKTOP-B019U82\\SQLEXPRESS;Database=CLS_DB;Integrated Security=SSPI;TrustServerCertificate=True;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to count the total number of computers, active computers, and non-active computers in the specified lab
                    string query = @"
                            SELECT 
                                COUNT(*) AS TotalCount,
                                SUM(CASE WHEN status = 'Active' THEN 1 ELSE 0 END) AS ActiveCount,
                                SUM(CASE WHEN status <> 'Active' THEN 1 ELSE 0 END) AS NonActiveCount
                            FROM Computers 
                            WHERE labID = @labID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@labID", labID);

                        // Execute the query and get the counts
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int totalCount = reader.GetInt32(0);        // Total number of computers
                                int activeCount = reader.GetInt32(1);       // Number of active computers
                                int nonActiveCount = reader.GetInt32(2);    // Number of non-active computers

                                // Display counts in their respective TextBoxes
                                counttextbox.Text = totalCount.ToString();
                                foundtextbox.Text = totalCount.ToString();
                                goodtextbox.Text = activeCount.ToString();
                                disabledtextbox.Text = nonActiveCount.ToString();
                                one= totalCount.ToString();
                                two = totalCount.ToString();
                                three= activeCount.ToString();
                                four= nonActiveCount.ToString();

                                // Calculate Technical Validity percentage
                                double technicalValidity = CalculateTechnicalValidity(totalCount, activeCount);

                                // Display Technical Validity in alltextbox as a whole number
                                alltextbox.Text = $"{Math.Round(technicalValidity)}%";
                                five= $"{Math.Round(technicalValidity)}%";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private double CalculateTechnicalValidity(int totalCount, int activeCount)
        {
            if (totalCount == 0) return 0; // Avoid division by zero
            return (double)activeCount / totalCount * 100;
        }




        public void GetComputerCountsInallLab(int targetLabID)
        {
            string connectionString = "Server=DESKTOP-B019U82\\SQLEXPRESS;Database=CLS_DB;Integrated Security=SSPI;TrustServerCertificate=True;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to count the total number of computers, active computers, and non-active computers in all labs
                    string query = @"
                    SELECT 
                        COUNT(*) AS TotalCount,
                        SUM(CASE WHEN status = 'Active' THEN 1 ELSE 0 END) AS ActiveCount,
                        SUM(CASE WHEN status <> 'Active' THEN 1 ELSE 0 END) AS NonActiveCount
                    FROM Computers";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute the query and get the counts
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int totalCount = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);        // Total number of computers
                                int activeCount = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);       // Number of active computers
                                int nonActiveCount = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);    // Number of non-active computers

                                // Display counts in their respective TextBoxes
                                counttextbox.Text = totalCount.ToString();
                                foundtextbox.Text = totalCount.ToString();
                                goodtextbox.Text = activeCount.ToString();
                                disabledtextbox.Text = nonActiveCount.ToString();
                                one = totalCount.ToString();
                                two = totalCount.ToString();
                                three = activeCount.ToString();
                                four = nonActiveCount.ToString();

                                // Calculate Technical Validity percentage
                                double technicalValidity = CalculateTechnicalValidity(totalCount, activeCount);

                                // Display Technical Validity in alltextbox as a whole number
                                alltextbox.Text = $"{Math.Round(technicalValidity)}%";
                                five = $"{Math.Round(technicalValidity)}%";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private double CalculateTechnicalValidityall(int totalCount, int activeCount)
        {
            if (totalCount == 0) return 0; // Avoid division by zero
            return (double)activeCount / totalCount * 100;
        }




        #region Excel File 
        private void printbtnexcel(object sender, EventArgs e)
        {
            // Set the license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Define the file path where you want to save the Excel file
            string filePath = @$"G:\Projects\TMC Projects\MyProject\Newest One\Projects\TMC Projects\Computer Labs Sketch\Computer Labs Sketch - Copyالصلاحية الفنية {labname}.xlsx";

            // Create a new Excel package
            using (ExcelPackage package = new ExcelPackage())
            {
                // Add a worksheet to the Excel workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Add a title above the table
                int titleRow = 3;
                worksheet.Cells[titleRow, 6].Value = $"الصلاحية الفنية {labname}";

                // Adjust the column width to fit the title text
                worksheet.Column(6).AutoFit();

                // Apply styles to the title
                worksheet.Cells[titleRow, 6, titleRow, 7].Merge = true; // Merge cells across the columns
                worksheet.Cells[titleRow, 6, titleRow, 7].Style.Font.Size = 20;
                worksheet.Cells[titleRow, 6, titleRow, 7].Style.Font.Bold = true;
                worksheet.Cells[titleRow, 6, titleRow, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[titleRow, 6, titleRow, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[titleRow, 6, titleRow, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[titleRow, 6, titleRow, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);
                worksheet.Cells[titleRow, 6, titleRow, 7].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);

                // Add headers to the worksheet
                worksheet.Cells[6, 6].Value = "العدد";
                worksheet.Cells[6, 7].Value = "الحالة";

                // Add some data
                worksheet.Cells[7, 6].Value = one;  // Ensure 'one' is defined elsewhere in your code
                worksheet.Cells[7, 7].Value = "قوة";

                worksheet.Cells[8, 6].Value = two;  // Ensure 'two' is defined elsewhere in your code
                worksheet.Cells[8, 7].Value = "موجود";

                worksheet.Cells[9, 6].Value = three;  // Ensure 'three' is defined elsewhere in your code
                worksheet.Cells[9, 7].Value = "صالح";

                worksheet.Cells[10, 6].Value = four;  // Ensure 'four' is defined elsewhere in your code
                worksheet.Cells[10, 7].Value = "عاطل";

                worksheet.Cells[11, 6].Value = five;  // Ensure 'five' is defined elsewhere in your code
                worksheet.Cells[11, 7].Value = "الصلاحية الفنية";

                // Apply font size and bold to headers
                worksheet.Cells[6, 6, 6, 7].Style.Font.Size = 30;
                worksheet.Cells[6, 6, 6, 7].Style.Font.Bold = true;
                worksheet.Cells[6, 6, 6, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[6, 6, 6, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                worksheet.Cells[6, 6, 6, 7].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);

                // Apply font size to data cells
                worksheet.Cells[7, 6, 11, 7].Style.Font.Size = 30;

                // Create a table from the data range
                var tableRange = worksheet.Cells[6, 6, 11, 7]; // Adjusted to match the data range
                var table = worksheet.Tables.Add(tableRange, "Table1");
                table.TableStyle = TableStyles.Medium2; // Use a predefined style for better appearance

                // Apply border to table cells
                worksheet.Cells[tableRange.Address].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[tableRange.Address].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[tableRange.Address].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[tableRange.Address].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                // AutoFit columns to the content
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Save the Excel package to the specified file path
                FileInfo excelFile = new FileInfo(filePath);
                package.SaveAs(excelFile);

                // Notify the user that the file has been saved successfully
                MessageBox.Show("Excel file has been saved successfully!");
            }
        }


        #endregion


        #region Text File
        private void printbtntxt(object sender, EventArgs e)
        {
            // Define the file path where you want to save the text file
            string filePath = @"G:\Projects\TMC Projects\MyProject\Newest One\Projects\TMC Projects\Computer Labs Sketch\Computer Labs Sketch - CopyGeneratedTable.txt";

            // Create a StringBuilder to hold the text content
            StringBuilder sb = new StringBuilder();

            // Add a title
            sb.AppendLine($"الصلاحية الفنية {labname}");
            sb.AppendLine(); // Add a blank line

            // Add headers
            sb.AppendLine("العدد\tالحالة");

            // Add data
            sb.AppendLine($"{one}\tقوة");  // Ensure 'one' is defined elsewhere in your code
            sb.AppendLine($"{two}\tموجود");  // Ensure 'two' is defined elsewhere in your code
            sb.AppendLine($"{three}\tصالح");  // Ensure 'three' is defined elsewhere in your code
            sb.AppendLine($"{four}\tعاطل");  // Ensure 'four' is defined elsewhere in your code
            sb.AppendLine($"{five}\tالصلاحية الفنية");  // Ensure 'five' is defined elsewhere in your code

            // Save the text content to the specified file path
            File.WriteAllText(filePath, sb.ToString());

            // Notify the user that the file has been saved successfully
            MessageBox.Show("Text file has been saved successfully!");
        }

        #endregion

        #region print button
        private void printbtn(object sender, EventArgs e)
        {
            // Create a FlowDocument
            FlowDocument flowDocument = new FlowDocument();
            flowDocument.PagePadding = new Thickness(50); // Add padding if needed

            // Add a title
            Paragraph titleParagraph = new Paragraph(new Run($"الصلاحية الفنية {labname}"))
            {
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                TextAlignment = TextAlignment.Center
            };
            flowDocument.Blocks.Add(titleParagraph);

            // Add a blank line
            flowDocument.Blocks.Add(new Paragraph(new Run(" ")));

            // Create a table
            Table table = new Table();
            table.CellSpacing = 0;

            // Define table columns
            TableColumn column1 = new TableColumn();
            TableColumn column2 = new TableColumn();

            table.Columns.Add(column1);
            table.Columns.Add(column2);

            // Create table row groups and add rows
            TableRowGroup rowGroup = new TableRowGroup();
            table.RowGroups.Add(rowGroup);

            // Add header row
            TableRow headerRow = new TableRow();
            rowGroup.Rows.Add(headerRow);

            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("العدد"))));
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("الحالة"))));

            // Example data - replace these with your actual data
            string[] dataLabels = { one, two, three, four, five };
            string[] dataValues = { "قوة", "موجود", "صالح", "عاطل", "الصلاحية الفنية" };

            for (int i = 0; i < dataLabels.Length; i++)
            {
                TableRow row = new TableRow();
                rowGroup.Rows.Add(row);

                row.Cells.Add(new TableCell(new Paragraph(new Run(dataLabels[i]))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(dataValues[i]))));
            }

            // Add the table to the FlowDocument
            flowDocument.Blocks.Add(table);

            // Create a PrintDialog
            PrintDialog printDialog = new PrintDialog();

            // Show the print dialog
            if (printDialog.ShowDialog() == true)
            {
                // Print the FlowDocument
                printDialog.PrintDocument(((IDocumentPaginatorSource)flowDocument).DocumentPaginator, "Print Table");
            }
        }


            #endregion




        private void UpdateTextBoxBasedOnVariable()
        {
            string text = string.Empty;

            // Check the value of the variable and set the TextBox text accordingly
            switch (TechnicalValidityPage.integer_button_number)
            {
                case 1:
                    text = "لمعمل 1";
                    break;
                case 2:
                    text = "لمعمل 2";
                    break;
                case 3:
                    text = "لمعمل 3";
                    break;
                case 4:
                    text = "لمعمل 4";
                    break;
                case 5:
                    text = "لفصل حاسب 1";
                    break;
                case 6:
                    text = "لفصل حاسب 2";
                    break;
                case 7:
                    text = "لمعمل سيسكو";
                    break; 
                case 8:
                    text = "لكل المعامل";
                    break;
                default:
                    text = string.Empty;
                    break;
            }

            labname = text;
            tvl.Text = text;
            AdjustTextBoxWidth(tvl);
        }

        private void AdjustTextBoxWidth(TextBox textBox)
        {
            // Use a TextBlock to measure the width of the current text
            TextBlock textBlock = new TextBlock
            {
                Text = textBox.Text,
                FontSize = textBox.FontSize,
                FontFamily = textBox.FontFamily,
                FlowDirection = textBox.FlowDirection,
                TextAlignment = textBox.TextAlignment
            };

            // Measure the width of the text
            Size textSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
            textBlock.Measure(textSize);

            // Set the width of the TextBox to the width required to fit the text
            textBox.Width = textBlock.DesiredSize.Width + 20; // Add some padding
        }

        private void Backbtn(object sender, RoutedEventArgs e)
        {
            TechnicalValidityPage ee = new TechnicalValidityPage();
            this.Hide();
            ee.Show();
        }

    }
}
