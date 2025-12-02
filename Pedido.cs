using System;
using System.Collections.Generic;
using System.Linq;

namespace ecommerce.Dominio.Entidades
{
    public class Pedido
    {
        public Guid Id { get; private set; }
        public Guid UsuarioId { get; private set; }
        public DateTime DataPedido { get; private set; }
        public decimal ValorTotal { get; private set; }
        public string Status { get; private set; } = null!;

        // Relação com Pagamento
        public Pagamento? Pagamento { get; private set; }

        private readonly List<PedidoItem> _itens;
        public IReadOnlyCollection<PedidoItem> Itens => _itens.AsReadOnly();

        // Construtor vazio para EF Core
        protected Pedido() { _itens = new List<PedidoItem>(); }

        public Pedido(Guid usuarioId)
        {
            Id = Guid.NewGuid(); 
            UsuarioId = usuarioId;
            DataPedido = DateTime.Now;
            Status = "Criado";
            _itens = new List<PedidoItem>();
        }

        public void AdicionarItem(Guid produtoId, string nome, int qtd, decimal preco)
        {
            // Passamos 'this.Id' para garantir que o item saiba a quem pertence
            var item = new PedidoItem(this.Id, produtoId, nome, qtd, preco);
            _itens.Add(item);
            CalcularTotal();
        }

        public void VincularPagamento(Pagamento pagamento)
        {
            if (Status == "Cancelado")
                throw new InvalidOperationException("Não é possível pagar um pedido cancelado.");

            this.Pagamento = pagamento;
            this.Status = "Aguardando Processamento";
        }

        public void FinalizarPedido()
        {
            this.Status = "Concluido";
        }

        public void Cancelar()
        {
            if (Status == "Cancelado")
            {
                throw new InvalidOperationException("Este pedido já foi cancelado anteriormente.");
            }

            this.Status = "Cancelado";
        }

        private void CalcularTotal()
        {
            ValorTotal = _itens.Sum(i => i.Subtotal);
        }
    }
}