using System;

namespace ecommerce.Dominio.Entidades
{
    public class PagamentoCartao : Pagamento
    {
        public string NumeroCartaoMascarado { get; private set; }
        public int Parcelas { get; private set; }

        public PagamentoCartao(Guid pedidoId, decimal valor, string numeroCartao, int parcelas)
            : base(pedidoId, valor)
        {
            if (parcelas < 1)
                throw new ArgumentException("Parcelas devem ser no mínimo 1");

            if (string.IsNullOrEmpty(numeroCartao) || numeroCartao.Length < 4)
                throw new ArgumentException("Número do cartão inválido.");

            NumeroCartaoMascarado = "****" + numeroCartao.Substring(numeroCartao.Length - 4);
            Parcelas = parcelas;
        }

        public override void Processar()
        {
            this.Status = "Em Análise";
        }
    }
}