using System;
using System.Collections.Generic;
using ecommerce.Dominio.Entidades;

namespace ecommerce.Infraestrutura.Data
{
    public class AppDbContext
    {
        // Listas que simulam as tabelas do banco de dados
        public List<Usuario> Usuarios { get; set; }
        public List<Categoria> Categorias { get; set; }
        public List<Produto> Produtos { get; set; }
        public List<Carrinho> Carrinhos { get; set; }
        public List<Pedido> Pedidos { get; set; }

        public AppDbContext()
        {
            // Inicializa as listas vazias
            Usuarios = new List<Usuario>();
            Categorias = new List<Categoria>();
            Produtos = new List<Produto>();
            Carrinhos = new List<Carrinho>();
            Pedidos = new List<Pedido>();

            // Cria dados falsos para facilitar seus testes
            Seed();
        }

        private void Seed()
        {
            // 1. Cria uma Categoria Padrão
            var categoria = new Categoria("Eletrônicos", "Gadgets e computadores");
            this.Categorias.Add(categoria);

            // 2. Cria um Produto vinculado a essa categoria
            // (Preço: 5000, Estoque: 10)
            var produto = new Produto("Notebook Gamer", "I7 16GB", 5000m, categoria.Id, 10);
            this.Produtos.Add(produto);

        }
    }
}