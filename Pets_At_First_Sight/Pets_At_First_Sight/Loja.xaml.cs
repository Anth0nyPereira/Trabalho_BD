using System;
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
    public partial class Loja : Page
    {
        public Loja()
        {
            InitializeComponent();
            My_Loja.ItemsSource = Container.produtos;
            CollectionViewSource.GetDefaultView(Container.produtos).Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LojaFiltros lojaFiltros = new LojaFiltros();
            NavigationService.Navigate(lojaFiltros);
        }

        private void Buy(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funcionalidade disponível em breve. Pedimos desculpa.", "Oops!", MessageBoxButton.OK);
        }
    }
}
