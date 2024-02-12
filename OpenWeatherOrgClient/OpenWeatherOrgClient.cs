using Microsoft.AspNetCore.WebUtilities;
using System.Runtime.CompilerServices;
using System.Text.Json;
using WeatherSourceAPI;
using WeatherSourceAPI.Models;

namespace OpenWeatherOrgClient;

public class OpenWeatherOrgClient : IWeatherSourceAPI
{
    const string OpenWeatherOrgURL = "https://api.openweathermap.org/data/2.5/weather";
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
            {"city", city}
        };

        var newUrl = new Uri(QueryHelpers.AddQueryString(OpenWeatherOrgURL, param));

        try
        {
            var response = await client.GetAsync(newUrl);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            var json = JsonSerializer.Deserialize<object>(responseBody);


            return new Weather();
        }
        catch (HttpRequestException e)
        {
            throw;
        }
    }
}
