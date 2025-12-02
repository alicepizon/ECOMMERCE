using Microsoft.AspNetCore.Mvc;
using ecommerce.Application.Services;
using ecommerce.Application.DTOs;
using System;

namespace ecommerce.MinhaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _service;

        public ProdutoController(ProdutoService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] CriarProdutoDTO dto)
        {
            try
            {
                _service.CadastrarProduto(dto);
                return StatusCode(201, new { mensagem = "Produto cadastrado com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult Listar()
        {
            // O Service já retorna a lista pronta
            return Ok(_service.ListarTodos());
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, [FromBody] CriarProdutoDTO dto)
        {
            try
            {
                _service.AtualizarProduto(id, dto.Nome, dto.Descricao, dto.Preco, dto.CategoriaId);

                return Ok(new
                {
                    mensagem = "Produto atualizado com sucesso!",
                    dados = dto 
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPost("{id}/repor-estoque")]
        public IActionResult ReporEstoque(Guid id, [FromBody] int quantidade)
        {
            try
            {
                _service.ReporEstoque(id, quantidade);
                return Ok(new { mensagem = "Estoque atualizado." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Desativar(Guid id)
        {
            try
            {
                _service.DesativarProduto(id);

                return Ok(new
                {
                    mensagem = "Produto desativado com sucesso!",
                    id = id
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}