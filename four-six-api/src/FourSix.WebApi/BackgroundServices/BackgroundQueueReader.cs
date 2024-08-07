using Amazon;
using Amazon.SQS;
using FourSix.Controllers.Adapters.Pedidos.AlteraStatusPedido;
using FourSix.Controllers.ViewModels;
using Newtonsoft.Json;

namespace FourSix.WebApi.BackgroundServices
{
    public class BackgroundQueueReader : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly AmazonSQSClient _amazonSQSClient;
        private readonly string _endpointQueue;
        private readonly IAlteraStatusPedidoAdapter _alteraStatusPedidoAdapter;

        public BackgroundQueueReader(IConfiguration configuration, IAlteraStatusPedidoAdapter alteraStatusPedidoAdapter)
        {
            _amazonSQSClient = new AmazonSQSClient(RegionEndpoint.USEast1);
            _endpointQueue = configuration.GetValue<string>("Endpoints:OrdersQueue");
            _alteraStatusPedidoAdapter = alteraStatusPedidoAdapter;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(ReadQueue, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async void ReadQueue(object? state)
        {
            var response = await _amazonSQSClient.ReceiveMessageAsync(_endpointQueue);

            foreach (var message in response.Messages)
            {
                try
                {
                    var pedidoModel = JsonConvert.DeserializeObject<PedidoModel>(message.Body);

                    if (pedidoModel != null)
                        await _alteraStatusPedidoAdapter.Alterar(pedidoModel.Id, pedidoModel.StatusId);

                    await _amazonSQSClient.DeleteMessageAsync(_endpointQueue, message.ReceiptHandle);
                }
                catch
                {
                }
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
