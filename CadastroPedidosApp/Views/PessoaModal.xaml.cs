using System.Windows;
using PedidoApp.Models;

namespace PedidoApp.Views
{
    public partial class PessoaModal : Window
    {
        public Pessoa PessoaEditada { get; private set; }

        public PessoaModal(Pessoa pessoa = null)
        {
            InitializeComponent();

            // Se veio pessoa, é edição
            if (pessoa != null)
            {
                txtNome.Text = pessoa.Nome;
                txtCpf.Text = pessoa.CPF;
                txtEndereco.Text = pessoa.Endereco;

                PessoaEditada = pessoa;
            }
        }

        private void Salvar_Click(object sender, RoutedEventArgs e)
        {
            if (PessoaEditada == null)
                PessoaEditada = new Pessoa();

            PessoaEditada.Nome = txtNome.Text;
            PessoaEditada.CPF = txtCpf.Text;
            PessoaEditada.Endereco = txtEndereco.Text;

            DialogResult = true;
            Close();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
