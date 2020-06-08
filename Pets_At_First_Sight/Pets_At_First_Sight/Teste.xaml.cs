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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data;
using System.Data.SqlClient;
using Pets_At_First_Sight.Classes;

namespace Pets_At_First_Sight
{
    /// <summary>
    /// Interaction logic for Teste.xaml
    /// </summary>
    public partial class Teste : Page
    {
        public Teste()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            SQLServerConnection.openConnection();
            SQLServerConnection.sql = "SELECT [nome] FROM projeto.ANIMAL";
            SQLServerConnection.command.CommandType = CommandType.Text;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.adapter = new SqlDataAdapter(SQLServerConnection.command);
            SQLServerConnection.datatable = new DataTable();
            SQLServerConnection.adapter.Fill(SQLServerConnection.datatable);
            datagrid.ItemsSource = SQLServerConnection.datatable.DefaultView;
            SQLServerConnection.closeConnection();
            */
            SQLServerConnection.openConnection();
            SQLServerConnection.sql = "SELECT [nome] FROM projeto.ANIMAL WHERE nome='Cão'";
            SQLServerConnection.command.CommandType = CommandType.Text;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.reader = SQLServerConnection.command.ExecuteReader();
            while (SQLServerConnection.reader.Read())
            {
                Label1.Content = SQLServerConnection.reader["nome"];
            }
            SQLServerConnection.closeConnection();
        }
    }
}
