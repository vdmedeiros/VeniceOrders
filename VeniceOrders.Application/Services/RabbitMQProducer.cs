using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using VeniceOrders.Application.DTOs;
using VeniceOrders.Application.Interfaces;

namespace VeniceOrders.Application.Services
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        private readonly IConfiguration _config;

        public RabbitMQProducer(IConfiguration config)
        {
            _config = config;
        }

        public void PublishOrder<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = _config["RabbitMQ:Host"]
            };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            string exchange = _config["RabbitMQ:Exchange"];
            string queue = _config["RabbitMQ:Queue"];
            string routingKey = _config["RabbitMQ:RoutingKey"];

            // Declare exchange & queue
            channel.ExchangeDeclare(exchange, ExchangeType.Direct, durable: true);
            channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind(queue, exchange, routingKey);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange, routingKey, properties, body);
        }
    }
}
