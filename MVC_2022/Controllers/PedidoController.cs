using Microsoft.AspNetCore.Mvc;
using MVC_2022.Models;
using MVC_2022.Repositories;
using MVC_2022.Repositories.Interfaces;

namespace MVC_2022.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedido;
        private readonly CarrinhoCompra _carrinho;

        public PedidoController(IPedidoRepository pedido, CarrinhoCompra carrinho)
        {
            _pedido = pedido;
            _carrinho = carrinho;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Pedido pedido)
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0.0m;

            //obtem os itens do carrinho de compra do cliente
            List<CarrinhoCompraItem> items = _carrinho.GetCarrinhoCompraItens();
            _carrinho.CarrinhoCompraItems = items;

            //verifica se existem itens de pedido
            if (_carrinho.CarrinhoCompraItems.Count == 0)
            {
                //Adiciona erro ao estado da model:
                ModelState.AddModelError("", "Seu carrinho esta vazio, que tal incluir um lanche...");
            }

            //calcula o total de itens e o total do pedido
            foreach (var item in items)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Lanche.LanchePreco * item.Quantidade);
            }

            //atribui os valores obtidos ao pedido
            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal = precoTotalPedido;

            //valida os dados do pedido
            if (ModelState.IsValid)
            {
                //cria o pedido e os detalhes
                _pedido.CriarPedido(pedido);

                //define mensagens ao cliente
                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :)";
                ViewBag.TotalPedido = _carrinho.GetCarrinhoCompraTotal();

                //limpa o carrinho do cliente
                _carrinho.LimparCarrinho();

                //exibe a view com dados do cliente e do pedido
                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            }
            return View(pedido);
        }
    }
}
