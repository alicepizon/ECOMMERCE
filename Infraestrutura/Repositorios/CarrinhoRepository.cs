using System;
using System.Linq;
using ecommerce.Dominio.Entidades;
using ecommerce.Dominio.Interfaces;
using ecommerce.Infraestrutura.Data;

namespace ecommerce.Infraestrutura.Repositorios
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private readonly AppDbContext _context;

        public CarrinhoRepository(AppDbContext context)
        {
            _context = context;
        }

        public Carrinho? ObterCarrinhoDoUsuario(Guid usuarioId)
        {
            return _context.Carrinhos.FirstOrDefault(c => c.UsuarioId == usuarioId);
        }

        public void Adicionar(Carrinho carrinho)
        {
            _context.Carrinhos.Add(carrinho);
        }

        public void Atualizar(Carrinho carrinho)
        {
            // O CarrinhoService chama isso após AdicionarItem/RemoverItem.
            // Em memória: a lista de itens dentro do objeto carrinho já mudou.
        }

        public void LimparCarrinho(Guid carrinhoId)
        {
            var carrinho = _context.Carrinhos.FirstOrDefault(c => c.Id == carrinhoId);
            if (carrinho != null)
            {
                _context.Carrinhos.Remove(carrinho);
            }
        }
    }
}