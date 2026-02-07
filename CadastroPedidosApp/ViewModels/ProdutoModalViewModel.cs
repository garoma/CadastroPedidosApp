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

        // Callback para a View fechar
        public Action<bool?> FecharJanela { get; set; }

        public string Nome
        {
            get => _nome;
            set { _nome = value; OnPropertyChanged(nameof(Nome)); }
        }

        public string Codigo
        {
            get => _codigo;
            set { _codigo = value; OnPropertyChanged(nameof(Codigo)); }
        }

        public string Valor
        {
            get => _valor;
            set { _valor = value; OnPropertyChanged(nameof(Valor)); }
        }

        public RelayCommand SalvarCommand { get; }
        public RelayCommand CancelarCommand { get; }

        public ProdutoModalViewModel(Produto produto = null)
        {
            ProdutoEditado = produto ?? new Produto();

            if (produto != null)
            {
                Nome = produto.Nome;
                Codigo = produto.Codigo;
                Valor = produto.Valor.ToString("F2", CultureInfo.InvariantCulture);
            }

            SalvarCommand = new RelayCommand(Salvar, PodeSalvar);
            CancelarCommand = new RelayCommand(Cancelar);
        }

        private bool PodeSalvar()
        {
            return string.IsNullOrWhiteSpace(this[nameof(Nome)]) &&
                   string.IsNullOrWhiteSpace(this[nameof(Codigo)]) &&
                   string.IsNullOrWhiteSpace(this[nameof(Valor)]);
        }

        private void Salvar()
        {
            ProdutoEditado.Nome = Nome;
            ProdutoEditado.Codigo = Codigo;

            if (!decimal.TryParse(Valor.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valorDecimal))
            {
                MessageBox.Show("Digite um valor válido.");
                return;
            }
            ProdutoEditado.Valor = valorDecimal;

            FecharJanela?.Invoke(true);
        }

        private void Cancelar()
        {
            FecharJanela?.Invoke(false);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string nome) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        #endregion

        #region IDataErrorInfo
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Nome): return string.IsNullOrWhiteSpace(Nome) ? "Nome obrigatório" : null;
                    case nameof(Codigo): return string.IsNullOrWhiteSpace(Codigo) ? "Código obrigatório" : null;
                    case nameof(Valor):
                        if (string.IsNullOrWhiteSpace(Valor)) return "Valor obrigatório";
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
