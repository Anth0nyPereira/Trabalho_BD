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

namespace Pets_At_First_Sight
{
    public partial class ViewPost_Favoritos : Page
    {
        public ViewPost_Favoritos()
        {
            InitializeComponent();
            Post_Template.ItemsSource = Container.animal_selecionado;
            CollectionViewSource.GetDefaultView(Container.animal_selecionado).Refresh();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Favoritos favs = new Favoritos();
            Container.animal_selecionado.RemoveAt(0);
            this.NavigationService.Navigate(favs);
        }

        Boolean flagAdo = true;

        private void Adopt(object sender, RoutedEventArgs e)
        {
            Button i = (Button)sender;
            PackIcon b = (PackIcon)i.Content;
            StackPanel s = (StackPanel)i.Parent;
            Grid gr = (Grid)s.Parent;
            Image u = (Image)gr.Children[1];
            String x = u.Source.ToString(); 
            Label r = (Label)gr.Children[2];
            Label n = (Label)gr.Children[3];
            Label y = (Label)gr.Children[4];
            Label g = (Label)gr.Children[5];

            String Nome_Bicho = n.Content.ToString();
            String Idades = y.Content.ToString();
            String Raca = r.Content.ToString();
            String genero = g.Content.ToString();


            if (flagAdo)
            {
                foreach (ANIMAL zzs in Container.animais)
                {
                    if (zzs.Nome == Nome_Bicho && zzs.Idade == Idades)
                    {
                        Container.adocoes.Add(zzs);

                        b.BeginInit();
                        b.Kind = PackIconKind.Star;
                        b.EndInit();
                        flagAdo = false;
                        new Adocoes();
                        break;

                    }

                }

            }
            else if (!flagAdo)
            {
                foreach (ANIMAL zzs in Container.animais)
                {
                    if (zzs.Nome == Nome_Bicho && zzs.Idade == Idades)
                    {
                        Container.adocoes.Remove(zzs);

                        b.BeginInit();
                        b.Kind = PackIconKind.StarOutline;
                        b.EndInit();
                        flagAdo = true;
                        new Adocoes();
                        break;

                    }

                }
            }


        }

        Boolean flagFav = true;
        private void Fave(object sender, RoutedEventArgs e)
        {
            Button i = (Button)sender;
            PackIcon b = (PackIcon)i.Content;
            StackPanel s = (StackPanel)i.Parent;
            Grid gr = (Grid)s.Parent;
            Image u = (Image)gr.Children[1];
            String x = u.Source.ToString();
            Label r = (Label)gr.Children[2];
            Label n = (Label)gr.Children[3];
            Label y = (Label)gr.Children[4];
            Label g = (Label)gr.Children[5];

            String Nome_Bicho = n.Content.ToString();
            String Idades = y.Content.ToString();
            String Raca = r.Content.ToString();
            String genero = g.Content.ToString();
            if (flagFav)
            {
                foreach (ANIMAL zzs in Container.animais)
                {
                    if (zzs.Nome == Nome_Bicho && zzs.Idade == Idades)
                    {
                        Container.favoritos.Add(zzs);

                        b.BeginInit();
                        b.Kind = PackIconKind.Heart;
                        b.EndInit();
                        flagFav = false;
                        new Favoritos();
                        break;

                    }

                }

            }
            else if (!flagFav)
            {
                foreach (ANIMAL zzs in Container.animais)
                {
                    if (zzs.Nome == Nome_Bicho && zzs.Idade == Idades)
                    {
                        Container.favoritos.Remove(zzs);

                        b.BeginInit();
                        b.Kind = PackIconKind.HeartOutline;
                        b.EndInit();
                        flagFav = true;
                        new Favoritos();
                        break;

                    }

                }
            }


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button starbutton = (Button)sender;
            PackIcon icon = (PackIcon)starbutton.Content;
            Grid gr = (Grid)starbutton.Parent;
            Label name_label = (Label)gr.Children[6];
            Label age_label = (Label)gr.Children[8];

            String Name = name_label.Content.ToString();
            String Age = age_label.Content.ToString();

            foreach (ANIMAL zzs in Container.animais)
            {
                if (zzs.Nome == Name && zzs.Idade == Age)
                {
                    if (zzs.Favorito)
                    {
                        Container.favoritos.Remove(zzs);
                        zzs.Favorito = false;

                        icon.BeginInit();
                        icon.Kind = PackIconKind.HeartOutline;
                        icon.EndInit();

                        new ViewPost_Favoritos();
                        new Favoritos();
                        new Inicio();

                        break;
                    }
                    else
                    {
                        zzs.Favorito = true;
                        Container.favoritos.Add(zzs);

                        icon.BeginInit();
                        icon.Kind = PackIconKind.Heart;
                        icon.EndInit();

                        new ViewPost_Favoritos();
                        new Favoritos();
                        new Inicio();

                        break;

                    }

                }

            }
        }
    }
}
