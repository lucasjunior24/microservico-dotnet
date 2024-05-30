using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RestauranteService.Dtos;

namespace RestauranteService.RabbitMClient
{
    public class RabbitMQClient : IRabbitMQClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        static string exchange = "trigger";
        public RabbitMQClient(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = Int32.Parse(_configuration["RabbitMQPort"])
            }.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);
        }
        public void PublicaRestaurante(RestauranteReadDto restauranteReadDto)
        {
            string mensagem = JsonSerializer.Serialize(restauranteReadDto);
            var body = Encoding.UTF8.GetBytes(mensagem);

            _channel.BasicPublish(exchange: exchange, routingKey: "", basicProperties: null, body: body);

        }
    }
}
