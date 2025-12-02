using Microsoft.AspNetCore.Mvc;
using ecommerce.Application.Services;
using ecommerce.Application.DTOs;
using System;

namespace ecommerce.MinhaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        // Injeção de Dependência do Service (Critério 8)
        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpPost("registrar")]
        public IActionResult Registrar([FromBody] RegistroUsuarioDTO dto)
        {
            try
            {
                var usuario = _service.RegistrarUsuario(dto);
                return CreatedAtAction(nameof(Registrar), new { id = usuario.Id }, usuario);
            }
            catch (InvalidOperationException ex) // Erro de Conflito (Email duplicado)
            {
                return Conflict(new { erro = ex.Message });
            }
            catch (ArgumentException ex) // Erro de Validação (Senha curta, etc)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            try
            {
                var usuario = _service.Login(dto);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { erro = ex.Message });
            }
        }

        // DELETE: api/usuario/{id}
        [HttpDelete("{id}")]
        public IActionResult Remover(Guid id)
        {
            try
            {
                _service.RemoverUsuario(id);
                return Ok(new { mensagem = "Usuário excluído com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        // PUT: api/usuario/{id}/perfil
        [HttpPut("{id}/perfil")]
        public IActionResult AtualizarPerfil(Guid id, [FromBody] AtualizarPerfilDTO dto)
        {
            try
            {
                _service.AtualizarPerfil(id, dto.Nome, dto.Telefone);

                return Ok(new
                {
                    mensagem = "Perfil atualizado com sucesso!",
                    dados = new
                    {
                        Nome = dto.Nome,
                        Telefone = dto.Telefone
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        // PUT: api/usuario/{id}/endereco
        [HttpPut("{id}/endereco")]
        public IActionResult AtualizarEndereco(Guid id, [FromBody] RegistroUsuarioDTO dto)
        {
            try
            {
                _service.AtualizarEndereco(
                    id,
                    dto.Rua ?? "",
                    dto.Numero ?? "",
                    dto.Cidade ?? "",
                    dto.Estado ?? "",
                    dto.Cep ?? "",
                    dto.Complemento ?? ""
                );

                return Ok(new
                {
                    mensagem = "Endereço atualizado com sucesso!",
                    dadosAtualizados = new
                    {
                        Rua = dto.Rua,
                        Numero = dto.Numero,
                        Cidade = dto.Cidade,
                        Estado = dto.Estado,
                        Cep = dto.Cep,
                        Complemento = dto.Complemento
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        // GET: api/usuario/{id}
        [HttpGet("{id}")]
        public IActionResult ObterPorId(Guid id)
        {
            try
            {
                var usuario = _service.ObterUsuarioPorId(id);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return NotFound(new { erro = ex.Message });
            }
        }
    }
}