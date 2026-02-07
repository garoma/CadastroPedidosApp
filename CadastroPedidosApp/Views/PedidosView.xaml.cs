using PedidoApp.ViewModels;
using System.Windows.Controls;

namespace PedidoApp.Views
{
    public partial class PedidosView : UserControl
    {
        public PedidosView()
        {
            InitializeComponent();
            DataContext = new PedidosViewModel();
        }
    }
}
