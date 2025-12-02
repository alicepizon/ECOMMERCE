using System;
using System.Collections.Generic;
using ecommerce.Dominio.Entidades;

namespace ecommerce.Dominio.Interfaces
{
    public interface IProdutoRepository
    {
        void Adicionar(Produto produto);
        void Atualizar(Produto produto);
        void Remover(Guid id); 
        Produto? ObterPorId(Guid id);
        List<Produto> ListarTodos();
    }
}