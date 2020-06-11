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
        public static List<ANIMAL> animais_adotados = new List<ANIMAL>();
        public static List<Produto> produtos = new List<Produto>();
        
        public static int animal_selecionado;
        public static int produto_selecionado;
        
        public static String current_user;

    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
