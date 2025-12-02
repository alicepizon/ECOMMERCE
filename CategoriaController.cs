using Microsoft.AspNetCore.Mvc;
using ecommerce.Application.Services;
using ecommerce.Application.DTOs;
using System;

namespace ecommerce.MinhaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _service;

        public CategoriaController(CategoriaService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Criar([FromBody] CategoriaDTO dto)
        {
            try
            {
                var categoria = _service.Criar(dto);
                return Created("", categoria);
            }
            catch (Exception ex) { return BadRequest(new { erro = ex.Message }); }
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_service.ListarTodas());
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, [FromBody] CategoriaDTO dto)
        {
            try
            {
                _service.Atualizar(id, dto);

                return Ok(new
                {
                    mensagem = "Categoria atualizada com sucesso!",
                    dados = dto
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(Guid id)
        {
            try
            {
                _service.Remover(id);

                return Ok(new { mensagem = "Categoria removida com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}