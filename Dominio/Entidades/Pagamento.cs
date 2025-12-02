using System;

namespace ecommerce.Dominio.Entidades
{
    // 'abstract' impede que alguém instancie um pagamento genérico
    public abstract class Pagamento
    {
        public Guid Id { get; private set; } 
        public Guid PedidoId { get; private set; } 
        public DateTime DataPagamento { get; private set; }
        public decimal Valor { get; private set; }

        // Protected permite que as classes filhas (Pix/Cartao) alterem o status
        public string Status { get; protected set; }

        protected Pagamento(Guid pedidoId, decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("Valor do pagamento deve ser positivo.");

            Id = Guid.NewGuid(); 
            PedidoId = pedidoId;
            Valor = valor;
            DataPagamento = DateTime.Now;
            Status = "Pendente";
        }

        // Método Abstrato: Garante o Polimorfismo (Critério 4 e 9)
        public abstract void Processar();
    }
}