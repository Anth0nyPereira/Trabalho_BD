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
            BitmapImage z = new BitmapImage(new Uri("Imagens\\NoImage.jpg", UriKind.Relative));
            bool passedValidation = true;

            if (CheckUsername(criar_username.Text.ToString()) == false)
            {
                passedValidation = false;
            }

            if(CheckName(criar_nome.Text.ToString()) == false)
            {
                passedValidation = false;
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
            }

            if (criar_localidade.Text.Length > 100)
            {
                MessageBox.Show("Morada demasiado longa.\nTente colocar algo abaixo de 100 caracteres.");
                passedValidation = false;
            }

            // checking if any mandadory fields are empty
            if (criar_pass.Password == "")
            {
                passedValidation = false;
                MessageBox.Show("Existem 1 ou mais campos em branco!");
            }

            
            if (criar_pass.Password.Length > 100)
            {
                passedValidation = false;
                MessageBox.Show("Localidade demasiado longa. Inválido!");
            }

            if (IsValidPass(criar_pass.Password.ToString()) == false)
            {
                MessageBox.Show("Password inválida!\nTem de conter pelo menos\n8 caracteres, 1 número\ne 1 sinal de pontuação!");
                criar_pass.Password = "";
            }

            if (CheckEmail(criar_email.Text.ToString()) == true && CheckUsername(criar_username.Text.ToString()) == true && criar_username.Text != "")
            {
                if (IsValidMailAddress(criar_email.Text.ToString()) == true && IsValidPass(criar_pass.Password.ToString()) == true)
                {
                    MessageBox.Show("Conta criada com sucesso!");
                    Container.contas.Add(new Conta() { Email = criar_email.Text.ToString(), Pass = criar_pass.Password.ToString(), NomePessoa = criar_nome.Text.ToString(), Username = criar_username.Text.ToString(), TipoConta = "Particular", Localidade = criar_localidade.Text.ToString(), Foto = InputImage.Source.ToString() });
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
                SQLServerConnection.sql = "EXEC @exists = projeto.ValidarLogin_Username @username";
                SQLServerConnection.command.Parameters.AddWithValue("@username", username);
                SqlParameter exists = new SqlParameter("@exists", SqlDbType.Bit);
                SQLServerConnection.command.CommandType = CommandType.Text;
                SQLServerConnection.command.CommandText = SQLServerConnection.sql;
                SQLServerConnection.closeConnection();

                if ((int)exists.Value == 1)
                {
                    MessageBox.Show("Já existe uma conta associada a esse username.");
                    return false;
                }
                return true;
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
                MessageBox.Show("Nome demasiado longo.\nTente colocar algo abaixo de 05 caracteres.");
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
            //checking db for an already existing email
            SQLServerConnection.openConnection();
            SQLServerConnection.sql = "EXEC @exists = projeto.ValidarConta_Email @email";
            SQLServerConnection.command.Parameters.AddWithValue("@email", email);
            SqlParameter exists = new SqlParameter("@exists", SqlDbType.Bit);
            SQLServerConnection.command.CommandType = CommandType.Text;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.closeConnection();

            if ((int)exists.Value == 1)
            {
                MessageBox.Show("Já existe uma conta associada a esse email.");
                return false;
            }
            return true;
        }

        private bool IsValidPass(string pass) // para validar a password de input do login; no caso da criação de conta, terá de confirmar
                                              // a utilização e pelo menos um caracter numérico e ainda um sinal de pontuação
        {
            int length = pass.Length;
            bool containsInt = pass.Any(char.IsDigit);
            bool containsPonct = pass.IndexOfAny(".,;:^~='?!-_>&$%#<>".ToCharArray()) != -1;
            if (length >= 8 && containsInt == true && containsPonct == true)
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
