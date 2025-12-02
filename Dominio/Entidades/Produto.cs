using System;

namespace ecommerce.Dominio.Entidades
{
    public class Produto
    {
        public Guid Id { get; private set; }
        public Guid CategoriaId { get; private set; }
        public string Nome { get; private set; } = null!;
        public string Descricao { get; private set; } = null!;
        public decimal Preco { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Status { get; private set; } = null!;
        public int QuantidadeEstoque { get; private set; }

        // Navegação
        public Categoria Categoria { get; private set; } = null!;

        public Produto(string nome, string descricao, decimal preco, Guid categoriaId, int estoqueInicial)
        {
            Id = Guid.NewGuid();
            Validar(nome, descricao, preco, estoqueInicial);

            this.CategoriaId = categoriaId;
            this.DataCadastro = DateTime.Now;
            this.Status = "Ativo";
        }

        public void Atualizar(string nome, string descricao, decimal preco, Guid categoriaId)
        {
            Validar(nome, descricao, preco, this.QuantidadeEstoque);

            this.CategoriaId = categoriaId;
        }

        public void ReporEstoque(int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentException("Quantidade de reposição deve ser positiva.");

            this.QuantidadeEstoque += quantidade;
        }

        public void BaixarEstoque(int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentException("Quantidade de baixa deve ser positiva.");

            if (this.QuantidadeEstoque < quantidade)
                throw new InvalidOperationException($"Estoque insuficiente. Disponível: {this.QuantidadeEstoque}");

            this.QuantidadeEstoque -= quantidade;
        }

        public void Desativar()
        {
            this.Status = "Inativo";
        }

        private void Validar(string nome, string descricao, decimal preco, int estoque)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome obrigatório.");
            if (preco <= 0)
                throw new ArgumentException("Preço deve ser maior que zero.");
            if (estoque < 0)
                throw new ArgumentException("Estoque não pode ser negativo.");

            this.Nome = nome;
            this.Descricao = descricao;
            this.Preco = preco;
            this.QuantidadeEstoque = estoque;
        }
    }
}