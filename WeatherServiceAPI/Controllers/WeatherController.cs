using Microsoft.AspNetCore.Mvc;
using WeatherServiceAPI.Core.Models;
using WeatherServiceAPI.Core.Services;

namespace WeatherServiceAPI.Controllers
{
    [ApiController]
    [Route("weather")]
    public class WeatherController : ControllerBase
    {
        private readonly IIpAddressService _ipAddressService;
        private readonly IGeolocationDataService _geolocationDataService;
        private readonly IWeatherDataService _weatherDataService;

        public WeatherController(IIpAddressService ipAddressService, IGeolocationDataService geolocationDataService, IWeatherDataService weatherDataService)
        {
            _ipAddressService = ipAddressService;
            _geolocationDataService = geolocationDataService;
            _weatherDataService = weatherDataService;
        }

        [HttpGet]
        public IActionResult GetWeatherData()
        {
            string apiKey = "a63333ff3d14433e97953934230806"; // change API Key here, if current weatherapi.com Key has expired

            string json = _ipAddressService.GetLocationAndIpAddressJson();

            if (Response.StatusCode != 200)
            {
                return StatusCode(Response.StatusCode, "Error occurred while fetching location and IP address data from 3rd party service.");
            }

            var ipAddress = _ipAddressService.ExtractIpAddressData(json);
            _ipAddressService.Create(ipAddress);

            var locationData = _geolocationDataService.ExtractGeolocationData(json, ipAddress);
            _geolocationDataService.Create(locationData);

            string weatherDataJson = _weatherDataService.GetWeatherDataJson(locationData, apiKey);

            if (Response.StatusCode != 200)
            {
                return StatusCode(Response.StatusCode, "Error occurred while fetching weather data from 3rd party service.");
            }

            WeatherData weatherData = _weatherDataService.ExtractWeatherDataFromJson(locationData, weatherDataJson);
            _weatherDataService.Create(weatherData);

            return Ok(_weatherDataService.PrintWeatherData(weatherData));
        }
    }
}




