using System;
using ecommerce.Dominio.Entidades;
using ecommerce.Dominio.Interfaces;
using ecommerce.Application.DTOs;

namespace ecommerce.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepo;

        public UsuarioService(IUsuarioRepository usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }

        public UsuarioRespostaDTO RegistrarUsuario(RegistroUsuarioDTO dto)
        {
            // 1. Verificar duplicidade
            // Se o email vier nulo no JSON, passamos string vazia para o repositório não quebrar
            var existente = _usuarioRepo.ObterPorEmail(dto.Email ?? "");
            if (existente != null)
                throw new InvalidOperationException("E-mail já cadastrado.");

            // 2. Criar Entidade
            var usuario = new Usuario(
                dto.Nome ?? "",
                dto.Email ?? "",
                dto.Senha ?? "",
                dto.Telefone ?? ""
            );

            // 3. Preencher Endereço
            usuario.AtualizarEndereco(
                dto.Rua ?? "",
                dto.Numero ?? "",
                dto.Cidade ?? "",
                dto.Estado ?? "",
                dto.Cep ?? "",
                dto.Complemento ?? ""
            );

            // 4. Persistir
            _usuarioRepo.Adicionar(usuario);

            // 5. Retornar DTO
            return new UsuarioRespostaDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email
            };
        }

        public UsuarioRespostaDTO Login(LoginDTO dto)
        {
            var usuario = _usuarioRepo.ObterPorEmail(dto.Email);

            if (usuario == null || !usuario.ValidarSenha(dto.Senha))
                throw new ArgumentException("Usuário ou senha inválidos.");

            return new UsuarioRespostaDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email
            };
        }

        public void RemoverUsuario(Guid id)
        {
            var usuario = _usuarioRepo.ObterPorId(id);
            if (usuario == null)
                throw new ArgumentException("Usuário não encontrado.");

            _usuarioRepo.Remover(id);
        }

        public void AtualizarEndereco(Guid usuarioId, string rua, string numero, string cidade, string estado, string cep, string complemento)
        {
            var usuario = _usuarioRepo.ObterPorId(usuarioId);
            if (usuario == null) throw new ArgumentException("Usuário não encontrado.");

            usuario.AtualizarEndereco(rua, numero, cidade, estado, cep, complemento ?? "");
            _usuarioRepo.Atualizar(usuario);
        }

        public void AtualizarPerfil(Guid usuarioId, string nome, string telefone)
        {
            var usuario = _usuarioRepo.ObterPorId(usuarioId);
            if (usuario == null) throw new ArgumentException("Usuário não encontrado.");

            usuario.AtualizarPerfil(nome, telefone);
            _usuarioRepo.Atualizar(usuario);
        }

        public UsuarioDetalhadoDTO ObterUsuarioPorId(Guid id)
        {
            var usuario = _usuarioRepo.ObterPorId(id);

            if (usuario == null)
                throw new ArgumentException("Usuário não encontrado.");

            return new UsuarioDetalhadoDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                Rua = usuario.Rua,
                Numero = usuario.Numero,
                Cidade = usuario.Cidade,
                Estado = usuario.Estado,
                Cep = usuario.Cep,
                Complemento = usuario.Complemento
            };
        }
    }
}