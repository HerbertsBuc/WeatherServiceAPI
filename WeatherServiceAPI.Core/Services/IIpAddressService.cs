using WeatherServiceAPI.Core.Models;

namespace WeatherServiceAPI.Core.Services
{
    public interface IIpAddressService : IEntityService<IpAddressData>
    {
        IpAddressData ExtractIpAddressData(string json);

        string GetLocationAndIpAddressJson();
    }
}
