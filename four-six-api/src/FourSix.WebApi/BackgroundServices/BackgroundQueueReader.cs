using Amazon;
using Amazon.SQS;
using FourSix.Controllers.Adapters.Pedidos.AlteraStatusPedido;
using FourSix.WebApi.Models;
using Newtonsoft.Json;

namespace FourSix.WebApi.BackgroundServices
{
    public class BackgroundQueueReader : IHostedService//, IDisposable
    {
        //private Timer _timer;
        //private readonly AmazonSQSClient _amazonSQSClient;
        private readonly string _endpointQueue;
        private readonly IAlteraStatusPedidoAdapter _alteraStatusPedidoAdapter;

        public BackgroundQueueReader(IConfiguration configuration, IAlteraStatusPedidoAdapter alteraStatusPedidoAdapter)
        {
            
            _endpointQueue = configuration.GetValue<string>("Endpoints:OrdersQueue");
            _alteraStatusPedidoAdapter = alteraStatusPedidoAdapter;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            ReadQueue(cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //_timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async void ReadQueue(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    AmazonSQSClient _amazonSQSClient = new AmazonSQSClient(RegionEndpoint.USEast1);

                    var response = await _amazonSQSClient.ReceiveMessageAsync(_endpointQueue, cancellationToken);

                    foreach (var message in response.Messages)
                    {
                        try
                        {
                            var pedidoModel = JsonConvert.DeserializeObject<PedidoQueueModel>(message.Body);

                            if (pedidoModel != null)
                                await _alteraStatusPedidoAdapter.Alterar(pedidoModel.Id, (Domain.Entities.PedidoAggregate.EnumStatusPedido)pedidoModel.StatusId);

                            await _amazonSQSClient.DeleteMessageAsync(_endpointQueue, message.ReceiptHandle);
                        }
                        catch
                        {
                        }
                    }
                }
                catch { }

                await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
            }
        }

        //public void Dispose()
        //{
        //    _timer?.Dispose();
        //}
    }
}
