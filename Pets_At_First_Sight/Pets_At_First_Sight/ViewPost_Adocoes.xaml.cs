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
using Pets_At_First_Sight.Classes;
using System.Data;

namespace Pets_At_First_Sight
{
    public partial class ViewPost_Adocoes : Page
    {
        public ViewPost_Adocoes()
        {
            InitializeComponent();
        }

        private void LoadGrid(object sender, RoutedEventArgs e)
        {
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
            Adocoes ad = new Adocoes();
            this.NavigationService.Navigate(ad);
        }
    }
}
