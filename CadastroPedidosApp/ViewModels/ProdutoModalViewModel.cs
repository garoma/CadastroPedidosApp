using PedidoApp.Models;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;

namespace PedidoApp.ViewModels
{
    public class ProdutoModalViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _nome;
        private string _codigo;
        private string _valor;

        public Produto ProdutoEditado { get; private set; }

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

        public string Codigo
        {
            get => _codigo;
            set
            {
                if (_codigo != value)
                {
                    _codigo = value;
                    OnPropertyChanged(nameof(Codigo));
                }
            }
        }

        public string Valor
        {
            get => _valor;
            set
            {
                if (_valor != value)
                {
                    _valor = value;
                    OnPropertyChanged(nameof(Valor));
                }
            }
        }

        public RelayCommand SalvarCommand { get; }
        public RelayCommand CancelarCommand { get; }

        public ProdutoModalViewModel(Produto produto = null)
        {
            if (produto != null)
            {
                ProdutoEditado = produto;
                Nome = produto.Nome;
                Codigo = produto.Codigo;
                Valor = produto.Valor.ToString("F2", CultureInfo.InvariantCulture);
            }

            SalvarCommand = new RelayCommand(Salvar, PodeSalvar);
            CancelarCommand = new RelayCommand(Cancelar);
        }

        private bool PodeSalvar()
        {
            // Validações básicas
            return string.IsNullOrWhiteSpace(this[nameof(Nome)]) &&
                   string.IsNullOrWhiteSpace(this[nameof(Codigo)]) &&
                   string.IsNullOrWhiteSpace(this[nameof(Valor)]);
        }

        private void Salvar()
        {
            if (ProdutoEditado == null)
                ProdutoEditado = new Produto();

            ProdutoEditado.Nome = Nome;
            ProdutoEditado.Codigo = Codigo;

            if (decimal.TryParse(Valor.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valorDecimal))
            {
                ProdutoEditado.Valor = valorDecimal;
            }
            else
            {
                MessageBox.Show("Digite um valor válido.");
                return;
            }

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
                    case nameof(Codigo):
                        return string.IsNullOrWhiteSpace(Codigo) ? "Código obrigatório" : null;
                    case nameof(Valor):
                        if (string.IsNullOrWhiteSpace(Valor))
                            return "Valor obrigatório";
                        if (!decimal.TryParse(Valor.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out _))
                            return "Valor inválido";
                        break;
                }
                return null;
            }
        }

        public string Error => null;
        #endregion
    }
}
