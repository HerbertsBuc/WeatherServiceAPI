using FluentAssertions;
using Moq.AutoMock;
using NUnit.Framework;
using WeatherServiceAPI.Core.Models;
using WeatherServiceAPI.Core.Services;
using WeatherServiceAPI.Data;
using WeatherServiceAPI.Services;
using WeatherServiceAPI.Services.Interfaces;

namespace WeatherServiceAPI.Tests
{
    public class WeatherDataServiceTests
    {
        private IWeatherDataService _weatherDataService;
        private GeolocationData _geolocationData;
        private IpAddressData _ipAddressData;
        private string _apiKey;
        private string _json;
        private IWeatherServiceDbContext _context;
        private AutoMocker _mocker;

        [SetUp]
        public void SetUp()
        {
            _mocker = new AutoMocker();

            var weatherDataConfigMock = _mocker.GetMock<IWeatherDataServiceConfig>();
            weatherDataConfigMock.Setup(x => x.ApiKey).Returns("a63333ff3d14433e97953934230806");

            _weatherDataService = new WeatherDataService(_context, weatherDataConfigMock.Object);

            _geolocationData = new GeolocationData
            {
                Id = 1,
                Latitude = 56.9496,
                Longitude = 24.0978,
                IpAddressId = 1
            };

            _ipAddressData = new IpAddressData
            {
                Id = 1,
                IpAddress = "87.246.148.235"
            };

            _apiKey = "a63333ff3d14433e97953934230806";

            _json = "{\"location\":{\"name\":\"Riga\",\"region\":\"Riga\",\"country\":\"Latvia\"," +
                    "\"lat\":56.95,\"lon\":24.1,\"tz_id\":\"Europe/Riga\",\"localtime_epoch\":1686808102," +
                    "\"localtime\":\"2023-06-15 8:48\"},\"current\":{\"last_updated_epoch\":1686807900," +
                    "\"last_updated\":\"2023-06-15 08:45\",\"temp_c\":21.0,\"temp_f\":69.8,\"is_day\":1," +
                    "\"condition\":{\"text\":\"Sunny\",\"icon\":\"//cdn.weatherapi.com/weather/64x64/day/113.png" +
                    "\",\"code\":1000},\"wind_mph\":6.9,\"wind_kph\":11.2,\"wind_degree\":90,\"wind_dir" +
                    "\":\"E\",\"pressure_mb\":1019.0,\"pressure_in\":30.09,\"precip_mm\":0.0,\"precip_in" +
                    "\":0.0,\"humidity\":40,\"cloud\":0,\"feelslike_c\":21.0,\"feelslike_f\":69.8,\"vis_km" +
                    "\":10.0,\"vis_miles\":6.0,\"uv\":6.0,\"gust_mph\":7.2,\"gust_kph\":11.5}}";
        }

        [Test]
        public void GetWeatherDataJson_ProvidedLocationDataAndApiKey_ReturnsString()
        {
            string weatherAndIpJson = _weatherDataService.GetWeatherDataJson(_geolocationData);

            string.IsNullOrEmpty(weatherAndIpJson).Should().Be(false);
        }

        [Test]
        public void GetWeatherDataJson_ProvidedInvalidResponse_ThrowsErrorMessageException()
        {
            string weatherAndIpJson = _weatherDataService.GetWeatherDataJson(_geolocationData);

            string.IsNullOrEmpty(weatherAndIpJson).Should().Be(false);
        }

        [Test]
        public void ExtractWeatherDataFromJson_ProvidedJson_ExtractsWeatherDataLocation()
        {
            WeatherData weatherData = _weatherDataService.ExtractWeatherDataFromJson(_geolocationData, _json);

            weatherData.Location.Should().Be("Riga");
        }

        [Test]
        public void ExtractWeatherDataFromJson_ProvidedJson_ExtractsWeatherDataTemperature()
        {
            WeatherData weatherData = _weatherDataService.ExtractWeatherDataFromJson(_geolocationData, _json);

            weatherData.Temperature.Should().Be(21.0);
        }

        [Test]
        public void ExtractWeatherDataFromJson_ProvidedJson_ExtractsWeatherDataConditions()
        {
            WeatherData weatherData = _weatherDataService.ExtractWeatherDataFromJson(_geolocationData, _json);

            weatherData.Conditions.Should().Be("Sunny");
        }

        [Test]
        public void ExtractWeatherDataFromJson_ProvidedJson_ExtractsWeatherDataWindSpeed()
        {
            WeatherData weatherData = _weatherDataService.ExtractWeatherDataFromJson(_geolocationData, _json);

            weatherData.WindSpeed.Should().Be(11.2);
        }

        [Test]
        public void ExtractWeatherDataFromJson_ProvidedJson_ExtractsWeatherDataWindDirection()
        {
            WeatherData weatherData = _weatherDataService.ExtractWeatherDataFromJson(_geolocationData, _json);

            weatherData.WindDirection.Should().Be("E");
        }

        [Test]
        public void ExtractWeatherDataFromJson_ProvidedJson_ExtractsWeatherDataHumidity()
        {
            WeatherData weatherData = _weatherDataService.ExtractWeatherDataFromJson(_geolocationData, _json);

            weatherData.Humidity.Should().Be(40);
        }

        [Test]
        public void ExtractWeatherDataFromJson_ProvidedJson_ExtractsWeatherDataLocationDataIdn()
        {
            WeatherData weatherData = _weatherDataService.ExtractWeatherDataFromJson(_geolocationData, _json);

            weatherData.LocationDataId.Should().Be(1);
        }

        [Test]
        public void ExtractWeatherDataFromJson_ProvidedJson_ExtractsWeatherDataIpAddressId()
        {
            WeatherData weatherData = _weatherDataService.ExtractWeatherDataFromJson(_geolocationData, _json);

            weatherData.IpAddressId.Should().Be(1);
        }

        [Test]
        public void PrintWeatherData_ProvidedWeatherData_PrintsFirstSixPropertiesInNewLines()
        {
            WeatherData weatherData = new WeatherData
            {
                Location = "Riga",
                Temperature = 21.0,
                Conditions = "Sunny",
                Humidity = 40,
                WindDirection = "E",
                WindSpeed = 11.2
            };

            string printInfo = $"Your location: Riga\n" +
                               $"Current temperature in Celsius: 21\n" +
                               $"Conditions: Sunny\n" +
                               $"Humidity percentage: 40\n" +
                               $"Wind direction: E\n" +
                               $"Wind speed km/h: 11.2";

            _weatherDataService.PrintWeatherData(weatherData).Should().Be(printInfo);
        }
    }
}