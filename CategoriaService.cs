using System;
using System.Collections.Generic;
using System.Linq;
using ecommerce.Dominio.Entidades;
using ecommerce.Dominio.Interfaces;
using ecommerce.Application.DTOs;

namespace ecommerce.Application.Services
{
    public class CategoriaService
    {
        private readonly ICategoriaRepository _repo;

        public CategoriaService(ICategoriaRepository repo)
        {
            _repo = repo;
        }

        public CategoriaRespostaDTO Criar(CategoriaDTO dto)
        {
            var categoria = new Categoria(dto.Nome, dto.Descricao);
            _repo.Adicionar(categoria);

            return new CategoriaRespostaDTO
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Descricao = categoria.Descricao
            };
        }

        public void Atualizar(Guid id, CategoriaDTO dto)
        {
            var categoria = _repo.ObterPorId(id);
            if (categoria == null) throw new ArgumentException("Categoria não encontrada.");

            categoria.Atualizar(dto.Nome, dto.Descricao);
            _repo.Atualizar(categoria);
        }

        public void Remover(Guid id)
        {
            var categoria = _repo.ObterPorId(id);
            if (categoria == null) throw new ArgumentException("Categoria não encontrada.");

            _repo.Remover(id);
        }

        public List<CategoriaRespostaDTO> ListarTodas()
        {
            return _repo.ListarTodas()
                .Select(c => new CategoriaRespostaDTO
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Descricao = c.Descricao
                }).ToList();
        }
    }
}