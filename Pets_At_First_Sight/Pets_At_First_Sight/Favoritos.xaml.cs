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
using System.Runtime.CompilerServices;

namespace Pets_At_First_Sight
{
    public partial class Favoritos : Page
    {
        public Favoritos()
        {
            InitializeComponent();

            My_Favoritos.ItemsSource = Container.favoritos;
            CollectionViewSource.GetDefaultView(Container.favoritos).Refresh();
        }
        private void nãogosto(object sender, RoutedEventArgs e)
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
                    Container.favoritos.Remove(zzs);
                    zzs.Favorito = false;
                    new Favoritos();
                    new Inicio();

                    break;
                }

            }
        }


        private void ViewPost(object sender, MouseButtonEventArgs e)
        {
            Grid gr = (Grid)sender;
            //PackIcon u = (PackIcon)gr.Children[0];
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
            ViewPost_Favoritos vpf = new ViewPost_Favoritos();
            NavigationService.Navigate(vpf);
        }
    }
}
