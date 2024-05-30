using RestauranteService.Dtos;

namespace RestauranteService.RabbitMClient
{
    public interface IRabbitMQClient
    {
        void PublicaRestaurante(RestauranteReadDto restauranteReadDto);
    }
}
