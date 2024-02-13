using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using WeatherSourceAPI;
using WeatherSourceAPI.Models;

namespace OpenWeatherOrgClient;

public class OpenWeatherOrgClient : IWeatherSourceAPI
{
    const string OpenWeatherOrgURL = "https://api.openweathermap.org/data/2.5/weather";
    const string OpenWeatherIconURL = "https://openweathermap.org/img/wn/{0}@2x.png";
    const string Units = "metric";
    const string Exclude = "daily,hourly,minutely,alerts";

    private readonly HttpClient client;

    public OpenWeatherOrgClient()
    {
        client = new HttpClient();
    }

    public async Task<Weather> GetCurrentWeather(string apiKey, string city)
    {
        var param = new Dictionary<string, string?>() {
            {"units", Units},
            {"exclude", Exclude},
            {"appid", apiKey},
            {"q", city}
        };

        var newUrl = new Uri(QueryHelpers.AddQueryString(OpenWeatherOrgURL, param));

        try
        {
            var response = await client.GetAsync(newUrl);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            var json = JObject.Parse(responseBody);
            var temperature = json["main"]?["temp"]?.ToString();
            var humidity = json["main"]?["humidity"]?.ToString();
            var windSpeed = json["wind"]?["speed"]?.ToString();
            var icon = json["weather"]?[0]?["icon"]?.ToString();

            var weather = new Weather
            {
                Temperature = double.Parse(temperature ?? "0"),
                Humidity = double.Parse(humidity ?? "0"),
                WindSpeed = double.Parse(windSpeed ?? "0"),
                Icon = string.Format(OpenWeatherIconURL, icon)
            };

            return weather;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}
