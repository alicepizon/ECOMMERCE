using System;
using System.Collections.Generic;
using ecommerce.Dominio.Entidades;

namespace ecommerce.Dominio.Interfaces
{
    public interface IPedidoRepository
    {
        void Adicionar(Pedido pedido);
        Pedido? ObterPorId(Guid id);
        List<Pedido> ObterPedidosDoUsuario(Guid usuarioId);
        void SalvarMudancas();
    }
}