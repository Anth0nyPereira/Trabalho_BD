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
using System.Data;
using System.Data.SqlClient;
using Pets_At_First_Sight.Classes;

namespace Pets_At_First_Sight
{
    public partial class InicioFiltros : Page
    {
        public InicioFiltros()
        {
            InitializeComponent();

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
            Filtrar.Clear();
            String especie = null;
            if (Especie.Text.ToString() == "Cão")
            {
                especie = "cao";
            } else if (Especie.Text.ToString() == "Gato")
            {
                especie = "gato";
            }

            int idade = Int32.Parse(Idade.Text.ToString());
            string genero = null;
            if (Genero.Text == null)
            {
              
            }
            else if (Genero.Text.ToString() == "Masculino")
            {
                genero = "M";
            } else if (Genero.Text.ToString() == "Feminino")
            {
                genero = "F";
            }
            string vacina = null;
            if (Vacinados.IsChecked == true)
            {
                vacina = "T";
            }
            string chip = null;
            if (Chip.IsChecked == true)
            {
                chip = "T";
            }

            string tipo = null;
            if (TipoDoador.Text.ToString() == "Particular" || TipoDoador.Text.ToString() == "Abrigo")
            {
                tipo = TipoDoador.Text.ToString();
            }

            SQLServerConnection.openConnection();
            SQLServerConnection.sql = "SELECT* FROM projeto.FiltrarAnimal(@especie , @genero, @idade, @vacina, @chip, @dono_tipo);";
            SQLServerConnection.command.Parameters.AddWithValue("@especie", especie != null ? especie : (object)DBNull.Value);
            SQLServerConnection.command.Parameters.AddWithValue("@genero", genero != null ? genero : (object)DBNull.Value);
            SQLServerConnection.command.Parameters.AddWithValue("@idade", idade != 0 ? idade : (object)DBNull.Value);
            SQLServerConnection.command.Parameters.AddWithValue("@vacina", vacina != null ? vacina : (object)DBNull.Value);
            SQLServerConnection.command.Parameters.AddWithValue("@chip", chip != null ? chip : (object)DBNull.Value);
            SQLServerConnection.command.Parameters.AddWithValue("@dono_tipo", tipo != null ? tipo : (object)DBNull.Value);
            SQLServerConnection.command.CommandType = CommandType.Text;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.reader = SQLServerConnection.command.ExecuteReader();
            while (SQLServerConnection.reader.Read())
            {
                ANIMAL animal = new ANIMAL();
                animal.Id =(int)SQLServerConnection.reader["id"];
                animal.Nome = SQLServerConnection.reader["nome"].ToString();
                animal.Especie = SQLServerConnection.reader["especie"].ToString();
                animal.Mensagem = SQLServerConnection.reader["descricao"].ToString();
                animal.Url_Image = SQLServerConnection.reader["fotografia"].ToString();
                animal.User_Name = SQLServerConnection.reader["dono_username"].ToString();

                if (SQLServerConnection.reader["tipo"].ToString() == "Particular")
                {
                    animal.Tipo_Doador = "particular";
                }
                else
                {
                    animal.Tipo_Doador = "abrigo";
                }


                if (SQLServerConnection.reader["raca"].ToString() == "cao")
                {
                    animal.Raca = "cão";
                }
                else
                {
                    animal.Raca = SQLServerConnection.reader["raca"].ToString();
                }


                if (SQLServerConnection.reader["genero"].ToString() == "F")
                {
                    animal.Genero = "feminino";
                }
                else
                {
                    animal.Genero = "masculino";
                }

                if (SQLServerConnection.reader["vacina"].ToString() == "T")
                {
                    animal.Vacinas = "sim";
                }
                else
                {
                    animal.Vacinas = "não";
                }

                if (SQLServerConnection.reader["chip"].ToString() == "T")
                {
                    animal.Chip = "sim";
                }
                else
                {
                    animal.Chip = "não";
                }

                if ((int)SQLServerConnection.reader["idade"] == 1)
                {
                    animal.Idade = "1 mês";
                }
                else if ((int)SQLServerConnection.reader["idade"] < 12)
                {
                    animal.Idade = (int)SQLServerConnection.reader["idade"] + " meses";
                }
                else if ((int)SQLServerConnection.reader["idade"] < 24)
                {
                    animal.Idade = "1 ano";
                }
                else
                {
                    animal.Idade = (int)SQLServerConnection.reader["idade"] / 12 + " anos";
                }
                Filtrar.Add(animal);
            }
            SQLServerConnection.closeConnection();

            Inicio inicio = new Inicio();
            inicio.Posts.ItemsSource = Filtrar;
            this.NavigationService.Navigate(inicio);
        }

    }
}
