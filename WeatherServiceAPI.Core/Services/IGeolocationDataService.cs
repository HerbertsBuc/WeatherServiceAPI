using WeatherServiceAPI.Core.Models;

namespace WeatherServiceAPI.Core.Services
{
    public interface IGeolocationDataService : IEntityService<GeolocationData>
    {
        GeolocationData ExtractGeolocationData(string json, IpAddressData ipAddress);
    }
}
