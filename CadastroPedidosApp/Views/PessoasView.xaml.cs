using PedidoApp.ViewModels;
using System.Windows.Controls;

namespace PedidoApp.Views
{
    public partial class PessoasView : UserControl
    {
        public PessoasView()
        {
            InitializeComponent();
            DataContext = new PessoasViewModel();
        }
    }
}
