using System;
using System.Collections.Generic;
using System.Linq;

namespace ecommerce.Dominio.Entidades
{ 
    public class Carrinho
    {
        public Guid Id { get; private set; }
        public Guid UsuarioId { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public decimal ValorTotal { get; private set; }

        // CRITÉRIO 5: Encapsulamento. A lista real é privada.
        // Ninguém de fora consegue dar .Add() ou .Clear() diretamente. 
        private readonly List<CarrinhoItem> _itens;

        // Exposição segura (apenas leitura)
        public IReadOnlyCollection<CarrinhoItem> Itens => _itens.AsReadOnly();

        protected Carrinho() { _itens = new List<CarrinhoItem>(); }

        public Carrinho(Guid usuarioId)
        {
            Id = Guid.NewGuid();
            UsuarioId = usuarioId;
            DataCriacao = DateTime.Now;
            _itens = new List<CarrinhoItem>();
        }

        public void AdicionarItem(Produto produto, int quantidade)
        {
            if (produto == null)
                throw new ArgumentNullException(nameof(produto));

            if (quantidade <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero.");

            // Verifica se o produto já está no carrinho
            var itemExistente = _itens.FirstOrDefault(i => i.ProdutoId == produto.Id);

            if (itemExistente != null)
            {
                itemExistente.AdicionarUnidades(quantidade);
            }
            else
            {
                // CRITÉRIO 1: Relação de composição (Carrinho cria o Item) [cite: 4]
                var novoItem = new CarrinhoItem(this.Id, produto.Id, produto.Nome, quantidade, produto.Preco);
                _itens.Add(novoItem);
            }

            CalcularValorTotal();
        }

        public void RemoverItem(Guid produtoId)
        {
            var item = _itens.FirstOrDefault(i => i.ProdutoId == produtoId);

            // CRITÉRIO 6: Tratamento de exceções orientado ao domínio [cite: 25]
            if (item == null)
                throw new ArgumentException("Item não encontrado no carrinho.");

            _itens.Remove(item);
            CalcularValorTotal();
        }

        public void AtualizarItem(Guid produtoId, int novaQuantidade)
        {
            var item = _itens.FirstOrDefault(i => i.ProdutoId == produtoId);

            if (item == null)
                throw new ArgumentException("Item não encontrado no carrinho.");

            if (novaQuantidade <= 0)
            {
                // Se o usuário colocar 0, removemos o item
                RemoverItem(produtoId);
            }
            else
            {
                item.AtualizarQuantidade(novaQuantidade);
                CalcularValorTotal();
            }
        }

        public void CalcularValorTotal()
        {
            ValorTotal = _itens.Sum(i => i.Subtotal);
        }
    }
} 