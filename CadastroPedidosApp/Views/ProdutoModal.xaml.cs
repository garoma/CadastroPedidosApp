using System.Windows;
using PedidoApp.Models;
using PedidoApp.ViewModels;

namespace PedidoApp.Views
{
    public partial class ProdutoModal : Window
    {
        public ProdutoModalViewModel ViewModel => DataContext as ProdutoModalViewModel;

        public ProdutoModal(Produto produto = null)
        {
            InitializeComponent();

            var vm = new ProdutoModalViewModel(produto);
            vm.FecharJanela = (resultado) =>
            {
                this.DialogResult = resultado;
                this.Close();
            };

            DataContext = vm;
        }
    }
}
