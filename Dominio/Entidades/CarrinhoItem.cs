using System;

namespace ecommerce.Dominio.Entidades
{
    public class CarrinhoItem
    {
        public Guid Id { get; private set; }
        public Guid CarrinhoId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string ProdutoNome { get; private set; } = null!; 
        public int Quantidade { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        public decimal Subtotal { get; private set; }

        public Carrinho Carrinho { get; private set; } = null!;

        protected CarrinhoItem() { }

        public CarrinhoItem(Guid carrinhoId, Guid produtoId, string nome, int quantidade, decimal preco)
        {
            if (quantidade <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            Id = Guid.NewGuid();
            CarrinhoId = carrinhoId;
            ProdutoId = produtoId;
            ProdutoNome = nome;
            PrecoUnitario = preco;

            AtualizarQuantidade(quantidade);
        }

        public void AtualizarQuantidade(int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentException("Quantidade mínima é 1.");

            Quantidade = quantidade;
            Subtotal = Quantidade * PrecoUnitario;
        }

        public void AdicionarUnidades(int unidades)
        {
            AtualizarQuantidade(this.Quantidade + unidades);
        }
    }
}