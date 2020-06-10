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
            dados_perfil();
            List<ANIMAL> op = MeusPosts();
            My_Posts.ItemsSource = op;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SQLServerConnection.openConnection();
            SQLServerConnection.sql = "SELECT* FROM projeto.CONTA WHERE username=@username2";
            SQLServerConnection.command.Parameters.AddWithValue("@username2", Container.utilizador_logado.First().Username.ToString());
            SQLServerConnection.command.CommandType = CommandType.Text;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.reader = SQLServerConnection.command.ExecuteReader();
            while (SQLServerConnection.reader.Read())
            {
                nome.Content = SQLServerConnection.reader["nome"];
                email.Content = SQLServerConnection.reader["email"];
                localidade.Content = SQLServerConnection.reader["morada"];
            }
            SQLServerConnection.closeConnection();
        }

        bool check = true;
        private void dados_perfil()
        {
            String content = Container.utilizador_logado.First().Foto.ToString();
            String z = null;
            if (content.Contains("pack://application:"))
            {
                z = content;
            } else
            {
                z = "pack://application:,,,/Imagens/" + content;
            }

            
            if (content.Contains("Imagens"))
            {
                int found = content.IndexOf("Imagens");
                String subs = content.Substring(found);
                z = "pack://application:,,,/Pets_At_First_Sight;component/" + subs;
            }
            
            Imagem.ImageSource= new BitmapImage(new Uri(z, UriKind.RelativeOrAbsolute));
            username.Content = Container.utilizador_logado.First().Username.ToString();
        }


        private void Criar_Post_Click(object sender, RoutedEventArgs e)
        {
            Post cursosPage = new Post();
            this.NavigationService.Navigate(cursosPage);
        }


        private List<ANIMAL> MeusPosts() {
            List<ANIMAL> my = new List<ANIMAL>();
            foreach (ANIMAL m in Container.animais)
            {
              
                if (m.User_Name.Equals(username.Content.ToString()))
                {
                    my.Add(m);
                }
            }
            return my;
        }

        private void ViewPost(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Grid gr = (Grid)sender;
            Image u = (Image)gr.Children[0];
            Label r = (Label)gr.Children[1];
            Label n = (Label)gr.Children[2];
            Label y = (Label)gr.Children[3];
            Label g = (Label)gr.Children[4];

            String Nome_Animal = n.Content.ToString();
            String Idades = y.Content.ToString();
            String Raca = r.Content.ToString();
            String genero = g.Content.ToString();

            foreach (ANIMAL animal in Container.animais)
            {
                if (animal.Nome == Nome_Animal && animal.Idade == Idades && animal.Raca == Raca && animal.Genero == genero)
                {
                    Container.animal_selecionado.Add(animal);
                }
            }
            ViewPost_Perfil post = new ViewPost_Perfil();
            NavigationService.Navigate(post);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Post x = new Post();
            this.NavigationService.Navigate(x);
        }

        private void PackIcon_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Container.utilizador_logado.Clear();
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
