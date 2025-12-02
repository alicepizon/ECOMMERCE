using System;
using System.Linq;
using ecommerce.Dominio.Entidades;
using ecommerce.Dominio.Interfaces;
using ecommerce.Infraestrutura.Data;

namespace ecommerce.Infraestrutura.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
        }

        public void Atualizar(Usuario usuario)
        {
            // Mantido vazio para simulação em memória (o objeto já está alterado na lista)
        }

        public void Remover(Guid id)
        {
            var usuario = ObterPorId(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }
        }

        public Usuario? ObterPorEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        public Usuario? ObterPorId(Guid id)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Id == id);
        }
    }
}