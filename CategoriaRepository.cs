using System;
using System.Collections.Generic;
using System.Linq;
using ecommerce.Dominio.Entidades;
using ecommerce.Dominio.Interfaces;
using ecommerce.Infraestrutura.Data;

namespace ecommerce.Infraestrutura.Repositorios
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
        }

        public void Atualizar(Categoria categoria)
        {
            // Em memória, a referência já atualiza. No EF Core, seria _context.Update
        }

        public void Remover(Guid id)
        {
            var categoria = ObterPorId(id);
            if (categoria != null)
            {
                // Validação extra: Não remover categoria se tiver produtos (opcional, mas bom)
                if (categoria.Produtos.Any())
                    throw new InvalidOperationException("Não é possível remover categoria com produtos vinculados.");

                _context.Categorias.Remove(categoria);
            }
        }

        public Categoria? ObterPorId(Guid id)
        {
            return _context.Categorias.FirstOrDefault(c => c.Id == id);
        }

        public List<Categoria> ListarTodas()
        {
            return _context.Categorias.ToList();
        }
    }
}