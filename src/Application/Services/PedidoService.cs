using good_hamburguer_system.Application.DTOs;
using good_hamburguer_system.Domain.Entities;
using good_hamburguer_system.Domain.Exceptions;
using good_hamburguer_system.Domain.Enums;

namespace good_hamburguer_system.Application.Services
{
    public class PedidoService
    {
        public Pedido CriarPedido(PedidoRequestDto dto)
        {
            if (dto.Itens == null || !dto.Itens.Any())
                throw new BusinessException("Pedido deve conter ao menos um item.");

            var itens = dto.Itens
                .Select(nome =>
                {
                    if (string.IsNullOrWhiteSpace(nome))
                        throw new BusinessException("Item inválido: nome vazio.");

                    var item = CardapioService.ObterPorNome(nome);

                    if (item == null)
                        throw new BusinessException($"Item '{nome}' não existe.");

                    return item;
                })
                .ToList();

            ValidarDuplicados(itens);

            var pedido = new Pedido
            {
                Sanduiche = itens.FirstOrDefault(i => i.Tipo == TipoItem.Sanduiche),
                Batata = itens.FirstOrDefault(i => i.Tipo == TipoItem.Batata),
                Refrigerante = itens.FirstOrDefault(i => i.Tipo == TipoItem.Refrigerante)
            };

            Calcular(pedido);

            return pedido;
        }

        private void Validar(Pedido pedido)
        {
            if (pedido.Sanduiche == null && pedido.Batata == null && pedido.Refrigerante == null)
                throw new BusinessException("Pedido vazio não é permitido.");

            var tipos = new List<TipoItem>();

            if (pedido.Sanduiche != null) tipos.Add(TipoItem.Sanduiche);
            if (pedido.Batata != null) tipos.Add(TipoItem.Batata);
            if (pedido.Refrigerante != null) tipos.Add(TipoItem.Refrigerante);

            if (tipos.Count != tipos.Distinct().Count())
                throw new BusinessException("Pedido contém itens duplicados por categoria.");
        }

        private void ValidarDuplicados(List<ItemMenu> itens)
        {
            var agrupados = itens
                .GroupBy(i => i.Tipo)
                .Where(g => g.Count() > 1)
                .ToList();

            if (agrupados.Any())
            {
                var tiposDuplicados = string.Join(", ", agrupados.Select(g => g.Key));
                throw new BusinessException($"Itens duplicados para: {tiposDuplicados}");
            }
        }

        private void Calcular(Pedido pedido)
        {
            var itens = new List<ItemMenu?>
            {
                pedido.Sanduiche,
                pedido.Batata,
                pedido.Refrigerante
            }
            .Where(i => i != null)
            .Select(i => i!)
            .ToList();

            pedido.Subtotal = itens.Sum(i => i.Preco);

            decimal percentualDesconto = 0;

            if (pedido.Sanduiche != null && pedido.Batata != null && pedido.Refrigerante != null)
                percentualDesconto = 0.20m;
            else if (pedido.Sanduiche != null && pedido.Refrigerante != null)
                percentualDesconto = 0.15m;
            else if (pedido.Sanduiche != null && pedido.Batata != null)
                percentualDesconto = 0.10m;

            pedido.Desconto = pedido.Subtotal * percentualDesconto;
            pedido.Total = pedido.Subtotal - pedido.Desconto;
        }
    }
}