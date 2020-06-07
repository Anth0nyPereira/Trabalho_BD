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
    public partial class InicioFiltros : Page
    {
        public InicioFiltros()
        {
            InitializeComponent();
            int num_filters = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("A sua seleção será descartada. Pretende continuar?", "Voltar", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Inicio inicio = new Inicio();
                    this.NavigationService.Navigate(inicio);
                    break;
                case MessageBoxResult.No:
                    break;
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<ANIMAL> Filtrar = new List<ANIMAL>();

            foreach (ANIMAL m in Container.animais)
            {
                Filtrar.Add(m);
            }

            foreach(ANIMAL animal in Container.animais)
            {
                String[] idadeAnimal = animal.Idade.Split(' ');

                if (Filtrar.Contains(animal) && Especie.SelectedItem != null && !animal.Raca.Equals(Especie.Text.ToString()))
                {
                    Filtrar.Remove(animal);
                }

                if (slide.Value != 0 && (idadeAnimal[1] == "anos" || idadeAnimal[1] == "ano") && Int32.Parse(idadeAnimal[0]) > slide.Value && Filtrar.Contains(animal))
                {
                    Filtrar.Remove(animal);
                }

                if (Filtrar.Contains(animal) && Genero.SelectedItem != null && !animal.Genero.Equals(Genero.Text.ToString()))
                {
                    Filtrar.Remove(animal);
                }

                if(Filtrar.Contains(animal) && Doador.SelectedItem != null && !animal.Tipo_Doador.Equals(Doador.Text.ToString()))
                {
                    Filtrar.Remove(animal);
                }
                if((bool)Vacinados.IsChecked && animal.Vacinas.Equals("Não") && Filtrar.Contains(animal))
                {
                    Filtrar.Remove(animal);
                }
                if ((bool)Chip.IsChecked && animal.Chip.Equals("Não") && Filtrar.Contains(animal))
                {
                    Filtrar.Remove(animal);
                }

            }
            Inicio inicio = new Inicio();
            inicio.Posts.ItemsSource = Filtrar;
            this.NavigationService.Navigate(inicio);

        }
    }
}
