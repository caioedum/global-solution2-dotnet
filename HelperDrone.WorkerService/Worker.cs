using HelperDrone.Contracts.Repositories;
using HelperDrone.Models;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace HelperDrone.WorkerService
{
    public class Worker : BackgroundService, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IDroneRepository _droneRepo;

        public Worker(IDroneRepository droneRepo)
        {
            _droneRepo = droneRepo;

            var factory = new ConnectionFactory()
            {
                HostName = "rabbitmq",
                UserName = "admin",
                Password = "password"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "alertas", durable: true, exclusive: false);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var mensagem = Encoding.UTF8.GetString(body);
                    var alerta = JsonSerializer.Deserialize<Alerta>(mensagem);

                    Console.WriteLine($"Processando alerta: {alerta?.Descricao}");

                    if (alerta?.Gravidade == "alta")
                    {
                        await AcionarDroneParaAreaAsync(alerta.IdArea);
                    }

                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    _channel.BasicNack(ea.DeliveryTag, false, true);
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            };

            _channel.BasicConsume(queue: "alertas", autoAck: false, consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task AcionarDroneParaAreaAsync(int idArea)
        {
            var dronesDisponiveis = _droneRepo.ObterDronesDisponiveis();
            Console.WriteLine($"Drone {dronesDisponiveis.First().IdDrone} acionado para área {idArea}");
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }
}
