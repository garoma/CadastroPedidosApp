using PedidoApp.Models;
using PedidoApp.Services;
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
            var nova = new Pessoa
            {
                Id = pessoas.Count + 1,
                Nome = "Nova Pessoa",
                CPF = "00000000000",
                Endereco = ""
            };

            pessoas.Add(nova);
            JsonDatabase.Save("pessoas.json", pessoas);

            PessoasFiltradas.Add(nova);
        }

        private void Editar()
        {
            if (PessoaSelecionada == null)
            {
                MessageBox.Show("Selecione uma pessoa.");
                return;
            }

            PessoaSelecionada.Nome += " (Editado)";
            JsonDatabase.Save("pessoas.json", pessoas);

            Buscar();
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
