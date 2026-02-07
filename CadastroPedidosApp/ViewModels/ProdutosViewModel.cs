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

        public ObservableCollection<Produto> ProdutosFiltrados { get; set; }

        public Produto ProdutoSelecionado { get; set; }

        public string FiltroNome { get; set; }
        public string FiltroCodigo { get; set; }

        public string ValorMin { get; set; }
        public string ValorMax { get; set; }

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
            string nome = FiltroNome?.ToLower() ?? "";
            string codigo = FiltroCodigo?.ToLower() ?? "";

            decimal min = 0;
            decimal max = decimal.MaxValue;

            decimal.TryParse(ValorMin, out min);
            decimal.TryParse(ValorMax, out max);

            var resultado = produtos.Where(p =>
                p.Nome.ToLower().Contains(nome) &&
                p.Codigo.ToLower().Contains(codigo) &&
                p.Valor >= min &&
                p.Valor <= max
            ).ToList();

            ProdutosFiltrados.Clear();

            foreach (var p in resultado)
                ProdutosFiltrados.Add(p);
        }

        //private void Incluir()
        //{
        //    var novo = new Produto
        //    {
        //        Id = produtos.Count + 1,
        //        Nome = "Novo Produto",
        //        Codigo = "000",
        //        Valor = 0
        //    };

        //    produtos.Add(novo);
        //    JsonDatabase.Save("produtos.json", produtos);

        //    ProdutosFiltrados.Add(novo);
        //}

        private void Incluir()
        {
            var modal = new ProdutoModal();
            modal.Owner = Application.Current.MainWindow;

            if (modal.ShowDialog() == true)
            {
                var novoProduto = modal.ProdutoEditado;

                novoProduto.Id = produtos.Count + 1;

                produtos.Add(novoProduto);

                JsonDatabase.Save("produtos.json", produtos);

                ProdutosFiltrados.Add(novoProduto);
            }
        }

        //private void Editar()
        //{
        //    if (ProdutoSelecionado == null)
        //    {
        //        MessageBox.Show("Selecione um produto.");
        //        return;
        //    }

        //    ProdutoSelecionado.Nome += " (Editado)";
        //    JsonDatabase.Save("produtos.json", produtos);

        //    Buscar();
        //}

        private void Editar()
        {
            if (ProdutoSelecionado == null)
            {
                MessageBox.Show("Selecione um produto.");
                return;
            }

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
            if (ProdutoSelecionado == null)
            {
                MessageBox.Show("Selecione um produto.");
                return;
            }

            produtos.Remove(ProdutoSelecionado);
            JsonDatabase.Save("produtos.json", produtos);

            ProdutosFiltrados.Remove(ProdutoSelecionado);
        }
    }
}
