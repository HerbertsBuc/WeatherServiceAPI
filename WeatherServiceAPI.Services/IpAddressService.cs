using System.Net;
using System.Text.Json;
using WeatherServiceAPI.Core.Models;
using WeatherServiceAPI.Core.Services;
using WeatherServiceAPI.Data;

namespace WeatherServiceAPI.Services
{
    public class IpAddressService : EntityService<IpAddressData>, IIpAddressService
    {
        public IpAddressService(IWeatherServiceDbContext context) : base(context)
        {

        }

        public string GetLocationAndIpAddressJson()
        {
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString("http://ip-api.com/json/");

                return json;
            }
        }

        public IpAddressData ExtractIpAddressData(string json)
        {
            var jsonDocument = JsonDocument.Parse(json);
            var root = jsonDocument.RootElement;

            string ipAddress = root.GetProperty("query").GetString();

            return new IpAddressData
            {
                IpAddress = ipAddress,
            };
        }

    }
}
