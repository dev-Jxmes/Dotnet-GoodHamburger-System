using Xunit;
using FluentAssertions;
using good_hamburguer_system.Application.DTOs;
using good_hamburguer_system.Application.Services;
using good_hamburguer_system.Domain.Exceptions;

namespace GoodHamburger.Tests;

public class PedidoServiceTests
{
    private readonly PedidoService _service = new();

    // DESCONTOS
    [Fact]
    public void Deve_Aplicar_20_PorCento_Quando_Completo()
    {
        var dto = new PedidoRequestDto
        {
            Itens = new List<string> { "X Bacon", "Batata frita", "Refrigerante" }
        };

        var pedido = _service.CriarPedido(dto);

        pedido.Subtotal.Should().Be(11.5m);
        pedido.Desconto.Should().Be(2.3m);
        pedido.Total.Should().Be(9.2m);
    }

    [Fact]
    public void Deve_Aplicar_15_PorCento_Sanduiche_Com_Refrigerante()
    {
        var dto = new PedidoRequestDto
        {
            Itens = new List<string> { "X Bacon", "Refrigerante" }
        };

        var pedido = _service.CriarPedido(dto);

        pedido.Subtotal.Should().Be(9.5m);
        pedido.Desconto.Should().Be(1.425m);
        pedido.Total.Should().Be(8.075m);
    }

    [Fact]
    public void Deve_Aplicar_10_PorCento_Sanduiche_Com_Batata()
    {
        var dto = new PedidoRequestDto
        {
            Itens = new List<string> { "X Bacon", "Batata frita" }
        };

        var pedido = _service.CriarPedido(dto);

        pedido.Subtotal.Should().Be(9m);
        pedido.Desconto.Should().Be(0.9m);
        pedido.Total.Should().Be(8.1m);
    }

    [Fact]
    public void Nao_Deve_Aplicar_Desconto_Quando_Apenas_Um_Item()
    {
        var dto = new PedidoRequestDto
        {
            Itens = new List<string> { "Refrigerante" }
        };

        var pedido = _service.CriarPedido(dto);

        pedido.Desconto.Should().Be(0);
        pedido.Total.Should().Be(2.5m);
    }

    // ERROS
    [Fact]
    public void Deve_Lancar_Erro_Para_Pedido_Vazio()
    {
        var dto = new PedidoRequestDto
        {
            Itens = new List<string>()
        };

        var act = () => _service.CriarPedido(dto);

        act.Should().Throw<BusinessException>()
            .WithMessage("*Pedido*");
    }

    [Fact]
    public void Deve_Lancar_Erro_Para_Item_Inexistente()
    {
        var dto = new PedidoRequestDto
        {
            Itens = new List<string> { "Pizza" }
        };

        var act = () => _service.CriarPedido(dto);

        act.Should().Throw<BusinessException>()
            .WithMessage("*não existe*");
    }

    [Fact]
    public void Deve_Lancar_Erro_Para_Duplicidade_De_Refrigerante()
    {
        var dto = new PedidoRequestDto
        {
            Itens = new List<string> { "Refrigerante", "Refrigerante" }
        };

        var act = () => _service.CriarPedido(dto);

        act.Should().Throw<BusinessException>()
            .WithMessage("*Refrigerante*");
    }

    [Fact]
    public void Deve_Lancar_Erro_Para_Dois_Sanduiches()
    {
        var dto = new PedidoRequestDto
        {
            Itens = new List<string> { "X Bacon", "X Egg" }
        };

        var act = () => _service.CriarPedido(dto);

        act.Should().Throw<BusinessException>()
            .WithMessage("*Sanduiche*");
    }


    // EDGE CASES
    [Fact]
    public void Deve_Aceitar_Input_Com_Espacos()
    {
        var dto = new PedidoRequestDto
        {
            Itens = new List<string> { "  X Bacon  " }
        };

        var pedido = _service.CriarPedido(dto);

        pedido.Total.Should().Be(7m);
    }

    [Fact]
    public void Deve_Aceitar_Ordem_Diferente()
    {
        var dto = new PedidoRequestDto
        {
            Itens = new List<string> { "Refrigerante", "Batata frita", "X Bacon" }
        };

        var pedido = _service.CriarPedido(dto);

        pedido.Desconto.Should().Be(2.3m);
    }
}