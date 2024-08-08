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
            Console.WriteLine($"{DateTime.Now} - Iniciado background service");

            ReadQueue(cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //_timer?.Change(Timeout.Infinite, 0);

            Console.WriteLine($"{DateTime.Now} - Parado background service");
            return Task.CompletedTask;
        }

        private async void ReadQueue(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var response = await _amazonSQSClient.ReceiveMessageAsync(_endpointQueue, cancellationToken);

                    if (response == null || response.Messages.Count == 0)
                    {
                        Console.WriteLine($"{DateTime.Now} - Sem novas mensagens");
                    }
                    else
                    {
                        Console.WriteLine($"{DateTime.Now} - Mensagens obtidas: {response.Messages.Count}");

                        foreach (var message in response.Messages)
                        {
                            try
                            {
                                var pedidoModel = JsonConvert.DeserializeObject<PedidoQueueModel>(message.Body);

                                if (pedidoModel != null)
                                {
                                    Console.WriteLine($"{DateTime.Now} - Alterando pedido {pedidoModel.Id} para status {(Domain.Entities.PedidoAggregate.EnumStatusPedido)pedidoModel.StatusId}");

                                    await _alteraStatusPedidoAdapter.Alterar(pedidoModel.Id, (Domain.Entities.PedidoAggregate.EnumStatusPedido)pedidoModel.StatusId);
                                }

                                Console.WriteLine($"{DateTime.Now} - Apagando mensagem");

                                await _amazonSQSClient.DeleteMessageAsync(_endpointQueue, message.ReceiptHandle, cancellationToken);

                                Console.WriteLine($"{DateTime.Now} - Mensagem apagada");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
            }
        }

        //public void Dispose()
        //{
        //    _timer?.Dispose();
        //}
    }
}
