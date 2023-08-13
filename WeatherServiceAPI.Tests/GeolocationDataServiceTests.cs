using FluentAssertions;
using NUnit.Framework;
using WeatherServiceAPI.Core.Models;
using WeatherServiceAPI.Core.Services;
using WeatherServiceAPI.Data;
using WeatherServiceAPI.Services;

namespace WeatherServiceAPI.Tests
{
    public class GeolocationDataServiceTests
    {
        private IGeolocationDataService _geolocationDataService;
        private IWeatherServiceDbContext _context;
        private string _json;
        private IpAddressData _ipAddress;

        [SetUp]
        public void SetUp()
        {
            _geolocationDataService = new GeolocationDataService(_context);

            _ipAddress = new IpAddressData
            {
                IpAddress = "87.246.148.23",
                Id = 1
            };

            _json = "{\"status\":\"success\",\"country\":\"Latvia\",\"countryCode\":\"LV\",\"region\":" +
                    "\"RIX\",\"regionName\":\"Riga\",\"city\":\"Riga\",\"zip\":\"LV-1063\",\"lat\":56.9496," +
                    "\"lon\":24.0978,\"timezone\":\"Europe/Riga\",\"isp\":\"SIA Tet\",\"org\":\"TET Home\"," +
                    "\"as\":\"AS12578 SIA Tet\",\"query\":\"87.246.148.235\"}";
        }

        [Test]
        public void ExtractGeolocationData_ProvidedJson_ReturnsLatitude()
        {
            GeolocationData locationData = _geolocationDataService.ExtractGeolocationData(_json, _ipAddress);

            locationData.Latitude.Should().Be(56.9496);
        }

        [Test]
        public void ExtractGeolocationData_ProvidedJson_ReturnsLongitude()
        {
            GeolocationData locationData = _geolocationDataService.ExtractGeolocationData(_json, _ipAddress);

            locationData.Longitude.Should().Be(24.0978);
        }

        [Test]
        public void ExtractGeolocationData_ProvidedJson_ReturnsIpAddressId()
        {
            GeolocationData locationData = _geolocationDataService.ExtractGeolocationData(_json, _ipAddress);

            locationData.IpAddressId.Should().Be(1);
        }
    }
}
