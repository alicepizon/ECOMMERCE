using System;

namespace ecommerce.Dominio.Entidades
{
    public class PedidoItem
    {
        public Guid Id { get; private set; }
        public Guid PedidoId { get; private set; } 
        public Guid ProdutoId { get; private set; }
        public string NomeProduto { get; private set; } = null!; 
        public int Quantidade { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        public decimal Subtotal { get; private set; }

        // Propriedade de navegação EF Core
        public Pedido Pedido { get; private set; } = null!; 

        protected PedidoItem() { }

        public PedidoItem(Guid pedidoId, Guid produtoId, string nome, int quantidade, decimal preco)
        {
            if (quantidade <= 0) throw new ArgumentException("Quantidade inválida.");
            if (preco < 0) throw new ArgumentException("Preço inválido.");

            Id = Guid.NewGuid(); 
            PedidoId = pedidoId;
            ProdutoId = produtoId;
            NomeProduto = nome;
            Quantidade = quantidade;
            PrecoUnitario = preco;
            Subtotal = quantidade * preco;
        }
    }
}