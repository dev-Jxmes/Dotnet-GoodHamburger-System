using good_hamburguer_system.Domain.Entities;
using good_hamburguer_system.Domain.Enums;

namespace good_hamburguer_system.Application.Services
{
    public static class CardapioService
    {
        public static List<ItemMenu> Itens = new()
        {
            new() { Nome = "X Burger", Preco = 5.00m, Tipo = TipoItem.Sanduiche },
            new() { Nome = "X Egg", Preco = 4.50m, Tipo = TipoItem.Sanduiche },
            new() { Nome = "X Bacon", Preco = 7.00m, Tipo = TipoItem.Sanduiche },

            new() { Nome = "Batata frita", Preco = 2.00m, Tipo = TipoItem.Batata },
            new() { Nome = "Refrigerante", Preco = 2.50m, Tipo = TipoItem.Refrigerante }
        };

        public static ItemMenu? ObterPorNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return null;

            nome = nome.Trim();

            return Itens.FirstOrDefault(i =>
                i.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        }
    }
}