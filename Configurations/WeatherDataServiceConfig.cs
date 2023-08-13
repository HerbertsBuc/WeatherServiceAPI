using WeatherServiceAPI.Services.Interfaces;

namespace WeatherServiceAPI.Configurations
{
    public class WeatherDataServiceConfig : IWeatherDataServiceConfig
    {
        public WeatherDataServiceConfig(IConfiguration configuration)
        {
            ApiKey = configuration["ApiKey"];
        }

        public string ApiKey { get; }
    }
}
