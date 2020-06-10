using Microsoft.Win32;
using Pets_At_First_Sight.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

    public partial class CriarConta : Page
    {
        public CriarConta()
        {
            InitializeComponent();
            InputImage.Source = new BitmapImage(new Uri("Imagens\\NoImage.jpg", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String username = null;
            String nome = null;
            String email = null;
            String localidade = null;
            String pass = null;
            String foto = InputImage.Source.ToString();

            bool passedValidation = true;

            if (CheckUsername(criar_username.Text.ToString()) == false)
            {
                passedValidation = false;
            }
            else
            {
                username = criar_username.Text;
            }

            if (CheckName(criar_nome.Text.ToString()) == false)
            {
                passedValidation = false;
            }
            else
            {
                nome = criar_nome.Text;
            }

            if (IsValidMailAddress(criar_email.Text.ToString()) == false)
            {
                MessageBox.Show("Email inválido!");
                passedValidation = false;

            }
            else if (IsValidMailAddress(criar_email.Text.ToString()) == true)
            {
                if (CheckEmail(criar_email.Text.ToString()) == false)
                {
                    passedValidation = false;
                }
                else
                {
                    email = criar_email.Text;
                }
            }
            

            if (criar_localidade.Text.Length > 100)
            {
                MessageBox.Show("Morada demasiado longa.\nTente colocar algo abaixo de 100 caracteres.");
                passedValidation = false;
            }
            else if (!String.IsNullOrEmpty(criar_localidade.Text))
            {
                localidade = criar_localidade.Text;
            }

            if (criar_pass.Password.Length > 100 || IsValidPass(criar_pass.Password.ToString()) == false)
            {
                passedValidation = false;
                MessageBox.Show("Password inválida!\nTem de conter pelo menos\n8 caracteres, 1 número\ne 1 sinal de pontuação!\nLimite de 100 caracteres!");
            }
            else
            {
                pass = criar_pass.Password.ToString();
            }

            if (passedValidation)
            {
                if (localidade == null)
                {
                    System.DBNull local = DBNull.Value;
                    SQLServerConnection.openConnection();
                    SQLServerConnection.sql = "projeto.InserirConta";
                    SQLServerConnection.command.Parameters.AddWithValue("@username", username);
                    SQLServerConnection.command.Parameters.AddWithValue("@nome", nome);
                    SQLServerConnection.command.Parameters.AddWithValue("@email", email);
                    SQLServerConnection.command.Parameters.AddWithValue("@morada", local);
                    SQLServerConnection.command.Parameters.AddWithValue("@fotografia", foto);
                    SQLServerConnection.command.Parameters.AddWithValue("@pass", pass);
                    SQLServerConnection.command.CommandType = CommandType.StoredProcedure;
                    SQLServerConnection.command.CommandText = SQLServerConnection.sql;
                    SQLServerConnection.command.ExecuteNonQuery();
                    SQLServerConnection.closeConnection();
                    SQLServerConnection.command.Parameters.Clear();
                    MessageBox.Show("Conta criada com sucesso!");
                    Login login = new Login();
                    this.NavigationService.Navigate(login);
                }
                else
                {
                    SQLServerConnection.openConnection();
                    SQLServerConnection.sql = "projeto.InserirConta";
                    SQLServerConnection.command.Parameters.AddWithValue("@username", username);
                    SQLServerConnection.command.Parameters.AddWithValue("@nome", nome);
                    SQLServerConnection.command.Parameters.AddWithValue("@email", email);
                    SQLServerConnection.command.Parameters.AddWithValue("@morada", localidade);
                    SQLServerConnection.command.Parameters.AddWithValue("@fotografia", foto);
                    SQLServerConnection.command.Parameters.AddWithValue("@pass", pass);
                    SQLServerConnection.command.CommandType = CommandType.StoredProcedure;
                    SQLServerConnection.command.CommandText = SQLServerConnection.sql;
                    SQLServerConnection.command.ExecuteNonQuery();
                    SQLServerConnection.closeConnection();
                    SQLServerConnection.command.Parameters.Clear();

                    MessageBox.Show("Conta criada com sucesso!");
                    Login login = new Login();
                    this.NavigationService.Navigate(login);
                }
            }

        }

        public bool CheckUsername(string username)
        {
            //checking length
            if (username == "")
            {
                MessageBox.Show("O username deve ser preenchido.");
                return false;
            }
            else if (username.Length > 20)
            {
                MessageBox.Show("Username demasiado longo.\nTente colocar algo abaixo de 20 caracteres.");
                return false;
            }
            else
            {
                //checking db for an already existing username
                SQLServerConnection.openConnection();
                SQLServerConnection.sql = "SELECT projeto.ValidarLogin_Username(@username) AS result;";
                SQLServerConnection.command.Parameters.AddWithValue("@username", username);
                SQLServerConnection.command.CommandType = CommandType.Text;
                SQLServerConnection.command.CommandText = SQLServerConnection.sql;
                SQLServerConnection.reader = SQLServerConnection.command.ExecuteReader();

                bool output = false;
                while (SQLServerConnection.reader.Read())
                {
                    output = (bool)SQLServerConnection.reader["result"];
                }

                SQLServerConnection.closeConnection();
                SQLServerConnection.command.Parameters.Clear();

                if (output.ToString().Equals("True"))
                {
                    MessageBox.Show("Username já associado a uma conta.");
                }

                return !output;
            }

        }

        private bool CheckName(string nome)
        {
            if (nome == "")
            {
                MessageBox.Show("O nome deve ser preenchido.");
                return false;
            }
            if (nome.Length > 50)
            {
                MessageBox.Show("Nome demasiado longo.\nTente colocar algo abaixo de 50 caracteres.");
                return false;
            }
            return true;
        }

        private bool IsValidMailAddress(string mailAddress)
        {
            if (mailAddress.Length > 75)
            {
                MessageBox.Show("Email demasiado longo.\nTente colocar algo abaixo de 75 caracteres.");
                return false;
            }
            else
            {
                return Regex.IsMatch(mailAddress, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            }

        }

        public bool CheckEmail(string email)
        {
            //checking db for an already existing username
            SQLServerConnection.openConnection();
            SQLServerConnection.sql = "SELECT projeto.ValidarLogin_Email(@email) AS result;";
            SQLServerConnection.command.Parameters.AddWithValue("@email", email);
            SQLServerConnection.command.CommandType = CommandType.Text;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.reader = SQLServerConnection.command.ExecuteReader();

            bool output = false;
            while (SQLServerConnection.reader.Read())
            {
                output = (bool)SQLServerConnection.reader["result"];
            }

            SQLServerConnection.closeConnection();
            SQLServerConnection.command.Parameters.Clear();

            if (output.ToString().Equals("True"))
            {
                MessageBox.Show("Email já associado a uma conta.");
            }

            return !output;
        }

        private bool IsValidPass(string pass) // confirmar a utilização de pelo menos um caracter numérico e um sinal de pontuação
        {
            int length = pass.Length;
            bool containsInt = pass.Any(char.IsDigit);
            bool containsPonct = pass.IndexOfAny(".,;:^~='?!-_>&$%#<>".ToCharArray()) != -1;
            if (length >= 8 && length <= 20 && containsInt == true && containsPonct == true)
            {
                return true;
            }
            else
            {
                return false;
            }
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Login log = new Login();
            this.NavigationService.Navigate(log);
        }
    }
}
