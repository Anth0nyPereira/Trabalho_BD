using System;
using MaterialDesignThemes.Wpf;
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
    public partial class Post_MaisInfo : Page
    {
        public Post_MaisInfo()
        {
            InitializeComponent();
            //CollectionViewSource.GetDefaultView(Container.animal_selecionado).Refresh();

        }

        private void LoadGrid(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("entrei na funcao view post");
            Grid grid = (Grid)sender;
            Label especie = (Label)grid.Children[7];
            Label nome = (Label)grid.Children[9];
            Label raca = (Label)grid.Children[11];
            Label idade = (Label)grid.Children[13];
            Label genero = (Label)grid.Children[15];
            Label imagem = (Label)grid.Children[16];
            Label tipo = (Label)grid.Children[18];
            Label nomeDoador = (Label)grid.Children[20];
            Label vacinas = (Label)grid.Children[22];
            Label chip = (Label)grid.Children[24];
            Border border = (Border)grid.Children[25];
            Label mensagem = (Label)border.Child;

            int id = Container.animal_selecionado;
            SQLServerConnection.openConnection();
            SQLServerConnection.sql = "SELECT FROM projeto.VerPost(@id)";
            SQLServerConnection.command.Parameters.AddWithValue("@id", id);
            SQLServerConnection.command.CommandType = CommandType.Text;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.reader = SQLServerConnection.command.ExecuteReader();
            while (SQLServerConnection.reader.Read())
            {
                nome.Content = SQLServerConnection.reader["nome"];
                especie.Content = SQLServerConnection.reader["especie"];
                raca.Content = SQLServerConnection.reader["raca"];
                idade.Content = SQLServerConnection.reader["idade"];
                genero.Content = SQLServerConnection.reader["genero"];
                imagem.Content = SQLServerConnection.reader["fotografia"];
                tipo.Content = SQLServerConnection.reader["tipo"];
                nomeDoador.Content = SQLServerConnection.reader["dono_username"];
                vacinas.Content = SQLServerConnection.reader["vacina"];
                chip.Content = SQLServerConnection.reader["chip"];
                mensagem.Content = SQLServerConnection.reader["descricao"];

            }
            SQLServerConnection.closeConnection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Inicio inicio = new Inicio();
            Container.animal_selecionado = 0;
            this.NavigationService.Navigate(inicio);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button starbutton = (Button)sender;
            PackIcon icon = (PackIcon)starbutton.Content;
            Grid gr = (Grid)starbutton.Parent;
            Label id = (Label)gr.Children[3];

            SQLServerConnection.openConnection();
            SQLServerConnection.sql = "projeto.AdotarAnimal";
            SQLServerConnection.command.Parameters.AddWithValue("@id", id);
            SQLServerConnection.command.Parameters.AddWithValue("@adotante_username", Container.current_user);
            SQLServerConnection.command.CommandType = CommandType.StoredProcedure;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.command.ExecuteNonQuery();
            SQLServerConnection.closeConnection();
            SQLServerConnection.command.Parameters.Clear();
            icon.Kind = PackIconKind.Star;

        }
    }
}
