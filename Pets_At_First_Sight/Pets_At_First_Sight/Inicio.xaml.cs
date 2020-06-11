using MaterialDesignThemes.Wpf;
using Pets_At_First_Sight.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    public partial class Inicio : Page
    {
        public Inicio()
        {

            InitializeComponent();
            GetAnimals();
            CollectionViewSource.GetDefaultView(Container.animais).Refresh();

        }

        public void GetAnimals()
        {
            Container.animais.Clear();

            SQLServerConnection.openConnection();

            SQLServerConnection.sql = "SELECT * FROM projeto.LISTAR_ANIMAIS;";
            SQLServerConnection.command.CommandType = CommandType.Text;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.reader = SQLServerConnection.command.ExecuteReader();
            SQLServerConnection.command.Parameters.Clear();
           
            while (SQLServerConnection.reader.Read())
            {
                ANIMAL animal = new ANIMAL();
                animal.Id = (int)SQLServerConnection.reader["id"];
                animal.Nome = SQLServerConnection.reader["nome"].ToString();
                MessageBox.Show(animal.Nome);
                animal.Raca = SQLServerConnection.reader["raca"].ToString();
                animal.Url_Image = SQLServerConnection.reader["fotografia"].ToString();
                animal.Mensagem = SQLServerConnection.reader["descricao"].ToString();
                animal.User_Name = SQLServerConnection.reader["dono_username"].ToString();


                if(SQLServerConnection.reader["tipo"].ToString() == "Particular")
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
                else if((int)SQLServerConnection.reader["idade"] < 24)
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
            Posts.ItemsSource = Container.animais;

        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            List<ANIMAL> listaFiltrar = new List<ANIMAL>();
            string input = pesquisa.Text;
            if (input != "")
            {
                SQLServerConnection.openConnection();
                SQLServerConnection.sql = "SELECT FROM projeto.FiltrarAnimal_Pesquisar(@valor);";
                SQLServerConnection.command.Parameters.AddWithValue("@valor", input);
                SQLServerConnection.command.CommandType = CommandType.Text;
                SQLServerConnection.command.CommandText = SQLServerConnection.sql;
                SQLServerConnection.reader = SQLServerConnection.command.ExecuteReader();
                SQLServerConnection.command.Parameters.Clear();

                while (SQLServerConnection.reader.Read())
                {
                    ANIMAL animal = new ANIMAL();
                    animal.Id = (int)SQLServerConnection.reader["id"];
                    animal.Nome = SQLServerConnection.reader["nome"].ToString();
                    animal.Especie = SQLServerConnection.reader["especie"].ToString();
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

                    listaFiltrar.Add(animal);
                }
                Posts.ItemsSource = listaFiltrar;
                SQLServerConnection.closeConnection();

            } 
            else
            {
                GetAnimals();
            }
         }
          

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InicioFiltros inicioFiltros = new InicioFiltros();
            NavigationService.Navigate(inicioFiltros);
        }

        public void Adopt(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Grid gr = (Grid)button.Parent;
            Label id = (Label)gr.Children[6];
            string username = Container.current_user.ToString();

            SQLServerConnection.openConnection();
            SQLServerConnection.sql = "projeto.AdotarAnimal";
            SQLServerConnection.command.Parameters.AddWithValue("@id", id);
            SQLServerConnection.command.Parameters.AddWithValue("@adotante_username", username);
            SQLServerConnection.command.CommandType = CommandType.StoredProcedure;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.command.ExecuteNonQuery();
            SQLServerConnection.closeConnection();
            SQLServerConnection.command.Parameters.Clear();
            new Inicio();
            new Adocoes();
        }

        private void ViewPost(object sender, MouseButtonEventArgs e)
        {
           Grid grd = (Grid)sender;
           Label id = (Label)grd.Children[6];
           Container.animal_selecionado = Int32.Parse(id.Content.ToString());
           Post_MaisInfo post_MaisInfo = new Post_MaisInfo();
           NavigationService.Navigate(post_MaisInfo);
        }
    }

 }

