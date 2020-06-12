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
            LoadGrid();
            CollectionViewSource.GetDefaultView(Container.ver_mais).Refresh();
        }

        private void LoadGrid()
        {
            Container.ver_mais.Clear();
            int id = Container.animal_selecionado;
            SQLServerConnection.openConnection();
            SQLServerConnection.sql = "SELECT * FROM projeto.VerPost(@id)";
            SQLServerConnection.command.Parameters.AddWithValue("@id", id);
            SQLServerConnection.command.CommandType = CommandType.Text;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.reader = SQLServerConnection.command.ExecuteReader();
            while (SQLServerConnection.reader.Read())
            {
                ANIMAL a = new ANIMAL();
                a.Nome = SQLServerConnection.reader["nome"].ToString();
                a.Especie = SQLServerConnection.reader["especie"].ToString();
                a.Raca = SQLServerConnection.reader["raca"].ToString();
                a.Idade = SQLServerConnection.reader["idade"].ToString();
                a.Genero = SQLServerConnection.reader["genero"].ToString();
                a.Url_Image = SQLServerConnection.reader["fotografia"].ToString();
                a.Tipo_Doador = SQLServerConnection.reader["tipo"].ToString();
                a.User_Name = SQLServerConnection.reader["dono_username"].ToString();
                a.Vacinas = SQLServerConnection.reader["vacina"].ToString();
                a.Chip = SQLServerConnection.reader["chip"].ToString();
                a.Mensagem = SQLServerConnection.reader["descricao"].ToString();
                Container.ver_mais.Add(a);
            }
            SQLServerConnection.closeConnection();
            Post_Template.ItemsSource = Container.ver_mais;
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
            int id = Container.animal_selecionado;
            SQLServerConnection.openConnection();
            SQLServerConnection.command.Parameters.Clear();
            SQLServerConnection.sql = "projeto.AdotarAnimal";
            SQLServerConnection.command.Parameters.AddWithValue("@id", id);
            SQLServerConnection.command.Parameters.AddWithValue("@adotante_username", Container.current_user);
            SQLServerConnection.command.CommandType = CommandType.StoredProcedure;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.command.ExecuteNonQuery();
            SQLServerConnection.closeConnection();
            SQLServerConnection.command.Parameters.Clear();
            icon.Kind = PackIconKind.Star;
            new Adocoes();
        }
    }
}
