using System;
using System.Linq;
using ecommerce.Dominio.Entidades;
using ecommerce.Dominio.Interfaces;
using ecommerce.Application.DTOs;

namespace ecommerce.Application.Services
{
    public class PedidoService
    {
        private readonly IPedidoRepository _pedidoRepo;
        private readonly ICarrinhoRepository _carrinhoRepo;
        private readonly IProdutoRepository _produtoRepo;

        public PedidoService(IPedidoRepository pedidoRepo, ICarrinhoRepository carrinhoRepo, IProdutoRepository produtoRepo)
        {
            _pedidoRepo = pedidoRepo;
            _carrinhoRepo = carrinhoRepo;
            _produtoRepo = produtoRepo;
        }

        public Pedido FinalizarPedido(CheckoutDTO dto)
        {
            // 1. Recuperar Carrinho
            var carrinho = _carrinhoRepo.ObterCarrinhoDoUsuario(dto.UsuarioId);
            if (carrinho == null || !carrinho.Itens.Any())
                throw new InvalidOperationException("Carrinho vazio ou não encontrado.");

            // 2. Criar Pedido
            var pedido = new Pedido(dto.UsuarioId);

            // 3. Migrar Itens e Baixar Estoque
            foreach (var itemCarrinho in carrinho.Itens)
            {
                var produto = _produtoRepo.ObterPorId(itemCarrinho.ProdutoId);

                // CORREÇÃO DO AVISO CS8602:
                // Validamos se o produto existe antes de mexer nele.
                if (produto == null)
                {
                    throw new InvalidOperationException($"O produto '{itemCarrinho.ProdutoNome}' não foi encontrado no cadastro.");
                }

                // Agora o compilador sabe que 'produto' não é nulo aqui
                produto.BaixarEstoque(itemCarrinho.Quantidade);

                pedido.AdicionarItem(produto.Id, produto.Nome, itemCarrinho.Quantidade, produto.Preco);

                _produtoRepo.Atualizar(produto);
            }

            // 4. Processar Pagamento (Factory + Polimorfismo)
            Pagamento pagamento = CriarPagamento(dto, pedido.Id, pedido.ValorTotal);
            pagamento.Processar();

            // 5. Salvar Tudo
            pedido.VincularPagamento(pagamento);
            _pedidoRepo.Adicionar(pedido);

            // 6. Limpar Carrinho
            _carrinhoRepo.LimparCarrinho(carrinho.Id);

            return pedido;
        }

        // Factory Method: Decide qual classe filha instanciar (Critério 9)
        private Pagamento CriarPagamento(CheckoutDTO dto, Guid pedidoId, decimal valor)
        {
            // O ToLower() garante que "Pix", "pix" ou "PIX" funcionem igual
            return dto.MetodoPagamento.ToLower() switch
            {
                "pix" => new PagamentoPix(
                    pedidoId,
                    valor,
                    dto.ChavePix ?? "" // Evita erro se vier nulo
                ),

                "cartao" => new PagamentoCartao(
                    pedidoId,
                    valor,
                    dto.NumeroCartao ?? "", // Evita erro se vier nulo
                    dto.Parcelas ?? 1       // Se não vier parcelas, assume 1x
                ),

                // Critério 6: Lança exceção específica se o tipo for desconhecido
                _ => throw new ArgumentException($"Método de pagamento '{dto.MetodoPagamento}' não suportado.")
            };
        }

        public void CancelarPedido(Guid pedidoId)
        {
            // 1. Busca o Pedido
            var pedido = _pedidoRepo.ObterPorId(pedidoId);
            if (pedido == null)
                throw new ArgumentException("Pedido não encontrado.");

            // Validação extra: Se já estiver cancelado, não faz nada (para não duplicar estoque)
            if (pedido.Status == "Cancelado")
                throw new InvalidOperationException("Este pedido já foi cancelado anteriormente.");

            // 2. Devolver itens ao estoque 
            foreach (var item in pedido.Itens)
            {
                var produto = _produtoRepo.ObterPorId(item.ProdutoId);

                // Se o produto ainda existir no cadastro, repõe o estoque
                if (produto != null)
                {
                    // Reutilizamos o método da Entidade Produto (Encapsulamento)
                    produto.ReporEstoque(item.Quantidade);

                    // Salva o produto com o novo saldo
                    _produtoRepo.Atualizar(produto);
                }
            }

            // 3. Mudar status do pedido (na memória)
            pedido.FinalizarPedido(); 

            pedido.Cancelar();

            // 4. Persistir alteração do pedido
            _pedidoRepo.SalvarMudancas(); 
        }
    }
}