using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WeatherServiceAPI.Core.Models;
using WeatherServiceAPI.Core.Services;
using WeatherServiceAPI.Services.Interfaces;

namespace WeatherServiceAPI.Controllers
{
    [ApiController]
    [Route("weather")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherDataExtractorService _dataExtractorService;

        public WeatherController(IWeatherDataExtractorService dataExtractorService)
        {
            _dataExtractorService = dataExtractorService;
        }

        [HttpGet]
        public IActionResult GetWeatherData()
        {
            try
            {
                return Ok(_dataExtractorService.GetWeatherData());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
