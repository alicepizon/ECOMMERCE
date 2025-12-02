using System;
using ecommerce.Dominio.Entidades;

namespace ecommerce.Dominio.Interfaces
{
    public interface ICarrinhoRepository
    {
        // O carrinho é sempre buscado pelo dono (Usuario)
        Carrinho? ObterCarrinhoDoUsuario(Guid usuarioId);

        void Adicionar(Carrinho carrinho);

        // Em memória, o update é automático pela referência, mas mantemos o contrato
        void Atualizar(Carrinho carrinho);

        // Usado após finalizar o pedido com sucesso
        void LimparCarrinho(Guid carrinhoId);
    }
}