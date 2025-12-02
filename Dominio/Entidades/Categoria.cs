using System;
using System.Collections.Generic;

namespace ecommerce.Dominio.Entidades
{
    public class Categoria
    {
        // Propriedades com 'private set' para garantir Encapsulamento 
        public Guid Id { get; private set; }
        public string Nome { get; private set; } = null!; 
        public string Descricao { get; private set; } = null!;

        // Propriedade de navegação (relacionamento do diagrama)
        public ICollection<Produto> Produtos { get; private set; }

        // Construtor para garantir o estado inicial válido
        public Categoria(string nome, string descricao)
        {
            Id = Guid.NewGuid();
            Validar(nome, descricao);
            Produtos = new List<Produto>();
        }

        public void Atualizar(string nome, string descricao)
        {
            Validar(nome, descricao);
            this.Nome = nome;
            this.Descricao = descricao;
        }

        // Método privado para centralizar regras de validação e lançar exceções 
        private void Validar(string nome, string descricao)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome da categoria é obrigatório.");

            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("A descrição da categoria é obrigatória.");

            this.Nome = nome;
            this.Descricao = descricao;
        }
    }
}