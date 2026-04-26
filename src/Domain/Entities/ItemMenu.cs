using good_hamburguer_system.Domain.Enums;
namespace good_hamburguer_system.Domain.Entities
{
    public class ItemMenu
    {
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public TipoItem Tipo { get; set; }
    }
}