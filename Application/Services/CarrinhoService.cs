using System;
using System.Linq;
using System.Collections.Generic;
using ecommerce.Dominio.Entidades;
using ecommerce.Dominio.Interfaces;
using ecommerce.Application.DTOs;

namespace ecommerce.Application.Services
{
    public class CarrinhoService
    {
        private readonly ICarrinhoRepository _carrinhoRepo;
        private readonly IProdutoRepository _produtoRepo;
        private readonly IUsuarioRepository _usuarioRepo; 

        public CarrinhoService(
            ICarrinhoRepository carrinhoRepo,
            IProdutoRepository produtoRepo,
            IUsuarioRepository usuarioRepo)
        {
            _carrinhoRepo = carrinhoRepo;
            _produtoRepo = produtoRepo;
            _usuarioRepo = usuarioRepo;
        }

        // 1. ADICIONAR ITEM
        public void AdicionarItem(AdicionarItemDTO dto)
        {
            // Validação de Integridade: Usuário existe?
            var usuario = _usuarioRepo.ObterPorId(dto.UsuarioId);
            if (usuario == null)
                throw new ArgumentException("Usuário não encontrado.");

            // Validação de Integridade: Produto existe?
            var produto = _produtoRepo.ObterPorId(dto.ProdutoId);
            if (produto == null)
                throw new ArgumentException("Produto não encontrado.");

            // Lógica do Carrinho
            var carrinho = _carrinhoRepo.ObterCarrinhoDoUsuario(dto.UsuarioId);

            if (carrinho == null)
            {
                carrinho = new Carrinho(dto.UsuarioId);
                _carrinhoRepo.Adicionar(carrinho);
            }

            carrinho.AdicionarItem(produto, dto.Quantidade);
            _carrinhoRepo.Atualizar(carrinho);
        }

        // 2. ATUALIZAR QUANTIDADE
        public void AtualizarItem(Guid usuarioId, Guid produtoId, int novaQuantidade)
        {
            // Validação de Usuário
            var usuario = _usuarioRepo.ObterPorId(usuarioId);
            if (usuario == null)
                throw new ArgumentException("Usuário não encontrado.");

            var carrinho = _carrinhoRepo.ObterCarrinhoDoUsuario(usuarioId);
            if (carrinho == null)
                throw new ArgumentException("Carrinho não encontrado.");

            // Chama o método da Entidade
            carrinho.AtualizarItem(produtoId, novaQuantidade);

            _carrinhoRepo.Atualizar(carrinho);
        }

        // 3. REMOVER ITEM
        public void RemoverItem(Guid usuarioId, Guid produtoId)
        {
            // Validação de Usuário
            var usuario = _usuarioRepo.ObterPorId(usuarioId);
            if (usuario == null)
                throw new ArgumentException("Usuário não encontrado.");

            var carrinho = _carrinhoRepo.ObterCarrinhoDoUsuario(usuarioId);
            if (carrinho == null)
                throw new ArgumentException("Carrinho não encontrado.");

            // Chama o método da Entidade
            carrinho.RemoverItem(produtoId);

            _carrinhoRepo.Atualizar(carrinho);
        }

        // 4. VISUALIZAR CARRINHO
        public CarrinhoRespostaDTO ObterCarrinho(Guid usuarioId)
        {
            // Validação de Usuário
            var usuario = _usuarioRepo.ObterPorId(usuarioId);
            if (usuario == null)
                throw new ArgumentException("Usuário não encontrado.");

            var carrinho = _carrinhoRepo.ObterCarrinhoDoUsuario(usuarioId);

            // Se não tem carrinho, retorna vazio (mas o usuário é válido)
            if (carrinho == null)
            {
                return new CarrinhoRespostaDTO
                {
                    UsuarioId = usuarioId,
                    ValorTotal = 0,
                    Itens = new List<ItemCarrinhoDTO>()
                };
            }

            // Mapeia para DTO
            return new CarrinhoRespostaDTO
            {
                Id = carrinho.Id,
                UsuarioId = carrinho.UsuarioId,
                ValorTotal = carrinho.ValorTotal,
                Itens = carrinho.Itens.Select(i => new ItemCarrinhoDTO
                {
                    ProdutoId = i.ProdutoId,
                    Nome = i.ProdutoNome,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario,
                    Subtotal = i.Subtotal
                }).ToList()
            };
        }
    }
}