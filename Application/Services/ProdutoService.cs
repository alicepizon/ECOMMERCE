using System;
using System.Collections.Generic;
using ecommerce.Dominio.Entidades;
using ecommerce.Dominio.Interfaces;
using ecommerce.Application.DTOs;

namespace ecommerce.Application.Services
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _produtoRepo;

        public ProdutoService(IProdutoRepository produtoRepo)
        {
            _produtoRepo = produtoRepo;
        }

        // Cadastro de Produto
        public void CadastrarProduto(CriarProdutoDTO dto)
        {
            if (dto.Preco <= 0)
                throw new ArgumentException("Preço inválido.");

            // O EstoqueInicial já entra na propriedade QuantidadeEstoque da entidade
            var produto = new Produto(dto.Nome, dto.Descricao, dto.Preco, dto.CategoriaId, dto.EstoqueInicial);

            _produtoRepo.Adicionar(produto);
        }

        // Listar todos os produtos
        public List<Produto> ListarTodos()
        {
            return _produtoRepo.ListarTodos();
        }

        // Atualizar dados básicos
        public void AtualizarProduto(Guid id, string nome, string descricao, decimal preco, Guid categoriaId)
        {
            var produto = _produtoRepo.ObterPorId(id);
            if (produto == null) throw new ArgumentException("Produto não encontrado.");

            produto.Atualizar(nome, descricao, preco, categoriaId);

            _produtoRepo.Atualizar(produto);
        }

        // Desativar produto (Soft Delete)
        public void DesativarProduto(Guid id)
        {
            var produto = _produtoRepo.ObterPorId(id);
            if (produto == null) throw new ArgumentException("Produto não encontrado.");

            produto.Desativar(); 
            _produtoRepo.Atualizar(produto); 
        }

        // Reposição de Estoque
        public void ReporEstoque(Guid id, int quantidade)
        {
            var produto = _produtoRepo.ObterPorId(id);
            if (produto == null) throw new ArgumentException("Produto não encontrado.");

            produto.ReporEstoque(quantidade);
            _produtoRepo.Atualizar(produto);
        }
    }
}