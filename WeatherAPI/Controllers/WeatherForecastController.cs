using Microsoft.AspNetCore.Mvc;
using WeatherSourceAPI;
using WeatherSourceAPI.Models;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(IConfiguration configuration, ILogger<WeatherForecastController> logger, IWeatherSourceAPI weatherSourceApi) : ControllerBase
    {
        private const string AppId = "AppId";

        private readonly ILogger<WeatherForecastController> _logger = logger;
        private readonly IWeatherSourceAPI _weatherSourceApi = weatherSourceApi;
        private readonly IConfiguration _configuration = configuration;

        [HttpGet("{city}/{userKey?}", Name = "GetWeatherForecast")]
        public async Task<Weather> Get(string city, string? userKey = null)
        {
            var appId = _configuration.GetValue<string>(AppId);
            var weather = await _weatherSourceApi.GetCurrentWeather(appId!, city);
            return weather;
        }
    }
}
