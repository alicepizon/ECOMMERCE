using System;
using ecommerce.Dominio.Entidades;

namespace ecommerce.Dominio.Interfaces
{
    public interface IUsuarioRepository
    {
        void Adicionar(Usuario usuario);
        void Atualizar(Usuario usuario); 
        void Remover(Guid id);
        Usuario? ObterPorEmail(string email); 
        Usuario? ObterPorId(Guid id);
    }
}