using good_hamburguer_system.Domain.Entities;

namespace good_hamburguer_system.Infrastructure.Repositories
{
    public class PedidoRepository
    {
        private static readonly List<Pedido> _pedidos = new();

        public List<Pedido> Listar() => _pedidos;

        public Pedido? Obter(Guid id) => _pedidos.FirstOrDefault(p => p.Id == id);

        public void Adicionar(Pedido pedido) => _pedidos.Add(pedido);

        public void Remover(Pedido pedido) => _pedidos.Remove(pedido);
    }
}