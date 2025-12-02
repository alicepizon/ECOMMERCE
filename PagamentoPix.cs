using System;

namespace ecommerce.Dominio.Entidades
{
    public class PagamentoPix : Pagamento
    {
        public string ChavePix { get; private set; } = null!;
        public string CodigoCopiaCola { get; private set; } = null!;

        public PagamentoPix(Guid pedidoId, decimal valor, string chavePix)
            : base(pedidoId, valor)
        {
            if (string.IsNullOrEmpty(chavePix))
                throw new ArgumentException("Chave Pix é obrigatória");

            ChavePix = chavePix;
        }

        public override void Processar()
        {
            CodigoCopiaCola = Guid.NewGuid().ToString() + "-PIX-HASH";
            this.Status = "Aprovado";
        }
    }
}