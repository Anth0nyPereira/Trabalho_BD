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
    public partial class Loja : Page
    {
        public Loja()
        {
            InitializeComponent();
            GetProducts();
            CollectionViewSource.GetDefaultView(Container.produtos).Refresh();
        }

        private void GetProducts()
        {
            Container.produtos.Clear();

            SQLServerConnection.openConnection();

            SQLServerConnection.sql = "SELECT * FROM projeto.LISTAR_PRODUTOS;";
            SQLServerConnection.command.CommandType = CommandType.Text;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.reader = SQLServerConnection.command.ExecuteReader();
            SQLServerConnection.command.Parameters.Clear();

            while (SQLServerConnection.reader.Read())
            {
                Produto prod = new Produto();
                prod.ID = (int)SQLServerConnection.reader["p_id"];
                prod.NomeProduto = SQLServerConnection.reader["p_name"].ToString();
                prod.TipoServico = SQLServerConnection.reader["tipo"].ToString();
                prod.Stock = (int)SQLServerConnection.reader["quantidade"];
                prod.Preco = SQLServerConnection.reader["preco"].ToString();
                prod.Empresa = SQLServerConnection.reader["nome"].ToString();

                Container.produtos.Add(prod);
            }
            SQLServerConnection.closeConnection();
            My_Loja.ItemsSource = Container.produtos;
        }


        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            List<Produto> listaFiltrar = new List<Produto>();
            string input = pesquisa.Text;
            if (input != "")
            {
                SQLServerConnection.openConnection();
                SQLServerConnection.sql = "SELECT FROM projeto.FiltrarProduto_Pesquisar(@valor);";
                SQLServerConnection.command.Parameters.AddWithValue("@valor", input);
                SQLServerConnection.command.CommandType = CommandType.Text;
                SQLServerConnection.command.CommandText = SQLServerConnection.sql;
                SQLServerConnection.reader = SQLServerConnection.command.ExecuteReader();
                SQLServerConnection.command.Parameters.Clear();

                while (SQLServerConnection.reader.Read())
                {
                    Produto prod = new Produto();
                    prod.ID = (int)SQLServerConnection.reader["id"];
                    prod.NomeProduto = SQLServerConnection.reader["p_name"].ToString();
                    prod.TipoServico = SQLServerConnection.reader["tipo"].ToString();
                    prod.Stock = (int)SQLServerConnection.reader["quantidade"];
                    prod.Preco = SQLServerConnection.reader["preco"].ToString();
                    prod.Empresa = SQLServerConnection.reader["nome"].ToString();

                    listaFiltrar.Add(prod);
                }
                My_Loja.ItemsSource = listaFiltrar;
                SQLServerConnection.closeConnection();

            }
            else
            {
                GetProducts();
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LojaFiltros lojaFiltros = new LojaFiltros();
            NavigationService.Navigate(lojaFiltros);
        }

        private void Buy(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Grid gr = (Grid)button.Parent;
            Label id = (Label)gr.Children[5];
            Container.produto_selecionado = (int)id.Content;
            CompraProduto cp = new CompraProduto();
            this.NavigationService.Navigate(cp);
        }
    }
}
