using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;
using PassLibrary;

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
            public string FileName;
            public Excel.Workbook XLWb { get; set; }
            public Excel.Worksheet Worksheet { get; set; }
            public Excel.Application XApp { get; set; }
            public int NumLastRow { get; set; }
            public static int Type_pass { get; set; }
           

            public Save()
            {
                FileName = "D:\\Repo2\\MyPass\\MyPass\\1.xlsx"; //имя Excel файла
                XApp = new Excel.Application();
                
                //XLWb = XApp.Workbooks.Open(FileName);           //открываем файл
                //Worksheet = XLWb.Sheets[1];                     //задаем странницу файла (1)
                //заполняем заголовки столбцов
                //Worksheet.Cells[1, "A"] = "Site";
                //Worksheet.Cells[1, "B"] = "Login";
                //Worksheet.Cells[1, "C"] = "Passwords";
                //NumLastRow = Worksheet.Cells[Worksheet.Rows.Count,"C"].End[Excel.XlDirection.xlUp].Row;
                Type_pass = 0;
            }
            
            ~Save()
            {
                XApp.Quit();
            }

        }
               

        //запись данных в файл
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            save.XLWb = save.XApp.Workbooks.Open(save.FileName);
            save.Worksheet = save.XLWb.Sheets[1];
            save.NumLastRow = save.Worksheet.Cells[save.Worksheet.Rows.Count, "C"].End[Excel.XlDirection.xlUp].Row;
            save.NumLastRow++;
            save.Worksheet.Cells[save.NumLastRow, "A"] = TBSite.Text;
            save.Worksheet.Cells[save.NumLastRow, "B"] = TBLogin.Text;
            save.Worksheet.Cells[save.NumLastRow, "C"] = PBPass.Password;
            MessageBox.Show("Save data!");
            save.XLWb.Close(true);
            
        }
        //очистка TextBox 
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
            if (textBox.Text == "What to find?")
            {
                textBox.Clear();
                textBox.Text = "www.";
                TBSite.Clear();
                TBLogin.Clear();
                PBPass.Clear();
            }
            
         }
        //генератор паролей
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            char simvol;
            System.Random random = new System.Random();
            for (int i = 0; i < Save.Type_pass; i++)
            {
                simvol = (char)random.Next(33, 127);
                PBPass.Password += simvol;
            }
        }
        //сложность генератора пароля
        private void RadioButton_Checked_easy(object sender, RoutedEventArgs e)
        {
            Save.Type_pass = 8;
            PBPass.Password = "";
        }

        private void RadioButton_Checked_middle(object sender, RoutedEventArgs e)
        {
            Save.Type_pass = 12;
            PBPass.Password = "";
        }

        private void RadioButton_Checked_heavy(object sender, RoutedEventArgs e)
        {
            Save.Type_pass = 16;
            PBPass.Password = "";
        }
        //показ базы паролей
        private void Button_Click_ShowDB(object sender, RoutedEventArgs e)
        {
            //if (File.Exists(save.FileName))
            //{
            //    Process.Start(save.FileName);
            //}
            //else
            //{
            //    MessageBox.Show("Not file!");
            //}

            DataBaseWorker.Conection();
            string mas = DataBaseWorker.GetData("SELECT Login FROM DBPass WHERE Password = 12345");
            MessageBox.Show(mas);
            DataBaseWorker.CloseConection();
        }

        private void Find(object sender, RoutedEventArgs e)
        {
            save.XLWb = save.XApp.Workbooks.Open(save.FileName);
            save.Worksheet = save.XLWb.Sheets[1];
            string data = SearchSite.Text;
            string Site = "";
            string Login = "";
            string Pas = "";
            int i = 2;
            int g = 1;
            bool flag = false;
                       
                while (save.Worksheet.Cells[i, g].Value2!=null)
                {
                    if (save.Worksheet.Cells[i, g].Value2 == data)
                    {
                        Site = save.Worksheet.Cells[i, g].Value2;
                        Login = save.Worksheet.Cells[i, g+1].Value2;
                        Pas = save.Worksheet.Cells[i, g + 2].Value2;

                        TBSite.Text = Site;
                        TBLogin.Text = Login;
                        PBPass.Password = Pas;
                        flag = true;
                    break;
                    }
                    i++;
                }

            if (flag!=true)
            {
                MessageBox.Show("Not found");
            }
            SearchSite.Text = "What to find?";
            save.XLWb.Close(true);

        }

        //показать пароль
        private void ChekShowPass_Checked(object sender, RoutedEventArgs e)
        {
            TBShowPass.Text = PBPass.Password;
            PBPass.Visibility = Visibility.Hidden;
            TBShowPass.Visibility = Visibility.Visible;
        }

        private void ChekShowPass_Unchecked(object sender, RoutedEventArgs e)
        {
            TBShowPass.Visibility = Visibility.Collapsed;
            PBPass.Visibility = Visibility.Visible;
        }
    }
}
