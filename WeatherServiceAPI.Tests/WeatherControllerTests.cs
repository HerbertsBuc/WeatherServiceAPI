using FluentAssertions;
using Moq.AutoMock;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WeatherServiceAPI.Core.Models;
using WeatherServiceAPI.Core.Services;
using WeatherServiceAPI.Data;
using Microsoft.Extensions.Caching.Memory;
using WeatherServiceAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Moq;
using WeatherServiceAPI.Services.Interfaces;

namespace WeatherServiceAPI.Tests
{
    public class WeatherControllerTests
    {
        private WeatherController _controller;
        private WeatherData _weatherData;
        private WeatherData _cachedWeatherData;
        private GeolocationData _geolocationData;
        private IpAddressData _ipAddressData;
        private AutoMocker _mocker;
        private WeatherServiceDbContext _context;
        private IIpAddressService _ipAddressService;
        private IGeolocationDataService _geolocationDataService;
        private IWeatherDataService _weatherDataService;
        private IMemoryCache _memoryCache;
        private string _apiKey;
        private IWeatherDataExtractorService _weatherDataExtractorService;

        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMocker();
            _controller = _mocker.CreateInstance<WeatherController>();

            _ipAddressData = new IpAddressData
            {
                Id = 1,
                IpAddress = "87.246.148.235"
            };

            _cachedWeatherData = new WeatherData
            {
                Location = "Riga",
                Temperature = 21.0,
                Conditions = "Sunny",
                Humidity = 40,
                WindDirection = "E",
                WindSpeed = 11.2
            };
        }

        [Test]
        public void GetWeatherData_ProvidedInvalidResponseCodeForGettingJson_ThrowsErrorMessage()
        {
            var errorMessage = "Error occurred while fetching location and IP address data from 3rd party service.";

            var weatherDataExtractorMock = _mocker.GetMock<IWeatherDataExtractorService>();
            weatherDataExtractorMock.Setup(w => w.GetWeatherData())
                .Throws<Exception>(() => new Exception(errorMessage));

            var httpContextMock = new Mock<HttpContext>();
           
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContextMock.Object
            };

            _controller.ControllerContext = controllerContext;

            var result = _controller.GetWeatherData();

            var objectResult = (ObjectResult)result;

            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual(errorMessage, objectResult.Value);
        }

        [Test]
        public void GetWeatherData_ProvidedInvalidResponseCodeForGettingWeatherJson_ThrowsErrorMessage()
        {
            var errorMessage = "Error occurred while fetching weather data from 3rd party service.";

            var weatherDataExtractorMock = _mocker.GetMock<IWeatherDataExtractorService>();
            weatherDataExtractorMock.Setup(w => w.GetWeatherData())
                .Throws<Exception>(() => new Exception(errorMessage));

            var httpContextMock = new Mock<HttpContext>();

            var controllerContext = new ControllerContext
            {
                HttpContext = httpContextMock.Object
            };

            _controller.ControllerContext = controllerContext;

            var result = _controller.GetWeatherData();

            var objectResult = (ObjectResult)result;

            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual(errorMessage, objectResult.Value);

        }

        [Test]
        public void GetWeatherData_ProvidedAllValues_ReturnsResult()
        {
            var cacheMock = _mocker.GetMock<IMemoryCache>();

            var httpContextMock = new Mock<HttpContext>();

            var controllerContext = new ControllerContext
            {
                HttpContext = httpContextMock.Object
            };

            cacheMock.Setup(x => x.CreateEntry(_ipAddressData.IpAddress)).Returns(Mock.Of<ICacheEntry>());

            _controller.ControllerContext = controllerContext;

            var result = _controller.GetWeatherData();

            result.Should().NotBeNull();
        }
    }
}
