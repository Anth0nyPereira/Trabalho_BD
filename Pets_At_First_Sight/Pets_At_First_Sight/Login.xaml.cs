using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;


using System.Data;
using System.Data.SqlClient;
using Pets_At_First_Sight.Classes;
using System.Security.Cryptography;

namespace Pets_At_First_Sight
{
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool validUsername = false;
            bool validPass = false;

            // Validação do username
            if (IsValidUsername(Username.Text.ToString()) == true)
            {
                validUsername = true;
            } else if (IsValidUsername(Username.Text.ToString()) == false)
            {
                MessageBox.Show("Username inválido!");
                Username.Text = "";
            }

            // Validação da palavra-passe
            if (IsValidPass(PasswordBox.Password.ToString()) == true)
            {
                validPass = true;
            }
            else if (IsValidPass(PasswordBox.Password.ToString()) == false)
            {
                MessageBox.Show("Password inválida!\nTem de conter pelo menos\n8 caracteres, 1 número\ne 1 caracter especial/sinal de pontuação!");
                PasswordBox.Password = "";
            }

            if (validUsername == true || validPass == true)
            {
                if (Username.Text.ToString() != null)
                {
                    bool existsUsername = ExistsUsername(Username.Text.ToString());
                    if (existsUsername == true)
                    {
                        bool existsPass = ExistsPass(Username.Text.ToString(), PasswordBox.Password.ToString());
                        if (existsPass == true)
                        {
                            MessageBox.Show("Login efetuado com sucesso!");
                            Container.current_user = Username.Text.ToString();
                            Windows App = new Windows();
                            this.NavigationService.Navigate(App);
                        }
                    }
                }

            }
        }
            private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ainda não é possível recuperar a sua password. \nPedimos desculpa.", "Oops!", MessageBoxButton.OK);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CriarConta cc = new CriarConta();
            this.NavigationService.Navigate(cc);
        }

        private bool IsValidUsername(string username)
        {
            if (username != null)
            {
                return true;
            } else
            {
                return false;
            }
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
            } else
            {
                return false;
            }
        }

        private static bool ExistsUsername(string username)
        {
            bool output = false;
            SQLServerConnection.openConnection();
            SQLServerConnection.sql = "SELECT projeto.ValidarLogin_Username(@username) AS result;";
            SQLServerConnection.command.Parameters.AddWithValue("@username", username);
            SQLServerConnection.command.CommandType = CommandType.Text;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.reader = SQLServerConnection.command.ExecuteReader();
            SQLServerConnection.command.Parameters.Clear();
            while (SQLServerConnection.reader.Read())
            {
                output = (bool)SQLServerConnection.reader["result"];
            }
            SQLServerConnection.closeConnection();
            

            if (output.ToString().Equals("False"))
            {
                MessageBox.Show("Não existe uma conta associada a esse username.");
                return false;
            } else
            {
                return true;
            }
        }

        private bool ExistsPass(string username, string pass)
        {
            bool output = false;
            SQLServerConnection.openConnection();
            SQLServerConnection.sql = "SELECT projeto.ValidarLogin_Password(@username2, @pass) AS result;";
            SQLServerConnection.command.Parameters.AddWithValue("@username2", username);
            SQLServerConnection.command.Parameters.AddWithValue("@pass", pass);
            SQLServerConnection.command.CommandType = CommandType.Text;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.reader = SQLServerConnection.command.ExecuteReader();
            SQLServerConnection.command.Parameters.Clear();
            while (SQLServerConnection.reader.Read())
            {
                output = (bool)SQLServerConnection.reader["result"];
            }
            SQLServerConnection.closeConnection();

            if (output.ToString().Equals("True"))
            {
                return true;
            }
            else
            {
                MessageBox.Show("não existe uma conta associada a essa pass.");
                return false;
            }

        }
    }
}
