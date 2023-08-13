using FluentAssertions;
using NUnit.Framework;
using WeatherServiceAPI.Core.Models;
using WeatherServiceAPI.Data;
using WeatherServiceAPI.Services;

namespace WeatherServiceAPI.Tests
{
    public class IpAddressServiceTests
    {
        private IpAddressService _ipAddressService;
        private IWeatherServiceDbContext _context;
        private string _json;

        [SetUp]
        public void SetUp()
        {
            _ipAddressService = new IpAddressService(_context);

            _json = "{\"status\":\"success\",\"country\":\"Latvia\",\"countryCode\":\"LV\",\"region\":" +
                    "\"RIX\",\"regionName\":\"Riga\",\"city\":\"Riga\",\"zip\":\"LV-1063\",\"lat\":56.9496," +
                    "\"lon\":24.0978,\"timezone\":\"Europe/Riga\",\"isp\":\"SIA Tet\",\"org\":\"TET Home\"," +
                    "\"as\":\"AS12578 SIA Tet\",\"query\":\"87.246.148.235\"}";
        }

        [Test]
        public void GetLocationAndIpAddressJson_UseMethod_ReturnsIpAddressAndLocationJson()
        {
            string locationAndIpAddressJson = _ipAddressService.GetLocationAndIpAddressJson();

            string.IsNullOrWhiteSpace(locationAndIpAddressJson).Should().Be(false);
        }

        [Test]
        public void ExtractIpAddressData_ProvidedJson_ReturnsIpAddress()
        {
            IpAddressData ipAddress = _ipAddressService.ExtractIpAddressData(_json);

            ipAddress.IpAddress.Should().Be("87.246.148.235");
        }
    }
}
