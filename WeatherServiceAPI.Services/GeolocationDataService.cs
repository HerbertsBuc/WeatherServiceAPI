using System.Text.Json;
using WeatherServiceAPI.Core.Models;
using WeatherServiceAPI.Core.Services;
using WeatherServiceAPI.Data;

namespace WeatherServiceAPI.Services
{
    public class GeolocationDataService : EntityService<GeolocationData>, IGeolocationDataService
    {
        public GeolocationDataService(IWeatherServiceDbContext context) : base(context)
        {

        }

        public GeolocationData ExtractGeolocationData(string json, IpAddressData ipAddress)
        {
            var jsonDocument = JsonDocument.Parse(json);
            var root = jsonDocument.RootElement;

            double latitude = root.GetProperty("lat").GetDouble();
            double longitude = root.GetProperty("lon").GetDouble();

            return new GeolocationData
            {
                Latitude = latitude,
                Longitude = longitude,
                IpAddressId = ipAddress.Id
            };
        }
    }
}