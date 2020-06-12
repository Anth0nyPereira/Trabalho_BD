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
    /// <summary>
    /// Interaction logic for NewAlimentar.xaml
    /// </summary>
    public partial class NewAlimentar : Page
    {
        public NewAlimentar()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult quit = MessageBox.Show("O seu donativo será descartado. Pretende continuar?", "Voltar", MessageBoxButton.YesNo);
            switch (quit)
            {
                case MessageBoxResult.Yes:
                    Doacoes p = new Doacoes();
                    this.NavigationService.Navigate(p);
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}
