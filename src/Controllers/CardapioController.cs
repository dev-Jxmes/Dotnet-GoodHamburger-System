using good_hamburguer_system.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace good_hamburguer_system.Controllers
{
    [ApiController]
    [Route("api/cardapio")]
    public class CardapioController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(CardapioService.Itens);
        }
    }
}