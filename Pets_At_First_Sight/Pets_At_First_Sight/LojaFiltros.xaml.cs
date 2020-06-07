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
    public partial class LojaFiltros : Page
    {
        public LojaFiltros()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("A sua seleção será descartada. Pretende continuar?", "Voltar", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Loja loja = new Loja();
                    this.NavigationService.Navigate(loja);
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<Produto> FiltrarProduto = new List<Produto>();

            foreach (Produto prod in Container.produtos)
            {
                FiltrarProduto.Add(prod);
            }

            foreach (Produto prod in Container.produtos)
            {
                if (FiltrarProduto.Contains(prod) && Tipo.SelectedItem != null && !prod.TipoServico.Equals(Tipo.Text.ToString()))
                {
                    FiltrarProduto.Remove(prod);
                }

                double price = Convert.ToDouble(prod.Preco);

                if (FiltrarProduto.Contains(prod) && slide2.Value != 0 && (price > slide2.Value))
                {
                    FiltrarProduto.Remove(prod);
                }

                if (FiltrarProduto.Contains(prod) && Empresa.SelectedItem != null && !prod.Empresa.Equals(Empresa.Text.ToString()))
                {
                    FiltrarProduto.Remove(prod);
                }
            }
            Loja loja = new Loja();
            loja.My_Loja.ItemsSource = FiltrarProduto;
            this.NavigationService.Navigate(loja);

        }
    }
}
