using System.Collections.Generic;
using System.Linq;

namespace PedidoApp.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        public Pessoa Cliente { get; set; }

        public List<PedidoItem> Itens { get; set; } = new List<PedidoItem>();

        public string Status { get; set; } = "Pendente";

        public bool Finalizado { get; set; } = false;

        public decimal Total => Itens.Sum(i => i.TotalItem);
    }
}
