using PedidoApp.Models;
using PedidoApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PedidoApp.ViewModels
{
    public class PedidoViewModel : BaseViewModel
    {
        public string NomeCliente { get; set; }

        public ObservableCollection<Produto> Produtos { get; set; }

        public Produto ProdutoSelecionado { get; set; }

        public ObservableCollection<PedidoItem> ItensPedido { get; set; }

        public RelayCommand AdicionarProdutoCommand { get; set; }
        public RelayCommand FinalizarPedidoCommand { get; set; }

        private Pessoa cliente;
        private Pedido pedidoAtual;

        public string TotalPedido => $"Total: R$ {ItensPedido.Sum(i => i.TotalItem)}";

        public PedidoViewModel(Pessoa pessoa)
        {
            cliente = pessoa;
            NomeCliente = $"Pedido para: {cliente.Nome}";

            Produtos = new ObservableCollection<Produto>(
                JsonDatabase.Load<Produto>("produtos.json")
            );

            ItensPedido = new ObservableCollection<PedidoItem>();

            pedidoAtual = new Pedido
            {
                Cliente = cliente
            };

            AdicionarProdutoCommand = new RelayCommand(AdicionarProduto);
            FinalizarPedidoCommand = new RelayCommand(FinalizarPedido);
        }

        private void AdicionarProduto()
        {
            if (ProdutoSelecionado == null)
            {
                MessageBox.Show("Selecione um produto.");
                return;
            }

            // Verifica se o produto já existe no pedido
            var existente = ItensPedido
                .FirstOrDefault(i => i.Produto.Id == ProdutoSelecionado.Id);

            if (existente != null)
            {
                // Se já existe, soma quantidade
                existente.Quantidade++;

                // Atualiza DataGrid
                OnPropertyChanged(nameof(ItensPedido));
            }
            else
            {
                // Se não existe, adiciona novo item
                var item = new PedidoItem
                {
                    Produto = ProdutoSelecionado,
                    Quantidade = 1
                };

                ItensPedido.Add(item);
            }

            // Atualiza total do pedido
            OnPropertyChanged(nameof(TotalPedido));
        }

        private void FinalizarPedido()
        {
            if (ItensPedido.Count == 0)
            {
                MessageBox.Show("Adicione produtos antes de finalizar.");
                return;
            }

            pedidoAtual.Itens = ItensPedido.ToList();
            pedidoAtual.Finalizado = true;
            pedidoAtual.Status = "Pendente";

            var pedidos = JsonDatabase.Load<Pedido>("pedidos.json");

            pedidoAtual.Id = pedidos.Count + 1;

            pedidos.Add(pedidoAtual);

            JsonDatabase.Save("pedidos.json", pedidos);

            MessageBox.Show("Pedido Finalizado com sucesso!");

        }
    }
}
