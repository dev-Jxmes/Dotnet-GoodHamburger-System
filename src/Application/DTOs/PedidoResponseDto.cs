namespace good_hamburguer_system.Application.DTOs
{
    public class PedidoResponseDto
    {
        public Guid Id { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Desconto { get; set; }
        public decimal Total { get; set; }
    }
}