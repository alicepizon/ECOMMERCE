using Microsoft.AspNetCore.Mvc;
using ecommerce.Application.Services;
using ecommerce.Application.DTOs;
using System;

namespace ecommerce.MinhaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly CarrinhoService _service;

        public CarrinhoController(CarrinhoService service)
        {
            _service = service;
        }

        // POST: api/carrinho/adicionar
        [HttpPost("adicionar")]
        public IActionResult AdicionarItem([FromBody] AdicionarItemDTO dto)
        {
            try
            {
                _service.AdicionarItem(dto);
                return Ok(new { mensagem = "Item adicionado ao carrinho!" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        // PUT: api/carrinho/item/{produtoId}
        [HttpPut("item/{produtoId}")]
        public IActionResult AtualizarItem(Guid produtoId, [FromBody] AtualizarItemRequest request)
        {
            try
            {
                _service.AtualizarItem(request.UsuarioId, produtoId, request.NovaQuantidade);
                return Ok(new { mensagem = "Quantidade atualizada!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        // DELETE: api/carrinho/item/{produtoId}?usuarioId=...
        [HttpDelete("item/{produtoId}")]
        public IActionResult RemoverItem(Guid produtoId, [FromQuery] Guid usuarioId)
        {
            try
            {
                _service.RemoverItem(usuarioId, produtoId);
                return Ok(new { mensagem = "Item removido." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        // GET: api/carrinho/{usuarioId}
        [HttpGet("{usuarioId}")]
        public IActionResult ObterCarrinho(Guid usuarioId)
        {
            try
            {
                var carrinho = _service.ObterCarrinho(usuarioId);
                return Ok(carrinho);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }

    public class AtualizarItemRequest
    {
        public Guid UsuarioId { get; set; }
        public int NovaQuantidade { get; set; }
    }
}