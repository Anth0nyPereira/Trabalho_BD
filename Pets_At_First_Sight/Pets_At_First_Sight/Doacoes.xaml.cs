using Pets_At_First_Sight.Classes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pets_At_First_Sight
{
    public partial class Doacoes : Page
    {
        public Doacoes()
        {
            InitializeComponent();
            List<DONATIVO> donativos = MeusDonativos();
            My_Doacoes.ItemsSource = donativos;
        }

        private List<DONATIVO> MeusDonativos()
        {
            List<DONATIVO> my = new List<DONATIVO>();

            SQLServerConnection.openConnection();
            SQLServerConnection.sql = "SELECT * FROM projeto.ListarDonativos(@username)";
            SQLServerConnection.command.Parameters.AddWithValue("@username", Container.current_user);
            SQLServerConnection.command.CommandType = CommandType.Text;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.reader = SQLServerConnection.command.ExecuteReader();
            while (SQLServerConnection.reader.Read())
            {
                DONATIVO don = new DONATIVO
                {
                    MeioPagamento = SQLServerConnection.reader["meio"].ToString(),
                    Quantia = (decimal)SQLServerConnection.reader["quantia"],
                    TipoAlimento = SQLServerConnection.reader["tipo"].ToString(),
                    QuantidadeAlimento = SQLServerConnection.reader["QUANTIDADE"].ToString(),
                };

                my.Add(don);
            }
            SQLServerConnection.command.Parameters.Clear();
            SQLServerConnection.closeConnection();
            return my;
        }

        private void Add_doacao(object sender, RoutedEventArgs e)
        {
            NewDonativo donation = new NewDonativo();
            this.NavigationService.Navigate(donation);
        }
    }
}
