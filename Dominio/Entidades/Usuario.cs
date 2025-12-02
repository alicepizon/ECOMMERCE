using System;
using System.Text.RegularExpressions;

namespace ecommerce.Dominio.Entidades
{
    public class Usuario
    {
        public Guid Id { get; private set; }

        public string Nome { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string Senha { get; private set; } = null!;
        public string Telefone { get; private set; } = null!;
        public string TipoUsuario { get; private set; } = null!;
        public DateTime DataCadastro { get; private set; }

        public string Rua { get; private set; } = null!;
        public string Numero { get; private set; } = null!;
        public string Cidade { get; private set; } = null!;
        public string Estado { get; private set; } = null!;
        public string Cep { get; private set; } = null!;

        public string? Complemento { get; private set; }

        protected Usuario() { }

        public Usuario(string nome, string email, string senha, string telefone, string tipoUsuario = "Cliente")
        {
            Id = Guid.NewGuid();

            Validar(nome, email, senha);

            Nome = nome;
            Email = email;
            Senha = senha;
            Telefone = telefone;
            TipoUsuario = tipoUsuario;
            DataCadastro = DateTime.Now;
        }

        public void AtualizarPerfil(string nome, string telefone)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório."); 

            Nome = nome;
            Telefone = telefone;
        }

        public void AtualizarEndereco(string rua, string numero, string cidade, string estado, string cep, string complemento)
        {
            
            if (string.IsNullOrWhiteSpace(cep)) throw new ArgumentException("CEP é obrigatório.");
            if (string.IsNullOrWhiteSpace(rua)) throw new ArgumentException("Rua é obrigatória.");
            if (string.IsNullOrWhiteSpace(numero)) throw new ArgumentException("Numero é obrigatório.");
            if (string.IsNullOrWhiteSpace(cidade)) throw new ArgumentException("Cidade é obrigatória.");
            if (string.IsNullOrWhiteSpace(estado)) throw new ArgumentException("Estado é obrigatório.");

            Rua = rua;
            Numero = numero;
            Cidade = cidade;
            Estado = estado;
            Cep = cep;
            Complemento = complemento;
        }

        public bool ValidarSenha(string senhaInformada)
        {
            // Professor, aqui a gente iria comparar o hash da senha informada com o hash armazenado.
            // Usando uma biblioteca como BCrypt para comparar o Hash
            // Mas como estamos armazenando a senha em texto puro, faremos uma comparação direta.
            return this.Senha == senhaInformada;
        }

        private void Validar(string nome, string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do usuário é obrigatório.");

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("Email inválido.");

            if (string.IsNullOrWhiteSpace(senha) || senha.Length < 6)
                throw new ArgumentException("A senha deve ter pelo menos 6 caracteres.");
        }
    }
}