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
    /// Interaction logic for CompraProduto.xaml
    /// </summary>
    public partial class CompraProduto : Page
    {
        public CompraProduto()
        {
            InitializeComponent();
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("A sua compra será descartada. Pretende continuar?", "Voltar", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Container.produto_selecionado = 0;
                    Loja loja = new Loja();
                    this.NavigationService.Navigate(loja);
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void Buy(object sender, RoutedEventArgs e)
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

            bool output = false;
            SQLServerConnection.openConnection();
            SQLServerConnection.sql = "SELECT projeto.ValidarQuantidade(@id, @qtty) AS result;";
            SQLServerConnection.command.Parameters.AddWithValue("@id", Container.produto_selecionado);
            SQLServerConnection.command.Parameters.AddWithValue("@qtty", quantidade);
            SQLServerConnection.command.CommandType = CommandType.Text;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.reader = SQLServerConnection.command.ExecuteReader();
            SQLServerConnection.command.Parameters.Clear();
            while (SQLServerConnection.reader.Read())
            {
                output = (bool)SQLServerConnection.reader["result"];
            }
            SQLServerConnection.closeConnection();

            if(output == false)
            {
                MessageBox.Show("Lamentamos! A quantidade escolhida não existe em stock.");
            }
            else
            {
                SQLServerConnection.openConnection();
                SQLServerConnection.sql = "projeto.InserirCompra";
                SQLServerConnection.command.Parameters.AddWithValue("@id_produto", Container.produto_selecionado);
                SQLServerConnection.command.Parameters.AddWithValue("@meio_pagamento", meio_pagamento);
                SQLServerConnection.command.Parameters.AddWithValue("@username", Container.current_user);
                SQLServerConnection.command.Parameters.AddWithValue("@quantidade", quantidade);
                SQLServerConnection.command.CommandType = CommandType.StoredProcedure;
                SQLServerConnection.command.CommandText = SQLServerConnection.sql;
                SQLServerConnection.command.ExecuteNonQuery();
                SQLServerConnection.closeConnection();
                SQLServerConnection.command.Parameters.Clear();
                MessageBox.Show("Compra efetuada com sucesso.\nObrigado.");
                Loja loja = new Loja();
                this.NavigationService.Navigate(loja);
            }
        }
    }

}
