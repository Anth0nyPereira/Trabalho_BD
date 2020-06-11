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
    public partial class LojaFiltros : Page
    {
        public LojaFiltros()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("A sua seleção será descartada. Pretende continuar?", "Voltar", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Loja loja = new Loja();
                    this.NavigationService.Navigate(loja);
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<Produto> Filtrar = new List<Produto>();
            String tipo = Tipo.SelectedItem.ToString();
            int preco = Int32.Parse(PrecoMax.Text.ToString());
            System.DBNull empresa = DBNull.Value;

            SQLServerConnection.openConnection();
            SQLServerConnection.sql = "SELECT* FROM projeto.FiltrarProduto(@tipo , @preco, @empresa)";
            SQLServerConnection.command.Parameters.AddWithValue("@especie", tipo == null ? (object)DBNull.Value : tipo);
            SQLServerConnection.command.Parameters.AddWithValue("@preco", preco == 0 ? (object)DBNull.Value : preco);
            SQLServerConnection.command.Parameters.AddWithValue("@empresa", empresa);
            SQLServerConnection.command.CommandType = CommandType.Text;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.reader = SQLServerConnection.command.ExecuteReader();
            while (SQLServerConnection.reader.Read())
            {
                Produto prod = new Produto();
                prod.ID = (int)SQLServerConnection.reader["id"];
                prod.NomeProduto = SQLServerConnection.reader["p_name"].ToString();
                prod.TipoServico = SQLServerConnection.reader["tipo"].ToString();
                prod.Stock = (int)SQLServerConnection.reader["quantidade"];
                prod.Preco = SQLServerConnection.reader["preco"].ToString();
                prod.Empresa = SQLServerConnection.reader["nome"].ToString();

                Filtrar.Add(prod);
            }
            SQLServerConnection.closeConnection();

            Loja loja = new Loja();
            loja.My_Loja.ItemsSource = Filtrar;
            this.NavigationService.Navigate(loja);
        }
    }
}
