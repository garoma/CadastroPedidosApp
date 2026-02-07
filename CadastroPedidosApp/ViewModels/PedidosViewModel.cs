using PedidoApp.Models;
using PedidoApp.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PedidoApp.ViewModels
{
    public class PedidosViewModel : BaseViewModel
    {
        public ObservableCollection<Pedido> Pedidos { get; set; }

        public Pedido PedidoSelecionado { get; set; }

        public RelayCommand StatusPagoCommand { get; set; }
        public RelayCommand StatusEnviadoCommand { get; set; }
        public RelayCommand StatusRecebidoCommand { get; set; }

        public PedidosViewModel()
        {
            Pedidos = new ObservableCollection<Pedido>(
                JsonDatabase.Load<Pedido>("pedidos.json")
            );

            StatusPagoCommand = new RelayCommand(() => AlterarStatus("Pago"));
            StatusEnviadoCommand = new RelayCommand(() => AlterarStatus("Enviado"));
            StatusRecebidoCommand = new RelayCommand(() => AlterarStatus("Recebido"));
        }

        private void AlterarStatus(string novoStatus)
        {
            if (PedidoSelecionado == null)
            {
                MessageBox.Show("Selecione um pedido.");
                return;
            }

            if (!PedidoSelecionado.Finalizado)
            {
                MessageBox.Show("Só é possível alterar status após finalizar o pedido.");
                return;
            }

            PedidoSelecionado.Status = novoStatus;

            var lista = Pedidos.ToList();

            JsonDatabase.Save("pedidos.json", lista);

            MessageBox.Show($"Status alterado para: {novoStatus}");

            OnPropertyChanged(nameof(Pedidos));
        }
    }
}
