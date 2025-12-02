using System;

namespace ecommerce.Application.DTOs
{
    // ENTRADA: Usado no POST /api/usuario/registrar
    public class RegistroUsuarioDTO
    {
        // Perfil
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? Telefone { get; set; }

        // Endereço
        public string? Rua { get; set; }
        public string? Numero { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? Cep { get; set; }
        public string? Complemento { get; set; }
    }

    // ENTRADA: Usado no POST /api/usuario/login
    public class LoginDTO
    {
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
    }

    // SAÍDA: O que a API devolve para o front (nunca devolva a senha!)
    public class UsuarioRespostaDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
    }

    // SAÍDA: Detalhes completos do usuário (Perfil + Endereço)
    public class UsuarioDetalhadoDTO
    {
        public Guid Id { get; set; }

        // Perfil
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telefone { get; set; } = null!;

        // Endereço
        public string Rua { get; set; } = null!;
        public string Numero { get; set; } = null!;
        public string Cidade { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string Cep { get; set; } = null!;
        public string? Complemento { get; set; }
    }
    
    public class AtualizarPerfilDTO
    {
        public string Nome { get; set; } = null!;
        public string Telefone { get; set; } = null!;
    }
}