using PedidoApp.Models;
using PedidoApp.ViewModels;
using System.Windows.Controls;

namespace PedidoApp.Views
{
    public partial class PedidoView : UserControl
    {
        public PedidoView(Pessoa cliente)
        {
            InitializeComponent();
            DataContext = new PedidoViewModel(cliente);
        }
    }
}
