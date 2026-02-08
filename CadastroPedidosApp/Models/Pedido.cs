//using System.Collections.Generic;
//using System.Linq;

//namespace PedidoApp.Models
//{
//    public class Pedido
//    {
//        public int Id { get; set; }

//        public Pessoa Cliente { get; set; }

//        public List<PedidoItem> Itens { get; set; } = new List<PedidoItem>();

//        public string Status { get; set; } = "Pendente";

//        public bool Finalizado { get; set; } = false;

//        public decimal Total => Itens.Sum(i => i.TotalItem);
//    }
//}

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PedidoApp.Models
{
    public class Pedido : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }

        private Pessoa cliente;
        public Pessoa Cliente
        {
            get => cliente;
            set { cliente = value; OnPropertyChanged(nameof(Cliente)); }
        }

        private decimal total;
        public decimal Total
        {
            get => total;
            set { total = value; OnPropertyChanged(nameof(Total)); }
        }

        private string status;
        public string Status
        {
            get => status;
            set { status = value; OnPropertyChanged(nameof(Status)); }
        }

        private bool finalizado;
        public bool Finalizado
        {
            get => finalizado;
            set { finalizado = value; OnPropertyChanged(nameof(Finalizado)); }
        }

        // ✅ Lista de itens do pedido
        public List<PedidoItem> Itens { get; set; } = new List<PedidoItem>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ItemPedido : INotifyPropertyChanged
    {
        private string nome;
        public string Nome
        {
            get => nome;
            set { nome = value; OnPropertyChanged(nameof(Nome)); }
        }

        private int quantidade;
        public int Quantidade
        {
            get => quantidade;
            set { quantidade = value; OnPropertyChanged(nameof(Quantidade)); }
        }

        private decimal valor;
        public decimal Valor
        {
            get => valor;
            set { valor = value; OnPropertyChanged(nameof(Valor)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
