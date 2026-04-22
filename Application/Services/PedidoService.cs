using good_hamburguer_system.Application.DTOs;
using good_hamburguer_system.Domain.Entities;
using good_hamburguer_system.Domain.Exceptions;

namespace good_hamburguer_system.Application.Services
{
    public class PedidoService
    {
        public Pedido CriarPedido(PedidoRequestDto dto)
        {
            var pedido = new Pedido
            {
                Sanduiche = dto.Sanduiche != null ? CardapioService.ObterPorNome(dto.Sanduiche) : null,
                Batata = dto.Batata != null ? CardapioService.ObterPorNome(dto.Batata) : null,
                Refrigerante = dto.Refrigerante != null ? CardapioService.ObterPorNome(dto.Refrigerante) : null
            };

            Validar(pedido);
            Calcular(pedido);

            return pedido;
        }

        private void Validar(Pedido pedido)
        {
            if (pedido.Sanduiche == null && pedido.Batata == null && pedido.Refrigerante == null)
                throw new BusinessException("Pedido vazio não é permitido.");

            if (pedido.Sanduiche?.Tipo != null && pedido.Sanduiche.Tipo != Domain.Enums.TipoItem.Sanduiche)
                throw new BusinessException("Item inválido para sanduíche.");
        }

        private void Calcular(Pedido pedido)
        {
            var itens = new List<ItemMenu?>
            {
                pedido.Sanduiche,
                pedido.Batata,
                pedido.Refrigerante
            }.Where(i => i != null).ToList();

            pedido.Subtotal = itens.Sum(i => i!.Preco);

            decimal desconto = 0;

            if (itens.Count == 3)
                desconto = 0.20m;
            else if (pedido.Sanduiche != null && pedido.Refrigerante != null)
                desconto = 0.15m;
            else if (pedido.Sanduiche != null && pedido.Batata != null)
                desconto = 0.10m;

            pedido.Desconto = pedido.Subtotal * desconto;
            pedido.Total = pedido.Subtotal - pedido.Desconto;
        }
    }
}