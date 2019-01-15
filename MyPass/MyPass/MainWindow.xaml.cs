using System.Windows;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;

namespace MyPass
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Save save = new Save();
        
        public MainWindow()
        {
            InitializeComponent();
          
        }

        class Save
        {
            public string FileName { get; set; }
            public Excel.Workbook XLWb { get; set; }
            public Excel.Worksheet Worksheet { get; set; }
            public Excel.Application XApp { get; set; }
            public int NumLastRow { get; set; }
            

           public Save()
            {
                FileName= "D:\\Repo2\\MyPass\\MyPass\\1.xlsx"; //имя Excel файла
                XApp = new Excel.Application();
                XLWb = XApp.Workbooks.Open(FileName);           //открываем файл
                Worksheet = XLWb.Sheets[1];                     //задаем странницу файла (1)
                //заполняем заголовки столбцов
                Worksheet.Cells[1, "A"] = "Site";
                Worksheet.Cells[1, "B"] = "Login";
                Worksheet.Cells[1, "C"] = "Passwords";
                NumLastRow = Worksheet.Cells[Worksheet.Rows.Count,"C"].End[Excel.XlDirection.xlUp].Row;
                
            }

            
            ~Save()
            {
                XLWb.Close(true);
                XApp.Quit();
            }

        }
         
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            save.NumLastRow++;
            save.Worksheet.Cells[save.NumLastRow, "A"] = TBSite.Text;
            save.Worksheet.Cells[save.NumLastRow, "B"] = TBLogin.Text;
            save.Worksheet.Cells[save.NumLastRow, "C"] = PBPass.Password;
            MessageBox.Show("Save data!");
        }

        private void TBSite_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Write Source")
            {
                textBox.Clear();
                textBox.Text = "www.";
            }
            
            if (textBox.Text == "Write Login")
            {
                textBox.Clear();

            }
         }
    }
}
