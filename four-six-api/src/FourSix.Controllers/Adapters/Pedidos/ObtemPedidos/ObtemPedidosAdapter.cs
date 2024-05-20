using FourSix.Controllers.Gateways.Integrations;
using FourSix.Controllers.ViewModels;
using FourSix.UseCases.UseCases.Pedidos.ObtemPedidos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FourSix.Controllers.Adapters.Pedidos.ObtemPedidos
{
    public class ObtemPedidosAdapter
        : IObtemPedidosAdapter
    {
        private readonly IObtemPedidosUseCase _useCase;
        private readonly IProdutoService _produtoService;

        public ObtemPedidosAdapter(IObtemPedidosUseCase useCase, IProdutoService produtoService)
        {
            _useCase = useCase;
            _produtoService = produtoService;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ObtemPedidosResponse))]
        public async Task<ObtemPedidosResponse> Listar()
        {
            var lista = await _useCase.Execute();

            var model = new List<PedidoModel>();
            lista.ToList().ForEach(f =>
            {
                var pedido = new PedidoModel(f);
                f.Itens.ToList().ForEach(i =>
                {
                    var produto = _produtoService.GetProduto(i.ProdutoId).Result;
                    pedido.AdicionarItem(i, produto);
                });
                model.Add(pedido);
            });

            return new ObtemPedidosResponse(model);
        }
    }
}
