using good_hamburguer_system.Domain.Enums;
namespace good_hamburguer_system.Domain.Entities
{
    public class Pedido
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public ItemMenu? Sanduiche { get; set; }
        public ItemMenu? Batata { get; set; }
        public ItemMenu? Refrigerante { get; set; }

        public decimal Subtotal { get; set; }
        public decimal Desconto { get; set; }
        public decimal Total { get; set; }
}
}