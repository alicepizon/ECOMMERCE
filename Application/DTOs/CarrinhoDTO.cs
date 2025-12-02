using System;

namespace ecommerce.Application.DTOs
{
    // ENTRADA: Usado no POST /api/carrinho/adicionar
    public class AdicionarItemDTO
    {
        public Guid UsuarioId { get; set; } // Quem está comprando
        public Guid ProdutoId { get; set; } // O que está comprando
        public int Quantidade { get; set; }
    }

    // ENTRADA (Bônus): Para cadastrar produtos no catálogo via API
    public class CriarProdutoDTO
    {
        public string Nome { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public decimal Preco { get; set; }
        public Guid CategoriaId { get; set; }
        public int EstoqueInicial { get; set; }
    }

    // SAÍDA: Para o usuário ver o carrinho
    public class CarrinhoRespostaDTO
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public decimal ValorTotal { get; set; } 
        public List<ItemCarrinhoDTO> Itens { get; set; } = new();
    }

    public class ItemCarrinhoDTO
    {
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; } = null!;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}