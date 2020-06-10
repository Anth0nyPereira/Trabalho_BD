using System;
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

namespace Pets_At_First_Sight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    static class Container
    {
        public static List<ANIMAL> animais = new List<ANIMAL>();
        public static List<ANIMAL> favoritos = new List<ANIMAL>();
        public static List<ANIMAL> adocoes = new List<ANIMAL>();
        public static int animal_selecionado;
        //public static List<ANIMAL> animal_selecionado = new List<ANIMAL>();
        public static List<Produto> produtos = new List<Produto>();
        public static List<Conta> contas = new List<Conta>();
        //public static List<Conta> utilizador_logado = new List<Conta>();
        public static String current_user;

    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            String s = "Imagens\\";

            // separador
            Container.produtos.Add(new Produto() { ID = "1000", TipoServico = "Produto", NomeProduto = "Roupão para cão", Empresa="Empresa A", Preco = "5,95", uImage= s + "escova_cabelo.jpg" });
            Container.produtos.Add(new Produto() { ID = "1001", TipoServico = "Produto", NomeProduto = "Roupão para gato", Empresa = "Empresa B", Preco = "6,95", uImage = s + "escova_cabelo.jpg" });
            Container.produtos.Add(new Produto() { ID = "1002", TipoServico = "Serviço", NomeProduto = "Esterilização para cão", Empresa = "Empresa C", Preco = "15,00", uImage = s + "escova_cabelo.jpg" });
            Container.produtos.Add(new Produto() { ID = "1003", TipoServico = "Serviço", NomeProduto = "Manicure para gato", Empresa = "Empresa D", Preco = "8,99", uImage = s + "escova_cabelo.jpg" });
            Container.produtos.Add(new Produto() { ID = "1003", TipoServico = "Produto", NomeProduto = "Ração tricolor para cão", Empresa = "Empresa E", Preco = "7,99", uImage = s + "escova_cabelo.jpg" });
            // separador
            Container.contas.Add(new Conta() { Email = "anthonypereira@ua.pt", Pass = "Olábomdia.0", NomePessoa = "Anthony Pereira", Username = "M0dernCaty0ga", TipoConta = "Particular", Localidade = "Ovar", Foto = "ImgPerfil.jpg" });
            Container.contas.Add(new Conta() { Email = "alexandracarvalho@ua.pt", Pass = "Olábomdia.1", NomePessoa = "Alexandra Carvalho", Username = "alexiis75", TipoConta = "Doador", Localidade = "Gaia", Foto = "Alexandra.jpg" });
            Container.contas.Add(new Conta() { Email = "joaots@ua.pt", Pass = "Olábomdia.2", NomePessoa = "João Soares", Username = "J0hnnySoares", TipoConta = "Cliente", Localidade = "Canelas", Foto = "Joao.jpg" });
            InicioFiltros inicioFiltros = new InicioFiltros();
        }
    }
}
