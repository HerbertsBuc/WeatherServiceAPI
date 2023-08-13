using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq.AutoMock;
using NUnit.Framework;
using WeatherServiceAPI.Core.Models;
using WeatherServiceAPI.Core.Services;
using WeatherServiceAPI.Data;
using WeatherServiceAPI.Services;

namespace WeatherServiceAPI.Tests
{
    public class EntityServiceTests
    {
        private IEntityService<WeatherData> _entityService;
        private WeatherData _weatherData;
        private GeolocationData _geolocationData;
        private IpAddressData _ipAddressData;
        private AutoMocker _mocker;
        private WeatherServiceDbContext _context;

        private readonly DbContextOptions<WeatherServiceDbContext> dbContextOptions = new DbContextOptionsBuilder<WeatherServiceDbContext>()
            .UseInMemoryDatabase(databaseName: "WeatherDataDbTest")
            .Options;

        [SetUp]
        public void Setup()
        {
            _context = new WeatherServiceDbContext(dbContextOptions);
            _context.Database.EnsureCreated();
            _mocker = new AutoMocker();
            _entityService = new EntityService<WeatherData>(_context);

            _ipAddressData = new IpAddressData
            {
                IpAddress = "87.246.148.235"
            };

            _geolocationData = new GeolocationData
            {
                Latitude = 56.9496,
                Longitude = 24.0978,
                IpAddressId = _ipAddressData.Id
            };

            _weatherData = new WeatherData
            {
                Location = "Riga",
                Temperature = 21.0,
                Conditions = "Sunny",
                Humidity = 40,
                WindDirection = "E",
                WindSpeed = 11.2,
                IpAddressId = _ipAddressData.Id,
                LocationDataId = _geolocationData.Id
            };
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public void Create_ProvidedIpAddressData_CreatesIpAddressData()
        {
            var dbServiceMock = _mocker.GetMock<IDbService>();
            dbServiceMock.Setup(x => x.Create(_ipAddressData)).Returns(_ipAddressData);

            _entityService.Create(_ipAddressData);

            _context.Addresses.Count().Should().Be(1);
        }

        [Test]
        public void Create_ProvidedGeoLocationData_CreatesGeolocationData()
        {
            var dbServiceMock = _mocker.GetMock<IDbService>();
            dbServiceMock.Setup(x => x.Create(_geolocationData)).Returns(_geolocationData);

            _entityService.Create(_geolocationData);

            _context.Locations.Count().Should().Be(1);
        }

        [Test]
        public void Create_ProvidedWeatherData_CreatesWeatherData()
        {
            var dbServiceMock = _mocker.GetMock<IDbService>();
            dbServiceMock.Setup(x => x.Create(_weatherData)).Returns(_weatherData);

            _entityService.Create(_weatherData);

            _context.Weather.Count().Should().Be(1);
        }

        [Test]
        public void Delete_ProvidedWeatherData_DeletesWeatherData()
        {
            var dbServiceMock = _mocker.GetMock<IDbService>();
            dbServiceMock.Setup(x => x.Create(_weatherData)).Returns(_weatherData);

            _entityService.Create(_weatherData);

            dbServiceMock.Setup(x => x.Delete(_weatherData));
            _entityService.Delete(_weatherData);

            _context.Weather.Count().Should().Be(0);
        }

        [Test]
        public void GetById_ProvidedWeatherDataId_ReturnsWeatherData()
        {
            var dbServiceMock = _mocker.GetMock<IDbService>();
            dbServiceMock.Setup(x => x.Create(_weatherData)).Returns(_weatherData);

            _entityService.Create(_weatherData);

            dbServiceMock.Setup(x => x.GetById<WeatherData>(1)).Returns(_weatherData);

            WeatherData weatherDataFromContext = _entityService.GetById(1);

            Assert.AreEqual(weatherDataFromContext.Location, _weatherData.Location);
            Assert.AreEqual(weatherDataFromContext.Conditions, _weatherData.Conditions);
            Assert.AreEqual(weatherDataFromContext.Temperature, _weatherData.Temperature);
            Assert.AreEqual(weatherDataFromContext.WindDirection, _weatherData.WindDirection);
            Assert.AreEqual(weatherDataFromContext.WindSpeed, _weatherData.WindSpeed);
            Assert.AreEqual(weatherDataFromContext.Humidity, _weatherData.Humidity);
        }

        [Test]
        public void Update_ProvidedWeatherData_UpdatesWeatherData()
        {
            var dbServiceMock = _mocker.GetMock<IDbService>();
            dbServiceMock.Setup(x => x.Create(_weatherData)).Returns(_weatherData);

            _entityService.Create(_weatherData);

            var updatedWeatherData = new WeatherData
            {
                Id = 1,
                Conditions = "Rainy"
            };

            _context.Entry(_weatherData).State = EntityState.Detached;

            dbServiceMock.Setup(x => x.Update<WeatherData>(_weatherData));

            _entityService.Update(updatedWeatherData);

            dbServiceMock.Setup(x => x.GetById<WeatherData>(1)).Returns(_weatherData);

            WeatherData returnedWeatherData = _entityService.GetById(1);

            returnedWeatherData.Conditions.Should().Be("Rainy");
        }

        [Test]
        public void GetAll_MethodCalled_ShouldReturnListOfAllWeatherDataEntriesFromContext()
        {
            var dbServiceMock = _mocker.GetMock<IDbService>();
            dbServiceMock.Setup(x => x.Create(_weatherData)).Returns(_weatherData);

            _entityService.Create(_weatherData);

            var listWeatherData = new List<WeatherData>
            {
                _weatherData
            };

            dbServiceMock.Setup(x => x.GetAll<WeatherData>()).Returns(listWeatherData);

            var listWeatherDataFromContext = _entityService.GetAll();

            listWeatherDataFromContext.Count.Should().Be(1);
        }
    }
}
