using Microsoft.AspNetCore.Mvc;
using WeatherSourceAPI;
using WeatherSourceAPI.Models;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherSourceAPI weatherSourceApi) : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger = logger;
        private readonly IWeatherSourceAPI _weatherSourceApi = weatherSourceApi;

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<Weather> Get()
        {
            var weather = await _weatherSourceApi.GetCurrentWeather("1d2d2ca177f4fb7f763bf3c4b230f27b", "London");
            return weather;
        }
    }
}
