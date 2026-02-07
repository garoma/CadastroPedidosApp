using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PedidoApp.Models
{
    public class PedidoItem : INotifyPropertyChanged
    {
        private int quantidade;

        public Produto Produto { get; set; }

        public int Quantidade
        {
            get => quantidade;
            set
            {
                quantidade = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalItem));
            }
        }

        public decimal TotalItem => Produto.Valor * Quantidade;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string nome = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        }
    }
}
