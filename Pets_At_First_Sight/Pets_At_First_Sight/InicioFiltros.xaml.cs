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
            string especie = null;
            string idade = null;
            string genero = null;
            string vacina = null;
            string chip = null;
            string tipo = null;

            foreach(ANIMAL animal in Container.animais)
            {
                
                if (Especie.SelectedItem != null)
                {
                    
                    Filtrar.Remove(animal);
                }

                if (slide.Value != 0)
                {
                    Filtrar.Remove(animal);
                }

                if (Genero.SelectedItem != null)
                {
                    Filtrar.Remove(animal);
                }

                if(Doador.SelectedItem != null)
                {
                    Filtrar.Remove(animal);
                }
                if((bool)Vacinados.IsChecked)
                {
                    Filtrar.Remove(animal);
                }
                if ((bool)Chip.IsChecked)
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
