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

        private void addMon(object sender, RoutedEventArgs e)
        {
            String quantidade = null;
            if(quantidade_.Text.Length < 50)
            {
                quantidade = quantidade_.Text;
            }

            String abrigoselecionado = abrigo.Text;

            String tipoComida = null;
            if(TipoComida.Text.Length < 50)
            {
                tipoComida = TipoComida.Text;
            }

            SQLServerConnection.openConnection();
            SQLServerConnection.sql = "projeto.PRInserirDonativoAlimentar";
            SQLServerConnection.command.Parameters.AddWithValue("@particular", Container.current_user);
            SQLServerConnection.command.Parameters.AddWithValue("@abrigo", abrigoselecionado);
            SQLServerConnection.command.Parameters.AddWithValue("@tipo", tipoComida != null ? tipoComida : (object)DBNull.Value);
            SQLServerConnection.command.Parameters.AddWithValue("@quantidade", quantidade != null ? quantidade : (object)DBNull.Value);
            SQLServerConnection.command.CommandType = CommandType.StoredProcedure;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.command.ExecuteNonQuery();
            SQLServerConnection.closeConnection();
            SQLServerConnection.command.Parameters.Clear();
            MessageBox.Show("Donativo efetuado com sucesso!\nObrigado.");
            Loja loja = new Loja();
            this.NavigationService.Navigate(loja);
        }
    }
}
