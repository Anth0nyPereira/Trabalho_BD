using MaterialDesignThemes.Wpf;
using Pets_At_First_Sight.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    public partial class Inicio : Page
    {
        public Inicio()
        {

            InitializeComponent();
            GetAnimals();
            Posts.ItemsSource = Container.animais;
            CollectionViewSource.GetDefaultView(Container.animais).Refresh();

        }

        public void GetAnimals()
        {
            Container.animais.Clear();

            SQLServerConnection.openConnection();

            SQLServerConnection.sql = "SELECT * FROM projeto.LISTAR_ANIMAIS;";
            SQLServerConnection.command.CommandType = CommandType.Text;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.reader = SQLServerConnection.command.ExecuteReader();
            SQLServerConnection.command.Parameters.Clear();
           
            while (SQLServerConnection.reader.Read())
            {
                ANIMAL animal = new ANIMAL();
                animal.Id = (int)SQLServerConnection.reader["id"];
                animal.Nome = SQLServerConnection.reader["nome"].ToString();
                animal.Especie = SQLServerConnection.reader["especie"].ToString();
                animal.Url_Image = SQLServerConnection.reader["fotografia"].ToString();
                animal.Mensagem = SQLServerConnection.reader["descricao"].ToString();
                animal.User_Name = SQLServerConnection.reader["dono_username"].ToString();


                if(SQLServerConnection.reader["tipo"].ToString() == "Particular")
                {
                    animal.Tipo_Doador = "particular";
                }
                else
                {
                    animal.Tipo_Doador = "abrigo";
                }


                if (SQLServerConnection.reader["raca"].ToString() == "cao")
                {
                    animal.Raca = "cão";
                }
                else
                {
                    animal.Raca = SQLServerConnection.reader["raca"].ToString();
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
                else if((int)SQLServerConnection.reader["idade"] < 24)
                {
                    animal.Idade = "1 ano";
                }
                else
                {
                    animal.Idade = (int)SQLServerConnection.reader["idade"] / 12 + " anos";
                }

                animal.Adotado = false;
                animal.Favorito = false;

                Container.animais.Add(animal);
            }
            SQLServerConnection.closeConnection();

        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            List<ANIMAL> Filtrar = new List<ANIMAL>();

            if (e.Key == Key.Return && pesquisa.Text != "")
            {

                foreach (ANIMAL m in Container.animais)
                {
                    if (m.User_Name.Equals(pesquisa.Text) | m.Idade.Equals(pesquisa.Text) | m.Raca.Equals(pesquisa.Text) | m.Genero.Equals(pesquisa.Text))
                    {
                        Filtrar.Add(m);
                    }
                }
                Posts.ItemsSource = Filtrar;

            }
            else if (e.Key == Key.Return && pesquisa.Text == "")
            {
                Posts.ItemsSource = null;
                Posts.ItemsSource = Container.animais;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InicioFiltros inicioFiltros = new InicioFiltros();
            NavigationService.Navigate(inicioFiltros);
        }
        Boolean flagAdo = true;

        public void Adopt(object sender, RoutedEventArgs e)
        {
            Button i = (Button)sender;
            PackIcon b = (PackIcon)i.Content;
            StackPanel s = (StackPanel)i.Parent;
            Grid gr = (Grid)s.Parent;
            Image u = (Image)gr.Children[5];
            String x = u.Source.ToString();
            Label r = (Label)gr.Children[1];
            Label n = (Label)gr.Children[2];
            Label y = (Label)gr.Children[3];
            Label g = (Label)gr.Children[4];

            String Nome_Bicho = n.Content.ToString();
            String Idades = y.Content.ToString();
            String Raca = r.Content.ToString();
            String genero = g.Content.ToString();

            foreach (ANIMAL zzs in Container.animais)
            {
                if (zzs.Nome == Nome_Bicho && zzs.Idade == Idades)
                {
                    if (zzs.Adotado == false)
                    {
                        zzs.Adotado = true;
                        Container.adocoes.Add(zzs);

                        b.BeginInit();
                        b.Kind = PackIconKind.Star;
                        b.EndInit();

                        new Adocoes();
                        break;
                    }
                    else if (zzs.Adotado)
                    {
                        Container.adocoes.Remove(zzs);
                        zzs.Adotado = false;

                        b.BeginInit();
                        b.Kind = PackIconKind.StarOutline;
                        b.EndInit();

                        new Adocoes();
                        break;
                    }


                }

            }
        }

        Boolean flagFav = true;
        public void Fave(object sender, RoutedEventArgs e)
        {

            Button i = (Button)sender;
            PackIcon b = (PackIcon)i.Content;
            StackPanel s = (StackPanel)i.Parent;
            Grid gr = (Grid)s.Parent;
            Image u = (Image)gr.Children[5];
            String x = u.Source.ToString();
            Label r = (Label)gr.Children[1];
            Label n = (Label)gr.Children[2];
            Label y = (Label)gr.Children[3];
            Label g = (Label)gr.Children[4];

            String Nome_Bicho = n.Content.ToString();
            String Idades = y.Content.ToString();
            String Raca = r.Content.ToString();
            String genero = g.Content.ToString();

            foreach (ANIMAL zzs in Container.animais)
            {
                if (zzs.Nome == Nome_Bicho && zzs.Idade == Idades)
                {
                    if (zzs.Favorito == false)
                    {
                        zzs.Favorito = true;
                        Container.favoritos.Add(zzs);

                        b.BeginInit();
                        b.Kind = PackIconKind.Heart;
                        b.EndInit();

                        new Favoritos();
                        break;
                    }
                    else if (zzs.Favorito == true)
                    {
                        Container.favoritos.Remove(zzs);
                        zzs.Favorito = false;

                        b.BeginInit();
                        b.Kind = PackIconKind.HeartOutline;
                        b.EndInit();

                        new Favoritos();
                        break;
                    }


                }

            }
        }

        private void ViewPost(object sender, MouseButtonEventArgs e)
        {
           Grid grd = (Grid)sender;
           Label id = (Label)grd.Children[6];
           Container.animal_selecionado = Int32.Parse(id.Content.ToString());
           Post_MaisInfo post_MaisInfo = new Post_MaisInfo();
           NavigationService.Navigate(post_MaisInfo);
        }
    }

    }

