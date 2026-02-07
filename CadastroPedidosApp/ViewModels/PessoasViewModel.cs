using PedidoApp.Models;
using PedidoApp.Services;
using PedidoApp.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PedidoApp.ViewModels
{
    public class PessoasViewModel : BaseViewModel
    {
        private List<Pessoa> pessoas;

        public ObservableCollection<Pessoa> PessoasFiltradas { get; set; }
        public Pessoa PessoaSelecionada { get; set; }

        public Pessoa  pessoa { get; set; }

        public string TextoFiltro { get; set; }

        public RelayCommand BuscarCommand { get; set; }
        public RelayCommand IncluirCommand { get; set; }
        public RelayCommand EditarCommand { get; set; }
        public RelayCommand ExcluirCommand { get; set; }
        public RelayCommand IncluirPedidoCommand { get; set; }

        public PessoasViewModel()
        {
            pessoas = JsonDatabase.Load<Pessoa>("pessoas.json");

            PessoasFiltradas = new ObservableCollection<Pessoa>(pessoas);

            BuscarCommand = new RelayCommand(Buscar);
            IncluirCommand = new RelayCommand(Incluir);
            EditarCommand = new RelayCommand(Editar);
            ExcluirCommand = new RelayCommand(Excluir);
            IncluirPedidoCommand = new RelayCommand(IncluirPedido);
        }

        private void Buscar()
        {
            var filtro = TextoFiltro?.ToLower() ?? "";

            var resultado = pessoas.Where(p =>
                p.Nome.ToLower().Contains(filtro) ||
                p.CPF.Contains(filtro)).ToList();

            PessoasFiltradas.Clear();

            foreach (var p in resultado)
                PessoasFiltradas.Add(p);
        }

        private void Incluir()
        {
            // Abre modal vazia (novo cadastro)
            var modal = new PessoaModal();
            modal.Owner = Application.Current.MainWindow;

            if (modal.ShowDialog() == true)
            {
                var novaPessoa = modal.ViewModel.PessoaEditada;

                // Gera ID simples
                novaPessoa.Id = pessoas.Count + 1;

                // Adiciona na lista principal
                pessoas.Add(novaPessoa);

                // Salva no JSON
                JsonDatabase.Save("pessoas.json", pessoas);

                // Atualiza lista da tela
                PessoasFiltradas.Add(novaPessoa);
            }
        }

        private void Editar()
        {
            if (PessoaSelecionada == null)
            {
                MessageBox.Show("Selecione uma pessoa.");
                return;
            }

            // Abre modal preenchida
            var modal = new PessoaModal(PessoaSelecionada);
            modal.Owner = Application.Current.MainWindow;

            if (modal.ShowDialog() == true)
            {
                // PessoaSelecionada já foi alterada dentro do modal

                // Salva no JSON
                JsonDatabase.Save("pessoas.json", pessoas);

                // Atualiza grid
                Buscar();
            }
        }

        private void Excluir()
        {
            if (PessoaSelecionada == null)
            {
                MessageBox.Show("Selecione uma pessoa.");
                return;
            }

            pessoas.Remove(PessoaSelecionada);
            JsonDatabase.Save("pessoas.json", pessoas);

            PessoasFiltradas.Remove(PessoaSelecionada);
        }

        private void IncluirPedido()
        {
            if (PessoaSelecionada == null)
            {
                MessageBox.Show("Selecione uma pessoa para criar um pedido.");
                return;
            }

            var main = (MainWindow)System.Windows.Application.Current.MainWindow;

            main.ConteudoTela.Content = new Views.PedidoView(PessoaSelecionada);
        }
    }
}
