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
        public static List<ANIMAL> animal_selecionado = new List<ANIMAL>();
        public static List<Produto> produtos = new List<Produto>();
        public static List<Conta> contas = new List<Conta>();
        public static List<Conta> utilizador_logado = new List<Conta>();
        public static String current_user;

    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            String s = "Imagens\\";

            Container.animais.Add(new ANIMAL() { Nome = "Cão Fofo", Idade = "5 meses", Genero = "Masculino", Raca = "Cão", Url_Image = s + "stock_dog1.jpg", User_Name = "Filipa", Tipo_Doador = "Abrigo", Vacinas = "Sim", Chip="Sim", Mensagem="fuhsd", Adotado=false, Favorito=false });
            Container.animais.Add(new ANIMAL() { Nome = "Princesa", Idade = "10 anos", Genero = "Feminino", Raca = "Gato", Url_Image = s + "stock_gato1.jpg", User_Name = "M0dernCaty0ga", Tipo_Doador = "Abrigo", Vacinas = "Sim", Chip = "Não", Mensagem = "fuhsd", Adotado = false, Favorito = false });
            Container.animais.Add(new ANIMAL() { Nome = "Piu", Idade = "5 anos", Genero = "Feminino", Raca = "Cão", Url_Image = s + "stock_dog1.jpg", User_Name = "M0dernCaty0ga", Tipo_Doador = "Particular", Vacinas = "Sim", Chip = "Sim", Mensagem = "fuhsd", Adotado = false, Favorito = false });
            Container.animais.Add(new ANIMAL() { Nome = "Gato das Botas", Idade = "7 anos", Genero = "Masculino", Raca = "Gato", Url_Image = s + "stock_gato1.jpg", User_Name = "Alex42000", Tipo_Doador = "Abrigo", Vacinas = "Não", Chip = "Sim", Mensagem = "fuhsd", Adotado = false, Favorito = false });
            Container.animais.Add(new ANIMAL() { Nome = "Simba", Idade = "2 anos", Genero = "Masculino", Raca = "Cão", Url_Image = s + "stock_dog1.jpg", User_Name = "J0hnnySoares", Tipo_Doador = "Particular", Vacinas = "Não", Chip = "Sim", Mensagem = "Cão Muito engraçado", Adotado = false, Favorito = false });
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
