using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherServiceAPI.Core.Models;
using WeatherServiceAPI.Core.Services;
using WeatherServiceAPI.Data;

namespace WeatherServiceAPI.Services
{
    public class WeatherDataService : EntityService<WeatherData>, IWeatherDataService
    {
        public WeatherDataService(IWeatherServiceDbContext context) : base(context)
        {

        }

        public string GetWeatherDataJson(GeolocationData locationData, string apiKey)
        {
            using (WebClient client = new WebClient())
            {
                string jsonUrl = $"http://api.weatherapi.com/v1/current.json?key={apiKey}&q=" +
                                 $"{locationData.Latitude.ToString().Replace(",", ".")}," +
                                 $"{locationData.Longitude.ToString().Replace(",", ".")}";

                return client.DownloadString(jsonUrl);
            }
        }

        public WeatherData ExtractWeatherDataFromJson(GeolocationData locationData, string json)
        {
            var jsonDocument = JsonDocument.Parse(json);
            var root = jsonDocument.RootElement;

            var location = root.GetProperty("location");
            string name = location.GetProperty("name").GetString();

            var current = root.GetProperty("current");
            double temperature = current.GetProperty("temp_c").GetDouble();
            string conditions = current.GetProperty("condition").GetProperty("text").GetString();
            double windSpeed = current.GetProperty("wind_kph").GetDouble();
            string windDirection = current.GetProperty("wind_dir").GetString();
            int humidity = current.GetProperty("humidity").GetInt32();

            return new WeatherData
            {
                Location = name,
                Temperature = temperature,
                Conditions = conditions,
                WindSpeed = windSpeed,
                WindDirection = windDirection,
                Humidity = humidity,
                LocationDataId = locationData.Id,
                IpAddressId = locationData.IpAddressId
            };
        }

        public string PrintWeatherData(WeatherData weatherData)
        {
            return $"Your location: {weatherData.Location}\n" +
                   $"Current temperature in Celsius: {weatherData.Temperature.ToString().Replace(",", ".")}\n" +
                   $"Conditions: {weatherData.Conditions}\n" +
                   $"Humidity percentage: {weatherData.Humidity}\n" +
                   $"Wind direction: {weatherData.WindDirection}\n" +
                   $"Wind speed km/h: {weatherData.WindSpeed.ToString().Replace(",", ".")}";
        }
    }
}
