using PedidoApp.Models;
using PedidoApp.Services;
using PedidoApp.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PedidoApp.ViewModels
{
    public class ProdutosViewModel : BaseViewModel
    {
        private List<Produto> produtos;
        private string filtroNome;
        private string filtroCodigo;
        private string valorMin;
        private string valorMax;

        public ObservableCollection<Produto> ProdutosFiltrados { get; set; }

        private Produto produtoSelecionado;
        public Produto ProdutoSelecionado
        {
            get => produtoSelecionado;
            set { produtoSelecionado = value; OnPropertyChanged(nameof(ProdutoSelecionado)); }
        }

        public string FiltroNome
        {
            get => filtroNome;
            set { filtroNome = value; OnPropertyChanged(nameof(FiltroNome)); }
        }

        public string FiltroCodigo
        {
            get => filtroCodigo;
            set { filtroCodigo = value; OnPropertyChanged(nameof(FiltroCodigo)); }
        }

        public string ValorMin
        {
            get => valorMin;
            set { valorMin = value; OnPropertyChanged(nameof(ValorMin)); }
        }

        public string ValorMax
        {
            get => valorMax;
            set { valorMax = value; OnPropertyChanged(nameof(ValorMax)); }
        }

        public RelayCommand BuscarCommand { get; set; }
        public RelayCommand IncluirCommand { get; set; }
        public RelayCommand EditarCommand { get; set; }
        public RelayCommand ExcluirCommand { get; set; }

        public ProdutosViewModel()
        {
            produtos = JsonDatabase.Load<Produto>("produtos.json");
            ProdutosFiltrados = new ObservableCollection<Produto>(produtos);

            BuscarCommand = new RelayCommand(Buscar);
            IncluirCommand = new RelayCommand(Incluir);
            EditarCommand = new RelayCommand(Editar);
            ExcluirCommand = new RelayCommand(Excluir);
        }

        private void Buscar()
        {
            string nome = FiltroNome?.Trim().ToLower() ?? "";
            string codigo = FiltroCodigo?.Trim().ToLower() ?? "";
            decimal.TryParse(ValorMin, out decimal min);
            decimal.TryParse(ValorMax, out decimal max);
            max = max == 0 ? decimal.MaxValue : max;

            var resultado = produtos.Where(p =>
                (string.IsNullOrEmpty(nome) || p.Nome.ToLower().Contains(nome)) &&
                (string.IsNullOrEmpty(codigo) || p.Codigo.ToLower().Contains(codigo)) &&
                p.Valor >= min &&
                p.Valor <= max
            ).ToList();

            ProdutosFiltrados.Clear();
            foreach (var p in resultado)
                ProdutosFiltrados.Add(p);
        }

        private void Incluir()
        {
            var modal = new ProdutoModal();
            modal.Owner = Application.Current.MainWindow;

            if (modal.ShowDialog() == true)
            {
                var novoProduto = modal.ViewModel.ProdutoEditado;
                novoProduto.Id = produtos.Count + 1;

                produtos.Add(novoProduto);
                JsonDatabase.Save("produtos.json", produtos);

                // Atualiza lista filtrada
                Buscar();
            }
        }

        private void Editar()
        {
            if (ProdutoSelecionado == null) { MessageBox.Show("Selecione um produto."); return; }

            var modal = new ProdutoModal(ProdutoSelecionado);
            modal.Owner = Application.Current.MainWindow;

            if (modal.ShowDialog() == true)
            {
                JsonDatabase.Save("produtos.json", produtos);
                Buscar();
            }
        }

        private void Excluir()
        {
            if (ProdutoSelecionado == null) { MessageBox.Show("Selecione um produto."); return; }

            produtos.Remove(ProdutoSelecionado);
            JsonDatabase.Save("produtos.json", produtos);
            ProdutosFiltrados.Remove(ProdutoSelecionado);
        }
    }
}
