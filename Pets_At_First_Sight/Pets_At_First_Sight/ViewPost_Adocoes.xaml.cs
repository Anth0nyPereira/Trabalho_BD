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
    public partial class ViewPost_Adocoes : Page
    {
        public ViewPost_Adocoes()
        {
            InitializeComponent();
            Post_Template.ItemsSource = Container.animal_selecionado;
            CollectionViewSource.GetDefaultView(Container.animal_selecionado).Refresh();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Adocoes add = new Adocoes();
            Container.animal_selecionado.RemoveAt(0);
            this.NavigationService.Navigate(add);
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
                    if (zzs.Adotado)
                    {
                        Container.adocoes.Remove(zzs);
                        zzs.Adotado = false;

                        icon.BeginInit();
                        icon.Kind = PackIconKind.StarOutline;
                        icon.EndInit();

                        new ViewPost_Adocoes();
                        new Adocoes();
                        new Inicio();

                        break;
                    }
                    else
                    {
                        zzs.Adotado = true;
                        Container.adocoes.Add(zzs);

                        icon.BeginInit();
                        icon.Kind = PackIconKind.Star;
                        icon.EndInit();

                        new ViewPost_Adocoes();
                        new Adocoes();
                        new Inicio();

                        break;

                    }

                }

            }
        }
    }
}
