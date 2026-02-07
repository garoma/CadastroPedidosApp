using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using PedidoApp.Models;
using PedidoApp.Helpers;

namespace PedidoApp.ViewModels
{
    public class PessoaModalViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _nome;
        private string _cpf;
        private string _endereco;

        public Pessoa PessoaEditada { get; private set; }

        // Callback para fechar a janela
        public Action<bool?> FecharJanela { get; set; }

        public string Nome
        {
            get => _nome;
            set
            {
                if (_nome != value)
                {
                    _nome = value;
                    OnPropertyChanged(nameof(Nome));
                }
            }
        }

        public string CPF
        {
            get => _cpf;
            set
            {
                if (_cpf != value)
                {
                    _cpf = value;
                    OnPropertyChanged(nameof(CPF));
                }
            }
        }

        public string Endereco
        {
            get => _endereco;
            set
            {
                if (_endereco != value)
                {
                    _endereco = value;
                    OnPropertyChanged(nameof(Endereco));
                }
            }
        }

        public RelayCommand SalvarCommand { get; }
        public RelayCommand CancelarCommand { get; }

        public PessoaModalViewModel(Pessoa pessoa = null)
        {
            if (pessoa != null)
            {
                PessoaEditada = pessoa;
                Nome = pessoa.Nome;
                CPF = pessoa.CPF;
                Endereco = pessoa.Endereco;
            }

            SalvarCommand = new RelayCommand(Salvar, PodeSalvar);
            CancelarCommand = new RelayCommand(Cancelar);
        }

        private bool PodeSalvar()
        {
            return string.IsNullOrWhiteSpace(this[nameof(Nome)]) &&
                   string.IsNullOrWhiteSpace(this[nameof(CPF)]);
        }

        private void Salvar()
        {
            if (PessoaEditada == null)
                PessoaEditada = new Pessoa();

            PessoaEditada.Nome = Nome;
            PessoaEditada.CPF = CPF;
            PessoaEditada.Endereco = Endereco;

            FecharJanela?.Invoke(true);
        }

        private void Cancelar()
        {
            FecharJanela?.Invoke(false);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string nome)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        }
        #endregion

        #region IDataErrorInfo
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Nome):
                        return string.IsNullOrWhiteSpace(Nome) ? "Nome obrigatório" : null;
                    case nameof(CPF):
                        string cpfLimpo = Regex.Replace(CPF ?? "", @"[^\d]", "");
                        if (string.IsNullOrWhiteSpace(CPF))
                            return "CPF obrigatório";
                        if (cpfLimpo.Length != 11)
                            return "CPF deve ter 11 dígitos";
                        if (!CpfHelper.ValidarCpf(cpfLimpo))
                            return "CPF inválido";
                        break;
                }
                return null;
            }
        }

        public string Error => null;
        #endregion
    }
}
