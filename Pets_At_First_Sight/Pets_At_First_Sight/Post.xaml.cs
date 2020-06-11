using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Pets_At_First_Sight.Classes;

namespace Pets_At_First_Sight
{
 
    public partial class Post : Page
    {
        public Post()
        {
            InitializeComponent();
            InputImage.Source = new BitmapImage(new Uri("Imagens\\NoImage.jpg", UriKind.Relative));

        }

        public void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }

        private void BtnLoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                InputImage.Source = new BitmapImage(fileUri);
            }
        }

        private void ButtonSubmeter_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage z = new BitmapImage(new Uri("Imagens\\NoImage.jpg", UriKind.Relative));
            String Especie = EspecieAnimal.Text.ToString();
            String Raca = RacaAnimal.Text.ToString();
            String Nome = NomeAnimal.Text.ToString();
            String Idade = IdadeAnimal.Text.ToString();
            String Genero = GeneroAnimal.Text.ToString();
            String _NomeDoador = Container.current_user;
            String _Vacinas = Vacinas.Text.ToString();
            String _Chip = Chip.Text.ToString();
            String Testo = PostTexto.Text.ToString();
            String Url_Image_ = InputImage.Source.ToString();
            if (Especie.Length == 0 | Genero.Length == 0)
            {
                MessageBox.Show("Prencher todos os campos!!");
            }
            else
            {
                SQLServerConnection.openConnection();
                SQLServerConnection.sql = "projeto.InserirAnimal";
                SQLServerConnection.command.Parameters.AddWithValue("@nome", Nome);
                SQLServerConnection.command.Parameters.AddWithValue("@especie", Especie);
                SQLServerConnection.command.Parameters.AddWithValue("@raca", Raca);
                SQLServerConnection.command.Parameters.AddWithValue("@genero", Genero);
                SQLServerConnection.command.Parameters.AddWithValue("@idade", Idade);
                SQLServerConnection.command.Parameters.AddWithValue("@vacina", _Vacinas);
                SQLServerConnection.command.Parameters.AddWithValue("@chip", _Chip);
                SQLServerConnection.command.Parameters.AddWithValue("@fotografia", Url_Image_);
                SQLServerConnection.command.Parameters.AddWithValue("@descricao", Testo);
                SQLServerConnection.command.Parameters.AddWithValue("@dono_username", Container.current_user);
                SQLServerConnection.command.CommandType = CommandType.StoredProcedure;
                SQLServerConnection.command.CommandText = SQLServerConnection.sql;
                SQLServerConnection.command.ExecuteNonQuery();
                SQLServerConnection.closeConnection();
                SQLServerConnection.command.Parameters.Clear();

                new Inicio();
                new Perfil();
                MessageBox.Show("Post Criado");
                Perfil cursosPage = new Perfil();
                this.NavigationService.Navigate(cursosPage);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult quit = MessageBox.Show("O seu post será descartado. Pretende continuar?", "Voltar", MessageBoxButton.YesNo);
            switch (quit)
            {
                case MessageBoxResult.Yes:
                    Perfil p = new Perfil();
                    this.NavigationService.Navigate(p);
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}
