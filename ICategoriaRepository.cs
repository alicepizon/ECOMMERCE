using System;
using System.Collections.Generic;
using ecommerce.Dominio.Entidades;

namespace ecommerce.Dominio.Interfaces
{
    public interface ICategoriaRepository
    {
        void Adicionar(Categoria categoria);
        void Atualizar(Categoria categoria);
        void Remover(Guid id);
        Categoria? ObterPorId(Guid id);
        List<Categoria> ListarTodas();
    }
}