using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassLibrary
{
    public static class DataBaseWorker
    {
        public static string connectString;
        static SqlConnection sqlConnection;
        static readonly string serverName = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Repo2\MyPass\MyPass\DatabaseMyPass.mdf;Integrated Security=True";
        static readonly string nameDataBase = ".\\DatabaseMyPass";

        public static void Conection()
        {
            connectString = serverName;

            sqlConnection = new SqlConnection(connectString);

            sqlConnection.Open();
         }

        public static void Conection(string login, string password)
        {
            connectString = "Data Source=.\\" + serverName + ";Initial Catalog=" + nameDataBase + ";User ID="+login+";Password="+password+";";

            sqlConnection = new SqlConnection(connectString);

            sqlConnection.Open();
        }

        public static void Conection(string login, string password, string serverName, string nameDataBase)
        {
            connectString = "Data Source=.\\" + serverName + ";Initial Catalog=" + nameDataBase + ";User ID=" + login + ";Password=" + password + ";";

            sqlConnection = new SqlConnection(connectString);

            sqlConnection.Open();
        }

        public static void Conection(string connectSrt)
        {
            connectString = connectSrt;

            sqlConnection = new SqlConnection(connectString);

            sqlConnection.Open();
        }


        //метод возврата из БД данных по столбцам

        public static List<string[]> GetData(string query,int coloum)
        {
            SqlCommand command = new SqlCommand(query,sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> response = new List<string[]>();
            while (reader.Read())
            {
                response.Add(new string[coloum]);
                for (int i = 0; i < coloum; i++)
                {
                    response[response.Count - 1][i] = reader[i].ToString();
                }
            }
            reader.Close();
            if (response.Count!= 0)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public static string GetData(string query)
        {
            SqlCommand command = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();

            string response = null;

            while (reader.Read())
            {
                response = reader[0].ToString();
            }

            reader.Close();
            return response;
        }

        public static void QueryWithoutResponse(string query)
        {
            SqlCommand command = new SqlCommand(query, sqlConnection);
            command.ExecuteNonQuery();
        }
               

        public static void CloseConection()
        {
            sqlConnection.Close();
        }

     
    }

}
