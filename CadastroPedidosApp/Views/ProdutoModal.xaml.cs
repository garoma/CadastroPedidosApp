using System.Globalization;
using System.Windows;
using PedidoApp.Models;

namespace PedidoApp.Views
{
    public partial class ProdutoModal : Window
    {
        public Produto ProdutoEditado { get; private set; }

        public ProdutoModal(Produto produto = null)
        {
            InitializeComponent();

            if (produto != null)
            {
                txtNome.Text = produto.Nome;
                txtCodigo.Text = produto.Codigo;
                txtValor.Text = produto.Valor.ToString("F2");

                ProdutoEditado = produto;
            }
        }

        private void Salvar_Click(object sender, RoutedEventArgs e)
        {
            if (ProdutoEditado == null)
                ProdutoEditado = new Produto();

            ProdutoEditado.Nome = txtNome.Text;
            ProdutoEditado.Codigo = txtCodigo.Text;

            // Converte valor digitado
            if (decimal.TryParse(txtValor.Text.Replace(",", "."),
                                NumberStyles.Any,
                                CultureInfo.InvariantCulture,
                                out decimal valor))
            {
                ProdutoEditado.Valor = valor;
            }
            else
            {
                MessageBox.Show("Digite um valor válido.");
                return;
            }

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
