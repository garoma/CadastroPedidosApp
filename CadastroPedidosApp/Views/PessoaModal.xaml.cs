using System.Windows;
using PedidoApp.Models;
using PedidoApp.ViewModels;

namespace PedidoApp.Views
{
    public partial class PessoaModal : Window
    {
        public PessoaModalViewModel ViewModel => DataContext as PessoaModalViewModel;

        public PessoaModal(Pessoa pessoa = null)
        {
            InitializeComponent();

            var vm = new PessoaModalViewModel(pessoa);
            vm.FecharJanela = (resultado) =>
            {
                this.DialogResult = resultado;
                this.Close();
            };

            DataContext = vm;
        }
    }
}
