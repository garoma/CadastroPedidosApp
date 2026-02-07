using PedidoApp.ViewModels;
using System.Windows;

namespace PedidoApp.Views
{
    public partial class ProdutoModal : Window
    {
        public ProdutoModalViewModel ViewModel { get; private set; }

        public ProdutoModal(Models.Produto produto = null)
        {
            InitializeComponent();

            ViewModel = new ProdutoModalViewModel(produto);
            ViewModel.FecharJanela = ResultadoFechar;

            DataContext = ViewModel;
        }

        private void ResultadoFechar(bool? resultado)
        {
            DialogResult = resultado;
            Close();
        }
    }
}
