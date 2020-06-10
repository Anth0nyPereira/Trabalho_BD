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
            // check if username exists in db
            /*
            SQLServerConnection.openConnection();
            SQLServerConnection.sql = "SELECT* FROM projeto.ValidarLogin_Email(@email)";
            //SQLServerConnection.sql = "EXEC @exists = projeto.ValidarLogin_Email @email";
            SQLServerConnection.command.Parameters.AddWithValue("@email", email);
            SqlParameter exists = new SqlParameter("@exists", SqlDbType.Bit);
            SQLServerConnection.command.CommandType = CommandType.Text;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            int exs = (int) SQLServerConnection.command.ExecuteScalar();
            SQLServerConnection.closeConnection();

            if ((int)exs.Value == 0)
            {
                MessageBox.Show("não existe uma conta associada a esse email.");
                return false;
            }
            return true;
            */
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
            // check if the inserted pass and its hashing correspond to any existing hash in the table
            /*
            SQLServerConnection.openConnection();
            SQLServerConnection.sql = "EXEC @exists = projeto.ValidarLogin_Password @pass";
            SQLServerConnection.command.Parameters.AddWithValue("@pass", pass);
            SqlParameter exists = new SqlParameter("@exists", SqlDbType.Bit);
            SQLServerConnection.command.CommandType = CommandType.Text;
            SQLServerConnection.command.CommandText = SQLServerConnection.sql;
            SQLServerConnection.closeConnection();
            /*
            if (exists == 0)
            {
                MessageBox.Show("Não existe nenhuma conta associada a essa password");
                return false;
            }
            */
            //return true;
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
                MessageBox.Show("Sucesso!");
                return true;
            }
            else
            {
                MessageBox.Show("não existe uma conta associada a essa pass.");
                MessageBox.Show(SHA512function(pass));
                return false;
            }

        }
        // https://stackoverflow.com/questions/11367727/how-can-i-sha512-a-string-in-c
        public static string SHA512function(string input)
        {
            /*
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                // Convert to text
                // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
                var hashstr = BitConverter.ToString(hashedInputBytes).Replace("-", "");
                return hashstr.ToString();
                */
            SHA512 alg = SHA512.Create();
            byte[] result = alg.ComputeHash(Encoding.UTF8.GetBytes(input));
            string hash = Encoding.UTF8.GetString(result);
            StringBuilder hex = new StringBuilder(result.Length);
            foreach (byte b in result)
                hex.AppendFormat("{0:x2}", b);
            string str = hex.ToString();
            str = str.ToUpper();
            str = "0x" + str;
            return str;
        }
    }
}
