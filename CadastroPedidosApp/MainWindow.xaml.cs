using PedidoApp.Views;
using System.Windows;

namespace PedidoApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ConteudoTela.Content = new PessoasView();
        }

        private void MenuPessoas_Click(object sender, RoutedEventArgs e)
        {
            ConteudoTela.Content = new PessoasView();
        }

        private void MenuProdutos_Click(object sender, RoutedEventArgs e)
        {
            ConteudoTela.Content = new ProdutosView();
        }

        private void MenuPedidos_Click(object sender, RoutedEventArgs e)
        {
            ConteudoTela.Content = new PedidosView();
        }

    }
}
