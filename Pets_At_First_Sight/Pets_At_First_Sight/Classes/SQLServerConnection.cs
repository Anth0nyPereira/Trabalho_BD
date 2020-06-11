using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;

namespace Pets_At_First_Sight.Classes
{
    class SQLServerConnection
    {


        public static String GetConnectionStrings()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["connectionStrings"].ToString();
            return connectionString;
        }

        public static String sql;
        public static SqlConnection connection = new SqlConnection();
        public static SqlCommand command = new SqlCommand("", connection);
        public static SqlDataReader reader;
        public static SqlDataReader reader2;
        public static DataTable datatable;
        public static SqlDataAdapter adapter;

        public static void openConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.ConnectionString = GetConnectionStrings();
                    connection.Open();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Falhou a conexão", "Tente Novamente!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void closeConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Falhou a conexão", "Tente Novamente!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

}
