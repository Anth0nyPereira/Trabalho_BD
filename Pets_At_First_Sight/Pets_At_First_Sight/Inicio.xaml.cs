using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class Inicio : Page
    {
        public Inicio()
        {

            InitializeComponent();

            Posts.ItemsSource = Container.animais;
            CollectionViewSource.GetDefaultView(Container.animais).Refresh();

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
        private void Dados_Originais()
        {
            String s = "Imagens\\";

            Container.animais.Add(new ANIMAL() { Nome = "Cãoasdasd", Idade = "5 meses", Genero = "Masculino", Raca = "Cão", Url_Image = s + "stock_dog1.jpg", User_Name = "Filipa" });
            Container.animais.Add(new ANIMAL() { Nome = "Princesa", Idade = "10 anos", Genero = "Feminino", Raca = "Gato", Url_Image = s + "stock_gato1.jpg", User_Name = "Anthony Pereira" });
            Container.animais.Add(new ANIMAL() { Nome = "Piu", Idade = "5 anos", Genero = "Feminino", Raca = "Cão", Url_Image = s + "stock_dog1.jpg", User_Name = "Anthony Pereira" });
            Container.animais.Add(new ANIMAL() { Nome = "No Name", Idade = "7 anos", Genero = "Feminino", Raca = "Gato", Url_Image = s + "stock_gato1.jpg", User_Name = "Alexandra" });
            Container.animais.Add(new ANIMAL() { Nome = "Stock#1", Idade = "8 meses", Genero = "Masculino", Raca = "Cão", Url_Image = s + "stock_dog1.jpg", User_Name = "João" });
            Posts.ItemsSource = Container.animais;
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

            Grid gr = (Grid)sender;
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
            Post_MaisInfo post_MaisInfo = new Post_MaisInfo();
            NavigationService.Navigate(post_MaisInfo);
        }
    }

    }

