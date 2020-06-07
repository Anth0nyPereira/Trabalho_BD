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
            bool goodEmail = false;
            bool goodPass = false;
            bool validEmail = false;
            bool validPass = false;

            // Validação do email
            if (IsValidMailAddress(Email.Text.ToString()) == true)
            {
                validEmail = true;
            } else if (IsValidMailAddress(Email.Text.ToString()) == false)
            {
                MessageBox.Show("Email inválido!");
                Email.Text = "";
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

            if (validEmail == true || validPass == true)
            {
                foreach (Conta c in Container.contas)
                {
                    if (Email.Text.ToString() == c.Email.ToString())
                    {
                        goodEmail = true;
                        if (PasswordBox.Password.ToString() == c.Pass.ToString())
                        {
                            goodPass = true;
                            Container.utilizador_logado.Add(c);
                            MessageBox.Show("Login efetuado com sucesso!");
                            Windows App = new Windows();
                            this.NavigationService.Navigate(App);

                        }
                    }
                }
                if (goodEmail == false && validEmail == true)
                {
                    MessageBox.Show("Não existe nenhuma conta registada com esse email.");
                    Email.Text = "";
                }
                if (goodPass == false && validPass == true)
                {
                    MessageBox.Show("Não existe nenhuma conta registada com essa password");
                    PasswordBox.Password = "";
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

        private bool IsValidMailAddress(string mailAddress)
        {
            return Regex.IsMatch(mailAddress, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
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
    }
}
