using WeatherSourceAPI.Models;

namespace WeatherSourceAPI;

public interface IWeatherSourceAPI
{
    Task<Weather> GetCurrentWeather(string apiKey, string city);
}
