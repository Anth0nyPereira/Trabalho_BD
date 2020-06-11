using MaterialDesignThemes.Wpf;
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
using static Pets_At_First_Sight.Inicio;

namespace Pets_At_First_Sight
{
    public partial class Adocoes : Page
    {
        public Adocoes()
        {
            InitializeComponent();
            GetAnimals();
        }

        public void GetAnimals()
        {
            Container.animais.Clear();

            SQLServerConnection.openConnection();

            SQLServerConnection.sql = "SELECT * FROM projeto.LISTAR_ADOTADOS(@username);";
            SQLServerConnection.command.Parameters.AddWithValue("@username", Container.current_user);
            SQLServerConnection.command.CommandType = CommandType.Text;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.reader = SQLServerConnection.command.ExecuteReader();
            SQLServerConnection.command.Parameters.Clear();

            while (SQLServerConnection.reader.Read())
            {
                ANIMAL animal = new ANIMAL();
                animal.Id = (int)SQLServerConnection.reader["id"];
                animal.Nome = SQLServerConnection.reader["nome"].ToString();
                animal.Raca = SQLServerConnection.reader["raca"].ToString();
                animal.Url_Image = SQLServerConnection.reader["fotografia"].ToString();
                animal.Mensagem = SQLServerConnection.reader["descricao"].ToString();
                animal.User_Name = SQLServerConnection.reader["dono_username"].ToString();

                
                if (SQLServerConnection.reader["tipo"].ToString() == "Particular")
                {
                    animal.Tipo_Doador = "particular";
                }
                else
                {
                    animal.Tipo_Doador = "abrigo";
                }
                


                if (SQLServerConnection.reader["especie"].ToString() == "cao")
                {
                    animal.Especie = "cão";
                }
                else
                {
                    animal.Especie = SQLServerConnection.reader["especie"].ToString();
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
                Container.animais.Add(animal);
            }
            SQLServerConnection.closeConnection();
            My_Adocoes.ItemsSource = Container.animais;

        }

        private void ViewPost(object sender, MouseButtonEventArgs e)
        {
            Grid grd = (Grid)sender;
            Label id = (Label)grd.Children[5];
            Container.animal_selecionado = Int32.Parse(id.Content.ToString());
            ViewPost_Adocoes post_MaisInfo = new ViewPost_Adocoes();
            NavigationService.Navigate(post_MaisInfo);
        }
    }
}
