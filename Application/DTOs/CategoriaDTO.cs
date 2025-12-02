using System;

namespace ecommerce.Application.DTOs
{
    // ENTRADA: Cadastrar/Atualizar
    public class CategoriaDTO
    {
        public string Nome { get; set; } = null!;
        public string Descricao { get; set; } = null!;
    }

    // SAÍDA: Listagem
    public class CategoriaRespostaDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Descricao { get; set; } = null!;
    }
}