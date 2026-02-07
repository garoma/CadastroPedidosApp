using PedidoApp.ViewModels;
using System.Windows.Controls;

namespace PedidoApp.Views
{
    public partial class ProdutosView : UserControl
    {
        public ProdutosView()
        {
            InitializeComponent();
            DataContext = new ProdutosViewModel();
        }
    }
}
