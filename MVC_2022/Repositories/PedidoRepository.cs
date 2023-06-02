using MVC_2022.Context;
using MVC_2022.Models;
using MVC_2022.Repositories.Interfaces;

namespace MVC_2022.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        public readonly AppDbContext _context;
        public readonly CarrinhoCompra _carrinho;

        public PedidoRepository(AppDbContext context, CarrinhoCompra carrinho)
        {
            _context = context;
            _carrinho = carrinho;
        }

        public void CriarPedido(Pedido pedido)
        {
            pedido.PedidoEnviado = DateTime.Now;
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            var carrinhoCompraItens = _carrinho.CarrinhoCompraItems;

            foreach (var carrinhoItem in carrinhoCompraItens)
            {
                var pedidoDetail = new PedidoDetalhe()
                {
                    Quantidade = carrinhoItem.Quantidade,
                    LancheId = carrinhoItem.Lanche.LancheId,
                    PedidoId = pedido.PedidoId,
                    Preco = carrinhoItem.Lanche.LanchePreco
                };
                _context.PedidoDetalhes.Add(pedidoDetail);
            }
            _context.SaveChanges();
        }
    }
}
