using System;
using System.Collections.Generic;
using System.Linq;
using ecommerce.Dominio.Entidades;
using ecommerce.Dominio.Interfaces;
using ecommerce.Infraestrutura.Data;

namespace ecommerce.Infraestrutura.Repositorios
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Produto produto)
        {
            _context.Produtos.Add(produto);
        }

        public void Atualizar(Produto produto)
        {
            // Em memória: Não precisa fazer nada, pois a referência do objeto já foi alterada no Service.
        }

        public void Remover(Guid id)
        {
            var produto = ObterPorId(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
            }
        }

        // Chamamos o preenchimento antes de retornar
        public Produto? ObterPorId(Guid id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

            if (produto != null)
            {
                PreencherCategoria(produto);
            }

            return produto;
        }

        // AQUI MUDOU: Preenchemos a categoria para cada item da lista
        public List<Produto> ListarTodos()
        {
            // Traz apenas os produtos que NÃO estão Inativos
            var produtos = _context.Produtos
                .Where(p => p.Status == "Ativo") 
                .ToList();

            foreach (var produto in produtos)
            {
                PreencherCategoria(produto);
            }

            return produtos;
        }

        // Método auxiliar privado para simular o JOIN
        private void PreencherCategoria(Produto produto)
        {
            // 1. Busca a categoria na lista de Categorias usando o ID
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == produto.CategoriaId);

            if (categoria != null)
            {
                // 2. Usa Reflection para setar a propriedade que tem 'private set'
                typeof(Produto).GetProperty("Categoria")?
                    .SetValue(produto, categoria);
            }
        }
    }
}