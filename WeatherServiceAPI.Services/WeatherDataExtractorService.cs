using Microsoft.Extensions.Caching.Memory;
using WeatherServiceAPI.Core.Models;
using WeatherServiceAPI.Core.Services;
using WeatherServiceAPI.Services.Interfaces;

namespace WeatherServiceAPI.Services
{
    public class WeatherDataExtractorService : IWeatherDataExtractorService
    {
        private readonly IIpAddressService _ipAddressService;
        private readonly IGeolocationDataService _geolocationDataService;
        private readonly IWeatherDataService _weatherDataService;
        private readonly IMemoryCache _memoryCache;
        private readonly IWeatherDataServiceConfig _weatherDataServiceConfig;

        public WeatherDataExtractorService(IIpAddressService ipAddressService, IGeolocationDataService geolocationDataService, IWeatherDataService weatherDataService, IMemoryCache memoryCache)
        {
            _ipAddressService = ipAddressService;
            _geolocationDataService = geolocationDataService;
            _weatherDataService = weatherDataService;
            _memoryCache = memoryCache;
        }

        public string GetWeatherData()
        {
            string json = _ipAddressService.GetLocationAndIpAddressJson();
            var ipAddress = _ipAddressService.ExtractIpAddressData(json);
            _ipAddressService.Create(ipAddress);

            string cacheKey = ipAddress.IpAddress;

            if (_memoryCache.TryGetValue(cacheKey, out WeatherData weatherData))
            {
                return _weatherDataService.PrintWeatherData(weatherData) +
                          "\nWeather data returned from last request because another request " +
                          "was made from the same IP address less than a minute ago";
            }

            var locationData = _geolocationDataService.ExtractGeolocationData(json, ipAddress);
            _geolocationDataService.Create(locationData);

            string weatherDataJson = _weatherDataService.GetWeatherDataJson(locationData);

            weatherData = _weatherDataService.ExtractWeatherDataFromJson(locationData, weatherDataJson);
            _weatherDataService.Create(weatherData);

            _memoryCache.Set(cacheKey, weatherData, TimeSpan.FromMinutes(1));

            return _weatherDataService.PrintWeatherData(weatherData);
        }
    }
}
