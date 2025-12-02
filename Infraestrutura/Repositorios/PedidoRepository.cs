using System;
using System.Collections.Generic;
using System.Linq;
using ecommerce.Dominio.Entidades;
using ecommerce.Dominio.Interfaces;
using ecommerce.Infraestrutura.Data;

namespace ecommerce.Infraestrutura.Repositorios
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
        }

        public Pedido? ObterPorId(Guid id)
        {
            return _context.Pedidos.FirstOrDefault(p => p.Id == id);
        }

        public List<Pedido> ObterPedidosDoUsuario(Guid usuarioId)
        {
            return _context.Pedidos.Where(p => p.UsuarioId == usuarioId).ToList();
        }

        public void SalvarMudancas()
        {
            // Método vazio para cumprir contrato de UnitOfWork se necessário
        }
    }
}