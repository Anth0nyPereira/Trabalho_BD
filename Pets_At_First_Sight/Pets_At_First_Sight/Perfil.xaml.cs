using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Windows.Media.Imaging;
using System.Security.Permissions;
using System.Data;
using System.Data.SqlClient;
using Pets_At_First_Sight.Classes;

namespace Pets_At_First_Sight
{
    public partial class Perfil : Page
    {
        public Perfil()
        {
            InitializeComponent();
            List<ANIMAL> op = MeusPosts();
            My_Posts.ItemsSource = op;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SQLServerConnection.openConnection();
            SQLServerConnection.sql = "SELECT* FROM projeto.CONTA WHERE username=@username2";
            SQLServerConnection.command.Parameters.AddWithValue("@username2", Container.current_user);
            SQLServerConnection.command.CommandType = CommandType.Text;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.reader = SQLServerConnection.command.ExecuteReader();
            while (SQLServerConnection.reader.Read())
            {
                nome.Content = SQLServerConnection.reader["nome"];
                email.Content = SQLServerConnection.reader["email"];
                localidade.Content = SQLServerConnection.reader["morada"];
                //Imagem.ImageSource = new BitmapImage(new Uri(SQLServerConnection.reader["fotografia"].ToString()));
            }
            SQLServerConnection.command.Parameters.Clear();
            SQLServerConnection.closeConnection();
        }

        private void Criar_Post_Click(object sender, RoutedEventArgs e)
        {
            Post cursosPage = new Post();
            this.NavigationService.Navigate(cursosPage);
        }


        private List<ANIMAL> MeusPosts() {
            List<ANIMAL> my = new List<ANIMAL>();
            SQLServerConnection.openConnection();
            SQLServerConnection.sql = "SELECT id, projeto.ANIMAL.nome AS nome, especie, raca, genero, vacina, chip, idade, projeto.ANIMAL.fotografia AS fotografia, descricao," +
                "dono_username, tipo FROM projeto.ANIMAL INNER JOIN projeto.CONTA ON projeto.ANIMAL.dono_username = projeto.CONTA.username WHERE dono_username=@username2";
            SQLServerConnection.command.Parameters.AddWithValue("@username2", Container.current_user);
            SQLServerConnection.command.CommandType = CommandType.Text;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.reader = SQLServerConnection.command.ExecuteReader();
            while (SQLServerConnection.reader.Read())
            {
                ANIMAL animal = new ANIMAL();
                animal.Id = (int)SQLServerConnection.reader["id"];
                animal.Nome = SQLServerConnection.reader["nome"].ToString();
                animal.Raca = SQLServerConnection.reader["raca"].ToString();
                animal.Url_Image = SQLServerConnection.reader["fotografia"].ToString();
                animal.Mensagem = SQLServerConnection.reader["descricao"].ToString();
                animal.User_Name = SQLServerConnection.reader["dono_username"].ToString();

                if (SQLServerConnection.reader["tipo"].ToString() == "Particular")
                {
                    animal.Tipo_Doador = "particular";
                }
                else
                {
                    animal.Tipo_Doador = "abrigo";
                }


                if (SQLServerConnection.reader["especie"].ToString() == "cao")
                {
                    animal.Especie = "cão";
                }
                else
                {
                    animal.Especie = SQLServerConnection.reader["especie"].ToString();
                }


                if (SQLServerConnection.reader["genero"].ToString() == "F")
                {
                    animal.Genero = "feminino";
                }
                else
                {
                    animal.Genero = "masculino";
                }

                if (SQLServerConnection.reader["vacina"].ToString() == "T")
                {
                    animal.Vacinas = "sim";
                }
                else
                {
                    animal.Vacinas = "não";
                }

                if (SQLServerConnection.reader["chip"].ToString() == "T")
                {
                    animal.Chip = "sim";
                }
                else
                {
                    animal.Chip = "não";
                }

                if ((int)SQLServerConnection.reader["idade"] == 1)
                {
                    animal.Idade = "1 mês";
                }
                else if ((int)SQLServerConnection.reader["idade"] < 12)
                {
                    animal.Idade = (int)SQLServerConnection.reader["idade"] + " meses";
                }
                else if ((int)SQLServerConnection.reader["idade"] < 24)
                {
                    animal.Idade = "1 ano";
                }
                else
                {
                    animal.Idade = (int)SQLServerConnection.reader["idade"] / 12 + " anos";
                }
                Container.animais.Add(animal);
                my.Add(animal);
            }
            SQLServerConnection.command.Parameters.Clear();
            SQLServerConnection.closeConnection();
            return my;
        }

        private void ViewPost(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Grid grd = (Grid)sender;
            Label id = (Label)grd.Children[4];
            Container.animal_selecionado = Int32.Parse(id.Content.ToString());
            ViewPost_Perfil post_MaisInfo = new ViewPost_Perfil();
            NavigationService.Navigate(post_MaisInfo);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Post x = new Post();
            this.NavigationService.Navigate(x);
        }

        private void PackIcon_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Container.current_user = null;
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
