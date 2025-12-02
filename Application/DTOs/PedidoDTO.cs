using System;

namespace ecommerce.Application.DTOs
{
    // ENTRADA: Usado no POST /api/pedido/finalizar
    public class CheckoutDTO
    {
        public Guid UsuarioId { get; set; }
        public string MetodoPagamento { get; set; } = null!; // Deve aceitar "pix" ou "cartao"

        // Campos Opcionais (Preencher um ou outro dependendo do método)
        public string? ChavePix { get; set; }
        public string? NumeroCartao { get; set; }
        public int? Parcelas { get; set; }
    }

    // SAÍDA: Resumo do pedido confirmado
    public class PedidoResumoDTO
    {
        public Guid PedidoId { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public string Status { get; set; } = null!;
    }
}