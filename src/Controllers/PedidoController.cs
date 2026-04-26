using good_hamburguer_system.Application.DTOs;
using good_hamburguer_system.Application.Services;
using good_hamburguer_system.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace good_hamburguer_system.Controllers
{
    [ApiController]
    [Route("api/pedidos")]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoService _service;
        private readonly PedidoRepository _repo;

        public PedidoController(PedidoService service, PedidoRepository repo)
        {
            _service = service;
            _repo = repo;
        }

        [HttpPost]
        public IActionResult Criar(PedidoRequestDto dto)
        {
            var pedido = _service.CriarPedido(dto);
            _repo.Adicionar(pedido);

            return Ok(pedido);
        }

        [HttpGet]
        public IActionResult Listar() => Ok(_repo.Listar());

        [HttpGet("{id}")]
        public IActionResult Obter(Guid id)
        {
            var pedido = _repo.Obter(id);
            if (pedido == null) return NotFound("Pedido não encontrado");

            return Ok(pedido);
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(Guid id)
        {
            var pedido = _repo.Obter(id);
            if (pedido == null) return NotFound();

            _repo.Remover(pedido);
            return NoContent();
        }
    }
}