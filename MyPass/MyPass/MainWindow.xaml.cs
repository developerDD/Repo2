using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;
using PassLibrary;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Windows.Data;

namespace MyPass
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //работа с Excel
        //Save save = new Save();
       public struct Info
        {
            public string a { get; set; }
            public string b { get; set; }
            public string v{ get; set; }


};

        public MainWindow()
        {
            InitializeComponent();
                     
        }
       
        class Save
        {
            //работа с Excel
            //public string FileName;
            //public Excel.Workbook XLWb { get; set; }
            //public Excel.Worksheet Worksheet { get; set; }
            //public Excel.Application XApp { get; set; }
            //public int NumLastRow { get; set; }
            public static int Type_pass { get; set; }
           

            public Save()
            {
                //FileName = "D:\\Repo2\\MyPass\\MyPass\\1.xlsx"; //имя Excel файла
                //XApp = new Excel.Application();
                
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
                //XApp.Quit();
            }

        }

        //запись данных в файл
        //запись в базу данных
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //работа с Excel
            //save.XLWb = save.XApp.Workbooks.Open(save.FileName);
            //save.Worksheet = save.XLWb.Sheets[1];
            //save.NumLastRow = save.Worksheet.Cells[save.Worksheet.Rows.Count, "C"].End[Excel.XlDirection.xlUp].Row;
            //save.NumLastRow++;
            //save.Worksheet.Cells[save.NumLastRow, "A"] = TBSite.Text;
            //save.Worksheet.Cells[save.NumLastRow, "B"] = TBLogin.Text;
            //save.Worksheet.Cells[save.NumLastRow, "C"] = PBPass.Password;

            //MessageBox.Show("Save data!");
            //save.XLWb.Close(true);

            // для удаления из базы данных DataBaseWorker.QueryWithoutResponse("DELETE FROM [DBPass] WHERE id=1");
            DataBaseWorker.Conection();
            string site = TBSite.Text;
            string log = TBLogin.Text;
            string pas = PBPass.Password;
            if (site!= "Write Source"&&log!= "Write Login"&& log != "")
            {
                string sqlExpression = String.Format("INSERT INTO DBPass(LoginSite,Login,Password) VALUES ('{0}', '{1}','{2}')", site, log, pas);
                DataBaseWorker.QueryWithoutResponse(sqlExpression);
                DataBaseWorker.CloseConection();
                TBSite.Text = "Write Source";
                TBLogin.Text = "Write Login";
                PBPass.Clear();
            }
            else
            {
                MessageBox.Show("Specify data to write", "Attantion", MessageBoxButton.OK, MessageBoxImage.Error);
            }


           
        }
        //очистка TextBox 
        private void TBSite_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Write Source")
            {
                textBox.Clear();
                textBox.Text = "www.";
                SearchSite.Text = "What to find?";
            }
            if (textBox.Text == "Write Login")
            {
                textBox.Clear();

            }
            if (textBox.Text == "What to find?")
            {
                textBox.Clear();
                textBox.Text = "www.";
                TBSite.Text = "Write Source";
                TBLogin.Text = "Write Login";
                PBPass.Clear();
            }
         }
        //генератор паролей
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Save.Type_pass != 0)
            {
                char simvol;
                System.Random random = new System.Random();
                for (int i = 0; i < Save.Type_pass; i++)
                {
                    simvol = (char)random.Next(33, 127);
                    PBPass.Password += simvol;
                }
                Save.Type_pass = 0;
            }
            else if (PBPass.Password != "" && Save.Type_pass == 0)
            {
                MessageBox.Show("Pass is set", "Attantion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Pass not set", "Attantion", MessageBoxButton.OK, MessageBoxImage.Error);
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
            //работа с Excel
            //if (File.Exists(save.FileName))
            //{
            //    Process.Start(save.FileName);
            //}
            //else
            //{
            //    MessageBox.Show("Not file!");
            //}

            DataBaseWorker.Conection();
            List<string[]>mas = DataBaseWorker.GetData("SELECT * FROM DBPass",4);

            Info info = new Info();
           
            if (mas != null)
            {
                foreach (var item in mas)
                {

                    info.a = item[1];
                    info.b = item[2];
                    info.v = item[3];
                    
                    listDB.Items.Add(info);
                }
                Grid_ListView.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("DataBase is empty!");
            }
            DataBaseWorker.CloseConection();
        }
        // поиск сайта по базе
        private void Find(object sender, RoutedEventArgs e)
        {
            //работа с Excel
            //save.XLWb = save.XApp.Workbooks.Open(save.FileName);
            //save.Worksheet = save.XLWb.Sheets[1];
            //string data = SearchSite.Text;
            //string Site = "";
            //string Login = "";
            //string Pas = "";
            //int i = 2;
            //int g = 1;
            //bool flag = false;

            //    while (save.Worksheet.Cells[i, g].Value2!=null)
            //    {
            //        if (save.Worksheet.Cells[i, g].Value2 == data)
            //        {
            //            Site = save.Worksheet.Cells[i, g].Value2;
            //            Login = save.Worksheet.Cells[i, g+1].Value2;
            //            Pas = save.Worksheet.Cells[i, g + 2].Value2;

            //            TBSite.Text = Site;
            //            TBLogin.Text = Login;
            //            PBPass.Password = Pas;
            //            flag = true;
            //        break;
            //        }
            //        i++;
            //    }

            //if (flag!=true)
            //{
            //    MessageBox.Show("Not found");
            //}
            //SearchSite.Text = "What to find?";
            //save.XLWb.Close(true);

            DataBaseWorker.Conection();
            List<string[]> mas = DataBaseWorker.GetData("SELECT * FROM DBPass", 4);
            bool flag = false;
            foreach (var item in mas)
            {
                if (item[1]==SearchSite.Text)
                {
                    TBSite.Text = item[1];
                    TBLogin.Text = item[2];
                    PBPass.Password = item[3];
                    flag = true;
                    break;
                }
            }
            if (flag==false)
            {
                MessageBox.Show("Not found", "Attantion",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            DataBaseWorker.CloseConection();

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
        //закрытие listview
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Grid_ListView.Visibility = Visibility.Hidden;
            //очищаем лист))
            listDB.Items.Clear();
        }
        //очистить базу данных
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
                    
            result = MessageBox.Show("Delete DataBase?", "Attantion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result==MessageBoxResult.Yes)
            {
                DataBaseWorker.Conection();
                string qury = "TRUNCATE TABLE DBPass";
                DataBaseWorker.QueryWithoutResponse(qury);
                DataBaseWorker.CloseConection();
            }
           
            
        }
        // открыть найденый адрес в браузере
        private void StartBroweser(object sender, RoutedEventArgs e)
        {
            if (TBSite.Text!="www."&& TBSite.Text!=null&& TBSite.Text != "Write Source")
            {
               

                Process.Start(TBSite.Text);
                TBSite.Text = "Write Source";
                TBLogin.Text = "Write Login";
                SearchSite.Text = "What to find?";
                PBPass.Clear();
            }
            else
            {
                MessageBox.Show("Site address is not specified", "Attantion",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PBPass_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(PBPass))
            {
                SearchSite.Text = "What to find?";
            }
        }
    }
}
