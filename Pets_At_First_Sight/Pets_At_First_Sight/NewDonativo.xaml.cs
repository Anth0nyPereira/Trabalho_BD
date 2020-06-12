using Pets_At_First_Sight.Classes;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for NewDonativo.xaml
    /// </summary>
    public partial class NewDonativo : Page
    {
        public NewDonativo()
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

        private void addMon(object sender, RoutedEventArgs e)
        {
            String meio_pagamento = null;
            int quantidade = 0;

            if ((bool)CM.IsChecked)
            {
                meio_pagamento = "Multibanco";
            }
            else if ((bool)MBW.IsChecked)
            {
                meio_pagamento = "MB WAY";
            }
            else if ((bool)PP.IsChecked)
            {
                meio_pagamento = "Paypal";
            }
            else if ((bool)TB.IsChecked)
            {
                meio_pagamento = "Transferência bancária";
            }

            quantidade = Int32.Parse(quantidade_.Text);

            String abrigoselecionado = abrigo.Text;

            SQLServerConnection.openConnection();
            SQLServerConnection.sql = "projeto.PRInserirDonativoMonetario";
            SQLServerConnection.command.Parameters.AddWithValue("@particular", Container.current_user);
            SQLServerConnection.command.Parameters.AddWithValue("@abrigo", abrigoselecionado);
            SQLServerConnection.command.Parameters.AddWithValue("@pagamento", meio_pagamento);
            SQLServerConnection.command.Parameters.AddWithValue("@quantia", quantidade);
            SQLServerConnection.command.CommandType = CommandType.StoredProcedure;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.command.ExecuteNonQuery();
            SQLServerConnection.closeConnection();
            SQLServerConnection.command.Parameters.Clear();
            MessageBox.Show("Donativo efetuado com sucesso!\nObrigado.");
            Doacoes d = new Doacoes();
            this.NavigationService.Navigate(d);
        }
    }
}
