using WeatherServiceAPI.Core.Models;

namespace WeatherServiceAPI.Core.Services
{
    public interface IWeatherDataService : IEntityService<WeatherData>
    {
        string GetWeatherDataJson(GeolocationData locationData, string apiKey);
        WeatherData ExtractWeatherDataFromJson(GeolocationData locationData, string json);
        public string PrintWeatherData(WeatherData weatherData);
    }
}
